using MVC_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MVC_Blog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            var db = new ApplicationDbContext();
            var post = db.Posts.OrderByDescending(p => p.Date).Take(db.Posts.Count());

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                post = post.Where(s => s.Title.Contains(searchString)
                                       || s.Body.Contains(searchString));
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(post.ToPagedList(pageNumber, pageSize));

            //return View(post.ToList());
        }
    }
}