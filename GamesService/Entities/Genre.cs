namespace GamesService.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public List<SubGenre> SubGenres { get; set; }
    }
}
