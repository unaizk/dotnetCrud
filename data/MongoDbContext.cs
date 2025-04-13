using MongoDB.Driver;
using dotnetCrud.Models;
using Microsoft.Extensions.Options;

namespace dotnetCrud.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }
        public IMongoCollection<User> Users
        {
            get
                {
                    return _database.GetCollection<User>("Users");
                }
        }
    }
}
