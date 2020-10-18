using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication29.Models;

namespace WebApplication29.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=Blog;Integrated Security=True";

        private bool _firstComment = true;

        public IActionResult Index()
        {
            BlogPostViewModel vm = new BlogPostViewModel();
            BlogPostDb db = new BlogPostDb(_connectionString);
            vm.Posts = db.Get5LatestBlogPosts();
            return View(vm);
        }    
        
        public IActionResult ViewBlogPost(int id)
        {
            BlogPostDb db = new BlogPostDb(_connectionString);

            CommentViewModel cvm = new CommentViewModel();            
            cvm.Post = db.GetBlogPostById(id);

            string value = Request.Cookies["Commenter"];
            _firstComment = String.IsNullOrEmpty(value);

            if (!_firstComment)
            {
                cvm.CommenterName = value;
            }

            return View(cvm);
        }

        [HttpPost]
        public IActionResult AddComment(Comment comment)
        {
            BlogPostDb db = new BlogPostDb(_connectionString);
            db.AddComment(comment);
                        
            if (_firstComment)
            {
                Response.Cookies.Append("Commenter", comment.Name);
            }

            return Redirect($"/home/viewblogpost?id={comment.BlogPostId}");
        }

        [HttpPost]
        public IActionResult AddBlogPost(BlogPost post)
        {
            BlogPostDb db = new BlogPostDb(_connectionString);
            int id = db.AddBlogPost(post);
            return Redirect($"/home/viewblogpost?id={id}");
        }
    }
}
