//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using System.Threading.Tasks;
using ZigOps.PaymentProcessing.Data.Models.Okra;

namespace ZigOps.PaymentProcessing.Data.Interface.Okra
{
    public interface IOkraRepository
    {
        Task CreateDirectDebit(IGenerateRequest _request, OkraQueueModel data, string token);
        Task<object> Callback(IGenerateRequest _request, string record, string token);
    }
}
