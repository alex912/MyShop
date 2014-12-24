using MyShop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models.DAL.Interfaces
{
    public interface ICart
    {
        IEnumerable<CartItem> GetAllCartItems();
        void InsertCartItem(CartItem item);
        void DeleteCartItemById(int id);
    }
}