using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication29.Models;

namespace WebApplication29.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult NewPost()
        {
            return View();
        }
    }
}
