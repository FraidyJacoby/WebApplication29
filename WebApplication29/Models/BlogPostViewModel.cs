using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication29.Models
{
    public class BlogPostViewModel
    {
        public List<BlogPost> Posts { get; set; }

        public string GetFirst200Char(int id)
        {
            BlogPostDb db = new BlogPostDb(@"Data Source=.\sqlexpress;Initial Catalog=Blog;Integrated Security=True");
            string body = db.GetBodyById(id);
            string result = "";
            for (int x = 0; x <= 200 && x < body.Length; x++)
            {
                result += body[x];
            }
            return result;
        }
    }
}
