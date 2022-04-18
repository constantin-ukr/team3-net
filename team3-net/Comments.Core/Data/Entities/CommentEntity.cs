using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comments.Core.Entities
{
    public class CommentEntity:BaseEntity
    {
        public string? FieldText { get; set; }
        
        //Navigation Properties
        //public int UserId { get; set; }
        //public User user { get; set; }

        //public <CategoryOfProduct> Tests { get; set; }

    }
}
