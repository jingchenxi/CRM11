using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM11.UI.Areas.Admin.ModelView
{
    /// <summary>
    /// 设置角色权限所需要的数据模型
    /// </summary>
    public class SetRolePerViewModel
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public int RoleId;

        /// <summary>
        /// 所有权限集合
        /// </summary>
        public List<MODEL.Permission> AllPers;

        /// <summary>
        /// 当前角色所拥有的权限
        /// </summary>
        public List<MODEL.Permission> RolePers;
    }
}