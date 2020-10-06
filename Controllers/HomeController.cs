using BlogCore.Data;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using System;
using BlogCore.Extension;

namespace BlogCore.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlogCoreContext _context;

        public HomeController(ILogger<HomeController> logger, BlogCoreContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("")]
        public async Task<IActionResult> Index(int? currentPage)
        {
            ViewBag.Title = "Technology - Marketing - Everyday";

            if (currentPage == null || currentPage == 0)
                currentPage = 1;

            var postsQuery = from p in _context.Posts
                             join c in _context.Categories
                                  on p.CategoryId equals c.Id
                             select new PostModel
                             {
                                 Id = p.Id,
                                 Url = p.Url,
                                 Title = p.Title,
                                 Content = p.Content,
                                 CategoryId = p.CategoryId,
                                 Image = p.Image,
                                 Summary = p.Summary,
                                 LastModificationTime = p.LastModificationTime == null
                                ? p.CreationTime : (DateTime)p.LastModificationTime,
                                 Category = new CategoryModel
                                 {
                                     Id = c.Id,
                                     Name = c.Name
                                 }
                             };

            var posts = await postsQuery.OrderByDescending(x => x.LastModificationTime).ToPagedListAsync<PostModel>((int)currentPage, 6);
            return View(posts);
        }

        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}