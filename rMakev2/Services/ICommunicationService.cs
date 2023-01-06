namespace rMakev2.Services
{
    public interface ICommunicationService
    {
        public Task SaveAsync(Models.App app);
        //Task  SaveContentAsync();   
    }
}
