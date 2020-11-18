//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------

namespace ZigOps.PaymentProcessing.Data.Models
{
    public class EmailModel
    {
        public string MessageBody { get; set; }

        public string Recipient { get; set; }

        public string RecipientName { get; set; }

        public string Subject { get; set; }
    }
}
