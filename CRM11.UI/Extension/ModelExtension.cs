using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM11.UI.Extension
{
    /// <summary>
    /// 实体类 的扩展方法
    /// </summary>
    public static class ModelExtension
    {
        /// <summary>
        /// 将 权限实体 对象 转成 权限视图模型 对象
        /// </summary>
        /// <param name="poco"></param>
        /// <returns></returns>
        public static CRM11.UI.Areas.Admin.ModelView.PermissionView ToViewModel(this MODEL.Permission poco)
        {
            return new CRM11.UI.Areas.Admin.ModelView.PermissionView()
            {
                perId = poco.perId,
                perParent = poco.perParent,
                perName = poco.perName,
                perRemark = poco.perRemark,
                perAreaName = poco.perAreaName,
                perControllerName = poco.perControllerName,
                perActionName = poco.perActionName,
                perFormMethod = poco.perFormMethod,
                perOperationType = poco.perOperationType,
                perJsMethodName = poco.perJsMethodName,
                perIco = poco.perIco,
                perIsLink = poco.perIsLink,
                perOrder = poco.perOrder,
                perIsShow = poco.perIsShow
            };
        }
    }
}