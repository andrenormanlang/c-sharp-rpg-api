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

        // POST: api/battle/bulk
        [HttpPost("bulk")]
        public async Task<ActionResult> CreateBattlesBulk([FromBody] IEnumerable<Battle> battles)
        {
            foreach (var battle in battles)
            {
                await _battleService.CreateBattleAsync(battle);
            }
            return Ok("Bulk battles created successfully.");
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
    }
}
