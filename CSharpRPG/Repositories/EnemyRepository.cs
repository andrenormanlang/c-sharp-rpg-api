using MongoDB.Driver;
using CSharpRPG.Data;
using CSharpRPG.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharpRPG.Repositories
{
    public class EnemyRepository : IEnemyRepository
    {
        private readonly IMongoCollection<Enemy> _enemies;

        public EnemyRepository(MongoDbContext context)
        {
            _enemies = context.Enemies;
        }

        public async Task<IEnumerable<Enemy>> GetAllEnemiesAsync()
        {
            return await _enemies.Find(enemy => true).ToListAsync();
        }

        public async Task<Enemy> GetEnemyByIdAsync(string id)
        {
            return await _enemies.Find<Enemy>(enemy => enemy.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateEnemyAsync(Enemy enemy)
        {
            await _enemies.InsertOneAsync(enemy);
        }

        public async Task<bool> UpdateEnemyAsync(string id, Enemy updatedEnemy)
        {
            var result = await _enemies.ReplaceOneAsync(enemy => enemy.Id == id, updatedEnemy);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteEnemyAsync(string id)
        {
            var result = await _enemies.DeleteOneAsync(enemy => enemy.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }

    // Define an interface for the repository
    public interface IEnemyRepository
    {
        Task<IEnumerable<Enemy>> GetAllEnemiesAsync();
        Task<Enemy> GetEnemyByIdAsync(string id);
        Task CreateEnemyAsync(Enemy enemy);
        Task<bool> UpdateEnemyAsync(string id, Enemy updatedEnemy);
        Task<bool> DeleteEnemyAsync(string id);
    }
}
