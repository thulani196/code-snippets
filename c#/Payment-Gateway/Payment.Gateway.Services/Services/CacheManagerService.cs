using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyCaching.Core;
using Microsoft.Extensions.Logging;
using Payment.Gateway.Data.Constants;
using Payment.Gateway.Data.Models.MtnModels;
using Payment.Gateway.Logic.Interfaces;

namespace Payment.Gateway.Logic.Services
{
    public class CacheManagerService : ICacheManager
    {
        private readonly IEasyCachingProviderFactory _factory;
        /// <summary>
        /// Defines the _logger
        /// </summary>
        private readonly ILogger<CacheManagerService> _logger;
        private readonly IHelpers _helpers;
        public CacheManagerService(IEasyCachingProviderFactory factory, ILogger<CacheManagerService> logger, IHelpers helpers)
        {
            _factory = factory;
            _logger = logger;
            _helpers = helpers;
        }

        /// <summary>
        /// Takes a string as a key parameter and check the in memory cache if an Item (Token) of the same key exist, if it doesn't, 
        /// calls the MTN API to generate and return a new token then stores it in Cache for future use.
        /// </summary>
        /// <param name="key">Cache Item Name</param>
        /// <returns>Access Token as String</returns>
        public async Task<string> GetOrSetTokenAsync(string key)
        {
            var provider = _factory.GetCachingProvider(Constants.EasyCachingFields.InMemoryCache);

            try
            {
                var tokenItem = await provider.GetAsync<string>(key);

                if (tokenItem.IsNull)
                {
                    var token = await _helpers.GetAccessTokenAsync<TokenObject>().ConfigureAwait(false);
                    await provider.SetAsync(key, token.AccessToken, TimeSpan.FromSeconds(token.ExpiresIn));

                    return token.AccessToken;
                }
                else
                {
                    return tokenItem.ToString();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return null;

        }

    }
}
