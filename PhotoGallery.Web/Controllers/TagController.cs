using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoGallery.Core;
using System.Web.WebPages2;

namespace PhotoGallery.Web.Controllers
{
    public class TagController : Controller
    {
        //
        // GET: /Tag/

        public ActionResult Index()
        {
            var tags = new List<Tag>()
            {
                new Tag()
                {
                    TagName="测试",
                    TagCount = 1
                }
            };

            return View(tags);
        }

        public ActionResult Thumbnail(string name)
        {

            // 暂时缓存图像
            Response.OutputCache(60);

            return File("~/Images/gallery-empty.png", "image/png");
        }

    }
}
