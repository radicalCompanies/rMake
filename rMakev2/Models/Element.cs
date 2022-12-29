using Microsoft.AspNetCore.Mvc.RazorPages;

namespace rMakev2.Models
{
    public class Element
    {
        public Element(Document document)
        {
            Document = document ?? throw new Exceptions("Document is null");
            Id = Guid.NewGuid().ToString();
            Content = "Si hay";
            Order = Document.Elements.Count() + 1;
            document.Elements.Add(this);
            

         }
        public string Id { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }
        public string DocumentId { get; set; }
        public Document Document { get; set; }       

    }
}
