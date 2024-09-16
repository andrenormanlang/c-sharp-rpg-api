using CSharpRPG.Models;
using CSharpRPG.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharpRPG.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _itemRepository.GetAllItemsAsync();
        }

        public async Task<Item> GetItemByIdAsync(string id)
        {
            return await _itemRepository.GetItemByIdAsync(id);
        }

        public async Task CreateItemAsync(Item item)
        {
            await _itemRepository.CreateItemAsync(item);
        }

        public async Task<bool> UpdateItemAsync(string id, Item updatedItem)
        {
            return await _itemRepository.UpdateItemAsync(id, updatedItem);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            return await _itemRepository.DeleteItemAsync(id);
        }
    }

    // Interface for the service
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetItemsAsync();
        Task<Item> GetItemByIdAsync(string id);
        Task CreateItemAsync(Item item);
        Task<bool> UpdateItemAsync(string id, Item updatedItem);
        Task<bool> DeleteItemAsync(string id);
    }
}
