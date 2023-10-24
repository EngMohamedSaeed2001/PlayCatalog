using MongoDB.Driver;
using PlayCatalog.Entity;

namespace PlayCatalog.Reository
{
    public class ItemRepo
    {
        private const string collectionName = "items";
        private readonly IMongoCollection<Item> dbCollection;
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;


        public ItemRepo()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("Catalog");
            dbCollection = database.GetCollection<Item>(collectionName);
        }

        public async Task<IReadOnlyCollection<Item>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<Item> GetAsync(Guid id)
        {
          FilterDefinition<Item> filterDefinition =filterBuilder.Eq(entity => entity.Id, id);

            return await dbCollection.Find(filterDefinition).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            await dbCollection.InsertOneAsync(item);
        }

        public async Task UpdateAsync(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            FilterDefinition<Item> filterDefinition = filterBuilder.Eq(entity => entity.Id, item.Id);

            await dbCollection.ReplaceOneAsync(filterDefinition,item);
        }

        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<Item> filterDefinition = filterBuilder.Eq(entity => entity.Id, id);

            await dbCollection.DeleteOneAsync(filterDefinition);
        }
    }
}
