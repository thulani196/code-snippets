//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using MongoDB.Driver;

namespace ZigOps.PaymentProcessing.Data.Interface
{
    interface IMongoClientBuilder
    {
        IMongoDatabase BuilderMongoClient();
    }
}
