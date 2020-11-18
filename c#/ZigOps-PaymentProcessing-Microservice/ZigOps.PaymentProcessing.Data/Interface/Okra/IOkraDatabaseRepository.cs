//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Microsoft.Azure.Cosmos.Table;
using System.Threading.Tasks;
using ZigOps.PaymentProcessing.Data.Models.Okra.Database;

namespace ZigOps.PaymentProcessing.Data.Interface.Okra
{
    public interface IOkraDatabaseRepository
    {
        Task<CloudStorageAccount> CreateStorageAccountAsync();
        Task<CloudTable> GetTableAsync(string tableName);
        Task<DirectDebits> CreateDirectDebitsAsync(DirectDebits directDebits, string tableName);
        Task<Callbacks> CreateCallbackResponseAsync(Callbacks callbacks, string tableName);
        Task<DirectDebits> GetDirectDebitsByStatusAsync(string status, string tableName);
    }
}
