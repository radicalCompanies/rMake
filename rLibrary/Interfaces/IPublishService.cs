using rLibrary.Entities.Domain;
using rLibrary.Entities.Objects;
using rLibrary.Entities.Objects.DTOs.Publish;
using rLibrary.Entities.Objects.DTOs.rPublish;

namespace rLibrary.Interfaces
{
    public interface IPublishService
    {
        Task<rLibraryResponse<PublishedProfile>> Publish(PublishOnceDto publishOnce);
        Task<rLibraryResponse<PublishingProfile>> OpenPublishingSession(OpenPubSessionDto openSessionDto);
        Task<rLibraryResponse<PublishingProfile>> PostDocument(Document document);
        Task<rLibraryResponse<PublishingProfile>> PutProject(Project project);
        Task<rLibraryResponse<PublishingProfile>> DeleteDocument(DeleteDocumentDto deleteDocumentDto);
        Task<rLibraryResponse<PublishedProfile>> ClosePublishingSession();
    }
}
