using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MyShop.Models.Entities;
using MyShop.Models;

namespace MyShop.Controllers
{
    public class CatalogController : MyShopController
    {
        // GET: Catalog
        public ActionResult Index(string itempath)
        {
            var listmode = (string)Request.QueryString["viewmode"];

            if (listmode == null) {
                listmode = Request.Cookies["viewmode"] != null ? Request.Cookies["viewmode"].Value : "grid";
            }

            if (listmode != "grid" && listmode != "list")
            {
                listmode = "grid";
            }

            Response.Cookies.Add(new HttpCookie("viewmode", listmode));
            ViewBag.viewmode = listmode;

            itempath = "/catalog/" + itempath;

            var path = itempath.Split(new char[]{'/'}, StringSplitOptions.RemoveEmptyEntries);
            var currentElement = MenuManager.RootMenu;
            var prevElement = default(Menu); // null

            for (int i = 0; i < path.Length ; i++ )
            {
                prevElement = currentElement;
                currentElement = currentElement.GetChildByPath(path[i], MyShopController.Region);
                if (currentElement == null)
                {
                    if (i == path.Length - 1 && path[i].EndsWith(".html"))
                    {
                        Item item = prevElement.GetItemByPath(path[i].Substring(0, path[i].IndexOf(".html")), MyShopController.Region);
                        if (item != null)
                        {
                            return View("Item", item);
                        }
                    }
                    return View("NotFound");
                }
                
            }

            return (currentElement.Items.Count > 0 || currentElement.Childs.Count > 0) ? View("List", currentElement) : View("EmptyMenu", currentElement);
        }


    }
}