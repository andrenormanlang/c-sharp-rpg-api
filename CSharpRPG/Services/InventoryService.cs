// InventoryService.cs
using ReactSharpRPG.Models;
using ReactSharpRPG.Repositories;
using ReactSharpRPG.Services;
using System.Threading.Tasks;


namespace ReactSharpRPG.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<Inventory> GetInventoryByCharacterIdAsync(string characterId)
        {
            return await _inventoryRepository.GetInventoryByCharacterIdAsync(characterId);
        }

        public async Task<bool> AddItemToInventoryAsync(string characterId, ItemEntry itemEntry)
        {
            return await _inventoryRepository.AddItemToInventoryAsync(characterId, itemEntry);
        }

        public async Task<bool> RemoveItemFromInventoryAsync(string characterId, string itemId)
        {
            return await _inventoryRepository.RemoveItemFromInventoryAsync(characterId, itemId);
        }
    }

    public interface IInventoryService
    {
        Task<Inventory> GetInventoryByCharacterIdAsync(string characterId);
        Task<bool> AddItemToInventoryAsync(string characterId, ItemEntry itemEntry);
        Task<bool> RemoveItemFromInventoryAsync(string characterId, string itemId);
    }


}
