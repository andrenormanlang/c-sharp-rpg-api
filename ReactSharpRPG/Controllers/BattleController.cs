using Microsoft.AspNetCore.Mvc;
using ReactSharpRPG.Models;
using ReactSharpRPG.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactSharpRPG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BattleController : ControllerBase
    {
        private readonly IBattleService _battleService;

        public BattleController(IBattleService battleService)
        {
            _battleService = battleService;
        }

        // Get all battles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Battle>>> GetBattles()
        {
            var battles = await _battleService.GetBattlesAsync();
            return Ok(battles);
        }

        // Get a battle by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Battle>> GetBattle(string id)
        {
            var battle = await _battleService.GetBattleByIdAsync(id);
            if (battle == null)
            {
                return NotFound();
            }
            return Ok(battle);
        }

        // Create a new battle
        [HttpPost]
        public async Task<ActionResult<Battle>> CreateBattle(Battle battle)
        {
            await _battleService.CreateBattleAsync(battle);
            return CreatedAtAction(nameof(GetBattle), new { id = battle.Id }, battle);
        }

        // Update a battle by ID
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBattle(string id, Battle updatedBattle)
        {
            var result = await _battleService.UpdateBattleAsync(id, updatedBattle);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Delete a battle by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBattle(string id)
        {
            var result = await _battleService.DeleteBattleAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
