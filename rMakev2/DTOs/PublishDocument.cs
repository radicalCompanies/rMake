using rMakev2.Models;

namespace rMakev2.DTOs
{
    public class PublishDocument
    {
        public string Id { get; set; } // Tal vez será bueno diferenciar Ids publicados de Ids de creación
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public DateTime PublicationDate { get; set; }
        public int Order { get; set; } // Esto puede cambiar! tomar en cuenta la fecha de publicación
        public string Content { get; set; }

        public List<Person> Authors { get; set; } = new List<Person>();
        public List<Person> Owners { get; set; } = new List<Person>(); // <- no me gusta tanto la idea...

        
        public PublishDocument(){}
        
        public PublishDocument(Document document)
        {
            Name = document.Name;
            PublicationDate = DateTime.Now.Date;
            ProjectId = document.ProjectId;
            Order = document.Order;

            //Need to add support for authors and owners
            Authors.Add(new Person { Id = Guid.Empty.ToString(), Name = "TBD" });
            Owners.Add(new Person { Id = Guid.Empty.ToString(), Name = "TBD" });

            //The character that separates the elements should be dynamic in order to suport different formats (html, txt, etc...)
            foreach (Element element in document.Elements.OrderBy(x => x.Order)) {
                Content += element.Content + "\n";
            }
            Content = Content.Remove(Content.Length - 1);
        }
    }
}
