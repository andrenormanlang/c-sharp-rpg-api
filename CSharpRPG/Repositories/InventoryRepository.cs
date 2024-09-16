using MongoDB.Driver;
using CSharpRPG.Data;
using CSharpRPG.Models;
using System.Threading.Tasks;

namespace CSharpRPG.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IMongoCollection<Inventory> _inventories;

        public InventoryRepository(MongoDbContext context)
        {
            _inventories = context.Inventories;
        }

        public async Task<Inventory> GetInventoryByCharacterIdAsync(string characterId)
        {
            return await _inventories.Find(inventory => inventory.CharacterId == characterId).FirstOrDefaultAsync();
        }

        public async Task<bool> AddItemToInventoryAsync(string characterId, ItemEntry itemEntry)
        {
            var update = Builders<Inventory>.Update.Push(i => i.Items, itemEntry);
            var result = await _inventories.UpdateOneAsync(i => i.CharacterId == characterId, update);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> RemoveItemFromInventoryAsync(string characterId, string itemId)
        {
            var update = Builders<Inventory>.Update.PullFilter(i => i.Items, entry => entry.Item.Id == itemId);
            var result = await _inventories.UpdateOneAsync(i => i.CharacterId == characterId, update);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }

    // Interface for the repository
    public interface IInventoryRepository
    {
        Task<Inventory> GetInventoryByCharacterIdAsync(string characterId);
        Task<bool> AddItemToInventoryAsync(string characterId, ItemEntry itemEntry);
        Task<bool> RemoveItemFromInventoryAsync(string characterId, string itemId);
    }
}
