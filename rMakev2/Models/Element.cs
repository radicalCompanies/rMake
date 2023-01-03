using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Eventing.Reader;

namespace rMakev2.Models
{
    public class Element
    {
        public Element(Document document)
        {
            Document = document ?? throw new Exceptions("Document is null");
            Id = Guid.NewGuid().ToString();
            Content = "";
            Order = Document.Elements.Count() + 1;
            DocumentId = document.Id;
            Document = document;
            Document.Elements.Add(this);
        }
        public string Id { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }
        public string Ideary { get; set; }
        public string DocumentId { get; set; }
        public Document Document { get; set; }       

        public void AddIdea(string Idea)
        {
            Ideary = Idea;
        }

    }
}
