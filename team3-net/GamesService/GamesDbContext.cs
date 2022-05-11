using GamesService.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamesService
{
    public class GamesDbContext : DbContext
    {
        public GamesDbContext(DbContextOptions<GamesDbContext> options) : base(options)

        {
            Database.EnsureCreated();
        }

        public DbSet<Game> Games { get; set; }

        //seed data

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Genre>()
        //    .HasMany(p => p.SubGenres)
        //    .WithOne(c => c.Genre)
        //    .IsRequired();

        //    //modelBuilder.Entity<Game>().HasData(
        //    //    new Game
        //    //    {
        //    //        Id = Guid.NewGuid(),
        //    //        Name = "XCOM 2",
        //    //        Description = "XCOM 2 is one of the best turn-based strategy games",
        //    //        Genre = new Genre
        //    //        {
        //    //            Id = Guid.NewGuid(),
        //    //            //Name = "Strategy",
        //    //            //SubGenres = new List<SubGenre>
        //    //            //{
        //    //            //    new SubGenre { Id = Guid.NewGuid(), Name = "Rally", GenreId = Guid.NewGuid()},
        //    //            //    new SubGenre { Id = Guid.NewGuid(), Name = "Formula", GenreId = Guid.NewGuid()}
        //    //            //}

        //    //        }
        //    //        //},
        //    //        //new Game
        //    //        //{
        //    //        //    Id = Guid.NewGuid(),
        //    //        //    Name = "Assassin's Creed Valhalla",
        //    //        //    Description = "If you’ve ever thought that the sailing, raiding and pillaging life of a Viking is for you, then Assassin’s Creed Valhalla is probably as close as you’re ever going to get to give it a go. ",
        //    //        //    Genre = new Genre
        //    //        //    {
        //    //        //        Id = Guid.NewGuid(),
        //    //        //        Name = "RPG"
        //    //        //    }
        //    //        //},
        //    //        //new Game
        //    //        //{
        //    //        //    Id = Guid.NewGuid(),
        //    //        //    Name = "Forza Horizon 5",
        //    //        //    Description = "With Phil's review of Forza Horizon 5, he zooms in on the level of refinement that's taken place in the Forza series",
        //    //        //    Genre = new Genre
        //    //        //    {
        //    //        //        Id = Guid.NewGuid(),
        //    //        //        Name = "Races",
        //    //        //        SubGenres = new List<SubGenre>
        //    //        //           { new SubGenre { Id = Guid.NewGuid(), Name = "Rally"},
        //    //        //             new SubGenre { Id = Guid.NewGuid(), Name = "Formula"}
        //    //        //           }
        //    //        //    }
        //    //    }
        //    //);
        //}

    }

}
