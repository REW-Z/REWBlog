using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REWBlogModel;

namespace REWBlogDAL
{
    public class DAL_types
    {
        /// <summary>
        /// 获取所有文章类型
        /// </summary>
        /// <returns></returns>
        public List<TYPES> GetAllTypes()
        {
            Entities dbc = new Entities();
            return dbc.TYPES.ToList();
        }
    }
}
