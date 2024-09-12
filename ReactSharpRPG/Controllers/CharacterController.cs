using Microsoft.AspNetCore.Mvc;
using ReactSharpRPG.Models;
using ReactSharpRPG.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactSharpRPG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        // Get all characters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Character>>> GetCharacters()
        {
            var characters = await _characterService.GetCharactersAsync();
            return Ok(characters);
        }

        // Get a character by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacter(string id)
        {
            var character = await _characterService.GetCharacterByIdAsync(id);
            if (character == null)
            {
                return NotFound();
            }
            return Ok(character);
        }

        // Create a new character
        [HttpPost]
        public async Task<ActionResult<Character>> CreateCharacter(Character character)
        {
            var success = await _characterService.CreateCharacterAsync(character);
            if (!success)
            {
                return BadRequest("Invalid ClassId. The class does not exist.");
            }

            return CreatedAtAction(nameof(GetCharacter), new { id = character.Id }, character);
        }


        // Update a character by ID
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCharacter(string id, Character updatedCharacter)
        {
            var result = await _characterService.UpdateCharacterAsync(id, updatedCharacter);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Delete a character by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(string id)
        {
            var result = await _characterService.DeleteCharacterAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

