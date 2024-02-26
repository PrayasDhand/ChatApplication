using ChatApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Context_Files
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Users> Users {get; set;}
        public virtual DbSet<Chats> Chats { get; set;}  

    }
}
