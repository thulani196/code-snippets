//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using System.Threading.Tasks;
using ZigOps.PaymentProcessing.Data.Interface.Okra;
using ZigOps.PaymentProcessing.Data.Models.Okra.Database;
using ZigOps.PaymentProcessing.Service.Interfaces.Okra;

namespace ZigOps.PaymentProcessing.Service.Services.Okra
{
    public class OkraDatabaseService : IOkraDatabaseService
    {
        private readonly IOkraDatabaseRepository _okraDatabaseRepository;

        public OkraDatabaseService(IOkraDatabaseRepository okraDatabaseRepository)
        {
            _okraDatabaseRepository = okraDatabaseRepository;
        }
        public async Task<DirectDebits> GetDirectDebitsByStatusAsync(string status, string tableName)
        {
            return await _okraDatabaseRepository.GetDirectDebitsByStatusAsync(status, tableName);
        }
    }
}
