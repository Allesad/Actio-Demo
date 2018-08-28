using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Actio.Common.Mongo
{
    public class MongoSeeder : IDatabaseSeeder
    {
        protected readonly IMongoDatabase Db;

        public MongoSeeder(IMongoDatabase db)
        {
            Db = db;
        }

        public async Task SeedAsync()
        {
            var collectionCursor = await Db.ListCollectionsAsync();
            var collections = await collectionCursor.ToListAsync();

            if (collections.Any()) return;
            await CustomSeedAsync();
        }

        protected virtual Task CustomSeedAsync()
        {
            return Task.CompletedTask;
        }
    }
}