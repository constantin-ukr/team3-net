using System.ComponentModel.DataAnnotations;

namespace CommentsService
{
    public class DTOs
    {
        public record CommentDto(Guid id, string name, string description, DateTime? created, Guid UserId);

        public record CreateCommentDto([Required] string name, string description);    

        public record UpdateCommentDto([Required] string name, string description);

        
        
    }
}
