using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace rMakev2.Models
{
    public class Document
    {
        public Document()
        {
            Elements = new List<Element>();
        }
        public Document(Project project)
        {
            Project = project ?? throw new Exceptions("Project is null");
            Id = Guid.NewGuid().ToString();
            Name = "";
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
        [JsonIgnore]
        public Project Project { get; set; }
        public string ProjectId { get; set; }
        public string ParentDocumentId { get; set; }
        
        public Element AddElement(Document document)
        {
            Element newElement = new Element(document);            
            return newElement;
        }
        public Element AddElement(Document document, int currentElement)
        {
            Element newElement = new Element(document, currentElement);
            return newElement;
        }
        public Element RemoveElement(Element element)
        {
            Elements.Remove(element);
            return element;
        }

        public void ChangeOrder(Element element, int order)
        {
            Element current = Elements.Where(w => w.Id == element.Id).SingleOrDefault();
            current.Order = order;
            current.Content = order.ToString();
        }

        public void ResetEdit()
        {
            foreach(var element in Elements)
            {
                element.EditItem = false;
            }
        }
      
    }
}
