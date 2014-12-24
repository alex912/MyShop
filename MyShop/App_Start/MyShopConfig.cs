using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MyShop.Models.DAL;
using MyShop.Models.DAL.Mssql;

namespace MyShop
{
    public static class MyShopConfig
    {
        public readonly static string DataConnection = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public readonly static string SlidesFolder = "Slideshow";
        public readonly static Dao Dao = new Dao(); 
        static MyShopConfig()
        {
            if (ConfigurationManager.AppSettings["DbDriver"] == "mssql")
            {
                Dao.SlidesDAO = new MssqlSlides();
                Dao.MenusDAO = new MssqlMenus();
                Dao.ItemsDAO = new MssqlItems();
                Dao.AccountsDAO = new MssqlAccounts();
                Dao.CartDAO = new MssqlCart();
            }
             
        }
    }
}