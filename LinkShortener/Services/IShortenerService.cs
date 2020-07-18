namespace LinkShortener.Services
{
    /// <summary>
    /// Service for generating short alias for short url
    /// </summary>
    public interface IShortenerService
    {
        /// <summary>
        /// Generate or get exists short alias for given full link
        /// </summary>
        /// <param name="fullLink"></param>
        /// <returns></returns>
        string GetShortAlias(string fullLink);
    }
}