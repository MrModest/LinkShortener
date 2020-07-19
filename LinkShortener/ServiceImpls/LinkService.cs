using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using LinkShortener.Entities;
using LinkShortener.Repositories;
using LinkShortener.Services;
using MongoDB.Bson;

namespace LinkShortener.ServiceImpls
{
    public class LinkService : ILinkService
    {
        private readonly IShortenerService _shortenerService;
        private readonly ILinkRepository _linkRepository;
        
        public LinkService(
            IShortenerService shortenerService,
            ILinkRepository linkRepository)
        {
            _shortenerService = shortenerService;
            _linkRepository = linkRepository;
        }
        
        public async Task<string> GetShortAlias(string fullLink, string userId)
        {
            var existedLink = await _linkRepository.GetByFullLink(fullLink, userId);

            return existedLink?.ShortAlias 
                ?? await GenerateShortAlias(fullLink, userId);
        }

        public async Task<string?> GetFullLinkAndIncreaseVisitCount(string shortAlias)
        {
            var link = await _linkRepository.IncreaseVisitedCountByAlias(shortAlias);

            return link?.FullLink;
        }

        public Task<IEnumerable<Link>> GetAllLinks(string userId)
        {
            return _linkRepository.GetAll(userId);
        }

        // 'GetHashCode() for String' does not guarantee unique code for different strings, but, nevertheless, the chance of collisions is not so high.
        // So, I use it and try to write a value to the DB, given the presence of an index in the database,
        // which will throw an error  if will getting collision and code will try again with a new id.
        private async Task<string> GenerateShortAlias(string fullLink, string userId)
        {
            var tryCount = 0;

            while (tryCount < 3)
            {
                try
                {
                    var id = ObjectId.GenerateNewId().ToString();
                    var shortAlias = _shortenerService.GenerateShortString(id.GetHashCode());

                    var newLink = new Link(id, userId, shortAlias, fullLink);

                    await _linkRepository.CreateNewLink(newLink);
                
                    return newLink.ShortAlias;
                }
                catch (MongoDB.Driver.MongoDuplicateKeyException)
                {
                    tryCount++;
                }
            }
            
            throw new InvalidOperationException("Three tries not enough for resolve collisions!");
        }
    }
}