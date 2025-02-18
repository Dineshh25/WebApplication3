using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.Models.Dtos;

namespace WebApplication3.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext dbContext;
        public UserService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

       
        public async Task<User> CreateUserAsync(CreateUser createuser)
        {
            var user = new User
            {
                Username = createuser.Username,
                Email = createuser.Email,
                IsDeleted = false
            };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return user;
        }
        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await dbContext.Users.Where(u => !u.IsDeleted).ToListAsync();
        }


        public async Task<User> UpdateUserAsync(Guid id,UpdateUser updateuser)
        {
           var existinguser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
            if (existinguser == null)
            {
                return null;
            }
            existinguser.Username = updateuser.Username;
            existinguser.Email = updateuser.Email;
        
            dbContext.Users.Update(existinguser);
            await dbContext.SaveChangesAsync();
            return existinguser;
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await dbContext.Users.FindAsync(userId);
            if (user != null)
            {
                user.IsDeleted = true;
                await dbContext.SaveChangesAsync();
            }
           
        }

        
    }
} 

