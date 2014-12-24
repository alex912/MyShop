using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Resources;

using MyShop.Models.Entities;
using MyShop.Models;

namespace MyShop.Controllers
{
    public class MyShopController : Controller
    {
        public static Region Region { get; set; }
        public static string Culture { get; set; }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            SetRegion();
            SetCulture();

            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Culture);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Region.Culture);

            return base.BeginExecuteCore(callback, state);
        }

        private void SetRegion()
        {
            var region = Request.Cookies["region"] != null ? Request.Cookies["region"].Value : null;
            if (region != null)
            {
                var testRegion = RegionManager.FindRegionByName(region);
                if (testRegion != null)
                {
                    Region = testRegion;
                    return;
                }
            }

            var culture = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null;
            if (culture == null || !RegionManager.IsCultureSupported(culture))
            {
                var defaultRegion = RegionManager.GetDefaultRegion();
                Region = defaultRegion;
                return;
            }

            var testRegion2 = RegionManager.FindRegionByCulture(culture);
            Region = (testRegion2 != null) ? testRegion2 : RegionManager.GetDefaultRegion();
        }

        private void SetCulture()
        {
            var culture = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null;
            if (culture == null || !RegionManager.IsCultureSupported(culture))
            {
                var defaultRegion = RegionManager.GetDefaultRegion();
                Culture = defaultRegion.Culture;
                return;
            }

            Culture = culture;
        }
        
    }
}