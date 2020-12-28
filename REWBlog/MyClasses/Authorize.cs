using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using REWBlogBLL;
using REWBlogModel;

namespace REWBlog.MyClasses
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