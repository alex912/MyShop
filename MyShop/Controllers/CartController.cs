using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult AddItem(int id)
        {
            var item = ItemManager.GetItemById(id);
            if (item != null)
            {
                return PartialView("ItemAdded", item);
            }
            return new EmptyResult();
        }
    }
}