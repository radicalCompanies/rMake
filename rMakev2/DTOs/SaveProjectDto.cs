
using Newtonsoft.Json;
using rMakev2.Models;

namespace rMakev2.DTOs
{

   
    public class SaveProjectDto
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string DataToken;
        public List<ProjectDTO> Projects { get; set; }

    }
    public class ProjectDTO
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public List<DocumentDTO> Documents { get; set; }
    }

    public class DocumentDTO
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public int Order { get; set; }
        public List<ElementDTO> Elements { get; set; }
        public string ProjectId { get; set; }

    }

    public class ElementDTO
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }
        public string Ideary { get; set; }
        public string DocumentId { get; set; }

    }
}
