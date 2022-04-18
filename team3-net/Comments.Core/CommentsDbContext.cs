using Comments.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Comments.Core
{

    public class CommentsDbContext : DbContext
    { 
        public CommentsDbContext(DbContextOptions<CommentsDbContext> options) : base(options)

        {
            Database.EnsureCreated();
        }

        public DbSet<CommentEntity> Comment { get; set; }

                //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
