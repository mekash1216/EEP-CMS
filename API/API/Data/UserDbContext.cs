namespace API.Model
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } // This DbSet represents the 'Users' table in the database
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("MyConnection");
            }
        }
    }
}
