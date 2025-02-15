using WebApplication3.Models;

namespace WebApplication3.Services
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(CreateUser createuser);
        Task<User> UpdateUserAsync(Guid id,UpdateUser updateuser);
        Task DeleteUserAsync(Guid userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid userId);
       
    }
}
