using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REWBlogDAL;
using REWBlogModel;

namespace REWBlogBLL
{
    public class BLL_noti
    {
        REWBlogDAL.DAL_noti dal_noti = new REWBlogDAL.DAL_noti();
        REWBlogDAL.DAL_users dal_users = new DAL_users();
        REWBlogDAL.DAL_relations dal_relations = new DAL_relations();
        /// <summary>
        /// 添加自己的消息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="typeid"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool AddNotiAboutMyself(string username,int typeid, string message)
        {
            int result = dal_noti.AddNotificatin(username, username, typeid, message);
            return result > 0 ? true : false;
        }
        /// <summary>
        /// 添加朋友的消息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="friendid"></param>
        /// <param name="typeid"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool AddNotiAboutFriend(string username, int friendid, int typeid, string message)
        {
            int result = dal_noti.AddNotificatin(username,dal_users.GetUserByUID(friendid).NAME, typeid, message);
            return result > 0 ? true : false;
        }
        /// <summary>
        /// 推送给关注者
        /// </summary>
        /// <param name="username"></param>
        /// <param name="typeid"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public int PushToFollowers(string username, int typeid, string message)
        {
            List<int> list_Id = dal_relations.GetAllFollowersByUID(dal_users.GetUserByUserName(username).UID);
            int num_push = 0;
            foreach(var item in list_Id)
            {
                int result = dal_noti.AddNotificatin(dal_users.GetUserByUID(item).NAME, username, typeid, message);
                num_push += result;
            }
            return num_push;
        }
        /// <summary>
        /// 获取用户所有消息
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<NOTIFICATIONS> GetAllByUserName(string username)
        {
            return dal_noti.GetAllByUserName(username);
        }


        /// <summary>
        /// 清空Notification表
        /// </summary>
        /// <returns></returns>
        public int ClearNotifications()
        {
            return dal_noti.DeleteAll();
        }
        /// <summary>
        /// 清空用户的Notifications
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int ClearNotiByUserName(string username)
        {
            return dal_noti.DeleteAllByUserName(username);
        }
        /// <summary>
        /// 全部已读
        /// </summary>
        /// <param name="noti"></param>
        public void AllNotiRead(string username)
        {
            foreach(var item in dal_noti.GetAllByUserName(username))
            {
                dal_noti.SetIsRead(item.NID);
            }
        }
        /// <summary>
        /// 设置为已读
        /// </summary>
        /// <param name="nid"></param>
        public void SetIsRead(int nid)
        {
            dal_noti.SetIsRead(nid);
        }
    }
}
