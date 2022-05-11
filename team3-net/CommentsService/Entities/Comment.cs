namespace CommentsService.Entities
{
    public class Comment:BaseEntity
    {
     
        public string Name { get; set; }    
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid UserId { get; set; }
        public Guid ItemId { get; set; }
 }
}
