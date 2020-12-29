using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using REWBlogBLL;
using REWBlogModel;


namespace REWBlog.Controllers
{
    public class PublishController : ApplicationController
    {

        // GET: Publish
        [Authorize]
        public ActionResult Index()
        {
            List<TYPES> list_type = bll_types.GetAllTypes();
            IEnumerable<SelectListItem> selectlist1 = list_type.Select(x => new SelectListItem { Text = x.T_NAME, Value = x.T_ID.ToString() });

            ViewBag.ListOfTypes = selectlist1;
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

        //处理图片上传
        [HttpPost]
        public JsonResult UploadImage()
        {
            int errCount = 0;
            var files = Request.Files;
            List<string> listOfPreviewPath = new List<string>();
            for(int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                var fileName = file.FileName;
                var guidName = Guid.NewGuid() + System.IO.Path.GetExtension(fileName);
                var saveDir = Server.MapPath(@"~/Images/ArticleImages/");
                var previewPath = "/Images/ArticleImages/" + guidName;

                try
                {
                    file.SaveAs(saveDir + guidName);
                }
                catch
                {
                    errCount++;
                }
                listOfPreviewPath.Add(previewPath);
            }


            var obj = new { errno = errCount, data = listOfPreviewPath.ToArray() };
            return Json(obj);
        }
    }
}