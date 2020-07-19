using System.Collections.Generic;
using System.Threading.Tasks;
using LinkShortener.Data;
using LinkShortener.Entities;
using LinkShortener.Repositories;
using MongoDB.Driver;

namespace LinkShortener.RepositoryImpls
{
    public class LinkRepository : ILinkRepository
    {
        private readonly IMongoCollection<Link> _links;

        public LinkRepository(ILinksDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _links = database.GetCollection<Link>(databaseSettings.LinksCollectionName);
        }
        
        public async Task<IEnumerable<Link>> GetAll(string userId) =>
            await _links.Find(l => l.UserId == userId).ToListAsync();

        public async Task<Link?> GetByAlias(string alias)
        {
            return await _links.Find(l => l.ShortAlias == alias)
                .FirstOrDefaultAsync();
        }

        public async Task<Link?> GetByFullLink(string fullLink, string userId)
        {
            return await _links.Find(l => l.FullLink == fullLink && l.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<Link> CreateNewLink(Link link)
        {
            await _links.InsertOneAsync(link);
            return link;
        }

        public async Task<Link> IncreaseVisitedCountByAlias(string shortAlias)
        {
            var options = new FindOneAndUpdateOptions<Link> { ReturnDocument = ReturnDocument.After};
            return await _links.FindOneAndUpdateAsync<Link>(
                l => l.ShortAlias == shortAlias,
                Builders<Link>.Update.Inc(l => l.VisitedCount, 1), 
                options);
        }
    }
}