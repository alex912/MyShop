using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MyShop.Models.Entities;
using System.Web.Mvc;

namespace MyShop.Models
{
    public static class MenuManager
    {
        private static Menu rootMenu;
        private static List<Menu> allMenus;

        public static Menu RootMenu
        {
            get { 
                if (rootMenu == null) {
                    UpdateCache();
                }

                return rootMenu;
            }
            private set 
            {
                rootMenu = value;
            }
        }

        public static void UpdateCache()
        {
            allMenus = new List<Menu>();

            rootMenu = new Menu() { MenuId = 0, Level = -1, Type = MenuType.TYPE_ROOT, IsActive = true };
            BuildSubmenus(rootMenu, rootMenu.Level + 1);
        }

        private static void BuildSubmenus(Menu menu, int level)
        {
            allMenus.Add(menu);
            List<Menu> childs = MyShopConfig.Dao.MenusDAO.GetAllMenusByParentId(menu.MenuId).ToList();

            menu.Childs = childs;
            
            foreach (var child in childs)
            {
                child.Region = RegionManager.FindRegionByName(child.Region.Name);
                child.Parent = menu;
                child.Level = level;
                child.Items = MyShopConfig.Dao.ItemsDAO.GetAllItemsByCatalogId(child.MenuId).ToList();
                foreach(var item in child.Items)
                {
                    item.Catalog = child;
                    item.Region = RegionManager.FindRegionByName(item.Region.Name);
                }
                BuildSubmenus(child, level+1);
            }
        }

        public static IEnumerable<Menu> GetAllMenus()
        {
            return allMenus;
        }

        public static string GenerateUrl(this UrlHelper url, Item item)
        { 
            return url.GenerateUrl(item.Catalog) + item.Path + ".html";
        }

        public static string GenerateUrl(this UrlHelper url, Menu menu)
        {
            var path = url.RequestContext.HttpContext.Request.ApplicationPath;
            var menupath = "";
            var currentMenu = menu;

            while(currentMenu != MenuManager.RootMenu)
            {
                menupath = currentMenu.Path + '/' + menupath;
                currentMenu = currentMenu.Parent;
            }

            return path + menupath;
        }

    }
}