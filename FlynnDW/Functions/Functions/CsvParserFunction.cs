using System;
using System.IO;
using System.Threading.Tasks;
using FlynnDW.Logic.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FlynnDW.Functions
{
    public  class CsvParserFunction
    {
        private readonly ILogger<CsvParserFunction> _logger;
        private readonly IFileParserService _service;
        public CsvParserFunction(ILogger<CsvParserFunction> logger, ITbeParser parser, IFileParserService service)
        {
            _logger = logger;
            _service = service;
        }
        [FunctionName("CsvParserFunction")]
        public  async Task RunAsync([BlobTrigger("dls/raw/tbe/{name}", Connection = "BlobConnection")]
            Stream myBlob, string name,Binder binder)
        {
            try
            {
                _logger.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
                await _service.ParseCsv(myBlob, name, binder);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                if (e.InnerException != null) _logger.LogError(e.InnerException.Message);
                _logger.LogError(e.StackTrace);
                throw;
            }
        }
    }
}