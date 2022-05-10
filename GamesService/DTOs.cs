using GamesService.Entities;
using System.ComponentModel.DataAnnotations;

namespace GamesService
{
    public class DTOs
    {
        public record GameDto(Guid id, string name, string description, Genre Genre);

        public record CreateGameDto([Required] string name, string description, Genre Genre);    

        public record UpdateGameDto([Required] string name, string description, Genre Genre);

        
        
    }
}
