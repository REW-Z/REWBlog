using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using REW的空间Model;

namespace REW的空间DAL
{
    public class DAL_users
    {
        /// <summary>
        /// 实例化上下文
        /// </summary>
        Entities dbc = new Entities();

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="userlogin"></param>
        /// <returns></returns>
        public bool TryAuthenticate(REW的空间Model.USERS userlogin)
        {
            USERS result = new USERS();

            
            try
            {
                result = dbc.USERS.FirstOrDefault(x => x.NAME == userlogin.NAME);
            }
            catch
            {
                return false;
            }
            
            if (result != null && result.PWD == userlogin.PWD)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 添加（注册）用户
        /// </summary>
        /// <param name="user_signup"></param>
        /// <returns></returns>
        public bool AddUser(USERS user_signup)
        {
            USERS user_new = new USERS();
            user_new.NAME = user_signup.NAME;
            user_new.PWD = user_signup.PWD;

            dbc.USERS.Add(user_new);
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
        /// 验证用户名是否已经存在
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool ExistName(string username)
        {
            return dbc.USERS.Any(x => x.NAME == username);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public bool ChangePassword(USERS user)
        {
            USERS user_change = dbc.USERS.FirstOrDefault(x => (x.UID == user.UID));
            user_change.PWD = user.PWD;
            int result = dbc.SaveChanges();
            return result > 0 ? true : false;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public bool DeleteUser(int uid)
        {
            USERS user = dbc.USERS.FirstOrDefault(x => x.UID == uid);
            dbc.USERS.Remove(user);
            int result = dbc.SaveChanges();
            return result > 0 ? true : false;
        }
        /// <summary>
        /// 按用户名获得用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public USERS GetUserByUserName(string username)
        {
            return dbc.USERS.FirstOrDefault(x => x.NAME == username);
        }
        /// <summary>
        /// 按UID获得用户
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public USERS GetUserByUID(int uid)
        {
            return dbc.USERS.FirstOrDefault(x => x.UID == uid); 
        }
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public List<USERS> GetAllUsers()
        {
            return dbc.USERS.ToList();
        }
        /// <summary>
        /// 获取最活跃的几个用户
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public IQueryable<USERS> GetActiveUsers(int number)
        {
            return dbc.USERS.OrderBy(x => x.POINTS).Take(number);
        }
        /// <summary>
        /// 用户加分
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="points">要加的分数</param>
        /// <returns></returns>
        public bool UserBonusPoint(string username, int points)
        {
            var user = dbc.USERS.FirstOrDefault(x => x.NAME == username);
            user.POINTS += points;
            var result = dbc.SaveChanges();
            return result > 0 ? true : false;
        }
    }

}
