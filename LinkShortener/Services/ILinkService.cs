using System.Collections.Generic;
using System.Threading.Tasks;

using LinkShortener.Entities;

namespace LinkShortener.Services
{
    /// <summary>
    /// Service for interaction with link
    /// </summary>
    public interface ILinkService
    {
        /// <summary>
        /// Get short alias for shirt url
        /// </summary>
        /// <param name="fullLink"></param>
        /// <returns></returns>
        Task<string> GetShortAlias(string fullLink);

        /// <summary>
        /// Get full link
        /// </summary>
        /// <param name="shortAlias"></param>
        /// <returns></returns>
        Task<string> GetFullLink(string shortAlias);

        /// <summary>
        /// Get all existed links
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Link>> GetAllLinks();
    }
}