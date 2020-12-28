using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using REWBlogBLL;
using REWBlogModel;

namespace REWBlog.Controllers
{
    public class ReadController : ApplicationController
    {

        // GET: Read        
        public ActionResult Index(int id)
        {
            ARTICLES article = bll_articles.GetArticleById(id);
            return View(article);
        }
    }
}