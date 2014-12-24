using MyShop.Models;
using MyShop.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Controllers
{
    public class CartController : MyShopController
    {
        
        [HttpPost]
        public JsonResult AddItem(AddToCartRequest model)
        {
            var result = new AddToCartResponse() { Success = false, Message = "" };
            
            if (ModelState.IsValid && CartManager.AddItemToCart(model.ItemId)) {
                result.Success = true;
                var item = ItemManager.GetItemById(model.ItemId);
                result.Message = item.Name + " was added to your shopping cart.";
            }

            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult RemoveItem(int cartItemId)
        {
            bool success = false;
            if (CartManager.RemoveItemFromCart(cartItemId))
            {
                success = true;
            }

            return Json(new { success }, JsonRequestBehavior.DenyGet);
        }
        

        public ActionResult GetCart()
        {
            var cart = CartManager.GetCart();
            return (cart == null) ? PartialView("EmptyCartPartial") : PartialView("GetCartPartial", cart);
        }

        public ActionResult Index()
        {
            var list = CartManager.GetCart();
            if (list == null)
            {
                return View("EmptyCart");
            }
            else
            {
                return View("Index", list);
            }
        }
    }
}