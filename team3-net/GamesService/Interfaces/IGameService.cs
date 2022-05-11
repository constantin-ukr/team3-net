using GamesService.Entities;

namespace GamesService.Interfaces
{
    public interface IGameService
    {
            Game GetById(int id);
            void Create(Game game);
            void Delete(int id);
         //   List<Game> GetAllForUser(int userId);
    }
}
