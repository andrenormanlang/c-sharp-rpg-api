using MongoDB.Driver;
using CSharpRPG.Data;
using CSharpRPG.Models;
using CSharpRPG.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace CSharpRPG.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly IMongoCollection<Character> _characters;
        private readonly IMongoCollection<Class> _classes;

        public CharacterRepository(MongoDbContext context)
        {
            _characters = context.Characters;
            _classes = context.Classes; // Initialize the class collection
        }

        public async Task<IEnumerable<Character>> GetAllCharactersAsync()
        {
            return await _characters.Find(character => true).ToListAsync();
        }

        public async Task<Character> GetCharacterByIdAsync(string id)
        {
            return await _characters.Find<Character>(character => character.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Character>> GetCharactersByUserIdAsync(string userId)
        {
            // Convert string to ObjectId
            if (!ObjectId.TryParse(userId, out var objectId))
            {
                throw new ArgumentException("Invalid ObjectId format for userId.");
            }

            // Query using the ObjectId
            var characters = await _characters.Find(c => c.UserId == objectId.ToString()).ToListAsync();
            return characters;
        }

        public async Task<IEnumerable<CharacterWithClassDto>> GetAllCharactersWithClassesAsync()
        {
            var characters = await _characters.Find(character => true).ToListAsync();
            var result = new List<CharacterWithClassDto>();

            foreach (var character in characters)
            {
                var characterClass = await _classes.Find<Class>(c => c.Id == character.ClassId).FirstOrDefaultAsync();
                if (characterClass != null)
                {
                    character.ClassName = characterClass.Name;  // Assign the ClassName from the Class entity
                }
                result.Add(new CharacterWithClassDto(character, characterClass));
            }

            return result;
        }

        public async Task<CharacterWithClassDto> GetCharacterWithClassByIdAsync(string id)
        {
            var character = await _characters.Find<Character>(c => c.Id == id).FirstOrDefaultAsync();
            if (character != null)
            {
                var characterClass = await _classes.Find<Class>(c => c.Id == character.ClassId).FirstOrDefaultAsync();
                return new CharacterWithClassDto(character, characterClass);
            }
            return null; // Return null if no character is found
        }



        public async Task<bool> CreateCharacterAsync(Character character)
        {
            await _characters.InsertOneAsync(character);
            return true; // Return true if successful
        }

        public async Task<bool> UpdateCharacterAsync(string id, Character updatedCharacter)
        {
            var result = await _characters.ReplaceOneAsync(character => character.Id == id, updatedCharacter);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteCharacterAsync(string id)
        {
            var result = await _characters.DeleteOneAsync(character => character.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }

    public interface ICharacterRepository
    {
        Task<IEnumerable<Character>> GetAllCharactersAsync();
        Task<Character> GetCharacterByIdAsync(string id);
        Task<IEnumerable<Character>> GetCharactersByUserIdAsync(string userId);
        Task<bool> CreateCharacterAsync(Character character);
        Task<bool> UpdateCharacterAsync(string id, Character updatedCharacter);
        Task<bool> DeleteCharacterAsync(string id);
        Task<IEnumerable<CharacterWithClassDto>> GetAllCharactersWithClassesAsync();
        Task<CharacterWithClassDto> GetCharacterWithClassByIdAsync(string id);

    }
}
