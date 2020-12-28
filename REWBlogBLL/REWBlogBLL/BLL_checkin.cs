using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REWBlogDAL;
using REWBlogModel;

namespace REWBlogBLL
{
    public class BLL_checkin
    {
        private DAL_checkin dal_checkin = new DAL_checkin();
        /// <summary>
        /// 今天是否已经签到
        /// </summary>
        /// <param name="username"></param>
        /// <returns>返回是否签到</returns>
        public bool IsCheckedIn(string username)
        {
            return dal_checkin.IsCheckedIn(username);
        }
        /// <summary>
        /// 用户签到
        /// </summary>
        /// <param name="username"></param>
        /// <returns>返回是否签到成功</returns>
        public bool CheckIn(string username)
        {
            var result = dal_checkin.CheckIn(username);
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
        /// 重置今天的签到
        /// </summary>
        /// <returns></returns>
        public int ResetCheckIn()
        {
            return dal_checkin.ResetCheckIn();
        }
    }
}
