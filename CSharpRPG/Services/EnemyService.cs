using CSharpRPG.Models;
using CSharpRPG.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharpRPG.Services
{
    public class EnemyService : IEnemyService
    {
        private readonly IEnemyRepository _enemyRepository;

        public EnemyService(IEnemyRepository enemyRepository)
        {
            _enemyRepository = enemyRepository;
        }

        public async Task<IEnumerable<Enemy>> GetAllEnemiesAsync()
        {
            return await _enemyRepository.GetAllEnemiesAsync();
        }

        public async Task<Enemy> GetEnemyByIdAsync(string id)
        {
            return await _enemyRepository.GetEnemyByIdAsync(id);
        }

        public async Task CreateEnemyAsync(Enemy enemy)
        {
            await _enemyRepository.CreateEnemyAsync(enemy);
        }

        public async Task<bool> UpdateEnemyAsync(string id, Enemy updatedEnemy)
        {
            return await _enemyRepository.UpdateEnemyAsync(id, updatedEnemy);
        }

        public async Task<bool> DeleteEnemyAsync(string id)
        {
            return await _enemyRepository.DeleteEnemyAsync(id);
        }
    }

    // Define an interface for the service
    public interface IEnemyService
    {
        Task<IEnumerable<Enemy>> GetAllEnemiesAsync();
        Task<Enemy> GetEnemyByIdAsync(string id);
        Task CreateEnemyAsync(Enemy enemy);
        Task<bool> UpdateEnemyAsync(string id, Enemy updatedEnemy);
        Task<bool> DeleteEnemyAsync(string id);
    }
}
