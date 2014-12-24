using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;
using MyShop.Models.Entities;
using MyShop.Models;

namespace MyShop
{
    public static class GlobalizationConfig
    {

        public static void SetDefaultRegions()
        {

            RegionManager.AddRegion(new Region { Name = "ru", Language = "Русский", Culture = "ru-RU", ImageFile = "it.png" });
            RegionManager.AddRegion(new Region { Name = "eu", Language = "English", Culture = "en-GB", ImageFile = "english.png" });
            RegionManager.AddRegion(new Region { Name = "it", Language = "Italian", Culture = "it-IT", ImageFile = "it.png" });
            RegionManager.AddRegion(new Region { Name = "fr", Language = "France", Culture = "fr-FR", ImageFile = "fr.png" });

            RegionManager.AddCulture("ru-RU");
            RegionManager.AddCulture("en-GB");
            RegionManager.AddCulture("fr-FR");
            RegionManager.AddCulture("it-IT");
        }



    }


}