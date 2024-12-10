// API.Data.StockDbContext.cs
using API.Model;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions<StockDbContext> options) : base(options)
        {
        }

        public DbSet<Stock> Stock { get; set; }
     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity properties, relationships, etc.
        }
    }
}
