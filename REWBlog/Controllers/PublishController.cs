using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using REW的空间BLL;
using REW的空间Model;

namespace REW的空间.Controllers
{
    public class PublishController : ApplicationController
    {

        // GET: Publish
        [Authorize]
        public ActionResult Index()
        {
            List<TYPES> list_type = bll_types.GetAllTypes();
            IEnumerable<SelectListItem> selectlist = list_type.Select(x => new SelectListItem { Text = x.T_NAME, Value = x.T_ID.ToString() });

        
            ViewBag.ListOfTypes = selectlist;
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Publish(ARTICLES article)
        {
            bool Success = bll_articles.AddArticle(article);
            ViewBag.IsSuccess = Success;
            bll_users.UserBonusPoint(article.A_AUTHOR, MvcApplication.publishBonusPoints);
            //推送消息
            bll_noti.AddNotiAboutMyself(User.Identity.Name, 0, article.A_ID.ToString());
            bll_noti.PushToFollowers(User.Identity.Name, 0, article.A_ID.ToString());
            return View(article);
        }
    }
}