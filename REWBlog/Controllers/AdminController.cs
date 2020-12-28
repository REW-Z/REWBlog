using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
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
            if (file != null)
            {
                string path = Server.MapPath("~/Images/Slides/" + Path.GetFileName(file.FileName));
                file.SaveAs(path);
            }
            return RedirectToAction("AdminSlideImg");
        }

        [Authorize]
        [AdminAuthorize]
        public ActionResult AdminDeleteNotifications()
        {
            int result = bll_noti.ClearNotifications();
            return RedirectToAction("Index", new { numNotiDel = result });
        }

        [Authorize]
        [AdminAuthorize]
        public ActionResult BackupDatabase()
        {
            //错误信息
            List<string> listOfError = new List<string>();
            //文件夹创建
            string dir = Server.MapPath("~/App_Data/DatabaseBackup/ARTICLES/");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            //读取数据库
            List<ARTICLES> articles = bll_articles.GetAllArticles();
            foreach (ARTICLES article in articles)
            {
                try
                {
                    XDocument doc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("Article",
                        new XAttribute("A_ID", article.A_ID),
                        new XAttribute("A_TID", article.A_TID),
                        new XElement("A_NAME", new XCData(article.A_NAME)),
                        new XElement("A_AUTHOR", new XCData(article.A_AUTHOR)),
                        new XElement("A_DATE", new XCData(article.A_DATE.ToString())),
                        new XElement("A_CONTENT", new XCData(article.A_CONTENT != null ? article.A_CONTENT : "-")),
                        new XElement("A_CATALOG", new XCData(article.A_CATALOG != null ? article.A_CATALOG : "-"))
                        )
                    );
                    doc.Save(dir + StringProcess.Validate(article.A_NAME) + ".xml");
                }
                catch
                {
                    listOfError.Add("文章：" + StringProcess.Validate(article.A_NAME) + " 未成功备份！！");
                }
            }

            System.Text.StringBuilder strb = new System.Text.StringBuilder();
            strb.Append("<p>");
            foreach (var item in listOfError)
            {
                strb.Append(item + "<br />");
            }
            strb.Append("</p><script>alert('已完成备份！');</script>");
            return Content(strb.ToString());
        }

        [Authorize]
        [AdminAuthorize]
        public string ImportBackup()
        {
            int count = 0;
            int errorCount = 0;
            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/App_Data/DatabaseBackup/ARTICLES/"));
            FileInfo[] fileInfos = dirInfo.GetFiles("*.xml");
            foreach (var file in fileInfos)
            {
                XDocument doc = XDocument.Load(file.FullName);
                try
                {
                    XElement eleArticle = doc.Element("Article");
                    string aidStr = eleArticle.Attribute("A_ID").Value;
                    string atidStr = eleArticle.Attribute("A_TID").Value;
                    string nameStr = eleArticle.Element("A_NAME").Value;
                    string authorStr = eleArticle.Element("A_AUTHOR").Value;
                    string dateStr = eleArticle.Element("A_DATE").Value;
                    string contentStr = eleArticle.Element("A_CONTENT").Value;
                    string catalogStr = eleArticle.Element("A_CATALOG").Value;

                    ARTICLES arti = new ARTICLES();
                    arti.A_ID = Convert.ToInt32(aidStr);
                    arti.A_TID = Convert.ToInt32(atidStr);
                    arti.A_NAME = nameStr;
                    arti.A_AUTHOR = authorStr;
                    DateTime date;
                    System.DateTime.TryParse(dateStr, out date);
                    arti.A_DATE = date;
                    arti.A_CONTENT = contentStr;
                    arti.A_CATALOG = catalogStr == "" ? null : catalogStr;

                    if (bll_articles.GetAllArticles().FindAll(x => x.A_NAME == arti.A_NAME).Count == 0)
                    {
                        bll_articles.AddArticle(arti);
                        count++;
                    }
                }
                catch
                {
                    errorCount++;
                }
            }
            return "完成：共导入" + count + "个文件。失败" + errorCount + "个文件。";
        }
    }
}