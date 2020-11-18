//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using System.Threading.Tasks;
using ZigOps.PaymentProcessing.Data.Models.TransferWise;

namespace ZigOps.PaymentProcessing.Service.Interfaces.TransferWise
{
    public interface ITransferWiseService
    {
        Task PaymentRequest(TransferWiseQueueModel data, string token);
        Task TransferRequest(string data);
        Task FundRequest(string data);
        Task<object> CreateProfile(string token);
        Task<object> CreateRecipient(CreateRecipientRequestModel data, string token);
        Task<object> CreateQuote(CreateQuoteRequestModel data, string token);
        Task<object> CreateTransfer(CreateTransferRequestModel data, string token);
        Task<object> FundTransfer(FundTransferModel data, string token);
    }
}
