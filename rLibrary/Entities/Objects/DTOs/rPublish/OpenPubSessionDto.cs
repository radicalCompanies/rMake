using rLibrary.Entities.Domain;

namespace rLibrary.Entities.Objects.DTOs.rPublish
{
    public class OpenPubSessionDto
    {
        public string ProjectId { get; set; }
        public List<Person> Authors { get; } = new List<Person>();
        public List<Person> Owners { get; } = new List<Person>(); // <- no me gusta tanto la idea...
    }
}
