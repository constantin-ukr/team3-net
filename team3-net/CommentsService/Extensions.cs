using CommentsService.Entities;
using static CommentsService.DTOs;

namespace CommentsService
{
    public static class Extensions
    {
        public static CommentDto AsDto (this Comment comment)
        {
            return new CommentDto(comment.Id, comment.Name, comment.Description, comment.CreatedDate, comment.UserId,comment.ItemId);
        }
    }
}
