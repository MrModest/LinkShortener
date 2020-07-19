namespace LinkShortener.Data
{
    public class LinksDatabaseSettings : ILinksDatabaseSettings
    {
        public string LinksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}