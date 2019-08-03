using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Final4.Models;
using Final4.Data;
using Microsoft.AspNetCore.Authorization;

namespace Final4.Controllers
{

    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext context_;

        public CommentsController(ApplicationDbContext context)
        {
            context_ = context;

        }

        [HttpGet]
        public ActionResult CommentView()
        {
            if (User.IsInRole("Admin"))
            {
                return View(context_.comments.ToList<Comment>());
            }

            var commentList = context_.comments.Where(x => x.CommenterName == User.Identity.Name);
            return View(commentList);
        }



        [HttpGet]
        [Authorize(Roles = "Admin,User")] 
        public IActionResult AddComment(int id)
        {
            var model = new Comment();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public IActionResult AddComment(int id, Comment comment)
        {
            comment.CommenterName = User.Identity.Name;
            context_.comments.Add(comment);
            context_.SaveChanges();
            return RedirectToAction("CommentView");
        }



        //Delete Comment
        [Authorize(Roles = "Admin,User")]
        public IActionResult DeleteComment(int? id)
        {
            Comment comment = context_.comments.Find(id);
            context_.Entry(comment).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context_.SaveChanges();
            return RedirectToAction("CommentView");
        }




    }
}