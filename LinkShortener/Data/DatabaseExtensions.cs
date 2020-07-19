using MongoDB.Driver;

namespace LinkShortener.Data
{
    public static class DatabaseExtensions
    {
        public static IMongoCollection<TDocument> GetCollection<TDocument>(this ILinksDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            return database.GetCollection<TDocument>(databaseSettings.LinksCollectionName);
        } 
    }
}