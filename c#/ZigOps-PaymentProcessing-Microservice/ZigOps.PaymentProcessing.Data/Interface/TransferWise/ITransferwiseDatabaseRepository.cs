//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Microsoft.Azure.Cosmos.Table;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZigOps.PaymentProcessing.Data.Models.TransferWise.Database;

namespace ZigOps.PaymentProcessing.Data.Interface.TransferWise
{
    public interface ITransferwiseDatabaseRepository
    {
        Task<CloudStorageAccount> CreateStorageAccountAsync();
        Task<CloudTable> GetTableAsync(string tableName);
        Task<Quotes> CreateQuoteAsync(Quotes quote, string tableName);
        Task<Transfers> CreateTransferAsync(Transfers transfer, string tableName);
        Task<Funded> FundTransferAsync(Funded fund, string tableName);
        Task<Quotes> GetQuoteByIdAsync(string id);
        Task<Transfers> GetTransfersByIdAsync(string id);
        Task<Funded> GetFundedByIdAsync(string id);
        Task<List<Quotes>> GetAllQuotesAsync();
        Task<List<Transfers>> GetAllTransfersAsync();
        Task<List<Funded>> GetAllFundedTransfersAsync();
    }
}
