namespace CommentsService.Entities
{
    public class Item:BaseEntity
    {
     
        public string Name { get; set; }    
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
