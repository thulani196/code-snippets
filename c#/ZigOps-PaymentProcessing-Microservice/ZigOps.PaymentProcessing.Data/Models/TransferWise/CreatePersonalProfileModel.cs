//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZigOps.PaymentProcessing.Data.Models.TransferWise
{
    public class CreatePersonalProfileModel
    {
        
        [JsonProperty("MyArray")]
        public List<PersonalProfile> Personal { get; set; }

        public class PersonalProfile
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
        }

    }
}
