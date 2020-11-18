//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using System.Threading.Tasks;
using ZigOps.PaymentProcessing.Data.Models;

namespace ZigOps.PaymentProcessing.Data.Interface
{
    public interface IEmailRepository
    {
        Task Send(EmailModel data);
    }
}
