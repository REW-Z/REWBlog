using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using REW的空间BLL;
using REW的空间Model;

namespace REW的空间.Controllers
{
    public class AccountController : ApplicationController
    {

        // GET: Account
        [Authorize]
        public ActionResult Index()
        {
            string FilePath = Server.MapPath("~/Images/ProfilePics/" + User.Identity.Name + ".png");
            #region 判断是否有头像并传递给视图
            if (System.IO.File.Exists(FilePath))
            {
                ViewBag.ImgSrc = "/Images/ProfilePics/" + User.Identity.Name + ".png";
            }
            else
            {
                ViewBag.ImgSrc = "/Images/ProfilePics/DefaultPic.png";
            }
            #endregion
            
            ViewBag.ArticleCount = bll_articles.GetArticlesByUserName(User.Identity.Name).Count;
            ViewBag.UserName = User.Identity.Name;
            ViewBag.AllFocus = bll_relations.GetAllFocusByUID(bll_users.GetUidByName(User.Identity.Name));
            ViewBag.AllFollower = bll_relations.GetAllFollowersByUID(bll_users.GetUidByName(User.Identity.Name));

            USERS user = bll_users.GetUserByUserName(User.Identity.Name);
            return View(user);
        }
        [Authorize]
        public ActionResult Notifications()
        {
            ViewBag.bll_articles = bll_articles;
            List<NOTIFICATIONS> list_noti = bll_noti.GetAllByUserName(User.Identity.Name);
            return View(list_noti);
        }
        public ActionResult ReadNotification(int id, string aid)
        {           
            bll_noti.SetIsRead(id);
            return Redirect("/Read/Index/" + aid);
        }
        public ActionResult AllRead()
        {
            bll_noti.AllNotiRead(User.Identity.Name);
            return RedirectToAction("Notifications");
        }
        public ActionResult ClearMyNotifications()
        {
            bll_noti.ClearNotiByUserName(User.Identity.Name);
            return RedirectToAction("Notifications");
        }

        [Authorize]
        public ActionResult SetProfilePic(HttpPostedFileBase file)
        {
            if(file != null)
            {
                string filePath = Server.MapPath("~/Images/ProfilePics/" + User.Identity.Name + ".png");
                file.SaveAs(filePath);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult ShowMyArticles()
        {
            List<ARTICLES> list = bll_articles.GetArticlesByUserName(this.User.Identity.Name);
            return View(list);
        }

        [Authorize]
        public ActionResult DeleteArticle(int id)
        {
            bll_articles.DeleteArticleById(id);
            return RedirectToAction("ShowMyArticles");
        }

        [Authorize]
        public ActionResult EditArticle(int id)
        {
            List<TYPES> list_type = bll_types.GetAllTypes();
            IEnumerable<SelectListItem> selectlist = list_type.Select(x => new SelectListItem { Text = x.T_NAME, Value = x.T_ID.ToString() });
            ViewBag.ListOfTypes = selectlist;

            ARTICLES article = bll_articles.GetArticleById(id);
            return View(article);
        }

        [Authorize]
        [ValidateInput(false)]
        public ActionResult EditSave(ARTICLES article)
        {
            bll_articles.EditSave(article);
            return RedirectToAction("ShowMyArticles");
        }

        [Authorize]
        public ActionResult ShowOtherUsers()
        {
            //适配
            List<int> list_fid = bll_relations.GetAllFocusByUID(bll_users.GetUidByName(User.Identity.Name));
            ViewBag.ListOfFocusID = list_fid;

            List<USERS> list = bll_users.GetAllUsers();
            return View(list);
        }

        [Authorize]
        public ActionResult FocusUser(int id, string ReturnUrl = null)
        {
            bool Success = bll_relations.AddRelation(bll_users.GetUidByName(User.Identity.Name), id);
            if (ReturnUrl != null)
            {
                return Redirect(ReturnUrl + "?id=" + id);
            }
            else
            {
                return RedirectToAction("ShowOtherUsers");
            }
        }
        [Authorize]
        public ActionResult CancelFocus(int id, string ReturnUrl = null)
        {
            bll_relations.CancelFocus(bll_users.GetUidByName(User.Identity.Name), id);
            
            if(ReturnUrl != null)
            {
                return Redirect(ReturnUrl + "?id=" + id);
            }
            else
            {
                return RedirectToAction("ShowOtherUsers");
            }
        }

        [Authorize]
        public ActionResult Visit(int id)
        {
            ViewBag.ArticleCount = bll_articles.GetArticlesByUserName(bll_users.GetUserByUID(id).NAME).Count;
            ViewBag.AllFocus = bll_relations.GetAllFocusByUID(id);
            ViewBag.AllFollower = bll_relations.GetAllFollowersByUID(id);
            ViewBag.ListOfFocusID = bll_relations.GetAllFocusByUID(bll_users.GetUidByName(User.Identity.Name));
            
            USERS user = bll_users.GetUserByUID(id);
            return View(user);
        }
        [HttpGet]
        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewBag.Uid = bll_users.GetUidByName(User.Identity.Name);
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(USERS user)
        {
            bll_users.ChangePassword(user);
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult AjaxCheckIn()
        {
            bll_users.UserBonusPoint(User.Identity.Name, 1);
            bll_checkin.CheckIn(User.Identity.Name);
            return PartialView();
        }
    }
}