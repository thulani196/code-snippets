using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace FlynnDW.Logic.Interfaces
{
    public interface ITbeParser
    {
        /// <summary>
        /// Interface for CSV parser
        /// </summary>
        /// <param name="inputBlob"></param>
        /// <param name="name"></param>
        /// <param name="binder"></param>
        /// <param name="sourceDir"></param>
        /// <param name="targetDir"></param>
        Task ParseFile(Stream inputBlob,string name,Binder binder,string sourceDir,string targetDir);

        /// <summary>
        /// Interface for XML parser
        /// </summary>
        /// <param name="inputBlob"></param>
        /// <param name="name"></param>
        /// <param name="binder"></param>
        /// <param name="targetDir"></param>
        Task ParseXmlAsync(Stream inputBlob,
            string name,
            Binder binder,
            string targetDir);
        /// <summary>
        /// Interface for ZipParser
        /// </summary>
        /// <param name="inputBlob"></param>
        /// <param name="name"></param>
        /// <param name="binder"></param>
        /// <param name="sourceDir"></param>
        /// <param name="targetDir"></param>
        Task ZipParser(Stream inputBlob
            , string name
            , Binder binder
            , string sourceDir
            , string targetDir);
    }
}