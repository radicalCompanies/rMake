using rLibrary.Entities.Domain;
using rLibrary.Entities.Objects;
using rLibrary.Entities.Objects.DTOs.Publish;
using rLibrary.Entities.Objects.DTOs.rPublish;
using rLibrary.Interfaces;

namespace rLibrary.Services
{
    public class PublishService : IPublishService
    {
        private readonly IDatabaseService dbService;

        public PublishService(IDatabaseService dbService)
        {
            this.dbService = dbService;
        }

        public Task<rLibraryResponse<PublishedProfile>> Publish(PublishOnceDto publishOnce)
        {
            throw new NotImplementedException();
        }

        public Task<rLibraryResponse<PublishedProfile>> ClosePublishingSession()
        {
            throw new NotImplementedException();
        }

        public Task<rLibraryResponse<PublishingProfile>> DeleteDocument(DeleteDocumentDto deleteDocumentDto)
        {
            throw new NotImplementedException();
        }

        public Task<rLibraryResponse<PublishingProfile>> OpenPublishingSession(OpenPubSessionDto openSessionDto)
        {
            throw new NotImplementedException();
        }

        public Task<rLibraryResponse<PublishingProfile>> PostDocument(Document document)
        {
            throw new NotImplementedException();
        }

        public Task<rLibraryResponse<PublishingProfile>> PutProject(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
