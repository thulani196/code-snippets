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
        /// Used to fetch json files from storage and extract content
        /// </summary>
        /// <param name="inputBlob">Input Json FIles</param>
        /// <param name="binder"> Used to write content to file</param>
        /// <param name="name">File name</param>
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
