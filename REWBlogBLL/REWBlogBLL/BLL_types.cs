using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REW的空间DAL;
using REW的空间Model;

namespace REW的空间BLL
{    
    public class BLL_types
    {
        DAL_types dal_types = new DAL_types();
        /// <summary>
        /// 获取所有类型
        /// </summary>
        /// <returns></returns>
        public List<TYPES> GetAllTypes()
        {
            return dal_types.GetAllTypes();
        }
    }
}
