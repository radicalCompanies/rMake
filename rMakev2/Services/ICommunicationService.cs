namespace rMakev2.Services
{
    public interface ICommunicationService
    {
        public Task SaveAsync(Models.App app);
        public Task<DTOs.SaveProjectDto> LoadAsync(string token);
        //Task  SaveContentAsync();   
    }
}
