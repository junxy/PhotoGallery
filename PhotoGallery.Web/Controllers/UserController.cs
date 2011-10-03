using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoGallery.Web.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        //[Authorize]
        public ActionResult Index()
        {
            var user = new PhotoGallery.Core.User() { DisplayName = "test" };
            return View(user);
        }

        public ActionResult View(int id)
        {
            return View();
        }

    }
}
