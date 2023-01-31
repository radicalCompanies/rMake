using rLibrary.Entities.Enums;

namespace rLibrary.Entities.Objects
{
    public class rLibraryResponse<T>
    {
        public LibraryCodes Code { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
}
