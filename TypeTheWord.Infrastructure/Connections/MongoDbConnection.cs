using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace TypeTheWord.Infrastructure.Connections
{
    public class MongoDbConnection
    {

        private string _databaseName = "TypeTheFuckingWord"; //Rename 
        
        private IMongoDatabase _database;

        private readonly string _connStr;
        public MongoDbConnection(IConfiguration config)
        {
            _connStr = config.GetConnectionString("Default");

            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(_connStr));
            MongoClient client = new MongoClient(settings);

            _database = client.GetDatabase(_databaseName);
        }

        public IMongoDatabase GetDatabase()
        {
            return _database;
        }
        
    }
}
