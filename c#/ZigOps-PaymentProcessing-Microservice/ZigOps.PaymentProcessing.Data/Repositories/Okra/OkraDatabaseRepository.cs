//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheZig.Keyvault.Library;
using ZigOps.PaymentProcessing.Data.Helpers;
using ZigOps.PaymentProcessing.Data.Interface.Okra;
using ZigOps.PaymentProcessing.Data.Models.Okra;
using ZigOps.PaymentProcessing.Data.Models.Okra.Database;

namespace ZigOps.PaymentProcessing.Data.Repositories.Okra
{
    public class OkraDatabaseRepository : IOkraDatabaseRepository
    {
        private readonly IConfiguration _config;
        private readonly ILogger<OkraDatabaseRepository> _logger;
        private readonly IKeyvault _keyVault;
        public OkraDatabaseRepository(IConfiguration config, ILogger<OkraDatabaseRepository> logger, IKeyvault keyvault)
        {
            _config = config;
            _logger = logger;
            _keyVault = keyvault;
        }
        public async Task<Callbacks> CreateCallbackResponseAsync(Callbacks callbacks, string tableName)
        {
            try
            {
                var currentTable = await GetTableAsync(tableName).ConfigureAwait(false);
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(callbacks);
                TableResult result = await currentTable.ExecuteAsync(insertOrMergeOperation);
                var stringResponse = JsonConvert.SerializeObject(result.Result);
                var response = JsonConvert.DeserializeObject<Callbacks>(stringResponse);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        public async Task<DirectDebits> CreateDirectDebitsAsync(DirectDebits directDebits, string tableName)
        {
            try
            {
                var currentTable = await GetTableAsync(tableName).ConfigureAwait(false);
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(directDebits);
                TableResult result = await currentTable.ExecuteAsync(insertOrMergeOperation);
                var stringResponse = JsonConvert.SerializeObject(result.Result);
                var response = JsonConvert.DeserializeObject<DirectDebits>(stringResponse);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        public async Task<CloudStorageAccount> CreateStorageAccountAsync()
        {
            CloudStorageAccount storageAccount;
            try
            {
                var dbConnectionString = await _keyVault.GetSecreteAsync(
                    _config["KeyvaultEndpoint"],
                    Constants.TABLE_DB_CONNECTION_STRING).ConfigureAwait(false);
                storageAccount = CloudStorageAccount.Parse(dbConnectionString);
            }
            catch (FormatException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
            return storageAccount;
        }

        public async Task<DirectDebits> GetDirectDebitsByStatusAsync(string status, string tableName)
        {
            try
            {
                var currentTable = await GetTableAsync(tableName).ConfigureAwait(false);
                var result = currentTable.CreateQuery<DirectDebits>()
                    .Where(response => JsonConvert.DeserializeObject<DirectDebitResponseModel>(response.Response).Status.Equals(status));
                 return (DirectDebits)result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        public async Task<CloudTable> GetTableAsync(string tableName)
        {
            var storageAccount = await CreateStorageAccountAsync();
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference(tableName);
            return table;
        }
    }
}
