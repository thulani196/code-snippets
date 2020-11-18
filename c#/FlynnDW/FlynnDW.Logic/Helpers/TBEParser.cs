using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FlynnDW.Data.Constants;
using FlynnDW.DataLayer.Models;
using FlynnDW.Logic.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static System.DateTime;

namespace FlynnDW.Logic.Helpers
{
    public class TbeParser:ITbeParser
    {
        private readonly ILogger<TbeParser> _logger;
        public TbeParser(ILogger<TbeParser> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Function to extract data from CSV files.
        /// </summary>
        /// <param name="inputBlob">Input file</param>
        /// <param name="name">File name</param>
        /// <param name="binder">Binder to write content to another file</param>
        /// <param name="sourceDir">Source for files</param>
        /// <param name="targetDir">Destination for output files</param>
        public async Task ParseFile(Stream inputBlob,
            string name,
            Binder binder,
            string sourceDir,
            string targetDir)
        {
            _logger.LogInformation("CSV parser helper started");
            try
            {
                // Use this create a Dictionary of the different CSVs
                // First string is the filename, second is the csv info
                var csvDict = new Dictionary<string, string>();

                inputBlob.Position = 0;
                using (var sr = new StreamReader(inputBlob, true))
                {
                    
                    var stream = sr.ReadLine();

                    csvDict.Add(Constants.FileTypes["000"], stream + Environment.NewLine);

                    // Read each line of the csv file and 
                    while (!sr.EndOfStream)
                    {
                        stream = sr.ReadLine();

                        if (stream != null)
                        {
                            var code = stream.Split(',')[2].Replace("\"", "");

                            // If our file list has a code then write to it. 
                            // Otherwise jsut write to the code folder.
                            var tblName = Constants.FileTypes.ContainsKey(code) ? Constants.FileTypes[code] : code;

                            // If there is an existing key, add the lines to it
                            // otherwise add a new key
                            if (csvDict.ContainsKey(tblName))
                            {
                                csvDict[tblName] += stream + Environment.NewLine;
                            }
                            else
                            {
                                csvDict.Add(tblName, stream + Environment.NewLine);
                            }
                        }
                    }
                }

                var currTime = UtcNow;
                var fileDateTime = currTime.ToString("yyyyMMdd_HHmmss");
                var pathYear = currTime.ToString("yyyy");
                var pathMonth = currTime.ToString("MM");
                var pathDay = currTime.ToString("dd");

                //  Now loop through Dict and write out to blobs
                if (csvDict.Count > 0)
                {
                    foreach (var d in csvDict)
                    {
                        var fileName = d.Key + "_" + fileDateTime + ".csv";
 
                        var attributes = new Attribute[]
                        {
                            
                        new BlobAttribute($"{targetDir}/{pathYear}/{pathMonth}/{pathDay}/{fileName}", FileAccess.Write),
                        new StorageAccountAttribute("BlobConnection")
                        }; 

                        _logger.LogInformation(fileName);  
                        using (var writer = await binder.BindAsync<TextWriter>(attributes))
                        {
                            await writer.WriteAsync(d.Value);
                        } 

                    }
                }

            }
            catch (Exception ex)
            { 
                var attributes = new Attribute[]
                {
                    new BlobAttribute($"{sourceDir}/errorfiles/{name}", FileAccess.Write),
                    new StorageAccountAttribute("BlobConnection")
                }; 
                _logger.LogInformation($"Writing error file out to {sourceDir}/errorfiles/{name}");

                using (var writer = await binder.BindAsync<TextWriter>(attributes))
                {
                    writer.Write(inputBlob);
                } 
                _logger.LogError(ex.Message);
            }

        }


        /// <summary>
        /// Function to extract data from XML data files
        /// </summary>
        /// <param name="inputBlob">Input file</param>
        /// <param name="name">File name</param>
        /// <param name="binder">Binder to write content to another file</param>
        /// <param name="targetDir">Destination for output files</param>
        public async Task ParseXmlAsync(Stream inputBlob,
            string name,
            Binder binder,
            string targetDir)
        {
  
            inputBlob.Position = 0;
            var serializer = new XmlSerializer(typeof(POSLog));

            using (var stream = new StreamReader(inputBlob))
            {
                var posLog = (POSLog) serializer.Deserialize(stream);
                var payload = JsonConvert.SerializeObject(posLog); 
                await SaveToJsonAsync(name, targetDir, binder, payload);
            }
           
        }

        /// <summary>
        /// This function extracts data files from a Zipped folder and dumps then in a directory on Blob storage in readiness to be extracted.
        /// </summary>
        /// <param name="inputBlob">Input file</param>
        /// <param name="name">File name</param>
        /// <param name="binder">Binder to write content to another file</param>
        /// <param name="sourceDir">Source for files</param>
        /// <param name="targetDir">Destination for output files</param>
        /// <returns></returns>
         public  async Task ZipParser(Stream inputBlob
            , string name
            , Binder binder 
            , string sourceDir
            , string targetDir
            )
        {

            try
            {
                inputBlob.Position = 0;

                var currTime = UtcNow;
                var fileDateTime = currTime.ToString("yyyyMMdd_HHmmss");
                var pathYear = currTime.ToString("yyyy");
                var pathMonth = currTime.ToString("MM");
                var pathDay = currTime.ToString("dd");

                using (var zip = new ZipArchive(inputBlob))
                {
                    foreach (var entry in zip.Entries)
                    { 
                        var tblName = ParseName(entry.FullName);
                        var newFileName = entry.FullName + "_" + fileDateTime; 
                        var newBlobFilePath = $"{targetDir}/{pathYear}/{pathMonth}/{pathDay}/{newFileName}";

                        // Write out the error files
                        //log.Info("Setting up error blob bindings");
                        var attributes = new Attribute[]
                        {
                            new BlobAttribute(newBlobFilePath, FileAccess.Write),
                            new StorageAccountAttribute("BlobConnection")
                        };

                        using (var writer = await binder.BindAsync<TextWriter>(attributes))
                        {
                            writer.Write(entry.Open());
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                // Write out the error files
                //log.Info("Setting up error blob bindings");
                var attributes = new Attribute[]
                {
                    new BlobAttribute($"{sourceDir}/errorfiles/{name}", FileAccess.Write),
                    new StorageAccountAttribute("BlobConnection")
                }; 
                _logger.LogInformation($"Writing error file out to {sourceDir}/errorfiles/{name}");

                using (var writer = await binder.BindAsync<TextWriter>(attributes))
                {
                    writer.Write(inputBlob);
                }

                _logger.LogError(ex.Message);
            } 
        }
        /// <summary>
        /// Generates filename.
        /// </summary>
        /// <param name="fileName">Filename</param>
        /// <returns></returns>
        private static string ParseName(string fileName)
        {
            var tableName = fileName.Substring(0, fileName.IndexOf("_", StringComparison.Ordinal));

            return tableName;
        }
        /// <summary>
        /// This function gets extracted data and write it to Blob storage in a file
        /// </summary>
        /// <param name="name">File name</param>
        /// <param name="binder">Binder to write content to another file</param>
        /// <param name="targetDir">Target directory for files</param>
        /// <param name="jsonData">Data to be written to json file</param>
        /// <returns></returns>
        private static async Task SaveToJsonAsync( string name, string targetDir, Binder binder, string jsonData)
        {
            // Generate filename
            var currTime = UtcNow;
            var fileDateTime = currTime.ToString("yyyyMMdd_HHmmss");
            var pathYear = currTime.ToString("yyyy");
            var pathMonth = currTime.ToString("MM");
            var pathDay = currTime.ToString("dd");

            var fileName = name + "_" + fileDateTime + ".json";
            
            var attributes = new Attribute[]
            {
                new BlobAttribute($"{targetDir}/{pathYear}/{pathMonth}/{pathDay}/{fileName}", FileAccess.Write),
                new StorageAccountAttribute("BlobConnection")
            };
            using (var writer = await binder.BindAsync<TextWriter>(attributes))
            {
                await writer.WriteAsync(jsonData);
            }
        }

    }
}
