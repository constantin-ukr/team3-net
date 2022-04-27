using CommentsService.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommentsService
{
    public class CommentsDbContext : DbContext
    {
        public CommentsDbContext(DbContextOptions<CommentsDbContext> options) : base(options)

        {
          //  Database.EnsureCreated();
        }

        public DbSet<Item> Items { get; set; }

        //seed data

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    Id = Guid.NewGuid(), 
                    Name = "Electronics",
                    Description = "Electronic Items",
                },
                new Item
                {
                    Id = Guid.NewGuid(),
                    Name = "Clothes",
                    Description = "Dresses",
                },
                new Item
                {
                    Id = Guid.NewGuid(),
                    Name = "Grocery",
                    Description = "Grocery Items",
                }
            );
        }

    }

}
