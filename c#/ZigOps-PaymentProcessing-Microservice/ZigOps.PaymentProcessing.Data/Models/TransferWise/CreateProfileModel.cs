//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZigOps.PaymentProcessing.Data.Models.TransferWise
{
    public class CreateProfileModel
    {
        
        [JsonProperty("MyArray")]
        public List<Profiles> Profile { get; set; }

        public class Profiles
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("details")]
            public Details Detail { get; set; }
        }

        public class Details
        {
            [JsonProperty("firstName")]
            public string FirstName { get; set; }

            [JsonProperty("lastName")]
            public string LastName { get; set; }

            [JsonProperty("dateOfBirth")]
            public string DateOfBirth { get; set; }

            [JsonProperty("phoneNumber")]
            public string PhoneNumber { get; set; }

            [JsonProperty("avatar")]
            public object Avatar { get; set; }

            [JsonProperty("occupation")]
            public object Occupation { get; set; }

            [JsonProperty("occupations")]
            public object Occupations { get; set; }

            [JsonProperty("primaryAddress")]
            public int PrimaryAddress { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("registrationNumber")]
            public string RegistrationNumber { get; set; }

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

            [JsonProperty("webpage")]
            public object Webpage { get; set; }

            [JsonProperty("businessCategory")]
            public string BusinessCategory { get; set; }

            [JsonProperty("businessSubCategory")]
            public object BusinessSubCategory { get; set; }
        }
    }
}
