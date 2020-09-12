using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogCore.Data;
using BlogCore.Models;

namespace BlogCore.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class UsersController : Controller
    {
        private readonly BlogCoreContext _context;

        public UsersController(BlogCoreContext context)
        {
            _context = context;
        }

        // GET: Admins/Users
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
