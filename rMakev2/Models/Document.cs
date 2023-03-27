using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
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
        public string Content { get; set; }
        public string ProjectId { get; set; }
        public string ParentDocumentId { get; set; }
        public bool IsOrdered { get; set; } = false;
        
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

        public List<string> separateElements()
        {
            var list = new List<string>();
            var pattern = "(<p>|</p>)";
            var isInTag = false;
            var inTagValue = String.Empty;

            foreach (var subStr in Regex.Split(Content, pattern))
            {
                if (subStr.Equals("<p>"))
                {
                    isInTag = true;
                    continue;
                }
                else if (subStr.Equals("</p>"))
                {
                    isInTag = false;
                    list.Add(String.Format("<p>{0}</p>", inTagValue));
                    continue;
                }

                if (isInTag)
                {
                    inTagValue = subStr;
                    continue;
                }

                if (!String.IsNullOrEmpty(subStr))
                    list.Add(subStr);

            }
            return list;
        }

        public void stringToElements()
        {
            List<string> elementList = separateElements();

            Elements = new List<Element>();

            foreach (var elementContent in elementList)
            {
                if(elementContent != "<p><br></p>")
                {
                    Element element = new Element(this);

                    element.Content = elementContent;

                }
            }
        }

        public void elementsToString()
        {
            string ContentString = "";

            foreach (var element in Elements.OrderBy(w => w.Order))
            {
                ContentString += element.Content;
            }

            Content = ContentString;

        }
    }
}
