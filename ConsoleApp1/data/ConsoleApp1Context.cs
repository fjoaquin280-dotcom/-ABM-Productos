using Microsoft.EntityFrameworkCore;
using ConsoleApp1.entities;

namespace ConsoleApp1.Data
{
    public class ConsoleApp1Context : DbContext
    {
        public ConsoleApp1Context(DbContextOptions<ConsoleApp1Context> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Notebook",
                    Price = 1500
                },
                new Product
                {
                    Id = 2,
                    Name = "Mouse",
                    Price = 50
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}