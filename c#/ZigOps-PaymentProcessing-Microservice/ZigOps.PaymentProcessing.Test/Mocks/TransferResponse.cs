//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------

namespace ZigOps.PaymentProcessing.Test.Mocks
{
    public class TransferResponse
    {
        /// <summary>
        /// Create transfer mocked response
        /// </summary>
        /// <returns></returns>
        public static string Transfer()
        {
            return "{\r\n" +
           "    \"id\": 1,\r\n" +
           "    \"user\": 1,\r\n" +
           "    \"targetAccount" +
           "\": 1,\r\n" +
           "    \"sourceAccount" +
           "\": null,\r\n" +
           "    \"quote\": 1,\r" +
           "\n" +
           "    \"quoteUuid\": " +
           "\"1\",\r\n" +
           "    \"status\": \"in" +
           "coming_payment_waiti" +
           "ng\",\r\n" +
           "    \"reference\": " +
           "\"to my employee\"," +
           "\r\n" +
           "    \"rate\": 0.0,\r" +
           "\n" +
           "    \"created\": \"2" +
           "020-11-01 20:56:46\"" +
           ",\r\n" +
           "    \"business\": 1," +
           "\r\n" +
           "    \"transferReques" +
           "t\": null,\r\n" +
           "    \"details\": {\r" +
           "\n" +
           "        \"reference" +
           "\": \"to my employee" +
           "\"\r\n" +
           "    },\r\n" +
           "    \"hasActiveIssue" +
           "s\": false,\r\n" +
           "    \"sourceCurrency" +
           "\": \"USD\",\r\n" +
           "    \"sourceValue\":" +
           " 0.49,\r\n" +
           "    \"targetCurrency" +
           "\": \"ZMW\",\r\n" +
           "    \"targetValue\":" +
           " 0.0,\r\n" +
           "    \"customerTransa" +
           "ctionId\": \"f98bff6" +
           "c-7830-4bfe-9cf5-828" +
           "789c6b797\"\r\n" +
           "}";
        }
    }
}
