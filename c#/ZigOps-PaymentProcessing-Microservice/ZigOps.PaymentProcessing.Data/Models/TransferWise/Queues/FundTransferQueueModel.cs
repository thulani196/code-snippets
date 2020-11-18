//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Newtonsoft.Json;

namespace ZigOps.PaymentProcessing.Data.Models.TransferWise
{
    public class FundTransferQueueModel : FundTransferModel
    {
        [JsonProperty("payrollRef")]
        public string PayrollRef { get; set; }

        [JsonProperty("employeeId")]
        public int EmployeeId { get; set; }

        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set; }

        [JsonProperty("Amount")]
        public double Amount { get; set; }

        [JsonProperty("CustomerTransactionId")]
        public string CustomerTransactionId { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("contact")]
        public Contact _Contact { get; set; }

        public class Contact
        {
            [JsonProperty("email")]
            public string Email { get; set; }
        }
    }
}
