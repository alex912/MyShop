using MyShop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace MyShop.Models
{
    public class CartManager
    {
        public static List<CartItem> Cart
        {
            get
            {
                var cache = HttpContext.Current.Cache.Get("CartCache") as List<CartItem>;
                if (cache != null)
                {
                    return cache;
                }

                UpdateCache();

                cache = HttpContext.Current.Cache.Get("CartCache") as List<CartItem>;
                return cache;
            }
        }


        public static void UpdateCache()
        {
            if (HttpContext.Current.Cache.Get("CartCache") != null)
            {
                HttpContext.Current.Cache.Remove("CartCache");
            }

            var cache = MyShop.MyShopConfig.Dao.CartDAO.GetAllCartItems().ToList();
            foreach(var c in cache)
            {
                c.Account = AccountManager.Accounts.FirstOrDefault(a => a.UserId == c.Account.UserId);
                c.Item = ItemManager.GetItemById(c.Item.Id);
            }
            HttpContext.Current.Cache.Add("CartCache", cache, null, Cache.NoAbsoluteExpiration, new TimeSpan(24, 0, 0), CacheItemPriority.Default, null);
        }

        public void AddItemToCart(Item item)
        {
            if (!AccountManager.IsLoggedInAsUser())
            {
                AccountManager.LoginAsGuest();
            }

            var cartItem = new CartItem();
            cartItem.Item = item;
            cartItem.Account = HttpContext.Current.Profile["Account"] as Account;
            cartItem.Date = DateTime.Now;

            MyShop.MyShopConfig.Dao.CartDAO.InsertCartItem(cartItem);
            UpdateCache();
        }

        public bool DeleteItemFromCart(int cartItemId)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                return false;

            var cartItem = Cart.FirstOrDefault(c => c.Id == cartItemId);
            if (cartItem == null)
                return false;

            var account = HttpContext.Current.Profile["Account"] as Account;
            if (cartItem.Account.UserId != account.UserId)
                return false;

            MyShopConfig.Dao.CartDAO.DeleteCartItemById(cartItemId);
            UpdateCache();
            return true;
        }

    }
}