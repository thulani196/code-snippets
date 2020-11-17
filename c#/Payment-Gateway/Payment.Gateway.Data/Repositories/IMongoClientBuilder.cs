using MongoDB.Driver;

namespace Payment.Gateway.Data.Repositories
{
  public  interface IMongoClientBuilder
    {
        IMongoDatabase BuilderMongoClient();
    }
}
