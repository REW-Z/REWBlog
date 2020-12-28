using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using REWBlog.MyClasses;
using REWBlogBLL;
using REWBlogModel;

namespace REWBlog.Controllers
{
    public class AdminController : ApplicationController
    {
        // GET: Admin
        [Authorize]
        [AdminAuthorize]
        public ActionResult Index(int numCheckDel = 0, int numNotiDel = 0)
        {
            //if (bll_users.IsAdmin(User.Identity.Name))
            //{
            //    return View();
            //}
            //else
            //{
            //    return Redirect("/Home/Index");
            //}
            ViewBag.numCheckDel = numCheckDel;
            ViewBag.numNotiDel = numNotiDel;
            return View();
        }


        /// <summary>
        /// 文章管理
        /// </summary>
        /// <param name="id"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [Authorize]
        [AdminAuthorize]
        public ActionResult AdminArticles(int id = 1, string keyword = "")
        {
            var articles = bll_articles.GetAllArticles().Where(x => (x.A_NAME.Contains(keyword) || x.A_AUTHOR.Contains(keyword)));
            int articlecount = articles.Count();
            int pagecount = (int)Math.Ceiling((decimal)articlecount / 10);
            int currentpage = id;

            ViewBag.pagecount = pagecount;
            ViewBag.currentpage = currentpage;
            ViewBag.articlecount = articlecount;

            List<ARTICLES> list_arti_current = articles.OrderByDescending(x => x.A_DATE).Skip((currentpage - 1) * 10).Take(10).ToList();

            return View(list_arti_current);
        }
        [Authorize]
        [AdminAuthorize]
        public ActionResult DeleteArticle(int id)
        {
            bll_articles.DeleteArticleById(id);
            return RedirectToAction("AdminArticles");
        }

        /// <summary>
        /// 用户管理
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [AdminAuthorize]
        public ActionResult AdminUsers(int id = 1, string keyword = "")
        {
            var users = bll_users.GetAllUsers().Where(x => x.NAME.Contains(keyword));

            int usercount = users.Count();
            int pagecount = (int)Math.Ceiling((decimal)usercount / 5);
            int currentpage = id;

            ViewBag.pagecount = pagecount;
            ViewBag.currentpage = currentpage;
            ViewBag.usercount = usercount;

            List<USERS> list_users_current = users.OrderBy(x => x.UID).Skip((currentpage - 1) * 10).Take(10).ToList();
            return View(list_users_current);

        }
        [Authorize]
        [AdminAuthorize]
        public ActionResult DeleteUser(int id)
        {
            string path = Server.MapPath("~/Images/ProfilePics/") + bll_users.GetUserByUID(id).NAME + ".png";
            bll_users.DeleteUserByUID(id);
            bll_relations.DeleteAllRelationsOfSomebody(id);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            return RedirectToAction("AdminUsers");
        }
        [Authorize]
        [AdminAuthorize]
        public ActionResult AdminDelCheck()
        {
            int num = bll_checkin.ResetCheckIn();
            return RedirectToAction("Index", new { numCheckDel = num });
        }

        [Authorize]
        [AdminAuthorize]
        public ActionResult AdminSlideImg()
        {
            return View();
        }
        [Authorize]
        [AdminAuthorize]
        public ActionResult AdminDeleteImg(string filename)
        {
            System.IO.File.Delete(Server.MapPath("~/Images/Slides/" + filename));
            return RedirectToAction("AdminSlideImg");
        }
        [Authorize]
        [AdminAuthorize]
        public ActionResult AdminAddImg(HttpPostedFileBase file)
        {
            if(file != null)
            {
                string path = Server.MapPath("~/Images/Slides/" + Path.GetFileName(file.FileName));
                file.SaveAs(path);
            }
            return RedirectToAction("AdminSlideImg");
        }

        public ActionResult AdminDeleteNotifications()
        {
            int result = bll_noti.ClearNotifications();
            return RedirectToAction("Index", new { numNotiDel = result });
        }
    }
}