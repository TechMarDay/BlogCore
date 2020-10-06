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
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "Admin")]
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
                                Url = p.Url,
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
        [Authorize(Roles = "Admin")]
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
                                Url = p.Url,
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id, Title, Url, Content,CategoryId,Summary, Image,LastModificationTime")] PostModel postModel)
        {
            if (ModelState.IsValid)
            {
                //Check Url exsitng
                var isUrlExisting = await _context.Posts.AnyAsync(x => x.Url == postModel.Url);
                if (isUrlExisting)
                    throw new Exception("Url đã tồn tại");

                var post = new Post
                {
                    Title = postModel.Title,
                    Url = postModel.Url,
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Title, Url, Content,CategoryId,Image,Summary, LastModificationTime")] PostModel postModel)
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

                    //Check Url exsitng
                    var isUrlExisting = await _context.Posts.AnyAsync(x => x.Url == postModel.Url 
                    && postModel.Url != post.Url);
                    if (isUrlExisting)
                        throw new Exception("Url đã tồn tại");

                    post.Title = postModel.Title;
                    post.Url = postModel.Url;
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
