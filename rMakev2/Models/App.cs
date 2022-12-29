namespace rMakev2.Models
{
    public class App
    {
        public App(string appId, string dataToken)
        {
            appId = appId ?? throw new Exceptions("App Key is required"); 
            Id = appId;
            Data = new Data(this);
            Ui = new Ui(this);            
            if (dataToken == null)
                dataToken = Guid.NewGuid().ToString();
            else
                dataToken = dataToken;
        }
        public string Id { get; set; }
        public string dataToken;
        public Data Data { get; set; }        
        public Ui Ui { get; set; }   
    }     
}
