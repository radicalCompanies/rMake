using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Text.Json;
using Blazorise;

namespace rMakev2.Models
{
    public class Ui : INotifyPropertyChanged
    {
        public Ui(Models.App app)
        {
            Id = Guid.NewGuid().ToString();
            App = app ?? throw new Exceptions("app is null");
            App = app;
            AppId = app.Id;
            app.Ui = this;
        }
        public string Id { get; set; }
        private Project selectedProject;
        public Project SelectedProject
        {
            get { return selectedProject; }
            set
            {
                selectedProject = value;
                OnPropertyChanged();
            }
        }
        private Document selectedDocument;
        public Document SelectedDocument
        {
            get { return selectedDocument; }
            set
            {
                selectedDocument = value;
                OnPropertyChanged();
            }
        }
        public string Json { get; set; } 
        public string AppId { get; set; }

        public bool EditProjectName { get; set; }
        public bool EditedProjectName { get; set; } = false;
        public bool BlockRTAFocus { get; set; } = false;





        public Modal SaveModal { get; set; }
        public Modal PublishModal { get; set; }
        public Modal MergeModal { get; set; }
        public App App { get; set; } 



        public void SelectProject(Project project)
        {
            SelectedProject = project;
            SelectedDocument = project.Documents.First();
        }
        public void SelectDocument(Document document)
        {
            SelectedDocument = document;
        }        
        public void SwitchEditName()
        {

            if (EditProjectName == true)
            {
                EditProjectName = false;
                BlockRTAFocus = false;
            }
            else
            {
                EditProjectName = true;
                BlockRTAFocus = true;
            }
        }
        public string JsonFn()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            Json = JsonSerializer.Serialize(App.Data, options);
            return Json;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged()
        {
            JsonFn();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
    
}