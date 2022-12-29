using System.Reflection.Metadata;
using System.Xml.Linq;

namespace rMakev2.Models
{
    public class Document
    {
        public Document(Project project)
        {
            Project = project ?? throw new Exceptions("Project is null");
            Id = Guid.NewGuid().ToString();
            Name = "New Document";
            CreationDate = DateTime.Now;
            Order = Project.Documents.Count() + 1;
            Elements = new List<Element>();
            Project = project;
            ProjectId = project.Id;
            AddElement(this);

        }
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public int Order { get; set; }
        public List<Element> Elements { get; set; }
        public Project Project { get; set; }
        public string ProjectId { get; set; }
        
        public Element AddElement(Document document)
        {
            Element newElement = new Element(document);            
            return newElement;
        }
        public Element RemoveElement(Element element)
        {
            Elements.Remove(element);
            return element;
        }
    }
}
