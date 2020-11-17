using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Payment.Gateway.Data.Models
{
    public class PaymentResponseBindModel
    {
        [FromQuery(Name = "vpc_Message")]
        public string VpcMessage { get; set; }

        [FromQuery(Name = "vpc_3DSXID")]
        public string Vpc3dSXID { get; set; }

        [FromQuery(Name = "vpc_AcqResponseCode")]
        public string VpcAcqResponseCode { get; set; }

        [FromQuery(Name = "vpc_Amount")]
        public string VpcAmount { get; set; }

        [FromQuery(Name = "vpc_AuthorizeId")]
        public string VpcAuthorizedId { get; set; }

        [FromQuery(Name = "vpc_BatchNo")]
        public string VpcBatchNo { get; set; }

        [FromQuery(Name = "vpc_Card")]
        public string VpcCard { get; set; }

        [FromQuery(Name = "vpc_Command")]
        public string VpcCommand { get; set; }

        [FromQuery(Name = "vpc_Currency")]
        public string VpcCurrency { get; set; }

        [FromQuery(Name = "vpc_MerchTxnRef")]
        public string VpcMerchTxnRef { get; set; }

        [FromQuery(Name = "vpc_OrderInfo")]
        public string VpcOrderInfo { get; set; }

        [FromQuery(Name = "vpc_ReceiptNo")]
        public string VpcReceiptNo { get; set; }

        [FromQuery(Name = "vpc_TransactionNo")]
        public string VpcTransactionNo { get; set; }

        [FromQuery(Name = "vpc_TxnResponseCode")]
        public string VpcTxnResponseCode { get; set; }

        [FromQuery(Name = "vpc_VerType")]
        public string VpcVerType { get; set; }
    }
}
