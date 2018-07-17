using CRM11.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM11.UI.Filters
{
    public class CheckPermissionAttribute : System.Web.Mvc.AuthorizeAttribute
    {

        Helper.OperationContext OperCurrent = Helper.OperationContext.Current;

        /// <summary>
        /// 请求区域黑名单，凡是在此集合中的区域，请求页面时都要检查登录权限和权限
        /// </summary>
        List<string> backUrl = new List<string> { "Admin" };
        /// <summary>
        ///  在过程请求授权时调用。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //检查请求路由中是否存在区域
            if (filterContext.RouteData.DataTokens.ContainsKey("area"))
            {
                string areaName = filterContext.RouteData.DataTokens["area"].ToString();

                //判断当前请求路由中的区域名称是否在区域黑名单中
                if (backUrl.Contains(areaName) == true)
                {                   
                    //检查控制器或动作方法是否存在跳过登录标签，如果有，则跳过登录检查(如登录页面)
                    if ((filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(CRM11.Utility.SkipLoginAttribute), false)
                       || filterContext.ActionDescriptor.IsDefined(typeof(CRM11.Utility.SkipLoginAttribute), false)) == false )
                    {

                        if (IsLogin())
                        {
                            LoadMenuButtons(filterContext);

                            //用户已登陆，检查控制器或动作方法是否存在跳过权限标签，如果有，则跳过权限检查
                            if ((filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(CRM11.Utility.SkipPermissionAttribute),false)
                                || filterContext.ActionDescriptor.IsDefined(typeof(CRM11.Utility.SkipPermissionAttribute),false)) == false)
                            {
                                //进行权限验证检查
                                if (HashPermission(areaName
                                    ,filterContext.ActionDescriptor.ControllerDescriptor.ControllerName
                                    ,filterContext.ActionDescriptor.ActionName
                                    ,filterContext.HttpContext.Request.HttpMethod) == false) //如果权限验证不通过
                                {
                                    filterContext.Result = SendMessage("你没有进行此项操作的权限。","/Admin/User/Login");
                                }
                            }

                        }else //用户没有登录
                        {
                            filterContext.Result = SendMessage("你尚未登录","/Admin/User/Login");
                        }
                    }

                }

            }
            //base.OnAuthorization(filterContext);
        }

        static List<MODEL.Permission> emptyButtons = new List<MODEL.Permission>();
        /// <summary>
        /// 加载权限按钮到ViewBag中
        /// </summary>
        /// <param name="filterContext"></param>
        void LoadMenuButtons(AuthorizationContext filterContext)
        {
            string areaName = filterContext.RouteData.DataTokens["area"].ToString();
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            string httpMethod = filterContext.HttpContext.Request.HttpMethod;

            MODEL.Permission userPermission =  GetUserPermissionFromSession(areaName, controllerName, actionName, httpMethod);

            if (userPermission !=null)
            {
                var sonButtonPermission = OperCurrent.UserNowPermission.Where(o => o.perOperationType == 2 && o.perParent == userPermission.perId && o.perIsDel == false).OrderBy(o=>o.perOrder).ToList();

                if (sonButtonPermission == null)
                {
                    filterContext.Controller.ViewBag.sonBtnData = emptyButtons;
                }else
                {
                    filterContext.Controller.ViewBag.sonBtnData = sonButtonPermission;
                }
            }else
            {
                filterContext.Controller.ViewBag.sonBtnData = emptyButtons;
            }
        }
        


        /// <summary>
        /// 检查权限后统一回复前台，（Ajax请求和普通请求）
        /// </summary>
        /// <param name="message">提示消息</param>
        /// <param name="backUrl">返回的URL</param>
        /// <returns></returns>
        private ActionResult SendMessage(string message, string backUrl)
        {
            if (HttpContext.Current.Request.Headers.AllKeys.Contains("X-Requested-With"))
            {
                return OperCurrent.AjaxMessageFaild(message, backUrl);
            }else
            {
                return OperCurrent.JsMessage(message, backUrl);
            }
        }

        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="areaName">区域名</param>
        /// <param name="controllerName">控制器名</param>
        /// <param name="actionName">动作方法名</param>
        /// <param name="httpMethod">请求方式</param>
        /// <returns></returns>
        private bool HashPermission(string areaName, string controllerName, string actionName, string httpMethod)
        {            
            return GetUserPermissionFromSession(areaName, controllerName, actionName, httpMethod) != null;
        }

        /// <summary>
        /// 获取用户在当前请求页面拥有的权限
        /// </summary>
        /// <param name="areaName"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        private MODEL.Permission GetUserPermissionFromSession(string areaName, string controllerName, string actionName, string httpMethod)
        {
            var formMethod = httpMethod.ToLower() == "get" ? 1 : 2;
           
            var curPer = OperCurrent.UserNowPermission.FirstOrDefault(p => p.perAreaName.IsSame(areaName)
            && p.perControllerName.IsSame(controllerName)
            && p.perActionName.IsSame(actionName)
            && p.perFormMethod == 3 ? true : (p.perFormMethod == formMethod));

            return curPer;
        }


        /// <summary>
        /// 检查是否登录
        /// </summary>
        /// <returns></returns>
        private bool IsLogin()
        {
            //首先检查登录页面Post时是否存入了用户信息的Session
            if (OperCurrent.UserNow == null)
            {
                //如果用户信息的Session为空，再检查是否存入了用户Id的Cookie，
                //如果在Session为空的条件下，Cookie不为空，则表示用户点击了保持登录状态选项,
                //故需要实现自动登录
                if (OperCurrent.UserId > -1) //Cookie存在
                {
                    //重新存入用户信息的Session
                    OperCurrent.UserNow = OperCurrent.BllSession.Employee.Where(e => e.empId == OperCurrent.UserId).SingleOrDefault().ToPOCO();
                    //重新存入用户权限信息Session
                    OperCurrent.UserNowPermission = OperCurrent.BllSession.Employee.GetUserPermission(OperCurrent.UserId);
                   
                }
                else
                {
                    return false;
                }               
            }

            return true;
        }

    }
}