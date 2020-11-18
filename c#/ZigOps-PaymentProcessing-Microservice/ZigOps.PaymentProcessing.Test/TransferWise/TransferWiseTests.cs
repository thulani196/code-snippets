//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Autofac.Extras.Moq;
using Moq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using ZigOps.PaymentProcessing.Data.Interface;
using ZigOps.PaymentProcessing.Data.Models.TransferWise;
using ZigOps.PaymentProcessing.Data.Repositories.TransferWise;
using ZigOps.PaymentProcessing.Test.Mocks;

namespace ZigOps.PaymentProcessing.Test
{
    /// <summary>
    /// TransferWise Unit Tests
    /// </summary>
    public class TransferWiseTests
    { 
        private readonly ITestOutputHelper _output;
        public TransferWiseTests(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact]
        public async Task CreateProfile_Successful()
        {
            string token = "yjydryj-tyfytk-hcgjcgc";
            HttpContent response = new StringContent(ProfileResponse.Profile());
            HttpResponseMessage message = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = response };

            var _iGenerateRequestsMock = AutoMock.GetLoose();
            _iGenerateRequestsMock.Mock<IGenerateRequest>()
                   .Setup(x => x.GETAsync(It.IsAny<string>(), It.IsAny<string>()))
                   .ReturnsAsync(message);
            IGenerateRequest generateRequest = _iGenerateRequestsMock.Create<IGenerateRequest>();

            var TransferwiseMock = AutoMock.GetLoose();
            var transferWiseRepository = TransferwiseMock.Create<TransferWiseRepository>();
            var actual = await transferWiseRepository.CreateProfile(generateRequest, token).ConfigureAwait(false);
            _output.WriteLine($"Response: {actual}");
            _output.WriteLine($"Type: {actual.GetType()}");
            
            Assert.True(actual != null);
            Assert.Equal(ProfileResponse.Profile(), actual);
            Assert.True(actual.GetType() == typeof(string));
        }

        [Fact]
        public async Task CreateRecipient_Successful()
        {
            string token = "yjydryj-tyfytk-hcgjcgc";
            HttpContent response = new StringContent(RecipientResponse.Recipient());
            HttpResponseMessage message = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = response };

            var _iGenerateRequestsMock = AutoMock.GetLoose();
            _iGenerateRequestsMock.Mock<IGenerateRequest>()
                   .Setup(x => x.POSTAsync(It.IsAny<string>(), It.IsAny<HttpContent>(), It.IsAny<string>()))
                   .ReturnsAsync(message);
            IGenerateRequest generateRequest = _iGenerateRequestsMock.Create<IGenerateRequest>();

            CreateRecipientRequestModel createRecipient = new CreateRecipientRequestModel
            {
                Currency = "NGN",
                Type = "sort_code",
                Profile = 1,
                AccountHolderName = "Test Account",
                LegalType = "PRIVATE",
                Detail = new CreateRecipientRequestModel.Details
                {
                    SortCode = "1",
                    AccountNumber = "1"
                }
            };

            var TransferwiseMock = AutoMock.GetLoose();
            var transferWiseRepository = TransferwiseMock.Create<TransferWiseRepository>();
            var actual = await transferWiseRepository.CreateRecipient(generateRequest, createRecipient, token).ConfigureAwait(false);
            _output.WriteLine($"Response: {actual}");
            _output.WriteLine($"Type: {actual.GetType()}");
            
            Assert.True(actual != null);
            Assert.True(actual.GetType() == typeof(CreateRecipientResponseModel));
        }

        [Fact]
        public async Task CreateQuote_Successful()
        {
            string token = "yjydryj-tyfytk-hcgjcgc";
            HttpContent response = new StringContent(QuoteResponse.Quote());
            HttpResponseMessage message = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = response };

            var _iGenerateRequestsMock = AutoMock.GetLoose();
            _iGenerateRequestsMock.Mock<IGenerateRequest>()
                   .Setup(x => x.POSTAsync(It.IsAny<string>(), It.IsAny<HttpContent>(), It.IsAny<string>()))
                   .ReturnsAsync(message);
            IGenerateRequest generateRequest = _iGenerateRequestsMock.Create<IGenerateRequest>();

            CreateQuoteRequestModel createQuote = new CreateQuoteRequestModel
            {
                Profile = 1,
                Source = "USD",
                Target = "ZMW",
                RateType = "FIXED",
                TargetAmount = 1000.0,
                Type = "BALANCE_PAYOUT"
            };

            var TransferwiseMock = AutoMock.GetLoose();
            var transferWiseRepository = TransferwiseMock.Create<TransferWiseRepository>();
            var actual = await transferWiseRepository.CreateQuote(generateRequest, createQuote, token).ConfigureAwait(false);
            _output.WriteLine($"Response: {actual}");
            _output.WriteLine($"Type: {actual.GetType()}");

            Assert.True(actual != null);
            Assert.True(actual.GetType() == typeof(CreateQuoteResponseModel));
        }

        [Fact]
        public async Task CreateTransfer_Successful()
        {
            string token = "yjydryj-tyfytk-hcgjcgc";
            HttpContent response = new StringContent(TransferResponse.Transfer());
            HttpResponseMessage message = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = response };

            var _iGenerateRequestsMock = AutoMock.GetLoose();
            _iGenerateRequestsMock.Mock<IGenerateRequest>()
                   .Setup(x => x.POSTAsync(It.IsAny<string>(), It.IsAny<HttpContent>(), It.IsAny<string>()))
                   .ReturnsAsync(message);
            IGenerateRequest generateRequest = _iGenerateRequestsMock.Create<IGenerateRequest>();

            CreateTransferRequestModel createTransfer = new CreateTransferRequestModel
            {
                TargetAccount = 1,
                Quote = 1,
                CustomerTransactionId = "uuidv4 string",
                Detail = new CreateTransferRequestModel.Details
                {
                    Reference = "my ref",
                    TransferPurpose = "verification.transfers.purpose.pay.bills",
                    SourceOfFunds = "verification.source.of.funds.other"
                }
            };

            var TransferwiseMock = AutoMock.GetLoose();
            var transferWiseRepository = TransferwiseMock.Create<TransferWiseRepository>();
            var actual = await transferWiseRepository.CreateTransfer(generateRequest, createTransfer, token).ConfigureAwait(false);
            _output.WriteLine($"Response: {actual}");
            _output.WriteLine($"Type: {actual.GetType()}");

            Assert.True(actual != null);
            Assert.True(actual.GetType() == typeof(CreateTransferResponseModel));
        }

        [Fact]
        public async Task FundTransfer_Successful()
        {
            string token = "yjydryj-tyfytk-hcgjcgc";
            HttpContent response = new StringContent(FundTransferResponse.Fund());
            HttpResponseMessage message = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = response };

            var _iGenerateRequestsMock = AutoMock.GetLoose();
            _iGenerateRequestsMock.Mock<IGenerateRequest>()
                   .Setup(x => x.POSTAsync(It.IsAny<string>(), It.IsAny<HttpContent>(), It.IsAny<string>()))
                   .ReturnsAsync(message);
            IGenerateRequest generateRequest = _iGenerateRequestsMock.Create<IGenerateRequest>();

            FundTransferModel fundTransfer = new FundTransferModel
            {
                Type = "BALANCE",
                Profile = "1",
                Transfer = 1
            };

            var TransferwiseMock = AutoMock.GetLoose();
            var transferWiseRepository = TransferwiseMock.Create<TransferWiseRepository>();
            var actual = await transferWiseRepository.FundTransfer(generateRequest, fundTransfer, token).ConfigureAwait(false);
            _output.WriteLine($"Response: {actual}");
            _output.WriteLine($"Type: {actual.GetType()}");

            Assert.True(actual != null);
            Assert.True(actual.GetType() == typeof(FundTransferResponseModel));
        }
    }
}
