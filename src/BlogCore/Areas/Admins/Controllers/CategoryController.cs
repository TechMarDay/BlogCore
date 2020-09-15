using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogCore.Data;
using BlogCore.Models;
using System;
using BlogCore.Entities;
using BlogCore.Extension;

namespace BlogCore.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class CategoryController : Controller
    {
        private readonly BlogCoreContext _context;

        public CategoryController(BlogCoreContext context)
        {
            _context = context;
        }

        // GET: Admins/Category
        public async Task<IActionResult> Index(int? currentPage)
        {
            if (currentPage == null || currentPage == 0)
                currentPage = 1;

            var categoriesQuery = _context.Categories.Select(x => new CategoryModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Image = x.Image,
                CategoryTypeDisplay = x.CategoryType.ToString(),
                LastModificationTime = x.LastModificationTime == null ? x.CreationTime : (DateTime)x.LastModificationTime
            });

            var categories = await categoriesQuery.ToPagedListAsync<CategoryModel>((int)currentPage, 5);
            return View(categories);
        }

        // GET: Admins/Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryModel = await _context.Categories
                .Where(m => m.Id == id)
                .Select(x => new CategoryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Image = x.Image,
                    LastModificationTime = x.LastModificationTime == null ? x.CreationTime : (DateTime)x.LastModificationTime
                })
                .FirstOrDefaultAsync();
            if (categoryModel == null)
            {
                return NotFound();
            }

            return View(categoryModel);
        }

        // GET: Admins/Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admins/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Image, CategoryType, LastModificationTime")] CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = categoryModel.Name,
                    Description = categoryModel.Description,
                    Image = categoryModel.Image,
                    CreationTime = DateTime.UtcNow,
                    CategoryType = categoryModel.CategoryType
                };

                await _context.AddAsync(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryModel);
        }

        // GET: Admins/Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryModel = await _context.Categories.Where(x => x.Id == id)
                .Select(x => new CategoryModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Image = x.Image,
                    Name = x.Name,
                    CategoryType = x.CategoryType
                })
                .FirstOrDefaultAsync();
            if (categoryModel == null)
            {
                return NotFound();
            }
            return View(categoryModel);
        }

        // POST: Admins/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Image, CategoryType, LastModificationTime")] CategoryModel categoryModel)
        {
            if (id != categoryModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var category = await _context.Categories.Where(x => x.Id == id)
                        .FirstOrDefaultAsync();
                    category.Name = categoryModel.Name;
                    category.Description = categoryModel.Description;
                    category.Image = categoryModel.Image;
                    category.LastModificationTime = DateTime.UtcNow;
                    category.CategoryType = categoryModel.CategoryType;

                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryModelExists(categoryModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoryModel);
        }

        // GET: Admins/Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryModel = await _context.Categories
                .Select(x => new CategoryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Image = x.Image,
                    LastModificationTime = x.LastModificationTime == null ? x.CreationTime : (DateTime)x.LastModificationTime
                })
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            return View(categoryModel);
        }

        // POST: Admins/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryModel = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(categoryModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryModelExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
