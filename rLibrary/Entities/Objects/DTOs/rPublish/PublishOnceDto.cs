using rLibrary.Entities.Domain;

namespace rLibrary.Entities.Objects.DTOs.Publish
{
    public class PublishOnceDto
    {
        public Project Project { get; set; }
        public List<Document> ProjectDocuments { get; set; }
    }
}
