//-------------------------------------------------------------------------
// Copyright (c) The Zig Group Ltd.  All rights reserved.
//-------------------------------------------------------------------------
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Security.Authentication;
using ZigOps.PaymentProcessing.Data.Interface;

namespace ZigOps.PaymentProcessing.Data.Builders
{
    public class MongoClientBuilder : IMongoClientBuilder
    {
        private readonly IConfiguration _config;
        public MongoClientBuilder(IConfiguration configuration)
        {
            _config = configuration;
        }

        IMongoDatabase IMongoClientBuilder.BuilderMongoClient()
        {
            string connectionString = this._config["DataBase:AuthenticationKey"];
            MongoClientSettings settings = MongoClientSettings.FromUrl(
              new MongoUrl(connectionString)
            );
            settings.SslSettings =
              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            var databaseName = this._config["DataBase:DataBaseName"];
            var db = mongoClient.GetDatabase(databaseName);
            return db;
        }
    }
}
