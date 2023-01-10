namespace rMakev2.Services
{
    public interface ICommunicationService
    {
        public Task SaveAsync(Models.App app);
        public Task<Models.App> LoadAsync(string token);
        //Task  SaveContentAsync();   
    }
}
