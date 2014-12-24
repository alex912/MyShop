using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var defaultHandler = new { controller = "Home", action = "Index" };

            routes.MapRoute("", "index.html", defaultHandler);

/*            routes.MapRoute("", "admin", defaultHandler);
            routes.MapRoute("", "admin/index.html", defaultHandler);
            routes.MapRoute("", "admin/{controller}/{action}.html", defaultHandler);
            routes.MapRoute("", "admin/{controller}.html", defaultHandler);
            routes.MapRoute("", "admin/{controller}/{action}/{id}", defaultHandler);
*/
            routes.MapRoute("", "catalog/{*itempath}", new { controller = "Catalog", action = "Index" });
            routes.MapRoute("", "{controller}/{action}/{id}.html", defaultHandler);
            routes.MapRoute("", "{controller}/{action}.html", defaultHandler);
            routes.MapRoute("", "{controller}.html", defaultHandler);

            
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional } );
        }
    }
}
