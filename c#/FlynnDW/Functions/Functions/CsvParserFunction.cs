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

        /// <summary>
        /// Function to parse and extract data from csv file
        /// </summary>
        /// <param name="myBlob">Path to folder containing csv file(s)</param>
        /// <param name="name">Name of file, dynamically populated according to file name in iteration</param>
        /// <param name="binder">Used to write content to files and save them in Blob Storage</param>
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