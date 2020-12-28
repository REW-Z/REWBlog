using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace REWBlog.Controllers
{
    public abstract class ApplicationController:Controller
    {
        public REWBlogBLL.BLL_articles bll_articles = new REWBlogBLL.BLL_articles();
        public REWBlogBLL.BLL_checkin bll_checkin = new REWBlogBLL.BLL_checkin();
        public REWBlogBLL.BLL_noti bll_noti = new REWBlogBLL.BLL_noti();
        public REWBlogBLL.BLL_relations bll_relations = new REWBlogBLL.BLL_relations();
        public REWBlogBLL.BLL_types bll_types = new REWBlogBLL.BLL_types();
        public REWBlogBLL.BLL_users bll_users = new REWBlogBLL.BLL_users();

        public ApplicationController()
        {
            ViewData["BllDependency"] = new { bll_users = this.bll_users, bll_noti = this.bll_noti };
        }
    }
}