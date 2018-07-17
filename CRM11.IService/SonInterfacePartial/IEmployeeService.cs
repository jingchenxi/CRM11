using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM11.IService
{
    public partial interface IEmployeeService
    {
        /// <summary>
        /// 根据用户的id标识查询此用户拥有的权限
        /// </summary>
        /// <param name="uid">用户的id标识</param>
        /// <returns></returns>
        List<MODEL.Permission> GetUserPermission(int uid);
    }
}
