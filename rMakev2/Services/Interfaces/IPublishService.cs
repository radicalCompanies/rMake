using rMakev2.DTOs;
using rMakev2.Models;

namespace rMakev2.Services.Interfaces
{
    public interface IPublishService
    {
        Task<PublishResult> PublishProject(Project project);
    }
}
