namespace rLibrary.Entities.Domain
{
    public class PublishedProfile
    {
        public int Version { get; set; }
        public int DocumentCount { get; set; }
        public Project Project { get; set; }
    }
}
