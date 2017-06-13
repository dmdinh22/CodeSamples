using Dinh.Mvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dinh.Mvc.Controllers
{
    public class BlogsPostController : BaseController
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        // www.website.com/BlogsPost/create
        // GET: BlogsPost/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogsPost/Create
        // <form action="/Blogs/Create" method="post">
        [HttpPost]
        public ActionResult Create(BlogCreateViewModel model)
        {
            if (ModelState.IsValid)
            {

            }

            // this happens, when the model is INVALID
            return View(model);
        }

        // GET:BlogsPost/Blog/{id}
        // www.myworld.com/BlogsPost/Blog/1
        public ActionResult Blog(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            BlogViewModel model = new BlogViewModel();
            model.ID = id.Value;
            model.Title = "The Best Blog Post Title";
            model.Email = "dmdinh22@mailinator.com;
            model.Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed eu sapien scelerisque, dictum nisi vehicula, tincidunt nunc. Quisque ut lacinia ipsum, ut fermentum nisl. Donec et volutpat lectus. Cras tincidunt imperdiet tincidunt. Mauris consequat vestibulum euismod. Phasellus velit mauris, laoreet ut lacus sagittis, ultricies dapibus arcu. Interdum et malesuada fames ac ante ipsum primis in faucibus. Donec nulla risus, imperdiet ut urna pulvinar, cursus dignissim turpis. Curabitur cursus vehicula facilisis. Donec odio enim, blandit id cursus vel, pretium et lectus. Curabitur id eros rhoncus, bibendum sem et, ultricies magna. Suspendisse placerat semper pellentesque. Aenean varius molestie elit, at elementum neque rutrum luctus. Mauris at nisi in neque aliquam pulvinar. Aenean eu aliquet nisl.";

            model.Comments = new List<BlogCommentViewModel>();
            model.Comments.Add(new BlogCommentViewModel()
            {
                ID = 1,
                Title = "Comment title",
                UserName = "admin",
                DateCreated = DateTime.Now,
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit."
            });
        

            return View(model);
         }
    }
}
