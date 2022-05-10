using CommentsService.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommentsService
{
    public class CommentsDbContext : DbContext
    {
        public CommentsDbContext(DbContextOptions<CommentsDbContext> options) : base(options)

        {
         Database.EnsureCreated();
        }

        public DbSet<Comment> Comments { get; set; }

        //seed data

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    Id = Guid.NewGuid(),
                    Name = "Electronics",
                    Description = "Electronic Items",
                },
                new Comment
                {
                    Id = Guid.NewGuid(),
                    Name = "Clothes",
                    Description = "Dresses",
                },
                new Comment
                {
                    Id = Guid.NewGuid(),
                    Name = "Grocery",
                    Description = "Grocery Items",
                }
            );
        }

    }

}
