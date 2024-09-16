using MongoDB.Driver;
using CSharpRPG.Data;
using CSharpRPG.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharpRPG.Repositories
{
    public class BattleRepository : IBattleRepository
    {
        private readonly IMongoCollection<Battle> _battles;

        public BattleRepository(MongoDbContext context)
        {
            _battles = context.Battles;
        }

        public async Task<IEnumerable<Battle>> GetAllBattlesAsync()
        {
            return await _battles.Find(battle => true).ToListAsync();
        }

        public async Task<Battle> GetBattleByIdAsync(string id)
        {
            return await _battles.Find(battle => battle.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateBattleAsync(Battle battle)
        {
            await _battles.InsertOneAsync(battle);
        }

        public async Task<bool> UpdateBattleAsync(string id, Battle updatedBattle)
        {
            var result = await _battles.ReplaceOneAsync(battle => battle.Id == id, updatedBattle);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteBattleAsync(string id)
        {
            var result = await _battles.DeleteOneAsync(battle => battle.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }

    // Define an interface for the repository
    public interface IBattleRepository
    {
        Task<IEnumerable<Battle>> GetAllBattlesAsync();
        Task<Battle> GetBattleByIdAsync(string id);
        Task CreateBattleAsync(Battle battle);
        Task<bool> UpdateBattleAsync(string id, Battle updatedBattle);
        Task<bool> DeleteBattleAsync(string id);
    }
}