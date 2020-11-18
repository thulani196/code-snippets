//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using System.Net.Http;
using System.Threading.Tasks;

namespace ZigOps.PaymentProcessing.Data.Interface
{
    /// <summary>
    /// Defines the <see cref="IGenerateRequest" />.
    /// </summary>
    public interface IGenerateRequest
    {
        // <summary>
        /// Defines the GETAsync.
        /// </summary>
        /// <param name="uri">The uri<see cref="string"/>.</param>
        /// <param name="token">The token<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{HttpResponseMessage}"/>.</returns>
        Task<HttpResponseMessage> GETAsync(string uri, string token);

        /// <summary>
        /// Defines the POSTAsync.
        /// </summary>
        /// <param name="uri">.</param>
        /// <param name="content">.</param>
        /// <param name="token">The token<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{HttpResponseMessage}"/>.</returns>
        Task<HttpResponseMessage> POSTAsync(string uri, HttpContent content, string token = null);
    }
}
