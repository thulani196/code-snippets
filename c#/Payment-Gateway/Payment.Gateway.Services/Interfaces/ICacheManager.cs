

namespace Payment.Gateway.Logic.Interfaces
{
    using Data.Models.MtnModels;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    
    /// <summary>
    /// ICacheManager defines methods that are used for cache handling/management
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Definition for a function that Get the Token value or Sets a new one if the key does not Exist/
        /// </summary>
        /// <param name="key">Unique Key to identify cache item</param>
        /// <returns>Cached item as String</returns>
        Task<string> GetOrSetTokenAsync(string key);
    }
}
