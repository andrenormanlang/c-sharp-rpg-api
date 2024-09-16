using MongoDB.Driver;
using CSharpRPG.Data;
using CSharpRPG.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharpRPG.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly IMongoCollection<Character> _characters;

        public CharacterRepository(MongoDbContext context)
        {
            _characters = context.Characters;
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
            return await _characters.Find(character => character.UserId == userId).ToListAsync();
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
    }
}
