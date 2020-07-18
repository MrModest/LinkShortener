namespace LinkShortener.Entities
{
    public class Link
    {
        public Link(string shortAlias, string fullLink, int visitedCount = 0)
        {
            ShortAlias = shortAlias;
            FullLink = fullLink;
            VisitedCount = visitedCount;
        }

        public string ShortAlias { get; }

        public string FullLink { get; }

        public int VisitedCount { get; set; }
    }
}