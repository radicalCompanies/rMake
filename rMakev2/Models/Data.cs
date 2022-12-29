using System.Text.Json.Serialization;
namespace rMakev2.Models
{
    public class Data
    {
        public Data(App app) {

            Id = Guid.NewGuid().ToString();
            App = app ?? throw new Exceptions("app is null");            
            Projects = new List<Project>();
            App = app;
            AppId = app.Id;
            app.Data = this;   
        }        
        public string Id { get; set; }
        public List<Project> Projects { get; set; }
        [JsonIgnore]
        public App App { get; set; }
        public string AppId { get; set; }

        public Project AddProject()
        {
            Project newProject = new Project(this);
            Projects.Add(newProject);
            return newProject;
        }
        public void RemoveProject(Project project)
        {
           Projects.Remove(project);            
        }
    }
}