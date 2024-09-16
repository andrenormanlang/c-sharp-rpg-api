using MongoDB.Driver;
using ReactSharpRPG.Data;
using ReactSharpRPG.Models;
using System.Threading.Tasks;

namespace ReactSharpRPG.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly IMongoCollection<Class> _classes;

        public ClassRepository(MongoDbContext context)
        {
            _classes = context.Classes;
        }

        public async Task<IEnumerable<Class>> GetAllClassesAsync()
        {
            return await _classes.Find(classEntity => true).ToListAsync();
        }

        public async Task<Class> GetClassByIdAsync(string id)
        {
            return await _classes.Find<Class>(classEntity => classEntity.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateClassAsync(Class classEntity)
        {
            await _classes.InsertOneAsync(classEntity);
        }

        public async Task<bool> UpdateClassAsync(string id, Class updatedClass)
        {
            var result = await _classes.ReplaceOneAsync(classEntity => classEntity.Id == id, updatedClass);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteClassAsync(string id)
        {
            var result = await _classes.DeleteOneAsync(classEntity => classEntity.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

    }
        public interface IClassRepository
        {
            Task<IEnumerable<Class>> GetAllClassesAsync();
            Task<Class> GetClassByIdAsync(string id);
            Task CreateClassAsync(Class classEntity);
            Task<bool> UpdateClassAsync(string id, Class updatedClass);
            Task<bool> DeleteClassAsync(string id);
        }


}
