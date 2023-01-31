namespace rLibrary.Entities.Domain
{
    public class Document
    {
        public string Id { get; }
        public string ProjectId { get; }
        public string Name { get; }
        public int Order { get; }
        public string Content { get; }
        public string ContentType { get; }
    }
}
