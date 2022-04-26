using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CommentsService.DTOs;

namespace CommentsService.Controllers
{

    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        //DataBase here
        private static readonly List<ItemDto> items = new()
        {
            new ItemDto(Guid.NewGuid(), "God of War", "Kratos and son are finally making their way to PC in a port of the much-loved action hack-and-slash.", DateTime.UtcNow),
            new ItemDto(Guid.NewGuid(), "Dying Light 2", "The sequel to Techland's zombies and parkour action game is finally arriving in 2022 after multiple delays.", DateTime.UtcNow),
            new ItemDto(Guid.NewGuid(), "Total War: Warhammer 3", "This is the final game in Creative Assembly's trilogy, bringing the last of the tabletop wargame's armies into digital form.", DateTime.UtcNow),
        };

        [HttpGet]
        public IEnumerable<ItemDto> Get()
        {
            return items;
        }


        //GET /items/{id}
        [HttpGet("{id}")]

        public ItemDto GetById(Guid id)
        {

            var item = items.Where(item => item.id == id).SingleOrDefault();
            if (item == null)
            {
                return item;
            }
            return item;
        }
        // POST /items/
        [HttpPost]
        public ActionResult<ItemDto> Post(CreateItemDto createItemDto)
        {
            var item = new ItemDto(Guid.NewGuid(), createItemDto.name, createItemDto.description, DateTime.UtcNow);
            items.Add(item);
            return CreatedAtAction(nameof(GetById), new { id = item.id }, item);
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = items.Where(item => item.id == id).SingleOrDefault();
            
            if (existingItem == null)
            {
                return NotFound();
            }

            var updateItem = existingItem with
            {
                name = updateItemDto.name,
                description = updateItemDto.description
            };

            var index = items.FindIndex(existingItem => existingItem.id == id);
            items[index] = updateItem;

            return NoContent();
        }
              
        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem.id == id);
            items.RemoveAt(index);
            return NoContent();
        }
    }

}
