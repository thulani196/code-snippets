
namespace Payment.Gateway.Data.Repositories
{
    using System.Security.Authentication;
    using Microsoft.Extensions.Configuration;
    using MongoDB.Driver;

    public class MongoClientBuilder : IMongoClientBuilder
    {
        private readonly IConfiguration _config;
        public MongoClientBuilder(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IMongoDatabase BuilderMongoClient()
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
