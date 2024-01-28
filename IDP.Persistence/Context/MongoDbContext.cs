using IDP.Application.Context;
using MongoDB.Driver;

namespace IDP.Persistence.Context
{
    public class MongoDbContext<T> : IMongoDbContext<T>
    {
        private readonly IMongoDatabase db;
        private readonly IMongoCollection<T> collection;

        public MongoDbContext()
        {
            var client = new MongoClient();
            db = client.GetDatabase("LogDb");
            collection = db.GetCollection<T>(typeof(T).Name);
        }
        public IMongoCollection<T> GetCollection()
        {
            return collection;
        }
    }
}
