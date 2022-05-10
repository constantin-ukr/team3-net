namespace GamesService.Entities
{
    public class Game:BaseEntity
    {
     
        public string Name { get; set; }    
        public string Description { get; set; }
        public Genre Genre { get; set; }

    }
}
