using MongoDB.Driver;
using ReactSharpRPG.Data;
using ReactSharpRPG.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactSharpRPG.Repositories
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

        public async Task CreateCharacterAsync(Character character)
        {
            await _characters.InsertOneAsync(character);
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

    // Define an interface for the repository
    public interface ICharacterRepository
    {
        Task<IEnumerable<Character>> GetAllCharactersAsync();
        Task<Character> GetCharacterByIdAsync(string id);
        Task CreateCharacterAsync(Character character);
        Task<bool> UpdateCharacterAsync(string id, Character updatedCharacter);
        Task<bool> DeleteCharacterAsync(string id);
    }
}
