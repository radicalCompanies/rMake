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
using rMakev2.Services;

namespace rMakev2.ViewModel
{
    public class RmakeViewModel : INotifyPropertyChanged
    {
        private ToastService _toastService;
        private ICommunicationService _communicationService;


        public RmakeViewModel(IToastService toast, ICommunicationService communicationService)
        {
            this._toastService = toast as Blazored.Toast.Services.ToastService;
            this._communicationService = communicationService;


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

            Thread p1;
            p1 = new Thread(new ThreadStart(Save));
            p1.Start();


            //Creo las entidades por defecto.
        }


        public void Save()
        {
            while (true)
            {

                Thread.Sleep(1000000000);
                HashMyContent();
                _communicationService.SaveAsync(App).Wait();
                this._toastService.ShowSuccess("Project Auto Saved");
            }
        }


        public void HideSaveModal()
        {
            App.Ui.SaveModal.Hide();
        }
        public void ShowSaveModal()
        {
            App.Ui.SaveModal.Show();
        }

        public void DocumentMenu()
        {
            App.Ui.DocumentMenu();
        }

        public void EditItem(Element element){
            App.Ui.EditItem(element);
        }


        public void HidePublishModal()
        {
            App.Ui.PublishModal.Hide();
        }
        public void ShowPublishModal()
        {
            App.Ui.PublishModal.Show();
        }
        public void EventSelectProject(ChangeEventArgs e)
        {
            string projectId = e.Value.ToString();
            Project project = Ui.SelectedProject = App.Data.Projects.Where(w => w.Id == projectId).SingleOrDefault();
            Ui.SelectProject(project);
            this._toastService.ShowInfo("You have changed the project to " + project.Name);
        }
        public void EventSelectDocumentMenu(Document document1)
        {
            Ui.SelectDocument(document1);
            Ui.SelectProject(document1.Project);
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
            //this._toastService.ShowSuccess("New project created");

        }
        public void DeleteProject()
        {

            if (App.Data.Projects.Count() >= 1)
            {
                App.Data.RemoveProject(Ui.SelectedProject);
                SelectProject(App.Data.Projects.First());
                //this._toastService.ShowSuccess("Project eliminated");
            }
            else
            {
                App.Data.RemoveProject(Ui.SelectedProject);
                this._toastService.ShowSuccess("Project eliminated");
                NewProject();
            }
        }
        public void DeleteProjectMenu(Project project)
        {
            if (App.Data.Projects.Count() > 1) {
                App.Data.RemoveProject(project);
                SelectProject(App.Data.Projects.First());
                SelectDocument(Ui.SelectedProject.Documents.First());

            }

            else if (App.Data.Projects.Count() == 1)
            {
                App.Data.RemoveProject(project);
                NewProject();
                SelectDocument(Ui.SelectedProject.Documents.First());
            }
        }

        public void ForkProject()
        {
            SelectProject(App.Data.ForkProject(Ui.SelectedProject));

            this._toastService.ShowSuccess("Project Forked");
        }
        public void CloneDocument()
        {
            this._toastService.ShowSuccess("Document cloned");
            SelectDocument(Ui.SelectedProject.CloneDocument(Ui.SelectedDocument));
        }
        public void NewDocument()
        {
            SelectDocument(Ui.SelectedProject.AddDocument(Ui.SelectedProject));
            //this._toastService.ShowSuccess("New document created");
        }

        public void NewDocumentMenu(Project project)
        {
            SelectDocument(project.AddDocument(project));
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

        public void DeleteDocumentMenu(Document document)
        {
            Project project = document.Project;

            
            if (project.Documents.Count() > 1)
            {
                
                project.RemoveDocument(document);

                if(document == Ui.SelectedDocument)
                {
                    SelectDocument(project.Documents.FirstOrDefault());
                }
                
            }
            else if (project.Documents.Count() == 1)
            {
                project.RemoveDocument(document);
                NewDocumentMenu(project);
            }
            
        }

        public void NewElement()
        {
            Ui.SelectedDocument.AddElement(Ui.SelectedDocument);  
        }
        public void NewElement(int currentOrder)
        {
            Ui.SelectedDocument.AddElement(Ui.SelectedDocument, currentOrder);
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
            HashMyContent();
            await _communicationService.SaveAsync(App);
            this._toastService.ShowSuccess("Project Saved");
        }
        public void SwitchProjectName()
        {
            Ui.SwitchEditName();


        }

        public void DisplayMenu()
        {
            Ui.ShowMenu();
        }
        public void Enter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                SwitchProjectName();
            }

        }
        public void MergeDocumentsIntoNewOne(Document First, Document Second)
        {

        }
        public void HashMyContent()
        {

            foreach (var project in App.Data.Projects)
            {
                foreach (var document in project.Documents)
                {
                    foreach (var element in document.Elements)
                    {
                        element.Hash = HashString(element.Content, element.Id);
                    }
                }
            }
        }
        public void BlockRTAFocus()
        {


            Ui.BlockRTAFocus = true;


        }
        public void UnBlockRTAFocus()
        {

            Ui.BlockRTAFocus = false;

        }
        public string HashString(string text, string salt)
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }
            if (salt == String.Empty)
            {
                salt = "rebelsalt";
            }
            // Uses SHA256 to create the hash
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                // Convert the string to a byte array first, to be processed
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text + salt);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                // Convert back to a string, removing the '-' that BitConverter adds
                string hash = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", String.Empty);

                return hash;
            }
        }

        public async Task LoadProyectAsync(string token)
        {

            SaveProjectDto savedContent = await _communicationService.LoadAsync(token);
            App.Data.Id = savedContent.Id;
            foreach (var proj in savedContent.Projects)
            {
                Project p = new Project();
                p.Name = proj.Name;
                p.Id = proj.Id;
                p.CreationDate = proj.CreationDate;
                p.Data = app.Data;
                p.DataId = app.Data.Id;
                p.ParentProjectId = proj.ParentProjectId;
                this.App.Data.Projects.Add(p);


                foreach (var doc in proj.Documents)
                {
                    var Pro = app.Data.Projects.Where(x => x.Id == proj.Id).FirstOrDefault();
                    Document d = new Document();
                    d.Name = doc.Name;
                    d.Id = doc.Id;
                    d.CreationDate = doc.CreationDate;
                    d.Order = doc.Order;
                    d.Project = Pro;
                    d.ProjectId = Pro.Id;
                    d.ParentDocumentId = doc.ParentDocumentId;
                    Pro.Documents.Add(d);
                    foreach (var ele in doc.Elements)
                    {
                        var Proj = app.Data.Projects.Where(x => x.Id == proj.Id).FirstOrDefault();
                        var docum = Proj.Documents.Where(x => x.Id == doc.Id).FirstOrDefault();
                        Element e = new Element();
                        e.Id = ele.Id;
                        e.Content = ele.Content;
                        e.Order = ele.Order;
                        e.Ideary = ele.Ideary;
                        e.DocumentId = ele.DocumentId;
                        e.Document = docum;
                        e.ParentElementId = ele.ParentElementId;
                        e.Hash = ele.Hash;
                        docum.Elements.Add(e);

                    }

                }


            }
            app.Ui.SaveModal = app.Ui.SaveModal;

            
            App.Data.RemoveProject(app.Ui.SelectedProject);
            app.Ui.SelectedProject = app.Data.Projects.Where(x => x.Id == savedContent.Ui.IdSelectedProject).FirstOrDefault();
            app.Ui.SelectedDocument = app.Ui.SelectedProject.Documents.Where(x => x.Id == savedContent.Ui.IdSelectedDocument).FirstOrDefault();



        }

        public void ShowAreaComment()
        {
            App.Ui.DisplayComents= App.Ui.DisplayComents == true? false : true;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }


    }
}
