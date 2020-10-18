using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication29.Models
{
    public class CommentViewModel
    {
        public BlogPost Post { get; set; }
        public string CommenterName { get; set; }
    }
}
