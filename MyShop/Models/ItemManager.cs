using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MyShop.Models.Entities;

namespace MyShop.Models
{
    public static class ItemManager
    {
        public static IEnumerable<Item> GetAllItems()
        {

            return MyShopConfig.Dao.ItemsDAO.GetAllItems().ToList();
        }

        public static Item GetItemById(int id)
        {
            return GetAllItems().ToList().FirstOrDefault(t => t.Id == id);
        }
    }
}