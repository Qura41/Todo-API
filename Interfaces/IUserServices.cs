using System.Runtime.InteropServices;
using TodoAPI.Contracts;
using TodoAPI.Models;

namespace TodoAPI.Interfaces
{
    public interface IUserServices
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task CreateUserAsync(CreateUserRequest request);
        Task UpdateUserAsync(Guid id, UpdateUserRequest request);
        Task DeleteUserAsync(Guid id);
    }
}