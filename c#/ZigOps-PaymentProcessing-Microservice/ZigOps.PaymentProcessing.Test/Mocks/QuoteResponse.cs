//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------

namespace ZigOps.PaymentProcessing.Test.Mocks
{
    public class QuoteResponse
    {
        /// <summary>
        /// Create Quote mocked response
        /// </summary>
        /// <returns></returns>
        public static string Quote()
        {
            return "{\r\n" +
           "    \"id\": 1,\r\n" +
           "    \"source\": \"US" +
           "D\",\r\n" +
           "    \"target\": \"ZM" +
           "W\",\r\n" +
           "    \"sourceAmount\"" +
           ": 3.98,\r\n" +
           "    \"targetAmount\"" +
           ": 10.0,\r\n" +
           "    \"type\": \"BALA" +
           "NCE_PAYOUT\",\r\n" +
           "    \"rate\": 20.538" +
           "6,\r\n" +
           "    \"createdTime\":" +
           " \"2020-11-04T15:33:" +
           "41.758Z\",\r\n" +
           "    \"createdByUserI" +
           "d\": 1,\r\n" +
           "    \"profile\": 1," +
           "\r\n" +
           "    \"business\": 1," +
           "\r\n" +
           "    \"rateType\": \"" +
           "FIXED\",\r\n" +
           "    \"deliveryEstima" +
           "te\": \"2020-11-05T2" +
           "1:00:00.000Z\",\r\n" +
           "    \"fee\": 3.49,\r" +
           "\n" +
           "    \"feeDetails\": " +
           "{\r\n" +
           "        \"transferwi" +
           "se\": 3.49,\r\n" +
           "        \"payIn\": 0" +
           ".0,\r\n" +
           "        \"discount\"" +
           ": 0.0,\r\n" +
           "        \"priceSetId" +
           "\": 134,\r\n" +
           "        \"partner\":" +
           " 0\r\n" +
           "    },\r\n" +
           "    \"allowedProfile" +
           "Types\": [\r\n" +
           "        \"PERSONAL\"" +
           ",\r\n" +
           "        \"BUSINESS\"" +
           "\r\n" +
           "    ],\r\n" +
           "    \"guaranteedTarg" +
           "etAmount\": false,\r" +
           "\n" +
           "    \"ofSourceAmount" +
           "\": false\r\n" +
           "}";
        }
    }
}
