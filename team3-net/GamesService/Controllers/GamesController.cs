using GamesService.Clients;
using GamesService.Entities;
using GamesService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static GamesService.DTOs;

namespace GamesService.Controllers
{

    [ApiController]
    [Route("games")]
    public class GamesController : ControllerBase
    {
        private readonly IRepository<Game> gamesRepository;
        private readonly CommentsClient commentsClient;

        public GamesController(IRepository<Game> gamesRepository, CommentsClient commentsClient)
        {
            this.gamesRepository = gamesRepository;
            this.commentsClient = commentsClient;
        }

        //GET /items
        [HttpGet]
        public async Task<IEnumerable<GameDto>> GetAsync()
        {
            var games = (await gamesRepository.GetAllItemsAsync())
                .Select(c => c.AsDto());
            return games;
        }


        //GET /items/{id}
        [HttpGet("{id}")]

        public async Task<ActionResult<GameDto>> GetByIdAsync(Guid id)
        {

            var game = await gamesRepository.GetItemByIdAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return game.AsDto();
        }
        //POST /items/
        [HttpPost]
        public async Task<ActionResult<GameDto>> PostAsync(CreateGameDto createItemDto)
        {
            var item = new Game
            {
                Name = createItemDto.name,
                Description = createItemDto.description,
                Genre = createItemDto.Genre,
                CommentId = createItemDto.commentId
            };

            await gamesRepository.CreateAsync(item);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        }

        //PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateGameDto updateItemDto)
        {
            var existingItem = await gamesRepository.GetItemByIdAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = updateItemDto.name;
            existingItem.Description = updateItemDto.description;

            await gamesRepository.UpdateAsync(existingItem);


            return NoContent();
        }

        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await gamesRepository.GetItemByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }
            await gamesRepository.DeleteAsync(item.Id);
            return NoContent();
        }
    }

}
