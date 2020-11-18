//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using System.Threading.Tasks;

namespace ZigOps.PaymentProcessing.Data.Interface
{
    public interface IMessagePublisher
    {
        Task Publish(string queue, string message);
    }
}
