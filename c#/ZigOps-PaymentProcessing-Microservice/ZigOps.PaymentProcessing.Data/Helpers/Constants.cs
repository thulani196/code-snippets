//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
namespace ZigOps.PaymentProcessing.Data.Helpers
{   
    /// <summary>
    /// Defines all constant variables accessed thoughout the application
    /// </summary>
    public class Constants
    {
        //Todo: These settings have to be set in the database
        public const bool IS_PRODUCTION_ENVIRONMENT_TRANSFERWISE = false;
        public const bool IS_PRODUCTION_ENVIRONMENT_OKRA = false;
        public const string TABLE_DB_CONNECTION_STRING = "zigops-db-connection-string";
        public const string PAYMENTS_TABLE_DB_CONNECTION_STRING = "UseDevelopmentStorage=true";
        public class TableDBConstant
        {
            public const string OKRA_DIRECT_DEBITS_PARTITION = "DirectDebits";
            public const string OKRA_CALLBACKS_PARTITION = "Callbacks";

            public const string OKRA_DIRECT_DEBITS_TABLE = "OkraDirectDebits";
            public const string OKRA_CALLBACKS_TABLE = "OkraCallbacks";

            public const string TRANSFERWISE_CREATE_QUOTES_PARTITION = "TWQuotes";
            public const string TRANSFERWISE_CREATE_TRANSFERS_PARTITION = "TWTransfers";
            public const string TRANSFERWISE_FUND_TRANSFERS_PARTITION = "TWFundTransfers";

            public const string TRANSFERWISE_QUOTES_TABLE = "TransferWiseQuotes";
            public const string TRANSFERWISE_TRANSFERS_TABLE = "TransferWiseTransfers";
            public const string TRANSFERWISE_FUNDED_TABLE = "TransferWiseFunded";

        }
        public class RequestMethod
        {
            public const string METHOD_TYPE_GET = "GET";
            public const string METHOD_TYPE_POST = "POST";
        }
        public class ApiEndpoints
        {
            public const string ROOT = "payment/v1/";
            public const string OKRA_DIRECT_DEBIT = ROOT + "okra/debit/create";

            public const string TRANSFERWISE_CREATE_PROFILE = ROOT + "transferwise/profile/create";
            public const string TRANSFERWISE_CREATE_RECIPIENT = ROOT + "transferwise/recipient/create";
        }
        public class OkraEndpoints
        {
            public const string OKRA_SANDBOX_BASE_URL = "https://api.okra.ng/sandbox/v1/";
            public const string OKRA_PRODUCTION_BASE_URL = "https://api.okra.ng/v1/";

            public const string OKRA_SANDBOX_PROCESS_DIRECT_DEBIT = OKRA_SANDBOX_BASE_URL + "debit/create";
            public const string OKRA_SANDBOX_CALLBACK_URL = OKRA_SANDBOX_BASE_URL + "callback";

            public const string OKRA_PRODUCTION_PROCESS_DIRECT_DEBIT = OKRA_PRODUCTION_BASE_URL + "debit/create";
            public const string OKRA_PRODUCTION_CALLBACK_URL = OKRA_PRODUCTION_BASE_URL + "callback";
        }

        public class OkraOptions
        {
            public const bool GARNISH = false;
            public const bool SUCCESS = true;
            public const bool TESTING = true;
            public const string STATUS = "pending";
        }

        public class TransferWiseEndpoints
        {
            public const string TRANSFERWISE_SANDBOX_BASE_URL = "https://api.sandbox.transferwise.tech/v1/";
            public const string TRANSFERWISE_PRODUCTION_BASE_URL = "https://api.transferwise.com/v1/";

            public const string TRANSFERWISE_SANDBOX_CREATE_PROFILE_URL = TRANSFERWISE_SANDBOX_BASE_URL + "profiles";
            public const string TRANSFERWISE_SANDBOX_CREATE_QUOTE_URL = TRANSFERWISE_SANDBOX_BASE_URL + "quotes";
            public const string TRANSFERWISE_SANDBOX_CREATE_RECIPIENT_URL = TRANSFERWISE_SANDBOX_BASE_URL + "accounts";
            public const string TRANSFERWISE_SANDBOX_CREATE_TRANSFER_URL = TRANSFERWISE_SANDBOX_BASE_URL + "transfers";
            public const string TRANSFERWISE_SANDBOX_FUND_TRANSFER_URL = "https://api.sandbox.transferwise.tech/v3/profiles/";

            public const string TRANSFERWISE_PRODUCTION_CREATE_PROFILE_URL = TRANSFERWISE_PRODUCTION_BASE_URL + "profiles";
            public const string TRANSFERWISE_PRODUCTION_CREATE_QUOTE_URL = TRANSFERWISE_PRODUCTION_BASE_URL + "quotes";
            public const string TRANSFERWISE_PRODUCTION_CREATE_RECIPIENT_URL = TRANSFERWISE_PRODUCTION_BASE_URL + "accounts";
            public const string TRANSFERWISE_PRODUCTION_CREATE_TRANSFER_URL = TRANSFERWISE_PRODUCTION_BASE_URL + "transfers";
            public const string TRANSFERWISE_PRODUCTION_FUND_TRANSFER_URL = "https://api.sandbox.transferwise.tech/v3/profiles/";
        }
        public class TransferWiseStatus
        {
            public const string CREATE_TRANSFER_SUCCESS_STATUS = "incoming_payment_waiting";
            public const string CREATE_TRANSFER_PENDING_STATUS = "processing";
            public const string FUND_TRANSFER_STATUS = "COMPLETED";
        }

        public class RequestHeaders
        {
            public const string APPLICATION_JSON_CONTENT_TYPE = "application/json";
            public const string HEADER_AUTHORIZATION = "Authorization";
            public const string BEARER = "Bearer";
        }

        public class SMTPSettings
        {
            public const string Server = "smtp.office365.com";
            public const int Port = 587;
            public const string SenderName = "The Zig LLC";
            public const string SenderEmail = "zweli@thezig.io";
            public const string Username = "zweli@thezig.io";
            public const string Chocklate = "VFg2Mix2cEBGd25WWX5V"; //This will be swept up soon
        }

        public class Queues
        {
            public const string OKRA_DIRECT_DEBIT = "okra-direct-debit";
            public const string TRANSFERWISE_CREATE_TRANSFER = "transferwise-create-transfer";
            public const string TRANSFERWISE_FUND_TRANSFER = "transferwise-fund-transfer";
            public const string TRANSFERWISE_DIRECT_DEBITS = "transferwise-direct-debits";
            public const string PAYRUN = "payrun";
        }
    }
}
