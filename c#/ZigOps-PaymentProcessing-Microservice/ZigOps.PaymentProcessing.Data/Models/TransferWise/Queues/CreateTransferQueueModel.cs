//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Newtonsoft.Json;

namespace ZigOps.PaymentProcessing.Data.Models.TransferWise
{
    public class CreateTransferQueueModel
    {
        [JsonProperty("payrollRef")]
        public string PayrollRef { get; set; }

        [JsonProperty("profile")]
        public int Profile { get; set; }

        [JsonProperty("employeeId")]
        public int EmployeeId { get; set; }

        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set; }

        [JsonProperty("Amount")]
        public double Amount { get; set; }

        [JsonProperty("contact")]
        public Contact _Contact { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("transferRequest")]
        public TransferRequest _TransferRequest { get; set; }
        public class Contact
        {
            [JsonProperty("email")]
            public string Email { get; set; }
        }

        public class Details
        {
            [JsonProperty("Reference")]
            public string Reference { get; set; }

            [JsonProperty("TransferPurpose")]
            public string TransferPurpose { get; set; }

            [JsonProperty("SourceOfFunds")]
            public string SourceOfFunds { get; set; }
        }

        public class TransferRequest
        {
            [JsonProperty("TargetAccount")]
            public int TargetAccount { get; set; }

            [JsonProperty("Quote")]
            public int Quote { get; set; }

            [JsonProperty("customerTransactionId")]
            public string customerTransactionId { get; set; }

            [JsonProperty("details")]
            public Details Details { get; set; }
        }
    }
}
