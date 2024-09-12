using ReactSharpRPG.Models;
using ReactSharpRPG.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactSharpRPG.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IClassRepository _classRepository; // Add Class repository

        public CharacterService(ICharacterRepository characterRepository, IClassRepository classRepository)
        {
            _characterRepository = characterRepository;
            _classRepository = classRepository; // Inject Class repository
        }

        public async Task<IEnumerable<Character>> GetCharactersAsync()
        {
            return await _characterRepository.GetAllCharactersAsync();
        }

        public async Task<Character> GetCharacterByIdAsync(string id)
        {
            return await _characterRepository.GetCharacterByIdAsync(id);
        }

        public async Task<bool> CreateCharacterAsync(Character character)
        {
            // Validate if the ClassId exists
            var classExists = await _classRepository.GetClassByIdAsync(character.ClassId);
            if (classExists == null)
            {
                // Handle the case when the class does not exist
                return false; // Alternatively, you can throw an exception
            }

            await _characterRepository.CreateCharacterAsync(character);
            return true;
        }

        public async Task<bool> UpdateCharacterAsync(string id, Character updatedCharacter)
        {
            return await _characterRepository.UpdateCharacterAsync(id, updatedCharacter);
        }

        public async Task<bool> DeleteCharacterAsync(string id)
        {
            return await _characterRepository.DeleteCharacterAsync(id);
        }
    }

    // Define an interface for the service
    public interface ICharacterService
    {
        Task<IEnumerable<Character>> GetCharactersAsync();
        Task<Character> GetCharacterByIdAsync(string id);
        Task<bool> CreateCharacterAsync(Character character);
        Task<bool> UpdateCharacterAsync(string id, Character updatedCharacter);
        Task<bool> DeleteCharacterAsync(string id);
    }
}
