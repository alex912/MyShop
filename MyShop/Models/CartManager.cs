using MyShop.Controllers;
using MyShop.Models.DAL;
using MyShop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace MyShop.Models
{
    public static class CartManager
    {

        public static bool AddItemToCart(int itemid)
        {
            var item = ItemManager.GetItemById(itemid);
            if (item != null)
            {
                CartManager.AddItemToCart(item);
                return true;
            }
            return false;
        }

        public static void AddItemToCart(Item item)
        {
            var account = default(Account);

            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                account = AccountManager.LoginAsGuest();
            }
            else
            {
                account = HttpContext.Current.Profile["Account"] as Account;
            }

            var cartItem = new CartItem();
            cartItem.Item = item;
            cartItem.Account = account;
            cartItem.Date = DateTime.Now;
            cartItem.Region = item.Region;

            MyShop.MyShopConfig.Dao.CartDAO.InsertCartItem(cartItem);
        }

        public static bool RemoveItemFromCart(int cartItemId)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                return false;

            var account = HttpContext.Current.Profile["Account"] as Account;
            var cart = MyShopConfig.Dao.CartDAO.GetCartItemsByUserId(account.UserId, MyShopController.Region).ToList();

            var cartItem = cart.FirstOrDefault(c => c.Id == cartItemId);
            if (cartItem == null)
                return false;

            
            if (cartItem.Account.UserId != account.UserId)
                return false;

            MyShopConfig.Dao.CartDAO.DeleteCartItemById(cartItemId);
            return true;
        }


        public static IEnumerable<CartItem> GetCart()
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                return null;

            var account = HttpContext.Current.Profile["Account"] as Account;
            var cart = MyShopConfig.Dao.CartDAO.GetCartItemsByUserId(account.UserId, MyShopController.Region).ToList();

            if (cart.Count == 0)
                return null;

            foreach(var c in cart)
            {
                c.Account = account;
                c.Item = ItemManager.GetItemById(c.Item.Id);
                c.Region = RegionManager.FindRegionByName(c.Region.Name);
            }

            return cart;
        }

        public static string GetTotalPrice(this HtmlHelper helper, IEnumerable<CartItem> cartItems)
        {
            var total = 0.0;
            foreach (var i in cartItems)
            {
                total += i.Item.Price;
            }
            return MyShopController.Region.Price(total);
        } 

    }
}