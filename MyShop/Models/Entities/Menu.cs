using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models.Entities
{
    public class Menu
    {
        public int MenuId { get; set; }
        public Menu Parent { get; set; }
        public List<Menu> Childs { get; set; }
        public int Level { get; set; }

        public string Name { get; set; }
        public string Desc { get; set; }
        public int Priority { get; set; }
        public MenuType Type { get; set; }
        public Region Region { get; set; }
        public string ImageName { get; set; }
        public string Url { get; set; }
        public bool IsNew { get; set; }
        public int Side { get; set; }

        public List<Item> Items { get; set; }
        public bool IsHot { get; set; }
        public bool IsActive { get; set; }
        public bool HasChilds()
        {
            return Childs != null && Childs.Count > 0;
        }
        public string Path { get; set; }

        public Menu GetChildByPath(string path, Region region)
        {
            foreach(var m in Childs)
            {
                if (m.Path == path && m.Region == region)
                    return m;
            }

            return null;
        }

        public Item GetItemByPath(string path, Region region)
        {
            foreach(var i in Items)
            {
                if (i.Path == path && i.Region == region)
                    return i;
            }

            return null;
        }
    }

    public enum MenuType
    {
        TYPE_ROOT      = -1,
        TYPE_HOME      = 0,
        TYPE_MEGAMENU1 = 1,
        TYPE_MEGAMENU2 = 2,
        TYPE_MEGAMENU3 = 3,
        TYPE_MEGAMENU4 = 4,
        TYPE_SUBMENU   = 5,
        TYPE_URL       = 6,
    }

}