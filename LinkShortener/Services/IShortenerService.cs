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

        /// <summary>
        /// Restore seed from string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        int RestoreSeedFromString(string str);
    }
}