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
using BlogCore.Entities;

namespace BlogCore.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class PostController : Controller
    {
        private readonly BlogCoreContext _context;

        public PostController(BlogCoreContext context)
        {
            _context = context;
        }

        // GET: Admins/Post
        public async Task<IActionResult> Index(int? currentPage)
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
                                Summary = p.Summary,
                                LastModificationTime = p.LastModificationTime == null 
                                ? p.CreationTime : (DateTime)p.LastModificationTime,
                                Category = new CategoryModel
                                {
                                    Id = c.Id,
                                    Name = c.Name
                                }
                            };

            var posts = await postsQuery.ToPagedListAsync<PostModel>((int)currentPage, 5);
            return View(posts);
        }

        // GET: Admins/Post/Details/5
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
                                Summary = p.Summary,
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

        // GET: Admins/Post/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.Select(x => new CategoryModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return View();
        }

        // POST: Admins/Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,CategoryId,Summary, Image,LastModificationTime")] PostModel postModel)
        {
            if (ModelState.IsValid)
            {
                var post = new Post
                {
                    Title = postModel.Title,
                    Content = postModel.Content,
                    Image = postModel.Image,
                    CategoryId = postModel.CategoryId,
                    Summary = postModel.Summary,
                    CreationTime = DateTime.UtcNow
                };
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(postModel);
        }

        // GET: Admins/Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
                                Summary = p.Summary,
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
            ViewBag.Categories = await _context.Categories.Select(x => new CategoryModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return View(postModel);
        }

        // POST: Admins/Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,CategoryId,Image,Summary, LastModificationTime")] PostModel postModel)
        {
            if (id != postModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var post = await _context.Posts.Where(x => x.Id == id)
                        .FirstOrDefaultAsync();

                    post.Title = postModel.Title;
                    post.Content = postModel.Content;
                    post.Image = postModel.Image;
                    post.Summary = postModel.Summary;
                    post.CategoryId = postModel.CategoryId;
                    post.LastModificationTime = DateTime.UtcNow;

                    _context.Update(post);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostModelExists(postModel.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Set<CategoryModel>(), "Id", "Id", postModel.CategoryId);
            return View(postModel);
        }

        // GET: Admins/Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
                                Summary = p.Summary,
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

        // POST: Admins/Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postModel = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(postModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostModelExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
