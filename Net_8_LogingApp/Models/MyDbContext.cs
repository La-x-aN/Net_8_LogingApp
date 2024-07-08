using Microsoft.EntityFrameworkCore;

namespace Net_8_LogingApp.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) :base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
    }
}
