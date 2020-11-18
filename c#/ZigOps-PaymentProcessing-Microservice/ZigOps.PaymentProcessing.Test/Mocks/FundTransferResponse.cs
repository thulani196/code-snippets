//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------

namespace ZigOps.PaymentProcessing.Test.Mocks
{
    public class FundTransferResponse
    {
        /// <summary>
        /// Fund transfer mocked response
        /// </summary>
        /// <returns></returns>
        public static string Fund()
        {
            return "{\r\n" +
                   "    \"type\": \"BALA" +
                   "NCE\",\r\n" +
                   "    \"status\": \"CO" +
                   "MPLETED\",\r\n" +
                   "    \"errorCode\": n" +
                   "ull,\r\n" +
                   "    \"errorMessage\"" +
                   ": null,\r\n" +
                   "    \"balanceTransac" +
                   "tionId\": 1\r\n" +
                   "}";
        }
    }
}
