﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        // GET: Post/Details/5
        [HttpGet("{url}")]
        public async Task<IActionResult> Details(string url)
        {
            if (string.IsNullOrEmpty(url))
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
                                Url = p.Url,
                                LastModificationTime = p.LastModificationTime == null
                               ? p.CreationTime : (DateTime)p.LastModificationTime,
                                Category = new CategoryModel
                                {
                                    Id = c.Id,
                                    Name = c.Name
                                }
                            };

            var postModel = await postQuery.Where(x => x.Url.Contains(url))
                .FirstOrDefaultAsync();


            if (postModel == null)
            {
                return NotFound();
            }

            ViewBag.Title = postModel.Title;
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

            var posts = await postsQuery.OrderByDescending(x => x.LastModificationTime).Where(x => x.CategoryId == id)
                .ToPagedListAsync<PostModel>((int)currentPage, 6);

            ViewBag.Title = posts.Items.FirstOrDefault() == null ?  posts.Items.FirstOrDefault()?.Title 
                : "Technology - Marketing - Everyday";

            return View(posts);
        }
    }
}