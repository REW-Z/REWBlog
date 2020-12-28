using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REWBlogModel;

namespace REWBlogDAL
{
    public class DAL_checkin
    {
        Entities dbc = new Entities();
        /// <summary>
        /// 是否已经签到
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool IsCheckedIn(string username)
        {
            var result = dbc.CHECKIN.FirstOrDefault(x => x.USERNAME == username);
            if (result != null)
            {
                if (result.LASTCHECKIN.Day == DateTime.Now.Day)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int CheckIn(string username)
        {
            if (dbc.CHECKIN.FirstOrDefault(x => x.USERNAME == username) != null)
            {
                dbc.CHECKIN.Remove(dbc.CHECKIN.FirstOrDefault(x => x.USERNAME == username));
            }
            dbc.CHECKIN.Add(new CHECKIN() { USERNAME = username, LASTCHECKIN = DateTime.Now });
            return dbc.SaveChanges();
        }
        /// <summary>
        /// 重置签到
        /// </summary>
        /// <returns></returns>
        public int ResetCheckIn()
        {
            var list = dbc.CHECKIN.Where(x => x.LASTCHECKIN.Day == DateTime.Now.Day);
            dbc.CHECKIN.RemoveRange(list);
            return dbc.SaveChanges();
        }
    }
}
