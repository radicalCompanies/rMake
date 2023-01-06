using RestSharp;
using rMakev2.DTOs;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace rMakev2.Services
{
    public class CommunicationService:ICommunicationService
    {
        public async Task SaveAsync(Models.App App)
        {
            var save = new SaveProjectDto();
            save.Id = App.Id;
            save.DataToken = App.DataToken;
            save.Projects = new List<ProjectDTO>();
            save.Ui = new UIDto();

            save.Ui.IdSelectedDocument = App.Ui.SelectedDocument.Id;
            save.Ui.IdSelectedProject = App.Ui.SelectedProject.Id;

            foreach (var item in App.Data.Projects)
            {
                ProjectDTO project = new ProjectDTO();
                project.Id = item.Id;
                project.Name = item.Name;
                project.CreationDate = item.CreationDate;
                project.Documents = new List<DocumentDTO>();
                save.Projects.Add(project);

                foreach (var itemDoc in item.Documents)
                {
                    DocumentDTO document = new DocumentDTO();
                    document.Id = itemDoc.Id;
                    document.Name = itemDoc.Name;
                    document.CreationDate = itemDoc.CreationDate;
                    document.Order = itemDoc.Order;
                    document.ProjectId = itemDoc.ProjectId;
                    document.Name = itemDoc.Name;
                    document.Elements = new List<ElementDTO>();
                    save.Projects.Where(x => x.Id == itemDoc.ProjectId).First().Documents.Add(document);

                    foreach (var itemElement in itemDoc.Elements)
                    {
                        ElementDTO element = new ElementDTO();
                        element.Id = itemElement.Id;
                        element.Content = itemElement.Content;
                        element.Order = itemElement.Order;
                        element.Ideary = itemElement.Ideary;
                        element.DocumentId = itemElement.DocumentId;
                        var pro = save.Projects.Where(x => x.Id == itemDoc.ProjectId).First();
                        pro.Documents.Where(x => x.Id == itemDoc.Id).First().Elements.Add(element);
                    }

                }


            }

            var client = new RestClient("https://localhost:7267/");
            var request = new RestRequest("api/item", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var options = new JsonSerializerOptions()
            {
                MaxDepth = 0,
                IgnoreReadOnlyProperties = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,

            };
            var objstr = JsonSerializer.Serialize(save, options);
            request.AddJsonBody(objstr);
            var sendReq = await client.ExecuteAsync(request);

        }
        public void Load()
        {
            //Logica
            //recibe el json
            //carga entiedades directas
            //crea selected projecy
            //crea selected document
        }

        
    }
}
