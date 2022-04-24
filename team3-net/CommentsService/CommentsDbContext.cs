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

        public DbSet<Item> Comment { get; set; }

    }
    
}
