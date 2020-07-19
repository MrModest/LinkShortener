namespace LinkShortener.Data
{
    public interface ILinksDatabaseSettings
    {
        string LinksCollectionName { get; }
        string ConnectionString { get; }
        string DatabaseName { get; }
    }
}