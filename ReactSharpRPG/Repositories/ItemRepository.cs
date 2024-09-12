using MongoDB.Driver;
using ReactSharpRPG.Data;
using ReactSharpRPG.Models;
using System.Threading.Tasks;

namespace ReactSharpRPG.Repositories
{

    public class ItemRepository : IItemRepository
    {
        private readonly IMongoCollection<Item> _items;

        public ItemRepository(MongoDbContext context)
        {
            _items = context.Items;
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _items.Find(item => true).ToListAsync();
        }

        public async Task<Item> GetItemByIdAsync(string id)
        {
            return await _items.Find(item => item.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateItemAsync(Item item)
        {
            await _items.InsertOneAsync(item);
        }

        public async Task<bool> UpdateItemAsync(string id, Item updatedItem)
        {
            var result = await _items.ReplaceOneAsync(item => item.Id == id, updatedItem);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var result = await _items.DeleteOneAsync(item => item.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
    // Interface for the repository
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item> GetItemByIdAsync(string id);
        Task CreateItemAsync(Item item);
        Task<bool> UpdateItemAsync(string id, Item updatedItem);
        Task<bool> DeleteItemAsync(string id);
    }
}