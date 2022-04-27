using CommentsService.Entities;
using CommentsService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CommentsService.DTOs;

namespace CommentsService.Controllers
{

    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository itemsRepository;

        public ItemsController(IItemRepository itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        //GET /items
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAsync()
        {
            var items = (await itemsRepository.GetAllItemsAsync())
                .Select(item => item.AsDto());
            return items;
        }


        //GET /items/{id}
        [HttpGet("{id}")]

        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {

            var item = await itemsRepository.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item.AsDto();
        }
        //POST /items/
        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
        {
            var item = new Item
            {
                Name = createItemDto.name,
                Description = createItemDto.description
            };

            await itemsRepository.CreateAsync(item);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        }

        //PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto)
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
