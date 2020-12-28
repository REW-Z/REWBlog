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
            var files = Request.Files;
            if (files.Count == 0)
            {
                var objError = new { uploaded = false, url = string.Empty };
                return Json(objError);
            }

            var file = files[0];
            var fileName = file.FileName;
            var guidName = Guid.NewGuid() + System.IO.Path.GetExtension(fileName);
            var saveDir = Server.MapPath(@"~/Images/ArticleImages/");
            var previewPath = "/Images/ArticleImages/" + guidName;
            

            bool result = true;
            try
            {
                file.SaveAs(saveDir + guidName);
            }
            catch
            {
                result = false;
            }

            var obj = new { uploaded = result, url = result ? previewPath : string.Empty };
            return Json(obj);
        }
    }
}