using System.ComponentModel.DataAnnotations;

namespace CommentsService
{
    public class DTOs
    {
        public record ItemDto(Guid id, string name, string description, DateTime? created);

        public record CreateItemDto([Required] string name, string description);    

        public record UpdateItemDto ([Required] string name, string description);

        
        
    }
}
