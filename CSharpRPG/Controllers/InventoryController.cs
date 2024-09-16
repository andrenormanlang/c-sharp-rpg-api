using Microsoft.AspNetCore.Mvc;
using ReactSharpRPG.Models;
using ReactSharpRPG.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactSharpRPG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // Get inventory for a character
        [HttpGet("{characterId}")]
        public async Task<ActionResult<Inventory>> GetInventory(string characterId)
        {
            var inventory = await _inventoryService.GetInventoryByCharacterIdAsync(characterId);
            if (inventory == null)
            {
                return NotFound();
            }
            return Ok(inventory);
        }

        // Add item to inventory
        [HttpPost("{characterId}/addItem")]
        public async Task<IActionResult> AddItemToInventory(string characterId, ItemEntry itemEntry)
        {
            var result = await _inventoryService.AddItemToInventoryAsync(characterId, itemEntry);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Remove item from inventory
        [HttpPost("{characterId}/removeItem")]
        public async Task<IActionResult> RemoveItemFromInventory(string characterId, string itemId)
        {
            var result = await _inventoryService.RemoveItemFromInventoryAsync(characterId, itemId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
