using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using REW的空间Model;
using REW的空间BLL;

namespace REW的空间.Controllers
{
    public class SearchController : ApplicationController
    {
        // GET: Search
        public ActionResult Index(int id = 1, string keyword = "")
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
    }
}