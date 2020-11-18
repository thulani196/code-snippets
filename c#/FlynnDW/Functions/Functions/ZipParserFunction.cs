using System;
using System.IO;
using System.Threading.Tasks;
using FlynnDW.Logic.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging; 

namespace FlynnDW.Functions
{
    public   class ZipParserFunction
    {
        private readonly ILogger<ZipParserFunction> _logger;
        private readonly IFileParserService _service;
        public ZipParserFunction(ILogger<ZipParserFunction> logger, IFileParserService service)
        {
            _logger = logger;
            _service = service;
        }
        /// <summary>
        /// Used to pick and extract contents of a zip file
        /// </summary>
        /// <param name="myBlob">Blob file object</param>
        /// <param name="name">File name</param>
        /// <param name="binder">Used to write content to files.</param>
        /// <returns></returns>
        [FunctionName("ZipParserFunction")]
        public  async Task RunAsync([BlobTrigger("dls/raw/zip/{name}", Connection = "BlobConnection")]
            Stream myBlob, string name,Binder binder)
        {
            try
            {
                _logger.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes"); 
                await _service.ParseZip(myBlob, name, binder); 
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