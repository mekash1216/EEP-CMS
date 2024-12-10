// IUserRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Model;

namespace API.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task<User> AddAsync(User user);
        Task<User> UpdateAsync(User user);
        Task DeleteAsync(Guid id);
    }
}
