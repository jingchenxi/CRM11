using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM11.MODEL
{
    public partial class Permission
    {
        /// <summary>
        /// 将权限表中的某一行Permission对象转换为自定义的树对象
        /// </summary>
        /// <returns></returns>
        public TreeNode ConvertToTreeNode()
        {
            return new TreeNode()
            {
                id = this.perId,
                text = this.perName,
                @checked = false,
                attributes = new
                {
                    url = "/" + this.perAreaName + "/" + this.perControllerName + "/" + this.perActionName,
                    isLink = this.perIsLink
                },
                state = "open",
            };
        }
    }
}
