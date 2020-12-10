using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace REW的空间.Controllers
{
    public abstract class ApplicationController:Controller
    {
        public REW的空间BLL.BLL_articles bll_articles = new REW的空间BLL.BLL_articles();
        public REW的空间BLL.BLL_checkin bll_checkin = new REW的空间BLL.BLL_checkin();
        public REW的空间BLL.BLL_noti bll_noti = new REW的空间BLL.BLL_noti();
        public REW的空间BLL.BLL_relations bll_relations = new REW的空间BLL.BLL_relations();
        public REW的空间BLL.BLL_types bll_types = new REW的空间BLL.BLL_types();
        public REW的空间BLL.BLL_users bll_users = new REW的空间BLL.BLL_users();

        public ApplicationController()
        {
            ViewData["BllDependency"] = new { bll_users = this.bll_users, bll_noti = this.bll_noti };
        }
    }
}