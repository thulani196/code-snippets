using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace FlynnDW.Logic.Interfaces
{
    public interface IFileParserService
    {
        Task ParseXmlToJson(Stream inputBlob, string name, Binder binder);
        Task ParseZip(Stream inputBlob, string name, Binder binder);
        Task ParseCsv(Stream inputBlob, string name, Binder binder);
    }
}