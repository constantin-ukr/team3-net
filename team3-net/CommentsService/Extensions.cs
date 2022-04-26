using CommentsService.Entities;
using static CommentsService.DTOs;

namespace CommentsService
{
    public static class Extensions
    {
        public static ItemDto AsDto (this Item item)
        {
            return new ItemDto(item.Id, item.Name, item.Description, item.CreatedDate);
        }
    }
}
