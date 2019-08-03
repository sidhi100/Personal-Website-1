
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final4.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string CommenterName { get; set; }
        public string CommentTopic { get; set; }
        public string CommentData { get; set; }

    }

}
