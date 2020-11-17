using System;
using System.IO;
using System.Threading.Tasks;
using FlynnDW.Data.Constants;
using FlynnDW.Logic.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;

namespace FlynnDW.Logic.Services
{
    public class FileParserService : IFileParserService
    {
        private readonly IConfiguration _config;
        private readonly ITbeParser _parser;
        public FileParserService(IConfiguration config, ITbeParser parser)
        {
            _config = config;
            _parser = parser;
        }

        public async Task ParseXmlToJson(Stream inputBlob,string name,Binder binder)
        {
            var tbeParsedRootDir = _config["PosLogParsedDir"];
            await _parser.ParseXmlAsync(inputBlob, name, binder, tbeParsedRootDir);
        }
        
        public async Task ParseZip(Stream inputBlob,string name,Binder binder)
        {
            var tbeRootDir = _config[Constants.AppSettings.TbeRootDir];
            var tbeParsedRootDir = _config[Constants.AppSettings.TbeParsedRootDir]; 
            await _parser.ZipParser(inputBlob, name, binder, tbeRootDir, tbeParsedRootDir);
        }
        
        public async Task ParseCsv(Stream inputBlob,string name,Binder binder)
        {
            var tbeRootDir = _config["TbeRootDir"];
            var tbeParsedRootDir = _config["TbeParsedDir"];
             await _parser.ParseFile(inputBlob, name, binder, tbeRootDir, tbeParsedRootDir);
        }
    }
}