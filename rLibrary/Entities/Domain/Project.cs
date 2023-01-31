namespace rLibrary.Entities.Domain
{
    public class Project
    {
        public string Id { get; }
        public DateTime PublicationDate { get; }
        public Dictionary<int, string> Documents { get; } = new Dictionary<int, string>();

        public List<Person> Authors { get; } = new List<Person>();
        public List<Person> Owners { get; } = new List<Person>(); // <- no me gusta tanto la idea...
        public string Sign { get; }
    }
}
