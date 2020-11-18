//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------

namespace ZigOps.PaymentProcessing.Test.Mocks
{
    public class ProfileResponse
    {
        /// <summary>
        /// Create Profile mocked response
        /// </summary>
        /// <returns></returns>
        public static string Profile()
        {
            return "[\r\n" +
           "    {\r\n" +
           "        \"id\": 1" +
           "        \"type\": \"" +
           "personal\",\r\n" +
           "        \"details\":" +
           " {\r\n" +
           "            \"firstN" +
           "ame\": \"Test" +
           "\",\r\n" +
           "            \"lastNa" +
           "me\": \"Tester\",\r" +
           "\n" +
           "            \"dateOf" +
           "Birth\": \"1985-06-2" +
           "0\",\r\n" +
           "            \"phoneN" +
           "umber\": \"+44203808" +
           "7139\",\r\n" +
           "            \"avatar" +
           "\": null,\r\n" +
           "            \"occupa" +
           "tion\": null,\r\n" +
           "            \"occupa" +
           "tions\": null,\r\n" +
           "            \"primar" +
           "yAddress\": 1" +
           "\r\n" +
           "        }\r\n" +
           "    },\r\n" +
           "    {\r\n" +
           "        \"id\": 1" +
           "        \"type\": \"" +
           "business\",\r\n" +
           "        \"details\":" +
           " {\r\n" +
           "            \"name\"" +
           ": \"The Company\",\r\n" +
           "            \"regist" +
           "rationNumber\": \"07" +
           "            \"acn\":" +
           " null,\r\n" +
           "            \"abn\":" +
           " null,\r\n" +
           "            \"arbn\"" +
           ": null,\r\n" +
           "            \"compan" +
           "yType\": \"LIMITED\"" +
           ",\r\n" +
           "            \"compan" +
           "yRole\": \"OWNER\"," +
           "\r\n" +
           "            \"descri" +
           "ptionOfBusiness\": " +
           "\"IT_SERVICES\",\r\n" +
           "            \"primar" +
           "yAddress\": 1" +
           ",\r\n" +
           "            \"webpag" +
           "e\": null,\r\n" +
           "            \"busine" +
           "ssCategory\": \"IT_S" +
           "ERVICES\",\r\n" +
           "            \"busine" +
           "ssSubCategory\": nul" +
           "l\r\n" +
           "        }\r\n" +
           "    }\r\n" +
           "]";
        }
    }
}
