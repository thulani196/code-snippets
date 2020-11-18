//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ZigOps.PaymentProcessing.Data.Interface;

namespace ZigOps.PaymentProcessing.Data.Helpers
{
    public class GenerateRequest : IGenerateRequest
    {
        /// <summary>
        /// Defines the _httpClient.
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateRequest"/> class.
        /// </summary>
        /// <param name="httpClient">The httpClient<see cref="HttpClient"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{GenerateRequest}"/>.</param>
        public GenerateRequest(HttpClient httpClient, ILogger<GenerateRequest> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// Generates a GET request
        /// </summary>
        /// <param name="uri">The uri<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{HttpResponseMessage}"/>.</returns>
        public async Task<HttpResponseMessage> GETAsync(string uri, string token = null)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.RequestHeaders.BEARER, token);
                HttpResponseMessage response = await _httpClient.GetAsync(uri).ConfigureAwait(false);
                return response;
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Generates a POST request.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="content"></param>
        /// <returns>The <see cref="Task{HttpResponseMessage}"/>.</returns>
        public async Task<HttpResponseMessage> POSTAsync(string uri, HttpContent content, string token = null)
        { 
            try
            {
                content.Headers.ContentType = new MediaTypeHeaderValue(Constants.RequestHeaders.APPLICATION_JSON_CONTENT_TYPE);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.RequestHeaders.BEARER, token);
                HttpResponseMessage response = await _httpClient.PostAsync(uri, content).ConfigureAwait(false);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}
