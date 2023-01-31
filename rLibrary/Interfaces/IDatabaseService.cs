using rLibrary.Entities.Enums;
using System.Threading.Tasks;

namespace rLibrary.Interfaces
{
    public interface IDatabaseService
    {
        public Task Init();
        public Task CreateCollection(DataBaseCollections collection);
        public Task<List<T>> GetDocuments<T>(DataBaseCollections collection, string query);
        public Task InsertDocument<T>(DataBaseCollections collection, string id, T Document);
        public Task UpdateDocument<T>(DataBaseCollections collection, string id, T Document);
        public Task DeleteDocument<T>(DataBaseCollections collection, string id);
    }
}
