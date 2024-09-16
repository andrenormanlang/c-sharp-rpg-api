using Microsoft.AspNetCore.Mvc;
using CSharpRPG.Models;
using CSharpRPG.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharpRPG.Controllers
{
    [ApiController]
    [Route("api/item")]  // Singular route
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        // GET: api/item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllItems()
        {
            var items = await _itemService.GetItemsAsync();
            return Ok(items);
        }

        // GET: api/item/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItemById(string id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // POST: api/item
        [HttpPost]
        public async Task<ActionResult<Item>> CreateItem([FromBody] Item item)
        {
            await _itemService.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
        }

        // POST: api/item/bulk
        [HttpPost("bulk")]
        public async Task<ActionResult> CreateItemsBulk([FromBody] IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                await _itemService.CreateItemAsync(item);
            }
            return Ok("Bulk items created successfully.");
        }

        // PUT: api/item/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(string id, [FromBody] Item updatedItem)
        {
            var result = await _itemService.UpdateItemAsync(id, updatedItem);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/item/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            var result = await _itemService.DeleteItemAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
