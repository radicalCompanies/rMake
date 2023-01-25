namespace rLibrary.Entities.Domain
{
    public class PublishingProfile
    {
        public Project Project { get; set; }
        public int DocumentCount { get; set; }
        public string PublishingToken { get; set; }
    }
}
