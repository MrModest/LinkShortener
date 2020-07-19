using System.Collections.Generic;
using System.Threading.Tasks;

using LinkShortener.Entities;

namespace LinkShortener.Repositories
{
    /// <summary>
    /// Link Repository
    /// </summary>
    public interface ILinkRepository
    {
        /// <summary>
        /// Get all links
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Link>> GetAll();
        
        /// <summary>
        /// Get link by alias
        /// </summary>
        /// <param name="alias"></param>
        /// <returns>Return 'null' if not found.</returns>
        Task<Link?> GetByAlias(string alias);
        
        /// <summary>
        /// Get link by full link
        /// </summary>
        /// <param name="fullLink"></param>
        /// <returns>Return 'null' if not found.</returns>
        Task<Link?> GetByFullLink(string fullLink);

        /// <summary>
        /// Create new link
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        Task<Link> CreateNewLink(Link link);

        /// <summary>
        /// Increment visited count and return updated link
        /// </summary>
        /// <param name="shortAlias"></param>
        /// <returns></returns>
        Task<Link> IncreaseVisitedCountByAlias(string shortAlias);
    }
}