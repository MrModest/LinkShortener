namespace LinkShortener.Services
{
    /// <summary>
    /// Service for generating short alias for short url
    /// </summary>
    public interface IShortenerService
    {
        /// <summary>
        /// Generate short string by integer seed.
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        string GenerateShortString(int seed);
    }
}