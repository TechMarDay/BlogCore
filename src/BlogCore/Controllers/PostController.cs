using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogCore.Data;
using BlogCore.Models;
using BlogCore.Extension;

namespace BlogCore.Controllers
{
    public class PostController : Controller
    {
        private readonly BlogCoreContext _context;

        public PostController(BlogCoreContext context)
        {
            _context = context;
        }

        //// GET: Post
        //public async Task<IActionResult> Index()
        //{
        //    var blogCoreContext = _context.PostModel.Include(p => p.Category);
        //    return View(await blogCoreContext.ToListAsync());
        //}

        // GET: Post/Details/5
        [HttpGet("bai-viet/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postQuery = from p in _context.Posts
                            join c in _context.Categories
                                 on p.CategoryId equals c.Id
                            select new PostModel
                            {
                                Id = p.Id,
                                Title = p.Title,
                                Content = p.Content,
                                CategoryId = p.CategoryId,
                                Image = p.Image,
                                LastModificationTime = p.LastModificationTime == null
                               ? p.CreationTime : (DateTime)p.LastModificationTime,
                                Category = new CategoryModel
                                {
                                    Id = c.Id,
                                    Name = c.Name
                                }
                            };

            var postModel = await postQuery.Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (postModel == null)
            {
                return NotFound();
            }

            return View(postModel);
        }

        [HttpGet("danh-muc/{id}")]
        public async Task<IActionResult> PostByCategory(int id, int? currentPage)
        {
            if (currentPage == null || currentPage == 0)
                currentPage = 1;

            var postsQuery = from p in _context.Posts
                             join c in _context.Categories
                                  on p.CategoryId equals c.Id
                             select new PostModel
                             {
                                 Id = p.Id,
                                 Title = p.Title,
                                 Content = p.Content,
                                 CategoryId = p.CategoryId,
                                 Image = p.Image,
                                 LastModificationTime = p.LastModificationTime == null
                                ? p.CreationTime : (DateTime)p.LastModificationTime,
                                 Category = new CategoryModel
                                 {
                                     Id = c.Id,
                                     Name = c.Name
                                 }
                             };

            var posts = await postsQuery.Where(x => x.CategoryId == id)
                .ToPagedListAsync<PostModel>((int)currentPage, 6);
            return View(posts);
        }

        //// GET: Post/Create
        //public IActionResult Create()
        //{
        //    ViewData["CategoryId"] = new SelectList(_context.Set<CategoryModel>(), "Id", "Id");
        //    return View();
        //}

        //// POST: Post/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Title,Content,CategoryId,Image,LastModificationTime")] PostModel postModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(postModel);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CategoryId"] = new SelectList(_context.Set<CategoryModel>(), "Id", "Id", postModel.CategoryId);
        //    return View(postModel);
        //}

        //// GET: Post/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var postModel = await _context.PostModel.FindAsync(id);
        //    if (postModel == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CategoryId"] = new SelectList(_context.Set<CategoryModel>(), "Id", "Id", postModel.CategoryId);
        //    return View(postModel);
        //}

        //// POST: Post/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,CategoryId,Image,LastModificationTime")] PostModel postModel)
        //{
        //    if (id != postModel.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(postModel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PostModelExists(postModel.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CategoryId"] = new SelectList(_context.Set<CategoryModel>(), "Id", "Id", postModel.CategoryId);
        //    return View(postModel);
        //}

        //// GET: Post/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var postModel = await _context.PostModel
        //        .Include(p => p.Category)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (postModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(postModel);
        //}

        //// POST: Post/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var postModel = await _context.PostModel.FindAsync(id);
        //    _context.PostModel.Remove(postModel);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool PostModelExists(int id)
        //{
        //    return _context.PostModel.Any(e => e.Id == id);
        //}
    }
}
