using GamesService.Entities;
using static GamesService.DTOs;

namespace GamesService
{
    public static class Extensions
    {
        public static GameDto AsDto (this Game game)
        {
            return new GameDto(game.Id, game.Name, game.Description, game.Genre, game.CommentId, game.UserId);
        }
    }
}
