using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM11.UI.Areas.Admin.Controllers
{
    using CRM11.UI.Helper;
    using CRM11.UI.ModelView;
    using CRM11.Utility;
    using System.Threading;

    public class UserController : BaseController
    {
        
        [HttpGet,SkipLogin]
        // GET: Admin/User
        public ActionResult Login()
        {
            Thread.Sleep(2000);
            return View();
        }

        [HttpPost,SkipLogin]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (Session[Vcode.VcodeName] !=null && model.LoginCode.IsSame(Session[Vcode.VcodeName].ToString()))
                {
                    var user = OperContext.BllSession.Employee.Where(e => e.empLoginName == model.LoginName).SingleOrDefault();

                    if (user == null)
                    {
                        return OperContext.AjaxMessageFaild("登录名错误", "/Admin/User/Login");
                    }

                    if (user.empLoginPwd.IsSame(model.LoginPwd.ToMD5()))
                    {

                        #region 将当前登录用户信息依据条件存入Session，Cookie
                        //将EF查询代理类对象转换为普通对象，再存入Session中，避免EF的引用也存入Session中
                        OperContext.UserNow = user.ToPOCO();

                        //如果用户选择了保持登录选择框，则将当前用户的Id标识存入Cookie，以便在登录状态保持及权限管理时进行处理
                        if (model.IsAlwaysLogin)
                        {
                            OperContext.UserId = user.empId;
                        }
                        #endregion

                        //查询当前登录用户的所有权限并存入OperContext的UserNowPermission属性(存入Session)中
                        OperContext.UserNowPermission = OperContext.BllSession.Employee.GetUserPermission(user.empId);
                       

                        return OperContext.AjaxMessageOk("登录成功", "/Admin/Manager/Index");
                    }

                    return OperContext.AjaxMessageFaild("登录密码错误");

                }else
                {
                    return OperContext.AjaxMessageFaild("验证码错误");
                }
            }
            return OperContext.AjaxMessageFaild("请不要关闭浏览器的Js功能选项");
           
        }
    }
}