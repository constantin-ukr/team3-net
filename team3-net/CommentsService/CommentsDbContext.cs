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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    Id = Guid.NewGuid(),
                    Name = "Electronics",
                    Description = "Electronic Items",
                    CreatedDate = DateTime.Now,
                    ItemId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                },
                new Comment
                {
                    Id = Guid.NewGuid(),
                    Name = "Clothes",
                    Description = "Dresses",
                    CreatedDate = DateTime.Now,
                    ItemId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                },
                new Comment
                {
                    Id = Guid.NewGuid(),
                    Name = "Grocery",
                    Description = "Grocery Items",
                    CreatedDate = DateTime.Now,
                    ItemId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                }
            );

            modelBuilder.Entity<User>().HasData(
               new User
               {
                   Id = Guid.NewGuid(),
                   Role = "Manager",
                   UserName = "Nick"
               },
              new User
              {
                  Id = Guid.NewGuid(),
                  Role = "Admin",
                  UserName = "Olga"
              },
              new User
              {
                  Id = Guid.NewGuid(),
                  Role = "Guest",
                  UserName = "Taras"
              }
           );
        }

    }

}
