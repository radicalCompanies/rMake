using rMakev2.Models;

namespace rMakev2.DTOs
{
    public class PublishProject
    {
        public string Id { get; }
        public DateTime PublicationDate { get; }
        public Dictionary<int, string> Documents { get; } = new Dictionary<int, string>();

        public List<Person> Authors { get; } = new List<Person>();
        public List<Person> Owners { get; } = new List<Person>(); // <- no me gusta tanto la idea...
        public string Sign { get; }

        private PublishProject(){}

        public PublishProject(Project project)
        {
            Id = Guid.NewGuid().ToString();
            PublicationDate = DateTime.Now.Date;
        }

        public PublishDocument AddDocument(Document doc)
        {
            PublishDocument publishDocument = new PublishDocument(this, doc);
            Documents.Add(publishDocument.Order, publishDocument.Id);
            return publishDocument;
        }
    }
}
