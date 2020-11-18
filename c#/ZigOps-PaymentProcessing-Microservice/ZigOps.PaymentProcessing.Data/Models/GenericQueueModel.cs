//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZigOps.PaymentProcessing.Data.Models
{
    /// <summary>
    /// Defines the generic request payload the direct debit queue must contain.
    /// </summary>
    public class GenericQueueModel
    {
        [JsonProperty("payroll_ref")]
        public string PayrollRef { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("sum_amount")]
        public double SumAmount { get; set; }

        [JsonProperty("authoriser_id")]
        public string AuthoriserId { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("employee")]
        public List<Employee> Employees { get; set; }
        public class Employee
        {
            [JsonProperty("employee_id")]
            public string EmployeeId { get; set; }

            [JsonProperty("amount")]
            public double Amount { get; set; }

            [JsonProperty("currency_code")]
            public string CurrencyCode { get; set; }

            [JsonProperty("source_account")]
            public string SourceAccount { get; set; }

            [JsonProperty("target_account")]
            public string TargetAccount { get; set; }

            [JsonProperty("contact")]
            public Contact Contact { get; set; }
        }
        public class Contact
        {
            [JsonProperty("email")]
            public string Email { get; set; }
        }
    }
}
