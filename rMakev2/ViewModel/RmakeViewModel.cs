﻿using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using rMakev2.Models;
using System.ComponentModel;
using System.Reflection.Metadata;
using Document = rMakev2.Models.Document;


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
            Project project= Ui.SelectedProject = App.Data.Projects.Where(w => w.Id == projectId).SingleOrDefault();
            Ui.SelectProject(project);
            this._toastService.ShowInfo("You have changed the project to " + project.Name);
        }
        public void EventSelectDocument(ChangeEventArgs e)
        {
            string documentId = e.Value.ToString();
            Document document= Ui.SelectedProject.Documents.Where(w => w.Id == documentId).SingleOrDefault();
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
        public void DeleteElement(string id)
        {
            Element ElementToDelete = Ui.SelectedDocument.Elements.Where(w => w.Id == id).SingleOrDefault();
            if (Ui.SelectedDocument.Elements.Count() >= 1)
            {
                
                Ui.SelectedDocument.RemoveElement(ElementToDelete);                
            }
            else
            {
                Ui.SelectedDocument.RemoveElement(ElementToDelete);
                NewElement();
            }
        }           
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged()
        {            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
