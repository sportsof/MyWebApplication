using Microsoft.EntityFrameworkCore;

namespace MyWebApplication.Data
{
    public class MyApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public MyApplicationContext(DbContextOptions<MyApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //OnModelCreating(modelBuilder);
        }
    }

}
