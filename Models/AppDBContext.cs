using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> option) : base(option)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
