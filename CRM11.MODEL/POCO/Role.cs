using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM11.MODEL
{
    public partial class Role
    {
        /// <summary>
        /// 手写的 ToPOCO方法
        /// </summary>
        /// <param name="isSelf">没有任何意义，为了和T4模板生成的方法重载而已</param>
        /// <returns></returns>
        public Role ToPOCO(bool isSelf)
        {
            return new Role()
            {
                roleId = this.roleId,
                roleDepId = this.roleDepId,
                roleName = this.roleName,
                roleIsDel = this.roleIsDel,
                roleAddTime = this.roleAddTime,
                //手动指定为导航属性赋值
                Department = this.Department.ToPOCO()//因为导航属性也是EF查询的，也是代理对象，所以也需要转成POCO
            };
        }
    }
}
