//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using System.Threading.Tasks;
using ZigOps.PaymentProcessing.Data.Interface;
using ZigOps.PaymentProcessing.Data.Interface.Okra;
using ZigOps.PaymentProcessing.Data.Models.Okra;
using ZigOps.PaymentProcessing.Data.Models.Okra.Database;
using ZigOps.PaymentProcessing.Service.Interfaces.Okra;

namespace ZigOps.PaymentProcessing.Service.Services.Okra
{
    public class OkraService : IOkraService
    {
        private readonly IOkraRepository _directDebitRepository;
        private readonly IGenerateRequest _generateRequest;

        public OkraService(IOkraRepository directDebitRepository,
                           IGenerateRequest generateRequest)
        {
            _directDebitRepository = directDebitRepository;
            _generateRequest = generateRequest;
        }
        public async Task CreateDirectDebit(OkraQueueModel data, string token)
        {
            await _directDebitRepository.CreateDirectDebit(_generateRequest, data, token);
        }
        public async Task Callback(string record, string token)
        {
            await _directDebitRepository.Callback(_generateRequest, record, token);
        }
    }
}
