//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Newtonsoft.Json;

namespace ZigOps.PaymentProcessing.Data.Models.TransferWise
{
    public class CreateRecipientResponseModel
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("business")]
        public int Business { get; set; }

        [JsonProperty("profile")]
        public int Profile { get; set; }

        [JsonProperty("accountHolderName")]
        public string AccountHolderName { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("details")]
        public Details Detail { get; set; }

        [JsonProperty("user")]
        public int User { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("ownedByCustomer")]
        public bool OwnedByCustomer { get; set; }

        public class Details
        {
            [JsonProperty("address")]
            public object Address { get; set; }

            [JsonProperty("email")]
            public object Email { get; set; }

            [JsonProperty("legalType")]
            public string LegalType { get; set; }

            [JsonProperty("accountNumber")]
            public string AccountNumber { get; set; }

            [JsonProperty("sortCode")]
            public string SortCode { get; set; }

            [JsonProperty("abartn")]
            public object Abartn { get; set; }

            [JsonProperty("accountType")]
            public object AccountType { get; set; }

            [JsonProperty("bankgiroNumber")]
            public object BankgiroNumber { get; set; }

            [JsonProperty("ifscCode")]
            public object IfscCode { get; set; }

            [JsonProperty("bsbCode")]
            public object BsbCode { get; set; }

            [JsonProperty("institutionNumber")]
            public object InstitutionNumber { get; set; }

            [JsonProperty("transitNumber")]
            public object TransitNumber { get; set; }

            [JsonProperty("phoneNumber")]
            public object PhoneNumber { get; set; }

            [JsonProperty("bankCode")]
            public object BankCode { get; set; }

            [JsonProperty("russiaRegion")]
            public object RussiaRegion { get; set; }

            [JsonProperty("routingNumber")]
            public object RoutingNumber { get; set; }

            [JsonProperty("branchCode")]
            public object BranchCode { get; set; }

            [JsonProperty("cpf")]
            public object Cpf { get; set; }

            [JsonProperty("cardNumber")]
            public object CardNumber { get; set; }

            [JsonProperty("idType")]
            public object IdType { get; set; }

            [JsonProperty("idNumber")]
            public object IdNumber { get; set; }

            [JsonProperty("idCountryIso3")]
            public object IdCountryIso3 { get; set; }

            [JsonProperty("idValidFrom")]
            public object IdValidFrom { get; set; }

            [JsonProperty("idValidTo")]
            public object IdValidTo { get; set; }

            [JsonProperty("clabe")]
            public object Clabe { get; set; }

            [JsonProperty("swiftCode")]
            public object SwiftCode { get; set; }

            [JsonProperty("dateOfBirth")]
            public object DateOfBirth { get; set; }

            [JsonProperty("clearingNumber")]
            public object ClearingNumber { get; set; }

            [JsonProperty("bankName")]
            public object BankName { get; set; }

            [JsonProperty("branchName")]
            public object BranchName { get; set; }

            [JsonProperty("businessNumber")]
            public object BusinessNumber { get; set; }

            [JsonProperty("province")]
            public object Province { get; set; }

            [JsonProperty("city")]
            public object City { get; set; }

            [JsonProperty("rut")]
            public object Rut { get; set; }

            [JsonProperty("token")]
            public object Token { get; set; }

            [JsonProperty("cnpj")]
            public object Cnpj { get; set; }

            [JsonProperty("payinReference")]
            public object PayinReference { get; set; }

            [JsonProperty("pspReference")]
            public object PspReference { get; set; }

            [JsonProperty("orderId")]
            public object OrderId { get; set; }

            [JsonProperty("idDocumentType")]
            public object IdDocumentType { get; set; }

            [JsonProperty("idDocumentNumber")]
            public object IdDocumentNumber { get; set; }

            [JsonProperty("targetProfile")]
            public object TargetProfile { get; set; }

            [JsonProperty("targetUserId")]
            public object TargetUserId { get; set; }

            [JsonProperty("taxId")]
            public object TaxId { get; set; }

            [JsonProperty("job")]
            public object Job { get; set; }

            [JsonProperty("nationality")]
            public object Nationality { get; set; }

            [JsonProperty("interacAccount")]
            public object InteracAccount { get; set; }

            [JsonProperty("bban")]
            public object Bban { get; set; }

            [JsonProperty("town")]
            public object Town { get; set; }

            [JsonProperty("postCode")]
            public object PostCode { get; set; }

            [JsonProperty("language")]
            public object Language { get; set; }

            [JsonProperty("IBAN")]
            public object IBAN { get; set; }

            [JsonProperty("iban")]
            public object Iban { get; set; }

            [JsonProperty("BIC")]
            public object BIC { get; set; }

            [JsonProperty("bic")]
            public object Bic { get; set; }
        }
    }
}
