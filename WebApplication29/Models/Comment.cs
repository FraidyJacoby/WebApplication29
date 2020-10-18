using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication29.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int BlogPostId { get; set; }
        public string Name { get; set; }
        public string CommentText { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
