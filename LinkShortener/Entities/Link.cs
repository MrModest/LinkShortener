namespace LinkShortener.Entities
{
    public class Link
    {
        public Link(string id, string userId, string shortAlias, string fullLink, int visitedCount = 0)
        {
            Id = id;
            UserId = userId;
            ShortAlias = shortAlias;
            FullLink = fullLink;
            VisitedCount = visitedCount;
        }

        public string Id { get; }

        public string UserId { get; }

        public string ShortAlias { get; }

        public string FullLink { get; }

        public int VisitedCount { get; }
    }
}