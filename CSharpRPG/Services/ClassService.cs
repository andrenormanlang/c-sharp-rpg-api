using CSharpRPG.Models;
using CSharpRPG.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharpRPG.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;

        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<IEnumerable<Class>> GetClassesAsync()
        {
            return await _classRepository.GetAllClassesAsync();
        }

        public async Task<Class> GetClassByIdAsync(string id)
        {
            return await _classRepository.GetClassByIdAsync(id);
        }

        public async Task CreateClassAsync(Class classEntity)
        {
            await _classRepository.CreateClassAsync(classEntity);
        }

        public async Task<bool> UpdateClassAsync(string id, Class updatedClass)
        {
            return await _classRepository.UpdateClassAsync(id, updatedClass);
        }

        public async Task<bool> DeleteClassAsync(string id)
        {
            return await _classRepository.DeleteClassAsync(id);
        }
    }

    // Define an interface for the service
    public interface IClassService
    {
        Task<IEnumerable<Class>> GetClassesAsync();
        Task<Class> GetClassByIdAsync(string id);
        Task CreateClassAsync(Class classEntity);
        Task<bool> UpdateClassAsync(string id, Class updatedClass);
        Task<bool> DeleteClassAsync(string id);
    }
}

