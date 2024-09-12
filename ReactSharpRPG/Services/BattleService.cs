using MongoDB.Driver;
using ReactSharpRPG.Data;
using ReactSharpRPG.Models;
using ReactSharpRPG.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactSharpRPG.Services
{
    public class BattleService : IBattleService
    {
        private readonly IBattleRepository _battleRepository;

        public BattleService(IBattleRepository battleRepository)
        {
            _battleRepository = battleRepository;
        }

        public async Task<IEnumerable<Battle>> GetBattlesAsync()
        {
            return await _battleRepository.GetAllBattlesAsync();
        }

        public async Task<Battle> GetBattleByIdAsync(string id)
        {
            return await _battleRepository.GetBattleByIdAsync(id);
        }

        public async Task CreateBattleAsync(Battle battle)
        {
            await _battleRepository.CreateBattleAsync(battle);
        }

        public async Task<bool> UpdateBattleAsync(string id, Battle updatedBattle)
        {
            return await _battleRepository.UpdateBattleAsync(id, updatedBattle);
        }

        public async Task<bool> DeleteBattleAsync(string id)
        {
            return await _battleRepository.DeleteBattleAsync(id);
        }
    }

    // Define an interface for the repository
    public interface IBattleService
    {
        Task<IEnumerable<Battle>> GetBattlesAsync();
        Task<Battle> GetBattleByIdAsync(string id);
        Task CreateBattleAsync(Battle battle);
        Task<bool> UpdateBattleAsync(string id, Battle updatedBattle);
        Task<bool> DeleteBattleAsync(string id);
    }
}