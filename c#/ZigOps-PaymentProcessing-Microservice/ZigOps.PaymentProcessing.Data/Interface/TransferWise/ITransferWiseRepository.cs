//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using System.Threading.Tasks;
using ZigOps.PaymentProcessing.Data.Models.TransferWise;

namespace ZigOps.PaymentProcessing.Data.Interface.TransferWise
{
    public interface ITransferWiseRepository
    {
        Task PaymentRequest(TransferWiseQueueModel data, string token);
        Task TransferRequest(string data);
        Task FundRequest(string data);
        Task<object> CreateProfile(IGenerateRequest request, string token);
        Task<object> CreateRecipient(IGenerateRequest request,CreateRecipientRequestModel data, string token);
        Task<object> CreateQuote(IGenerateRequest request, CreateQuoteRequestModel data, string token);
        Task<object> CreateTransfer(IGenerateRequest request, CreateTransferRequestModel data, string token);
        Task<object> FundTransfer(IGenerateRequest request, FundTransferModel data, string token);
    }
}