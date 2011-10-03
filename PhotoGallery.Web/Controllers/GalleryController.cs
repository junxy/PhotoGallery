using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoGallery.Core;

namespace PhotoGallery.Web.Controllers
{
    public class GalleryController : Controller
    {
        //
        // GET: /Gallery/

        public ActionResult Index()
        {
            var galleries = new List<Gallery>() { 
                new Gallery()
                {
                    Id=1,
                    Name="我",
                    PhotoCount = 0
                }
            };

            return View(galleries);
        }

        public ActionResult View(int id)
        {
            return View();
        }

        
        public ActionResult Thumbnail(int id)
        {
            return File("~/Images/gallery-empty.png", "image/png");
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Gallery gallery)
        {
            if (ModelState.IsValid)
            {
                return Content("ok");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Upload()
        {
            return Content("ok");
        }

    }
}
