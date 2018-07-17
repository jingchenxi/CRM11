using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRM11.UI.ModelView
{
    /// <summary>
    /// 登录视图模型
    /// </summary>
    public class LoginModel
    {
        [DisplayName("登录名"), Required(ErrorMessage = "登录名不能为空")]
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        [DisplayName("登录密码"), Required(ErrorMessage = "登录密码不能为空")]
        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPwd { get; set; }

        [DisplayName("验证码"), Required(ErrorMessage = "验证码不能为空")]
        /// <summary>
        /// 验证码
        /// </summary>
        public string LoginCode { get; set; }

        [DisplayName("是否保持登录?")]
        /// <summary>
        /// 是否保持登录
        /// </summary>
        public bool IsAlwaysLogin { get; set; }
    }
}