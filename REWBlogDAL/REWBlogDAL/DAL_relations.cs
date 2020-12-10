using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REW的空间Model;

namespace REW的空间DAL
{
    public class DAL_relations
    {
        Entities dbc = new Entities();

        /// <summary>
        /// 添加关系
        /// </summary>
        /// <param name="followerId"></param>
        /// <param name="focusId"></param>
        /// <returns></returns>
        public bool AddRelation(int followerId, int focusId)
        {
            RELATIONS relation = new RELATIONS { R_FollowID = followerId, R_FocusID = focusId };
            dbc.RELATIONS.Add(relation);
            int result = dbc.SaveChanges();
            return result > 0 ? true : false;
        }
        /// <summary>
        /// 删除关系
        /// </summary>
        /// <param name="followerId"></param>
        /// <param name="focusId"></param>
        /// <returns></returns>
        public bool DeleteRelation(int followerId, int focusId)
        {
            IQueryable<RELATIONS> relations = dbc.RELATIONS.Where(x => (x.R_FollowID == followerId && x.R_FocusID == focusId));
            dbc.RELATIONS.RemoveRange(relations);
            int result = dbc.SaveChanges();
            return result > 0 ? true : false;
        }
        /// <summary>
        /// 删除关于某人的所有关系
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public bool DeleteAllRelationsOfSomebody(int uid)
        {
            IQueryable<RELATIONS> relations = dbc.RELATIONS.Where(x => (x.R_FocusID == uid && x.R_FollowID == uid));
            dbc.RELATIONS.RemoveRange(relations);
            int result = dbc.SaveChanges();
            return result > 0 ? true : false;
        }
        /// <summary>
        /// 获取所有关注
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public List<int> GetAllFocusByUID(int uid)
        {
            return dbc.RELATIONS.Where(x => x.R_FollowID == uid).Select(x => x.R_FocusID).ToList();
        }
        /// <summary>
        /// 获取所有粉丝
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public List<int> GetAllFollowersByUID(int uid)
        {
            return dbc.RELATIONS.Where(x => x.R_FocusID == uid).Select(x => x.R_FollowID).ToList();
        }
    }
}
