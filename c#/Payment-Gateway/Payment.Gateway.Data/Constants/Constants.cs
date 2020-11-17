namespace Payment.Gateway.Data.Constants
{
    public  static class Constants
    {
        public  class AppSettings
        {
            public const string Paymentserviceurlname = "vpc-PaymentServerURL";
            public const string VpcMerchant = "GatewayProperties:vpc-Merchant";
            public const string VpcAccessCode = "GatewayProperties:vpc-AccessCode";
            public const string VpcOperatorName = "GatewayProperties:vpc-OperatorName";
            public const string VpcOperatorPassword = "GatewayProperties:vpc-OperatorPassword";
            public const string VpcPaymentReturnUrl = "GatewayProperties:vpc-PaymentReturnUrl";
            public const string VpcPaymentServerUrl = "GatewayProperties:vpc-PaymentServerURL";
            public const string VpcSecureSecret = "GatewayProperties:vpc-SecureSecret";
            public const string MtnAuthString = "MtnConfiguration:AuthString"; 
            public const string ChargeBackClient = "ChargeBackClient";
            public const string MtnRequestToPayUrl = "MtnConfiguration:RequestToPay";
        }

        public static class VpcRequestFields
        {
            public const string vpc_SecureHash = "vpc_SecureHash";
            public const string vpc_SecureHashType = "vpc_SecureHashType";
            public const string vpc_SecureHashType_Value = "SHA256";
            public const string vpc_TxnResponseCode = "vpc_TxnResponseCode";
            public const string vpc_Message = "vpc_Message";
            public const string vpc_Version = "vpc_Version";
            public const string vpc_Command = "vpc_Command";
            public const string vpc_Merchant = "vpc_Merchant";
            public const string vpc_MerchTxnRef = "vpc_MerchTxnRef";
            public const string vpc_OrderInfo = "vpc_OrderInfo";
            public const string vpc_Amount = "vpc_Amount";
            public const string vpc_Currency = "vpc_Currency";
            public const string vpc_Card = "vpc_Card";
            public const string vpc_CardNum = "vpc_CardNum";
            public const string vpc_CardExp = "vpc_CardExp";
            public const string vpc_Gateway = "vpc_Gateway";
            public const string vpc_ReturnURL = "vpc_ReturnURL";
            public const string vpc_CardSecurityCode = "vpc_CardSecurityCode";
            public const string vpc_AccessCode = "vpc_AccessCode";
            public const string vpc_command = "pay";
            public const string vpc_version = "1";
            public const string vpc_gateway = "ssl";
        }
        
        
        public static class StaticFormFields
        {
            public const string TYPE = "type";
            public const string TYPE_VALUE = "hidden";
            public const string NAME = "name";
            public const string VALUE = "value";
            public const string FORM = "<form id='PaymentRedirectForm' name='PaymentRedirectForm' method='post' action='{0}'> </form>";
            public const string INPUT = "input";
        }

        public static class VpcRequestValues
        {
            public const string vpc_version = "1";
            public const string vpc_Gateway = "1";
            public const string vpc_EncodingFormat = "ISO-8859-1";
            public const string vpc_ContentType = "application/x-www-form-urlencoded; charset=iso-8859-1";
        }

        public static class VpcRefundFields
        {
            public const string vpc_TransNo = "vpc_TransNo";
            public const string vpc_User = "vpc_User";
            public const string vpc_Password = "vpc_Password";
            public const string vpc_command = "refund";
        }

        public static class VpcVoidFields
        {
            public const string vpc_Command = "voidPurchase";
        }

        public static class VpcQueryDRFields
        {
            public const string vpc_Command = "queryDR";
        }

        public static class MtnRequestFields
        {
            public const string subscriptionKey = "Ocp-Apim-Subscription-Key";
            public const string referenceId = "X-Reference-Id";
            public const string authorizationKey = "Authorization";
            public const string targetEnvironmentKey = "X-Target-Environment";
            public const string targetEnvironmentValue = "sandbox";
            public const string MakePaymentEndPoint = "/collection/v1_0/requesttopay";
            public const string RetrievePaymentEndPoint = "/collection/v1_0/requesttopay/{0}";
            public const string ContentType = "application/json";
            public const string UserEndPoint = "/v1_0/apiuser";
            public const string TokenEndPoint = "/collection/token/";
            public const string MtnClientName = "MtnClient";
            public const string BearerKeyWord = "Bearer {0}";
            public const string BasicAuthKeyword = "Basic {0}";
            public const int uuidMaxLength = 36;
        }

        public static class EasyCachingFields
        {
            public const string InMemoryCache = "InMemoryCache";
            public const string CacheKey = "Token";
        }
    }
}
