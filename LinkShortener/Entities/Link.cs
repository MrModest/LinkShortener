namespace LinkShortener.Entities
{
    public class Link
    {
        public Link(string id, string shortAlias, string fullLink, int visitedCount = 0)
        {
            Id = id;
            ShortAlias = shortAlias;
            FullLink = fullLink;
            VisitedCount = visitedCount;
        }

        public string Id { get; }

        public string ShortAlias { get; }

        public string FullLink { get; }

        public int VisitedCount { get; set; }
    }
}