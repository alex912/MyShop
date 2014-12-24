using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MyShop.Models;

namespace MyShop.Controllers
{
    public class ServiceController : MyShopController
    {
        // GET: Service
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Region(string region)
        {
            if (!string.IsNullOrEmpty(region))
            {
                if (RegionManager.FindRegionByName(region) != null)
                {
                    Response.Cookies.Add(new HttpCookie("region", region));
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult RebuildSlides()
        {
            SlideManager.RebuildCache();
            return RedirectToAction("Index", "Home");
        }

    }
}