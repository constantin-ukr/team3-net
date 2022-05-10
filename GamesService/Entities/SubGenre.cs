namespace GamesService.Entities
{
    public class SubGenre : BaseEntity
    {

        public string Name { get; set; }
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
