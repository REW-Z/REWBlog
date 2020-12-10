using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REW的空间Model;

namespace REW的空间BLL
{
    public class BLL_articles
    {
        REW的空间DAL.DAL_articles dal_arti = new REW的空间DAL.DAL_articles();

        /// <summary>
        /// 获取所有文章
        /// </summary>
        /// <returns></returns>
        public List<ARTICLES> GetAllArticles()
        {
            return dal_arti.GetAllArticles();
        }
        /// <summary>
        /// 添加文章
        /// </summary>
        /// <returns></returns>
        public bool AddArticle(ARTICLES article)
        {
            return  dal_arti.AddArticle(article);
        }
        /// <summary>
        /// 按ID获取文章
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public ARTICLES GetArticleById(int aid)
        {
            return dal_arti.GetArticleById(aid);
        }
        /// <summary>
        /// 按用户名获取文章
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<ARTICLES> GetArticlesByUserName(string username)
        {
            return dal_arti.GetArticlesByUName(username);
        }
        ///删除文章（按照id）
        public bool DeleteArticleById(int id)
        {
            return dal_arti.DeleteArticleByAID(id);
        }
        /// <summary>
        /// 保存文章更改
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public bool EditSave(ARTICLES article)
        {
            return dal_arti.EditSave(article);
        }
    }
}
