using Microsoft.AspNetCore.Mvc;
using CSharpRPG.Models;
using CSharpRPG.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using CSharpRPG.Dtos;

namespace CSharpRPG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        // Only one declaration for ICharacterService
        private readonly ICharacterService _characterService;

        // Inject ICharacterService via the constructor
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        // Get all characters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterWithClassDto>>> GetCharacters()
        {
            var charactersWithClasses = await _characterService.GetCharactersWithClassesAsync();
            return Ok(charactersWithClasses);
        }

        // Get a character by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterWithClassDto>> GetCharacter(string id)
        {
            var characterWithClass = await _characterService.GetCharacterWithClassByIdAsync(id);
            if (characterWithClass == null)
            {
                return NotFound();
            }
            return Ok(characterWithClass);
        }

        // Get characters by UserId
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCharactersByUserId(string userId)
        {
            if (!ObjectId.TryParse(userId, out _))
            {
                return BadRequest("Invalid user ID format.");
            }

            var charactersWithClasses = await _characterService.GetCharactersWithClassesByUserIdAsync(userId);
            if (charactersWithClasses == null || !charactersWithClasses.Any())
            {
                return NotFound("No characters found for this user.");
            }

            return Ok(charactersWithClasses);
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
