using System;
using System.IO;
using FlynnDW.Logic.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FlynnDW.Functions
{
    public class ParseToJsonFunction
    {
        private readonly IFileParserService _service;
        private readonly ILogger<ParseToJsonFunction> _logger;
        public ParseToJsonFunction(IFileParserService service, ILogger<ParseToJsonFunction> logger)
        {
            _service = service;
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputBlob"></param>
        /// <param name="binder"></param>
        /// <param name="name"></param>
        [FunctionName("ParseToJson")]
        public  async void Run(
            [BlobTrigger( "dls/raw/poslog/{name}", Connection = "BlobConnection")] 
            Stream inputBlob, 
            Binder binder,
            string name)
        { 
           try
           {
               _logger.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {inputBlob.Length} Bytes");
               await _service.ParseXmlToJson(inputBlob, name, binder);
           }
           catch (Exception e)
           {
                  _logger.LogError(e.Message);
                  if (e.InnerException != null) _logger.LogError(e.InnerException.Message);
                  _logger.LogError(e.StackTrace);
           }
        }
    }
}
