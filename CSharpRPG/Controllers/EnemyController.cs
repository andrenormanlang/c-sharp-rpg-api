using Microsoft.AspNetCore.Mvc;
using CSharpRPG.Models;
using CSharpRPG.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharpRPG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnemyController : ControllerBase
    {
        private readonly IEnemyService _enemyService;

        public EnemyController(IEnemyService enemyService)
        {
            _enemyService = enemyService;
        }

        // Get all enemies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enemy>>> GetEnemies()
        {
            var enemies = await _enemyService.GetAllEnemiesAsync();
            return Ok(enemies);
        }

        // Get a specific enemy by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Enemy>> GetEnemy(string id)
        {
            var enemy = await _enemyService.GetEnemyByIdAsync(id);
            if (enemy == null)
            {
                return NotFound();
            }
            return Ok(enemy);
        }

        // Create a new enemy
        [HttpPost]
        public async Task<ActionResult<Enemy>> CreateEnemy(Enemy enemy)
        {
            await _enemyService.CreateEnemyAsync(enemy);
            return CreatedAtAction(nameof(GetEnemy), new { id = enemy.Id }, enemy);
        }

        // POST: api/enemy/bulk
        [HttpPost("bulk")]
        public async Task<ActionResult> CreateEnemiesBulk([FromBody] IEnumerable<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                await _enemyService.CreateEnemyAsync(enemy);
            }
            return Ok("Bulk enemies created successfully.");
        }


        // Update an enemy
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEnemy(string id, Enemy updatedEnemy)
        {
            var result = await _enemyService.UpdateEnemyAsync(id, updatedEnemy);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("bulk")]
        public async Task<ActionResult> UpdateEnemiesBulk([FromBody] IEnumerable<Enemy> updatedEnemies)
        {
            foreach (var enemy in updatedEnemies)
            {
                var result = await _enemyService.UpdateEnemyAsync(enemy.Id, enemy);
                if (!result)
                {
                    // Return NotFound if any enemy is not found, you can also choose to continue updating the rest
                    return NotFound($"Enemy with ID {enemy.Id} not found.");
                }
            }
            return Ok("Bulk enemies updated successfully.");
        }


        // Delete an enemy
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnemy(string id)
        {
            var result = await _enemyService.DeleteEnemyAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
