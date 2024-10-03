using CSharpRPG.Models;
using CSharpRPG.Repositories;
using CSharpRPG.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharpRPG.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IClassRepository _classRepository;

        public CharacterService(ICharacterRepository characterRepository, IClassRepository classRepository)
        {
            _characterRepository = characterRepository;
            _classRepository = classRepository;
        }

        public async Task<IEnumerable<Character>> GetCharactersAsync()
        {
            return await _characterRepository.GetAllCharactersAsync();
        }

        public async Task<Character> GetCharacterByIdAsync(string id)
        {
            return await _characterRepository.GetCharacterByIdAsync(id);
        }

        public async Task<IEnumerable<Character>> GetCharactersByUserIdAsync(string userId)
        {
            try
            {
                return await _characterRepository.GetCharactersByUserIdAsync(userId);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error fetching characters for userId {userId}: {ex.Message}");
                return new List<Character>(); // Return an empty list in case of an error
            }
        }

        public async Task<IEnumerable<CharacterWithClassDto>> GetCharactersWithClassesAsync()
        {
            return await _characterRepository.GetAllCharactersWithClassesAsync();
        }


        public async Task<CharacterWithClassDto> GetCharacterWithClassByIdAsync(string id)
        {
            return await _characterRepository.GetCharacterWithClassByIdAsync(id);
        }

        public async Task<IEnumerable<CharacterWithClassDto>> GetCharactersWithClassesByUserIdAsync(string userId)
        {
            var characters = await _characterRepository.GetCharactersByUserIdAsync(userId);
            var result = new List<CharacterWithClassDto>();

            foreach (var character in characters)
            {
                var characterClass = await _classRepository.GetClassByIdAsync(character.ClassId);
                result.Add(new CharacterWithClassDto(character, characterClass));
            }

            return result;
        }



        public async Task<bool> CreateCharacterAsync(Character character)
        {
            // Fetch the class data for the selected class
            var classEntity = await _classRepository.GetClassByIdAsync(character.ClassId);

            if (classEntity == null)
            {
                // If class does not exist, return false
                return false;
            }

            // Set base stats from the class
            character.Health = classEntity.BaseHealth;
            character.Attack = classEntity.BaseAttack;
            character.Defense = classEntity.BaseDefense;
            character.Experience = classEntity.BaseExperience;
            character.Level = classEntity.BaseLevel;

            // Set new base attributes
            character.Mana = classEntity.BaseMana;
            character.Stamina = classEntity.BaseStamina;
            character.Speed = classEntity.BaseSpeed;

            // Create the character
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
        Task<IEnumerable<Character>> GetCharactersByUserIdAsync(string userId); // Add this line
        Task<bool> CreateCharacterAsync(Character character);
        Task<bool> UpdateCharacterAsync(string id, Character updatedCharacter);
        Task<bool> DeleteCharacterAsync(string id);
        Task<IEnumerable<CharacterWithClassDto>> GetCharactersWithClassesAsync();
        Task<CharacterWithClassDto> GetCharacterWithClassByIdAsync(string id); // Add this line
        Task<IEnumerable<CharacterWithClassDto>> GetCharactersWithClassesByUserIdAsync(string userId);

    }
}
