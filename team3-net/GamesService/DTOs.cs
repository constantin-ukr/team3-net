using GamesService.Entities;
using System.ComponentModel.DataAnnotations;

namespace GamesService
{
    public class DTOs
    {
        public record GameDto(Guid id, string name, string description, Genre Genre, Guid commentId, Guid UserId);
       
        public record CreateGameDto([Required] string name, string description, Genre Genre, Guid commentId);    

        public record UpdateGameDto([Required] string name, string description, Genre Genre);

        public record CommentDto(Guid id, string name, string description, DateTime? created);

    }
}
