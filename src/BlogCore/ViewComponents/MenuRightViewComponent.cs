using BlogCore.Data;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.ViewComponents
{
    public class MenuRightViewComponent : ViewComponent
    {
        private readonly BlogCoreContext _context;

        public MenuRightViewComponent(BlogCoreContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.CategoriesPrograming = await _context.Categories
                .Where(x => x.CategoryType == CategoryTypeEnum.Type.Programming)
                .Select(x => new CategoryModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

            ViewBag.CategoriesMarketing = await _context.Categories
                .Where(x => x.CategoryType == CategoryTypeEnum.Type.Marketing)
                .Select(x => new CategoryModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

            return View();
        }

    }
}
