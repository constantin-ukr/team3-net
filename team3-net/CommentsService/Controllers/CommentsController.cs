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
        private readonly IRepository<Comment> itemsRepository;

        public CommentsController(IRepository<Comment> itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        //GET /items
        [HttpGet]
        public async Task<IEnumerable<CommentDto>> GetAsync()
        {
            var comments = (await itemsRepository.GetAllItemsAsync())
                .Select(c => c.AsDto());
            return comments;
        }


        //GET /items/{id}
        [HttpGet("{id}")]

        public async Task<ActionResult<CommentDto>> GetByIdAsync(Guid id)
        {

            var comment = await itemsRepository.GetItemByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return comment.AsDto();
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

            await itemsRepository.CreateAsync(item);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        }

        //PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateCommentDto updateItemDto)
        {
            var existingItem = await itemsRepository.GetItemByIdAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = updateItemDto.name;
            existingItem.Description = updateItemDto.description;

            await itemsRepository.UpdateAsync(existingItem);


            return NoContent();
        }

        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await itemsRepository.GetItemByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }
            await itemsRepository.DeleteAsync(item.Id);
            return NoContent();
        }
    }

}
