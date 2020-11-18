//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using System.Threading.Tasks;
using ZigOps.PaymentProcessing.Data.Interface;
using ZigOps.PaymentProcessing.Data.Interface.TransferWise;
using ZigOps.PaymentProcessing.Data.Models.TransferWise;
using ZigOps.PaymentProcessing.Service.Interfaces.TransferWise;

namespace ZigOps.PaymentProcessing.Service.Services.TransferWise
{
    public class TransferWiseService : ITransferWiseService
    {
        private readonly ITransferWiseRepository _transferWiseRepository;
        private readonly IGenerateRequest _generateRequest;

        public TransferWiseService(ITransferWiseRepository transferWiseRepository, IGenerateRequest generateRequest)
        {
            _transferWiseRepository = transferWiseRepository;
            _generateRequest = generateRequest;
        }
        public async Task PaymentRequest(TransferWiseQueueModel data, string token)
        {
            await _transferWiseRepository.PaymentRequest(data, token).ConfigureAwait(false);
        }
        public async Task TransferRequest(string data)
        {
            await _transferWiseRepository.TransferRequest(data).ConfigureAwait(false);
        }
        public async Task FundRequest(string data)
        {
            await _transferWiseRepository.FundRequest(data).ConfigureAwait(false);
        }
        public async Task<object> CreateProfile(string token)
        {
            return await _transferWiseRepository.CreateProfile(_generateRequest, token).ConfigureAwait(false);
        }
        public async Task<object> CreateRecipient(CreateRecipientRequestModel data, string token)
        {
            return await _transferWiseRepository.CreateRecipient(_generateRequest, data, token).ConfigureAwait(false);
        }
        public async Task<object> CreateTransfer(CreateTransferRequestModel data, string token)
        {
            return await _transferWiseRepository.CreateTransfer(_generateRequest, data, token).ConfigureAwait(false);
        }
        public async Task<object> CreateQuote(CreateQuoteRequestModel data, string token)
        {
            return await _transferWiseRepository.CreateQuote(_generateRequest, data, token).ConfigureAwait(false);
        }
        public async Task<object> FundTransfer(FundTransferModel data, string token)
        {
            return await _transferWiseRepository.FundTransfer(_generateRequest, data, token).ConfigureAwait(false);
        }
    }
}
