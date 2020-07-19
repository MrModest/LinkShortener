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
        
        public async Task<IEnumerable<Link>> GetAll() =>
            await _links.Find(book => true).ToListAsync();

        public async Task<Link?> GetByAlias(string alias)
        {
            return await _links.Find(link => link.ShortAlias == alias)
                .FirstOrDefaultAsync();
        }

        public async Task<Link?> GetByFullLink(string fullLink)
        {
            return await _links.Find(link => link.FullLink == fullLink)
                .FirstOrDefaultAsync();
        }

        public async Task<Link> CreateNewLink(Link link)
        {
            await _links.InsertOneAsync(link);
            return link;
        }

        public async Task<Link> UpdateLink(Link updatedLink)
        {
            var options = new FindOneAndUpdateOptions<Link> { ReturnDocument = ReturnDocument.After};
            return await _links.FindOneAndUpdateAsync<Link>(
                link => link.Id == updatedLink.Id,
                Builders<Link>.Update.Inc(link => link.VisitedCount, 1), 
                options);
        }
    }
}