using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MyShop.Models.Entities;

namespace MyShop.Models
{
    public static class RegionManager
    {
        private static HashSet<Region> regions = new HashSet<Region>();
        private static HashSet<string> cultures = new HashSet<string>();

        public static IEnumerable<Region> EnumerateRegions()
        {
            return regions;
        }

        public static bool IsCultureSupported(string culture)
        {
            return cultures.Contains(culture);
        }

        public static Region FindRegionByName(string name)
        {
            return regions.FirstOrDefault(t => t.Name == name);
        }

        public static Region FindRegionByCulture(string culture)
        {
            return regions.FirstOrDefault(t => t.Culture == culture);
        }

        public static Region GetDefaultRegion()
        {
            return FindRegionByName("ru");
        }

        public static void AddCulture(string culture)
        {
            cultures.Add(culture);
        }

        public static void AddRegion(Region region)
        {
            regions.Add(region);
        }
    }
}