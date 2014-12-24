using MyShop.Models;
using MyShop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : MyShopController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddItem()
        {
            return View();
        }

        public ActionResult ListItems()
        {
            return View(ItemManager.GetAllItems());
        }

        [HttpPost]
        public ActionResult AddItem(Item item)
        {
            return View(item);
        }

        public ActionResult EditItem(int id)
        {
            return View(ItemManager.GetItemById(id));
        }

        public ActionResult EditItem(Item item)
        {
            return View(item);
        }

        
        public ActionResult RebuildCache()
        {
            MenuManager.UpdateCache();
            AccountManager.UpdateCache();
            return RedirectToAction("Index", "Home");
        }
    }
}