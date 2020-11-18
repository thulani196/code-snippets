using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Text;

using static System.DateTime;
using Microsoft.WindowsAzure.Storage;
using System.Collections.Generic;
using FlynnDW.Data.Constants;

namespace FlynnDW.Functions
{
    public static class HttpTriggerFunc
    {
        [FunctionName("HttpTriggerFunc")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request."); string connectionString = "BlobConnection";
            CloudStorageAccount blobAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = blobAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("dls");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            //await ListBlobsFlatListingAsync(blobContainer, name);

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }

        private static async Task ParseFile(MemoryStream inputBlob,
           string name,
            Binder binder,
           string sourceDir,
           string targetDir)
        {
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


                        BlobAttribute blobAttribute = new BlobAttribute($"{targetDir}/{pathYear}/{pathMonth}/{pathDay}/{fileName}", FileAccess.Write);
                        StorageAccountAttribute storageAccountAttribute = new StorageAccountAttribute("BlobConnection");
                        var attributes = new Attribute[]
                        {
                        blobAttribute,
                        storageAccountAttribute
                        };

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

                using (var writer = await binder.BindAsync<TextWriter>(attributes))
                {
                    writer.Write(inputBlob);
                }
            }
        }
    }
}
