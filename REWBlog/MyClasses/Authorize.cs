using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using REW的空间BLL;
using REW的空间Model;

namespace REW的空间.MyClasses
{
    public class AdminAuthorize:System.Web.Mvc.AuthorizeAttribute
    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            BLL_users bll_users = new BLL_users();

            return bll_users.IsAdmin(httpContext.User.Identity.Name);
        }
    }
}