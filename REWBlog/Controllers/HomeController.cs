using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using REW的空间Model;
using REW的空间BLL;

namespace REW的空间.Controllers
{
    public class HomeController : ApplicationController
    {
        // GET: Home
        public ActionResult Index()
        {
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/Images/Slides"));
            FileInfo[] files = dir.GetFiles();
            ViewBag.Imgs = files;
            return View();
        }

        /// <summary>
        /// 关于信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            return View();
        }


        /// <summary>
        /// 登录动作方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Errors = false;
            return View();
        }

        [HttpPost]
        public ActionResult Login(USERS userlogin, string ReturnUrl = null)
        {
            ViewBag.Errors = false;
            if (ModelState.IsValid)
            {
                //登录验证
                bool AuthSuccess = bll_users.TryAuthenticate(userlogin);

                if (AuthSuccess)
                {
                    ///票据cookie
                    FormsAuthentication.SetAuthCookie(userlogin.NAME, true);

                    ///添加角色
                    //
                    //bool IsAdmin = bll_user.IsAdmin(userlogin.NAME);
                    //if (IsAdmin)
                    //{
                    //    if (Roles.RoleExists("Admins"))
                    //    {
                    //        Roles.AddUserToRole(userlogin.NAME, "Admins");
                    //    }
                    //    else
                    //    {
                    //        Roles.CreateRole("Admins");
                    //        Roles.AddUserToRole(userlogin.NAME, "Admins");
                    //    }
                    //}

                    //检查ReturnUrl是否存在
                    if (string.IsNullOrEmpty(ReturnUrl))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return Redirect(ReturnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("Login", "用户名或者密码错误");
                    ViewBag.Errors = true;
                    return View();
                }
            }
            else
            {
                ViewBag.Errors = true;
                return View();
            }
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(USERS user_signup)
        {
            
            if (ModelState.IsValid)
            {
                bool IsSuccess = bll_users.AddUser(user_signup);
                if (IsSuccess)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("Signup", "注册失败，用户名已存在！");
                    return View();
                }
            }
            else
            {
                return View();
            }
            
            
        }
        /// <summary>
        /// 分部视图：文章
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public ActionResult PartialArticlesAction(string tid)
        {
            List<ARTICLES> list = bll_articles.GetAllArticles().OrderByDescending(x => x.A_DATE).Take(5).ToList();
            return PartialView(list);
        }
        /// <summary>
        /// 分部视图：类型
        /// </summary>
        /// <returns></returns>
        public ActionResult PartialTypesAction()
        {
            List<TYPES> list = bll_types.GetAllTypes();
            return PartialView(list);
        }
        /// <summary>
        /// 分布视图：活跃用户
        /// </summary>
        /// <returns></returns>
        public ActionResult PartialActiveUsers()
        {
            List<USERS> list_users = bll_users.GetActiveUsers(5).OrderByDescending(x => x.POINTS).ToList();
            return PartialView(list_users);
        }
        /// <summary>
        /// AJAX
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AjaxArticles(int id = 0)
        {
            List<ARTICLES> list;

            if (id > 0)
            {
                list = bll_articles.GetAllArticles().Where(x => x.A_TID == id).OrderByDescending(x => x.A_DATE).Take(5).ToList();
            }
            else
            {
                list = bll_articles.GetAllArticles().OrderByDescending(x => x.A_DATE).Take(5).ToList();
            }
            
            return PartialView(list);
        }
    }
}