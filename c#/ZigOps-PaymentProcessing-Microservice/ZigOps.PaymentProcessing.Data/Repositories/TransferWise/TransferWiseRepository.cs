//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using ZigOps.PaymentProcessing.Data.Helpers;
using ZigOps.PaymentProcessing.Data.Interface;
using ZigOps.PaymentProcessing.Data.Interface.TransferWise;
using ZigOps.PaymentProcessing.Data.Models;
using ZigOps.PaymentProcessing.Data.Models.TransferWise;
using ZigOps.PaymentProcessing.Data.Models.TransferWise.Database;

namespace ZigOps.PaymentProcessing.Data.Repositories.TransferWise
{
    /// <summary>
    /// TransferWise data layer integration point
    /// </summary>
    public class TransferWiseRepository : ITransferWiseRepository
    {
        private readonly ILogger<TransferWiseRepository> _logger;
        private readonly ITransferwiseDatabaseRepository _transferwiseDatabaseRepository;
        private readonly IGenerateRequest _generateRequest;
        private readonly IEmailRepository _emailRepository;
        private readonly IMessagePublisher _messagePublisher;

        public TransferWiseRepository(
            ILogger<TransferWiseRepository> logger,
            ITransferwiseDatabaseRepository transferwiseDatabaseRepository,
            IGenerateRequest generateRequest, 
            IEmailRepository emailRepository,
            IMessagePublisher messagePublisher)
        {
            _logger = logger;
            _transferwiseDatabaseRepository = transferwiseDatabaseRepository;
            _generateRequest = generateRequest;
            _emailRepository = emailRepository;
            _messagePublisher = messagePublisher;
        }
        /// <summary>
        /// first step in creating a payment
        /// </summary>
        /// <param name="data">it takes a TransferWiseQueueModel model</param>
        /// <param name="token">transferwise api token</param>
        /// <returns>Task</returns>
        public async Task PaymentRequest(TransferWiseQueueModel data, string token)
        {
            try
            {
                foreach (GenericQueueModel.Employee employee in data.Employees)
                {
                    CreateQuoteRequestModel quoteRequest = new CreateQuoteRequestModel() 
                    {
                        Profile = int.Parse(data.Profile),
                        Source = data.SourceCurrency,
                        Target = employee.CurrencyCode,
                        RateType = data.RateType,
                        TargetAmount = employee.Amount,
                        Type = "BALANCE_PAYOUT"
                    };
                    CreateQuoteResponseModel quote = (CreateQuoteResponseModel)await CreateQuote(
                        _generateRequest,
                        quoteRequest,
                        token).ConfigureAwait(false);
                    string quoteRequestString = JsonConvert.SerializeObject(quoteRequest);
                    string quoteResponseString = JsonConvert.SerializeObject(quote);
                    //log to database
                    Quotes logQoute = new Quotes
                    {
                        Request = quoteRequestString,
                        Response = quoteResponseString
                    };
                    await _transferwiseDatabaseRepository.CreateQuoteAsync(
                        logQoute,
                        Constants.TableDBConstant.TRANSFERWISE_QUOTES_TABLE).ConfigureAwait(false);
                    //generate a create transfer queue message
                    CreateTransferQueueModel transferQueueModel = new CreateTransferQueueModel()
                    {
                        PayrollRef = data.PayrollRef,
                        Profile = int.Parse(data.Profile),
                        EmployeeId = int.Parse(employee.EmployeeId),
                        CurrencyCode = employee.CurrencyCode,
                        Amount = employee.Amount,
                        Type = data.FundTransferType,
                        Token = data.Token,
                        _Contact = new CreateTransferQueueModel.Contact
                        {
                            Email = employee.Contact.Email
                        },
                        _TransferRequest = new CreateTransferQueueModel.TransferRequest
                        {
                            TargetAccount = int.Parse(employee.EmployeeId),
                            Quote = quote.Id,
                            customerTransactionId = Guid.NewGuid().ToString(),
                            Details = new CreateTransferQueueModel.Details
                            {
                                Reference = data.Reference,
                                SourceOfFunds = data.SourceOfFunds,
                                TransferPurpose = data.TransferPurpose
                            }
                        },
                    };
                    string message = JsonConvert.SerializeObject(transferQueueModel);
                    await _messagePublisher.Publish(
                        Constants.Queues.TRANSFERWISE_CREATE_TRANSFER,
                        message).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
        /// <summary>
        /// Starts the transfer process by sending a debit/credit request
        /// </summary>
        /// <param name="data">json string payload</param>
        /// <returns></returns>
        public async Task TransferRequest(string data)
        {
            try
            {
                CreateTransferQueueModel request = JsonConvert.DeserializeObject<CreateTransferQueueModel>(data);
                string requestString = JsonConvert.SerializeObject(request._TransferRequest);
                string token = request.Token;
                CreateTransferRequestModel transferRequest = JsonConvert.DeserializeObject<CreateTransferRequestModel>(requestString);
                CreateTransferResponseModel transfer = (CreateTransferResponseModel)await CreateTransfer(
                    _generateRequest,
                    transferRequest,
                    token).ConfigureAwait(false);
                //log to database
                string transferResponseString = JsonConvert.SerializeObject(transfer);
                Transfers logTransfers = new Transfers
                {
                    Request = requestString,
                    Response = transferResponseString
                };
                await _transferwiseDatabaseRepository.CreateTransferAsync(
                    logTransfers,
                    Constants.TableDBConstant.TRANSFERWISE_TRANSFERS_TABLE);
                if (transfer.Status == Constants.TransferWiseStatus.CREATE_TRANSFER_SUCCESS_STATUS)
                {
                    //publish to fund transfer queue
                    FundTransferQueueModel fundTransferModel = JsonConvert.DeserializeObject<FundTransferQueueModel>(data);
                    fundTransferModel.Transfer = transfer.Id;
                    fundTransferModel.CustomerTransactionId = transferRequest.CustomerTransactionId;
                    fundTransferModel.Type = request.Type;
                    fundTransferModel.Token = request.Token;
                    string message = JsonConvert.SerializeObject(fundTransferModel);
                    await _messagePublisher.Publish(Constants.Queues.TRANSFERWISE_FUND_TRANSFER, message).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
        /// <summary>
        /// Funds the generated transfer request
        /// </summary>
        /// <param name="data">json string payload</param>
        /// <returns></returns>
        public async Task FundRequest(string data)
        {
            try
            {
                FundTransferQueueModel fundTransferQueueModel = JsonConvert.DeserializeObject<FundTransferQueueModel>(data);
                string token = fundTransferQueueModel.Token;
                FundTransferModel fundTransferModel = JsonConvert.DeserializeObject<FundTransferModel>(data);
                FundTransferResponseModel fundtransfer = (FundTransferResponseModel) await FundTransfer(
                    _generateRequest,
                    fundTransferModel,
                    token).ConfigureAwait(false);
                //log to database
                string fundTransferRequest = JsonConvert.SerializeObject(fundTransferModel);
                string fundTransferResponse = JsonConvert.SerializeObject(fundtransfer);
                Funded logFunded = new Funded
                {
                    Request = fundTransferRequest,
                    Response = fundTransferResponse
                };

                await _transferwiseDatabaseRepository.FundTransferAsync(
                    logFunded,
                    Constants.TableDBConstant.TRANSFERWISE_FUNDED_TABLE);

                if (fundtransfer.Status == Constants.TransferWiseStatus.FUND_TRANSFER_STATUS)
                {
                    //transaction successfully funded
                    //send email
                    EmailModel email = new EmailModel()
                    {
                        MessageBody = $"Net pay of " +
                        $"{fundTransferQueueModel.CurrencyCode} " +
                        $"{fundTransferQueueModel.Amount} Credited to Employee " +
                        $"{fundTransferQueueModel.EmployeeId}",
                        Subject = "ZigOps Payroll",
                        Recipient = fundTransferQueueModel._Contact.Email,
                        RecipientName = "Employee"
                    };
                    await _emailRepository.Send(email);
                    //send to payrun microservice
                    NotificationModel notification = new NotificationModel()
                    {
                        Publisher = "ZigOps-PaymentProcessor-Microservice",
                        Status = fundtransfer.Status
                    };
                    string message = JsonConvert.SerializeObject(notification);
                    await _messagePublisher.Publish(Constants.Queues.PAYRUN, message).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
        /// <summary>
        /// This method generates a business and personal transferwise profile
        /// </summary>
        /// <param name="request">Implements the IGenerate interface</param>
        /// <param name="token">transferwise api token</param>
        /// <returns></returns>
        public async Task<object> CreateProfile(IGenerateRequest request, string token)
        {
            try
            {
                var uriBuilder = new UriBuilder(
                    Constants.IS_PRODUCTION_ENVIRONMENT_TRANSFERWISE ? 
                    Constants.TransferWiseEndpoints.TRANSFERWISE_PRODUCTION_CREATE_PROFILE_URL : 
                    Constants.TransferWiseEndpoints.TRANSFERWISE_SANDBOX_CREATE_PROFILE_URL
                );
                Uri finalUrl = uriBuilder.Uri;
                string uri = finalUrl.ToString();
                var response = await request.GETAsync(uri, token).ConfigureAwait(false);
                var jsonString = await response.Content.ReadAsStringAsync();
                return jsonString;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request">Implements the IGenerate interface</param>
        /// <param name="data">CreateRecipientRequestModel model</param>
        /// <param name="token">transferwise api token</param>
        /// <returns></returns>
        public async Task<object> CreateRecipient(IGenerateRequest request,CreateRecipientRequestModel data, string token)
        {
            try
            {   
                string payload = JsonConvert.SerializeObject(data);
                var response = await request.POSTAsync(
                    Constants.IS_PRODUCTION_ENVIRONMENT_TRANSFERWISE ?
                    Constants.TransferWiseEndpoints.TRANSFERWISE_PRODUCTION_CREATE_RECIPIENT_URL :
                    Constants.TransferWiseEndpoints.TRANSFERWISE_SANDBOX_CREATE_RECIPIENT_URL,
                    new StringContent(payload), token);
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CreateRecipientResponseModel>(jsonString);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
        /// <summary>
        /// Creates a quote for currency conversion
        /// </summary>
        /// <param name="request">Implements the IGenerate interface</param>
        /// <param name="data">CreateQuoteRequestModel model</param>
        /// <param name="token">transferwise api token</param>
        /// <returns>CreateQuoteResponseModel object</returns>
        public async Task<object> CreateQuote(IGenerateRequest request, CreateQuoteRequestModel data, string token)
        {
            try
            {
                string payload = JsonConvert.SerializeObject(data);
                var response = await request.POSTAsync(
                    Constants.IS_PRODUCTION_ENVIRONMENT_TRANSFERWISE ?
                    Constants.TransferWiseEndpoints.TRANSFERWISE_PRODUCTION_CREATE_QUOTE_URL :
                    Constants.TransferWiseEndpoints.TRANSFERWISE_SANDBOX_CREATE_QUOTE_URL,
                    new StringContent(payload), token);
                var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<CreateQuoteResponseModel>(jsonString);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
        /// <summary>
        /// Create transfer method
        /// </summary>
        /// <param name="request">Implements the IGenerate interface</param>
        /// <param name="data">CreateTransferRequestModel model</param>
        /// <param name="token">transferwise api token</param>
        /// <returns>CreateTransferResponseModel object</returns>
        public async Task<object> CreateTransfer(IGenerateRequest request, CreateTransferRequestModel data, string token)
        {
            try
            {
                string payload = JsonConvert.SerializeObject(data);
                var response = await request.POSTAsync(
                    Constants.IS_PRODUCTION_ENVIRONMENT_TRANSFERWISE ?
                    Constants.TransferWiseEndpoints.TRANSFERWISE_PRODUCTION_CREATE_TRANSFER_URL :
                    Constants.TransferWiseEndpoints.TRANSFERWISE_SANDBOX_CREATE_TRANSFER_URL,
                    new StringContent(payload), token);
                var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<CreateTransferResponseModel>(jsonString);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
        /// <summary>
        /// Fund transfer method
        /// </summary>
        /// <param name="request">Implements the IGenerate interface</param>
        /// <param name="data">FundTransferModel model</param>
        /// <param name="token">transferwise api token</param>
        /// <returns>FundTransferResponseModel object</returns>
        public async Task<object> FundTransfer(IGenerateRequest request, FundTransferModel data, string token)
        {
            try
            {
                string root = Constants.IS_PRODUCTION_ENVIRONMENT_TRANSFERWISE ? 
                    Constants.TransferWiseEndpoints.TRANSFERWISE_PRODUCTION_FUND_TRANSFER_URL : 
                    Constants.TransferWiseEndpoints.TRANSFERWISE_SANDBOX_FUND_TRANSFER_URL;

                string endpoint = $"{root}{ data.Profile}/transfers/{ data.Transfer}/payments";
                var uriBuilder = new UriBuilder(endpoint);
                Uri finalUrl = uriBuilder.Uri;
                string uri = finalUrl.ToString();

                string payloadString = JsonConvert.SerializeObject(data);
                var jsonPayload = JsonConvert.DeserializeObject<FundTransferRequestModel>(payloadString);
                string jsonPayloadString = JsonConvert.SerializeObject(jsonPayload);
                var response = await request.POSTAsync(
                    uri,
                    new StringContent(jsonPayloadString), token);
                var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<FundTransferResponseModel>(jsonString);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}
