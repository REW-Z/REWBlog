using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REW的空间Model;

namespace REW的空间DAL
{
    public class DAL_noti
    {
        Entities dbc = new Entities();
        /// <summary>
        /// 添加消息提示
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="whose">关于谁的消息</param>
        /// <param name="typeid">消息类型ID</param>
        /// <param name="message">消息内容</param>
        /// <returns></returns>
        public int AddNotificatin(string username, string whose, int typeid, string message)
        {
            dbc.NOTIFICATIONS.Add(new NOTIFICATIONS() { USERNAME = username, WHOSE = whose, NOTI_TYPE = typeid, MESSAGE = message });
            return dbc.SaveChanges();
        }
        /// <summary>
        /// 获取用户的所有消息
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<NOTIFICATIONS> GetAllByUserName(string username)
        {
            return dbc.NOTIFICATIONS.Where(x => x.USERNAME == username).ToList();
        }
        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <returns>删除的数据条数</returns>
        public int DeleteAll()
        {
            foreach(var entity in dbc.NOTIFICATIONS)
            {
                dbc.NOTIFICATIONS.Remove(entity);
            }
            return dbc.SaveChanges();
        }
        /// <summary>
        /// 删除用户的所有消息
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int DeleteAllByUserName(string username)
        {
            foreach(var entity in dbc.NOTIFICATIONS)
            {
                if(entity.USERNAME == username)
                {
                    dbc.NOTIFICATIONS.Remove(entity);
                }
            }
            return dbc.SaveChanges();
        }
        /// <summary>
        /// 设为已读
        /// </summary>
        public void SetIsRead(int nid)
        {
            var noti = dbc.NOTIFICATIONS.FirstOrDefault(x => x.NID == nid);
            noti.READ = true;
            dbc.SaveChanges();
        }
    }
}
