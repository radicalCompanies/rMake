using RestSharp;
using rMakev2.DTOs;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;
using rMakev2.Models;

namespace rMakev2.Services
{
    public class CommunicationService : ICommunicationService
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
            var objstr = System.Text.Json.JsonSerializer.Serialize(save, options);
            request.AddJsonBody(objstr);
            var sendReq = await client.ExecuteAsync(request);

        }
        public async Task<Models.App> LoadAsync(string token)
        {
            HttpClient hc = new HttpClient();
            //token = "db1f334e-431e-47bd-a826-887005652c86";
            
            string url = "https://localhost:7267/api/item/" + token;
            var response = await hc.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var resultContent = response.Content.ReadAsStringAsync().Result;
                var loadedSaveProject = JsonConvert.DeserializeObject<SaveProjectDto>(resultContent);

                Models.App app = new Models.App(loadedSaveProject.Id, loadedSaveProject.DataToken);
                Data dat = new Data(app);
                app.Data = dat;


                foreach (var proj in loadedSaveProject.Projects)
                {
                    Project p = new Project();
                    p.Name = proj.Name;
                    p.Id = proj.Id;
                    p.CreationDate = proj.CreationDate;
                    p.Data = app.Data;
                    p.DataId = app.Data.Id;

                    app.Data.Projects.Add(p);


                    foreach (var doc in proj.Documents)
                    {
                        var Pro = app.Data.Projects.Where(x => x.Id == proj.Id).FirstOrDefault();

                        Document d = new Document();
                        d.Name = doc.Name;
                        d.Id = doc.Id;
                        d.CreationDate = doc.CreationDate;
                        d.Order= doc.Order;
                        d.Project = Pro;
                        d.ProjectId = Pro.Id;

                        Pro.Documents.Add(d);

                        foreach (var ele in doc.Elements)
                        {
                            var Proj = app.Data.Projects.Where(x => x.Id == proj.Id).FirstOrDefault();
                            var docum = Proj.Documents.Where(x => x.Id == doc.Id).FirstOrDefault();

                            Element e = new Element();
                            e.Id= ele.Id;
                            e.Content= ele.Content;
                            e.Order= ele.Order;
                            e.Ideary = ele.Ideary;
                            e.DocumentId= ele.DocumentId;
                            e.Document = docum;

                            docum.Elements.Add(e);

                        }

                    }


                }

                app.Ui.SelectedProject = app.Data.Projects.Where(x => x.Id == loadedSaveProject.Ui.IdSelectedProject).FirstOrDefault();
                app.Ui.SelectedDocument = app.Ui.SelectedProject.Documents.Where(x => x.Id == loadedSaveProject.Ui.IdSelectedDocument).FirstOrDefault();


                return app;
            }




            return null;
            
            //Logica
            //recibe el json
            //carga entiedades directas
            //crea selected projecy
            //crea selected document
        }


    }
}
