//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheZig.Keyvault.Library;
using ZigOps.PaymentProcessing.Data.Helpers;
using ZigOps.PaymentProcessing.Data.Interface.TransferWise;
using ZigOps.PaymentProcessing.Data.Models.TransferWise.Database;

namespace ZigOps.PaymentProcessing.Data.Repositories.TransferWise
{
    public class TransferwiseDatabaseRepository : ITransferwiseDatabaseRepository
    {
        private readonly IConfiguration _config;
        private readonly ILogger<TransferwiseDatabaseRepository> _logger;
        private readonly IKeyvault _keyVault;

        public TransferwiseDatabaseRepository(IConfiguration config, ILogger<TransferwiseDatabaseRepository> logger, IKeyvault keyvault)
        {
            _config = config;
            _logger = logger;
            _keyVault = keyvault;
        }
        public async Task<CloudStorageAccount> CreateStorageAccountAsync()
        {
            CloudStorageAccount storageAccount;
            try
            {
                Console.WriteLine($"Keyvault url {_config["KeyvaultEndpoint"]}");
                Console.WriteLine($"Keyvault url {Constants.TABLE_DB_CONNECTION_STRING}");
                var dbConnectionString = await _keyVault.GetSecreteAsync(
                   _config["KeyvaultEndpoint"],
                   Constants.TABLE_DB_CONNECTION_STRING).ConfigureAwait(false);
                //Console.WriteLine($"Con string: {dbConnectionString}");
                storageAccount = CloudStorageAccount.Parse(dbConnectionString);
            }
            catch(FormatException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
            catch(ArgumentException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
            return storageAccount;
        }
        public async Task<CloudTable> GetTableAsync(string tableName)
        {
            var storageAccount = await CreateStorageAccountAsync();
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference(tableName);
            return table;
        }
        public async Task<Quotes> CreateQuoteAsync(Quotes quote, string tableName)
        {
            try
            {
                var currentTable = await GetTableAsync(tableName).ConfigureAwait(false);
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(quote);
                TableResult result = await currentTable.ExecuteAsync(insertOrMergeOperation);
                var stringResponse = JsonConvert.SerializeObject(result.Result);
                var response = JsonConvert.DeserializeObject<Quotes>(stringResponse);
                return response;
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
        public Task<List<Quotes>> GetAllQuotesAsync()
        {
            throw new NotImplementedException();
        }
        public Task<Quotes> GetQuoteByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Transfers> CreateTransferAsync(Transfers transfer, string tableName)
        {
            try
            {
                var currentTable = await GetTableAsync(tableName).ConfigureAwait(false);
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(transfer);
                TableResult result = await currentTable.ExecuteAsync(insertOrMergeOperation);
                var stringResponse = JsonConvert.SerializeObject(result.Result);
                var response = JsonConvert.DeserializeObject<Transfers>(stringResponse);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        public async Task<Funded> FundTransferAsync(Funded fund, string tableName)
        {
            try
            {
                var currentTable = await GetTableAsync(tableName).ConfigureAwait(false);
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(fund);
                TableResult result = await currentTable.ExecuteAsync(insertOrMergeOperation);
                var stringResponse = JsonConvert.SerializeObject(result.Result);
                var response = JsonConvert.DeserializeObject<Funded>(stringResponse);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        public Task<Transfers> GetTransfersByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Funded> GetFundedByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Transfers>> GetAllTransfersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Funded>> GetAllFundedTransfersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
