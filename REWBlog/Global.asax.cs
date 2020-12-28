using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace REWBlog
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //全局变量
        public static int publishBonusPoints;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            publishBonusPoints = 2;
        }
    }
}
