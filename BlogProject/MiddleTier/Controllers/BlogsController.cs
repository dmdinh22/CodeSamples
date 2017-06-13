using Dinh.Mvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Dinh.Mvc.Controllers
{
    public class BlogsController : BaseController
    {
        // GET: Blogs
        public ActionResult Index()
        {
            return View();
        }

        // GET: SinglePage
        public ActionResult Legacy()
        {
            return View();
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Blog/{id}
        public ActionResult Blog([FromUri] int id)
        {
            // Created our view model
            ItemViewModel<int> model = new ItemViewModel<int>();

            //Hydrate our view model
            model.Item = id;

            return View(model);
        }
    }
}