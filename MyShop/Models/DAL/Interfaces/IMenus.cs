using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MyShop.Models.Entities;

namespace MyShop.Models.DAL.Interfaces
{
    public interface IMenus
    {
        IEnumerable<Menu> GetAllMenusByParentId(int parentid);
    }
}