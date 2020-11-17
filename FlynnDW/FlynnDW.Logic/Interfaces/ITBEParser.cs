using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace FlynnDW.Logic.Interfaces
{
    public interface ITbeParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputBlob"></param>
        /// <param name="name"></param>
        /// <param name="binder"></param>
        /// <param name="sourceDir"></param>
        /// <param name="targetDir"></param>
        /// <returns></returns>
        Task ParseFile(Stream inputBlob,string name,Binder binder,string sourceDir,string targetDir);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputBlob"></param>
        /// <param name="name"></param>
        /// <param name="binder"></param>
        /// <param name="targetDir"></param>
        /// <returns></returns>
        Task ParseXmlAsync(Stream inputBlob,
            string name,
            Binder binder,
            string targetDir);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputBlob"></param>
        /// <param name="name"></param>
        /// <param name="binder"></param>
        /// <param name="sourceDir"></param>
        /// <param name="targetDir"></param>
        /// <returns></returns>
        Task ZipParser(Stream inputBlob
            , string name
            , Binder binder
            , string sourceDir
            , string targetDir);

    }
}