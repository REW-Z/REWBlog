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

        //图片上传接口：WangEditor
        [HttpPost]
        public JsonResult UploadImageWangEditor()
        {
            string saveDir = Server.MapPath(@"~/Images/ArticleImages/");
            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }


            int errCount = 0;
            var files = Request.Files;
            List<string> listOfPreviewPath = new List<string>();
            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                var fileName = file.FileName;
                var guidName = Guid.NewGuid() + System.IO.Path.GetExtension(fileName);
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

        //图片上传接口：EditorMd
        [HttpPost]
        public JsonResult UploadImageEditorMd()
        {
            /// ```json
            /// {
            ///     success: 0 | 1,           // 0 表示上传失败，1 表示上传成功
            ///     message: "提示的信息，上传成功或上传失败及错误信息等。",
            ///     url: "图片地址"        // 上传成功时才返回
            /// }
            ///
            /// ```

            bool isSuccess;
            string msg = "";

            string saveDir = Server.MapPath(@"~/Images/ArticleImages/");
            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }


            var file = Request.Files[0];
            var fileName = file.FileName;
            var guidName = Guid.NewGuid() + System.IO.Path.GetExtension(fileName);
            var previewPath = "/Images/ArticleImages/" + guidName;

            try
            {
                file.SaveAs(saveDir + guidName);
                isSuccess = true;
            }
            catch
            {
                isSuccess = false;
            }


            var obj = new { success = isSuccess ? 1 : 0, message = msg, url = previewPath };
            return Json(obj);
        }
    }
}