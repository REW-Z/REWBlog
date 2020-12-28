using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REWBlogModel;

namespace REWBlogDAL
{
    public class DAL_articles
    {
        Entities dbc = new Entities();

        /// <summary>
        /// 获取所有文章
        /// </summary>
        /// <returns></returns>
        public List<ARTICLES> GetAllArticles()
        {
            return dbc.ARTICLES.ToList();
        }
        /// <summary>
        /// 添加文章
        /// </summary>
        /// <returns></returns>
        public bool AddArticle(ARTICLES article)
        {
            dbc.ARTICLES.Add(article);
            int result = dbc.SaveChanges();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        /// <summary>
        /// 按ID获取文章
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public ARTICLES GetArticleById(int aid)
        {
            ARTICLES article = dbc.ARTICLES.FirstOrDefault(x => x.A_ID == aid);
            return article;
        }
        /// <summary>
        /// 获取用户发表的文章
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public List<ARTICLES> GetArticlesByUName(string username)
        {
            var result = from arti in dbc.ARTICLES
                         where arti.A_AUTHOR == username
                         orderby arti.A_ID
                         select arti;

            return result.ToList();
        }
        /// <summary>
        /// 按ID删除文章
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public bool DeleteArticleByAID(int aid)
        {
            var entity = dbc.ARTICLES.FirstOrDefault(x => x.A_ID == aid);
            dbc.ARTICLES.Remove(entity);
            int result = dbc.SaveChanges();

            if(result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 保存文章编辑
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public bool EditSave(ARTICLES article)
        {
            ARTICLES arti_edit = dbc.ARTICLES.FirstOrDefault(x => x.A_ID == article.A_ID);
            arti_edit.A_NAME = article.A_NAME;
            arti_edit.A_DATE = article.A_DATE; // cheat:日期自定义
            arti_edit.A_CATALOG = article.A_CATALOG;
            arti_edit.A_CONTENT = article.A_CONTENT;
            int result = dbc.SaveChanges();
            if(result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
