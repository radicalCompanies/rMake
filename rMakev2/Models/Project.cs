    namespace rMakev2.Models
{
    public class Project
    {
        public Project(Data data)
        {
            Data = data ?? throw new Exceptions("Data is null");
            Id = Guid.NewGuid().ToString();
            Name = "Project ("+data.Projects.Count() +")";
            CreationDate = DateTime.Now;
            Documents = new List<Document>();
            Data = data;
            DataId = data.Id;
            AddDocument(this);


        }
        
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public List<Document> Documents { get; set; }
        public Data Data { get; set; }
        public string DataId { get; set; }
        public string ParentProjectId { get; set; }

                
        public Document AddDocument(Project project)
        {
            {
                Document newDocument = new Document(project);
                Documents.Add(newDocument);
                return newDocument;
            }
        }

        public Document RemoveDocument(Document document)
        {
            Documents.Remove(document);
            return document;
        }

        internal Document CloneDocument(Document document)
        {
            Document newDocument = new Document(document.Project);
            newDocument.Name = document.Name + "(Cloned)";
            newDocument.ParentDocumentId = document.Id;
            Documents.Add(newDocument);
            

            foreach (var item in document.Elements)
            {
                newDocument.Elements.Add(item);
            }
            //Quita el Primer Element sin texto
            newDocument.Elements.RemoveAt(0);
            return newDocument;
        }
    }
}
