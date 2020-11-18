//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using System.Threading.Tasks;
using ZigOps.PaymentProcessing.Data.Models.Okra;

namespace ZigOps.PaymentProcessing.Service.Interfaces.Okra
{
    public interface IOkraService
    {
        Task CreateDirectDebit(OkraQueueModel data, string token);
        Task Callback(string record, string token);
    }
}
