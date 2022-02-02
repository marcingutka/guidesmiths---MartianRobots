using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MartianRobots.Data.Providers
{
    public class DatabaseProvider<T> : IDatabaseProvider<T>
    {
        private readonly IMongoDatabase database;

        public DatabaseProvider(IMongoClient client, IOptions<DatabaseConfig> config)
        {
            database = client.GetDatabase(config.Value.MongoDB);
            CheckConnection();
        }

        public IMongoCollection<T> GetCollection()
        {
            return database.GetCollection<T>(typeof(T).Name);
        }

        private void CheckConnection()
        {
            var command = new BsonDocumentCommand<BsonDocument>(new BsonDocument { { "ping", 1 } });
            try
            {
                database.RunCommand(command);
            }
            catch (TimeoutException)
            {
                throw new TimeoutException("Connection to MongoDB is not established. Check if server is running.");
            }
        }
    }
}
