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
            var list = MyShopConfig.Dao.ItemsDAO.GetAllItems().ToList();
            
            foreach(var item in list)
            {
                item.Catalog = MenuManager.GetAllMenus().FirstOrDefault(m => m.MenuId == item.Catalog.MenuId);
                item.Region = RegionManager.FindRegionByName(item.Region.Name);
            }
            return list;
        }

        public static Item GetItemById(int id)
        {
            return GetAllItems().ToList().FirstOrDefault(t => t.Id == id);
        }
    }
}