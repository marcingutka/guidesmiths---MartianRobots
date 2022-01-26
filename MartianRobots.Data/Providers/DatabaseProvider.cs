using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace MartianRobots.Data.Providers
{
    public class DatabaseProvider<T> : IDatabaseProvider<T>
    {
        private readonly IMongoClient client;
        private readonly IOptions<DatabaseConfig> config;


        public DatabaseProvider(IMongoClient client, IOptions<DatabaseConfig> config)
        {
            this.client = client;
            this.config = config;
        }

        public IMongoCollection<T> GetCollection()
        {
            var database = client.GetDatabase(config.Value.MongoDB);
            return database.GetCollection<T>(typeof(T).Name);
        }
    }
}
