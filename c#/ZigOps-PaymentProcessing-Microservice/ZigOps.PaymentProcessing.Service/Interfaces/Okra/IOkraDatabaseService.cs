//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using System.Threading.Tasks;
using ZigOps.PaymentProcessing.Data.Models.Okra.Database;

namespace ZigOps.PaymentProcessing.Service.Interfaces.Okra
{
    public interface IOkraDatabaseService
    {
        Task<DirectDebits> GetDirectDebitsByStatusAsync(string status, string tableName);
    }
}
