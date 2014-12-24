using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MyShop.Models.Entities;
using MyShop.Models.DAL;
using MyShop.Controllers;

namespace MyShop.Models
{
    public static class SlideManager
    {
        private static List<Slide> slidesCache;

        public static IEnumerable<Slide> GetAllSlidesByRegion()
        {
            return GetAllSlidesByRegion(MyShopController.Region);
        }

        public static IEnumerable<Slide> GetAllSlidesByRegion(string region)
        {
            Region reg = RegionManager.FindRegionByName(region);
            if (reg != null)
                return GetAllSlidesByRegion(reg);
            else 
                throw new Exception("Wrong region " + region);
        }

        public static IEnumerable<Slide> GetAllSlidesByRegion(Region region)
        {
            if (slidesCache == null)
                RebuildCache();

            return slidesCache.Where(t => t.Region == region);
        }

        public static void RebuildCache()
        {
            slidesCache = MyShopConfig.Dao.SlidesDAO.GetAllSlides().ToList();
            foreach(var s in slidesCache)
            {
                s.Region = RegionManager.FindRegionByName(s.Region.Name);
            }
        }
    }
}