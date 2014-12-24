using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MyShop.Models.DAL.Interfaces;

namespace MyShop.Models.DAL
{
    public class Dao
    {
        public ISlides SlidesDAO { get; set; }
        public IMenus MenusDAO { get; set; }
        public IItems ItemsDAO { get; set; }
        public IAccounts AccountsDAO { get; set; }
        public ICart CartDAO { get; set; }
    }
}