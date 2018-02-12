using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace JrBlog.Controllers
{
    public class PostController : Controller
    {
        private readonly IpostRepository _postRepo;
        private readonly ILogger<PostController> _log;

        public PostController(IpostRepository postRepo, ILogger<PostController> log)
        {
            _postRepo = postRepo;
            _log = log;
        }
        // GET: Post
        public ActionResult Index()
        {
            return View(_postRepo.PostList());
        }

        // GET: Post/Details/5
        public ActionResult Details(int id)
        {
            return View(_postRepo.GetById(id));
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            return View(new Post());
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post newPost)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _postRepo.Add(newPost);

                    return RedirectToAction(nameof(Index));

                }
            }
            catch(Exception ex)
            {
                //return View();
                _log.LogError(ex, "Error creating post.");
            }
            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            return View();
        }


        // GET: Post/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_postRepo.GetById(id));
        }

        // POST: Post/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Post editedPost)
        {
            try
            {
                _postRepo.Edit(editedPost);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception)
            {
               // TODO Log the Exception
            }
            return View(editedPost);
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_postRepo.GetById(id));
        }

        // POST: Post/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Post PostToDelete)
        {
            try
            {
                _postRepo.Delete(PostToDelete);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception)
            {
                // TODO Log the Exception
            }
            return View(PostToDelete);
        }
        public ActionResult Resources()
        {
            return View();
        }
    }
}