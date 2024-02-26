using ChatApplication.Context_Files;
using ChatApplication.Models;
using Firebase.Auth;
using Firebase.Auth.Repository;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Repositories.Identity
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext context;

        public UserRepository(UserContext context)
        {
            this.context = context;
        }

        public async Task<Users> AddUserAsync(Users user)
        {
            var result = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public void DeleteUser()
        {
            throw new NotImplementedException();
        }

        public async Task<Users> GetUserByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<Users>> GetUsersAsync()
        {
            return await context.Users.ToListAsync();
        }

        public (UserInfo userInfo, FirebaseCredential credential) ReadUser()
        {
            throw new NotImplementedException();
        }

        public void SaveUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool UserExists()
        {
            throw new NotImplementedException();
        }
    }
}
