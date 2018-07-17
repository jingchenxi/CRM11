using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM11.MODEL
{
    /// <summary>
    /// Ajax返回消息的数据模型
    /// </summary>
    public class AjaxMessage
    {
        public AjaxStatu Status { get; set; }

        public string Message { get; set; }

        public string BackUrl { get; set; }

        public object Data { get; set; }

        /// <summary>
        /// 因为easyUI的 datagrid组件 在对 非法请求时返回消息 对象 里 必须包含一个 rows属性
        /// </summary>
        public string rows
        {
            get
            {
                return "";
            }
        }
    }

    /// <summary>
    /// 描述Ajax操作请求的状态
    /// </summary>
    public enum AjaxStatu
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        OK = 1,
        /// <summary>
        /// 操作失败
        /// </summary>
        Failed = 2,
        /// <summary>
        /// 用户没有登录
        /// </summary>
        NoLogin = 3,
        /// <summary>
        /// 没有访问权限
        /// </summary>
        NoPower = 4,
        /// <summary>
        /// 发生异常
        /// </summary>
        Error = 5
    }
}
