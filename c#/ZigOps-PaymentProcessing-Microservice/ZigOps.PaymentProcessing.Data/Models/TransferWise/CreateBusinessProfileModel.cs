//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZigOps.PaymentProcessing.Data.Models.TransferWise
{
    public class CreateBusinessProfileModel
    {
        [JsonProperty("MyArray")]
        public List<BusinessProfile> Business { get; set; }

        public class BusinessProfile
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("details")]
            public Details Details { get; set; }
        }

        public class Details
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("registrationNumber")]
            public string RegistrationNumber;

            [JsonProperty("acn")]
            public object Acn { get; set; }

            [JsonProperty("abn")]
            public object Abn { get; set; }

            [JsonProperty("arbn")]
            public object Arbn { get; set; }

            [JsonProperty("companyType")]
            public string CompanyType { get; set; }

            [JsonProperty("companyRole")]
            public string CompanyRole { get; set; }

            [JsonProperty("descriptionOfBusiness")]
            public string DescriptionOfBusiness { get; set; }

            [JsonProperty("primaryAddress")]
            public int PrimaryAddress { get; set; }

            [JsonProperty("webpage")]
            public object Webpage { get; set; }

            [JsonProperty("businessCategory")]
            public string BusinessCategory { get; set; }

            [JsonProperty("businessSubCategory")]
            public object BusinessSubCategory { get; set; }
        }
    }
}
