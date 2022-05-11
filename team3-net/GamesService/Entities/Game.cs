namespace GamesService.Entities
{
    public class Game : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid CommentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Genre Genre { get; set; }
    }
}
