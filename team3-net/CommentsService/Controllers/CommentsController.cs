using CommentsService.Entities;
using CommentsService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CommentsService.DTOs;

namespace CommentsService.Controllers
{

    [ApiController]
    [Route("comments")]
    public class CommentsController : ControllerBase
    {
        private readonly IRepository<Comment> commentsRepository;
        private readonly IRepository<User> usersRepository;

        //Seed data for users
        List<User> users = new List<User>()
        {
           new User() { Id = Guid.NewGuid() },
           new User() { Id = Guid.NewGuid() },
           new User() { Id = Guid.NewGuid() }
        };


        public CommentsController(IRepository<Comment> commentsRepository, IRepository<User> usersRepository)
        {
            this.commentsRepository = commentsRepository;
            this.usersRepository = usersRepository;
        }

        //GET /items
        [HttpGet]
        public async Task<IEnumerable<CommentDto>> GetAsync()
        {
            var comments = (await commentsRepository.GetAllItemsAsync())
                .Select(c => c.AsDto());
            return comments;
        }


        //GET /items/{id}
        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<CommentDto>>> GetAllCommentsByUserIdAsync(Guid userId)
        {
         
            if (userId == Guid.Empty)
            {
                return BadRequest();
            }

            var users = await usersRepository.GetAllItemsAsync();
           
            foreach (var user in users)
            {
                if (user.Id == userId)
                {
                    var comments = (await commentsRepository.GetAllItemsAsync()).Where(x => x.UserId == userId);
                    return comments;
                }
                
            }
          

           return BadRequest();
        }
        //POST /items/
        [HttpPost]
        public async Task<ActionResult<CommentDto>> PostAsync(CreateCommentDto createItemDto)
        {

            var item = new Comment
            {
                Name = createItemDto.name,
                Description = createItemDto.description,
                CreatedDate = DateTime.UtcNow
            };

            await commentsRepository.CreateAsync(item);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        }

        //PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateCommentDto updateItemDto)
        {
            var existingItem = await commentsRepository.GetItemByIdAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = updateItemDto.name;
            existingItem.Description = updateItemDto.description;

            await commentsRepository.UpdateAsync(existingItem);


            return NoContent();
        }

        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await commentsRepository.GetItemByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }
            await commentsRepository.DeleteAsync(item.Id);
            return NoContent();
        }
    }

}
