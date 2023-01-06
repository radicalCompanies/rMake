using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using rMakev2.DTOs;
using Microsoft.AspNetCore.Components.Web;
using rMakev2.Models;
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Xml.Linq;
using Document = rMakev2.Models.Document;
using RestSharp;
using Microsoft.JSInterop;

namespace rMakev2.ViewModel
{
    public class RmakeViewModel : INotifyPropertyChanged
    {
        private ToastService _toastService;
      

        
        public RmakeViewModel(IToastService toast)
        {
            this._toastService = toast as Blazored.Toast.Services.ToastService;
          


            InitializeData();
        }

        public RmakeViewModel()
        {

            InitializeData();
        }
        private Models.App app;
        public Models.App App
        {
            get { return app; }
            set
            {
                app = value;
                OnPropertyChanged();
            }
        }
        private Ui ui;
        public Ui Ui
        {
            get { return ui; }
            set
            {
                ui = value;
                OnPropertyChanged();
            }
        }



        public void InitializeData()
        {
            App = new Models.App("Rebel", "codename-rebel-creator");
            Ui = new Models.Ui(App);
            App.Data.Projects.Add(new Project(App.Data));
            var ProjectZero = App.Data.Projects.First();
            Ui.SelectedProject = ProjectZero;
            Ui.SelectedDocument = ProjectZero.Documents.First();
            //Creo las entidades por defecto.
        }
        public void EventSelectProject(ChangeEventArgs e)
        {
            string projectId = e.Value.ToString();
            Project project = Ui.SelectedProject = App.Data.Projects.Where(w => w.Id == projectId).SingleOrDefault();
            Ui.SelectProject(project);
            this._toastService.ShowInfo("You have changed the project to " + project.Name);
        }
        public void EventSelectDocument(ChangeEventArgs e)
        {
            string documentId = e.Value.ToString();
            Document document = Ui.SelectedProject.Documents.Where(w => w.Id == documentId).SingleOrDefault();
            Ui.SelectDocument(document);
        }
        public void SelectProject(Project project)
        {
            Ui.SelectProject(project);
        }
        public void SelectDocument(Document document)
        {
            Ui.SelectDocument(document);
        }
        public void NewProject()
        {
            SelectProject(App.Data.AddProject());
            this._toastService.ShowSuccess("New project created");

        }
        public void DeleteProject()
        {

            if (App.Data.Projects.Count() >= 1)
            {
                App.Data.RemoveProject(Ui.SelectedProject);
                SelectProject(App.Data.Projects.First());
                this._toastService.ShowSuccess("Project eliminated");
            }
            else
            {
                App.Data.RemoveProject(Ui.SelectedProject);
                this._toastService.ShowSuccess("Project eliminated");
                NewProject();
            }
        }
        public void NewDocument()
        {
            SelectDocument(Ui.SelectedProject.AddDocument(Ui.SelectedProject));
            this._toastService.ShowSuccess("New document created");
        }
        public void DeleteDocument()
        {
            if (Ui.SelectedProject.Documents.Count() > 1)
            {
                if (Ui.SelectedProject.Documents.Count() >= 1)
                {
                    Ui.SelectedProject.RemoveDocument(Ui.SelectedDocument);
                    SelectDocument(Ui.SelectedProject.Documents.First());
                }
                else
                {
                    Ui.SelectedProject.RemoveDocument(Ui.SelectedDocument);
                    NewDocument();
                }
            }
        }
        public void NewElement()
        {
            Ui.SelectedDocument.AddElement(Ui.SelectedDocument);
        }
        public void DeleteElement(Element element)
        {

            if (Ui.SelectedDocument.Elements.Count() >= 1)
            {

                Ui.SelectedDocument.RemoveElement(element);
            }
            else
            {
                Ui.SelectedDocument.RemoveElement(element);
                NewElement();
            }
        }
        public void MoveUp(Element element)
        {
            var current = Ui.SelectedDocument.Elements.Where(w => w.Order == element.Order).SingleOrDefault();
            var Previous = Ui.SelectedDocument.Elements.Where(w => w.Order == element.Order - 1).SingleOrDefault();

            if (Previous != null)
            {

                Ui.SelectedDocument.Elements.SingleOrDefault(w => w.Id == current.Id).Order--;
                Ui.SelectedDocument.Elements.SingleOrDefault(w => w.Id == Previous.Id).Order++;


            }

        }
        public void MoveDown(Element element)
        {
            var current = Ui.SelectedDocument.Elements.Where(w => w.Order == element.Order).SingleOrDefault();
            var next = Ui.SelectedDocument.Elements.Where(w => w.Order == element.Order + 1).SingleOrDefault();

            if (next != null)
            {

                Ui.SelectedDocument.Elements.SingleOrDefault(w => w.Id == current.Id).Order++;
                Ui.SelectedDocument.Elements.SingleOrDefault(w => w.Id == next.Id).Order--;


            }
        }


        public async Task SaveContentAsync()
        {
            var save = new SaveProjectDto();
            save.Id = App.Id;
            save.DataToken = App.DataToken;
            save.Projects = new List<ProjectDTO>();

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
                    save.Projects.Where(x=>x.Id == itemDoc.ProjectId).First().Documents.Add(document);

                    foreach (var itemElement in itemDoc.Elements)
                    {
                        ElementDTO element = new ElementDTO();
                        element.Id = itemElement.Id;
                        element.Content = itemElement.Content;
                        element.Order = itemElement.Order;
                        element.Ideary = itemElement.Ideary;
                        element.DocumentId= itemElement.DocumentId;
                        var pro = save.Projects.Where(x => x.Id == itemDoc.ProjectId).First();
                        pro.Documents.Where(x=>x.Id == itemDoc.Id).First().Elements.Add(element);
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

            
        public void SwitchProjectName()
        {
            Ui.SwitchEditName();
           
        }
        public void Enter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                SwitchProjectName();
            }
            
        }
  

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
