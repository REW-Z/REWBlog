using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REW的空间Model;

namespace REW的空间BLL
{
    public class BLL_users
    {
        REW的空间DAL.DAL_users dal_users = new REW的空间DAL.DAL_users();
        public void HelloWorld()
        {

        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="userlogin"></param>
        /// <returns></returns>
        public bool TryAuthenticate(REW的空间Model.USERS userlogin)
        {
            bool Success = dal_users.TryAuthenticate(userlogin);
            return Success;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool ChangePassword(USERS user)
        {
            return dal_users.ChangePassword(user);
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public bool DeleteUserByUID(int uid)
        {
            return dal_users.DeleteUser(uid);
        }
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="user_signup"></param>
        /// <returns></returns>
        public bool AddUser(USERS user_signup)
        {
            if (dal_users.ExistName(user_signup.NAME))
            {
                return false;
            }
            else
            {
                return dal_users.AddUser(user_signup);
            }            
        }       
        /// <summary>
        /// 根据用户名获取用户实体
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public USERS GetUserByUserName(string username)
        {
            return dal_users.GetUserByUserName(username);
        }
        /// <summary>
        /// 按UID获得用户
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public USERS GetUserByUID(int uid)
        {
            return dal_users.GetUserByUID(uid);
        }
        /// <summary>
        /// 验证是否为管理员
        /// </summary>
        /// <returns></returns>
        public bool IsAdmin(string username)
        {
            USERS user = dal_users.GetUserByUserName(username);
            if(user.ROLE == "Admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public List<USERS> GetAllUsers()
        {
            return dal_users.GetAllUsers();
        }
        /// <summary>
        /// 根据用户名获得UID
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int GetUidByName(string username)
        {
            USERS user = dal_users.GetUserByUserName(username);
            return user.UID;
        }
        /// <summary>
        /// 获取最活跃的用户
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public IQueryable<USERS> GetActiveUsers(int number)
        {
            return dal_users.GetActiveUsers(number);
        }
        /// <summary>
        /// 用户加分
        /// </summary>
        /// <param name="username"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        public bool UserBonusPoint(string username, int points)
        {
            return dal_users.UserBonusPoint(username,points);
        }
    }
}
