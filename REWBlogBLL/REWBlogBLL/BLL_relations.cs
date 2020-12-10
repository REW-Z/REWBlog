using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REW的空间DAL;
using REW的空间Model;

namespace REW的空间BLL
{
    public class BLL_relations
    {
        REW的空间DAL.DAL_relations dal_relations = new DAL_relations();
        /// <summary>
        /// 添加关系
        /// </summary>
        /// <param name="followerId"></param>
        /// <param name="focusId"></param>
        /// <returns></returns>
        public bool AddRelation(int followerId, int focusId)
        {
            return dal_relations.AddRelation(followerId, focusId);
        }
        /// <summary>
        /// 获取所有关注
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public List<int> GetAllFocusByUID(int uid)
        {
            return dal_relations.GetAllFocusByUID(uid);
        }
        /// <summary>
        /// 获取所有粉丝
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public List<int> GetAllFollowersByUID(int uid)
        {
            return dal_relations.GetAllFollowersByUID(uid);
        }
        /// <summary>
        /// 取关
        /// </summary>
        /// <param name="followerid"></param>
        /// <param name="focusid"></param>
        /// <returns></returns>
        public bool CancelFocus(int followerid, int focusid)
        {
            return dal_relations.DeleteRelation(followerid, focusid);
        }

        /// <summary>
        /// 删除关于某人的所有关系
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public bool DeleteAllRelationsOfSomebody(int uid)
        {
            return dal_relations.DeleteAllRelationsOfSomebody(uid);
        }
    }
}
