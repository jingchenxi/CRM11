using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM11.UI.Helper
{
    using CRM11.MODEL;
    using CRM11.Utility;
    using System.Web.Mvc;
    using System.Web.WebPages;

    /// <summary>
    /// UI层的操作上下文，提供UI层常用的属性和方法
    /// </summary>
    public class OperationContext
    {
        /// <summary>
        ///取得当前请求线程的UI操作上下文(一次请求线程只new一个OperationContext对象)
        /// </summary>
        public static OperationContext Current
        {
            get
            {
                object OperationCurrent = System.Runtime.Remoting.Messaging.CallContext.GetData(typeof(OperationContext).FullName);
                if (OperationCurrent == null)
                {
                    OperationCurrent = new OperationContext();
                    //相当于存入Session中，Session键为typeof(OperationContext).FullName
                    System.Runtime.Remoting.Messaging.CallContext.SetData(typeof(OperationContext).FullName, OperationCurrent);
                }

                return OperationCurrent as OperationContext;
            }
        }



        CRM11.IService.IServiceDbSession _bllSession;
        /// <summary>
        /// 取得Bll数据仓储对象，因为_bllSession为OperationContext对象成员，
        /// 所以_bllSession也具有一个线程只有一个BLLSession仓储对象的特点
        /// 故BllSession属性不需要再从CallContext中获取
        /// </summary>
        public CRM11.IService.IServiceDbSession BllSession
        {
            get
            {
                if (_bllSession == null)
                    _bllSession = Utility.DI.GetObject<IService.IServiceDbSession>("bllSession");
                return _bllSession;
            }

        }
        //存储用户信息对象的Session键
        public const string CURRENTUSERINFO_SESSION_KEY = "userinfo";
        /// <summary>
        /// 存储和获取当前登录用户信息的属性(因为此对象在控制器中将会频繁使用，故将其封装在OperationContext对象中，以便调用)
        /// 如果未赋值使用此属性，返回null
        /// </summary>
        public MODEL.Employee UserNow
        {
            ///从Session中获取当前用户登录信息，,此集合存入Session中，方便跨页面调取
            get
            {
                object userInfo = HttpContext.Current.Session[CURRENTUSERINFO_SESSION_KEY];
                if (userInfo != null)
                {
                    return userInfo as MODEL.Employee;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[CURRENTUSERINFO_SESSION_KEY] = value;
            }
        }



        public const string USERID_COOKIE_KEY = "userid";
        /// <summary>
        /// 存储和获取点击了保持登录状态选项框用户的ID标识，此标识存入Cookie中，方便保存在客户的浏览器中
        /// 如果Cookie中值为空，则返回-1
        /// </summary>
        public int UserId
        {
            get
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[USERID_COOKIE_KEY];
                if (cookie != null && string.IsNullOrEmpty(cookie.Value) == false)
                    return cookie.Value.ToDecryptString().AsInt();
                else
                    return -1;
                
            }
            set
            {
                HttpCookie cookie = new HttpCookie(USERID_COOKIE_KEY, value.ToString().ToEncryptString());
                cookie.Expires = DateTime.Now.AddMinutes(30);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }


        public const string USERPERMISSION_KEY = "upermission";
        /// <summary>
        /// 存储和获取当前用户所有权限对象的集合,此集合存入Session中，方便跨页面调取
        /// </summary>
        public List<MODEL.Permission> UserNowPermission
        {
            get
            {
                object permissionList = HttpContext.Current.Session[USERPERMISSION_KEY];
                if (permissionList != null)
                {
                    return permissionList as List<MODEL.Permission>;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[USERPERMISSION_KEY] = value;
            }
        }



        //---------------------针对Ajax的消息的统一回复方法---------------------

        /// <summary>
        /// 返回针对Ajax 统一回复消息的方法
        /// </summary>
        /// <param name="ajaxStatu">此次请求操作的状态</param>
        /// <param name="message">请求附带的信息</param>
        /// <param name="backUrl">要跳转的Url，可为空</param>
        /// <param name="data">ajax请求要返回的数据</param>
        /// <returns></returns>
        public JsonResult BackAjaxMessage(AjaxStatu ajaxStatu, string message, string backUrl, object data=null)
        {
            AjaxMessage ajaxMessage = new AjaxMessage()
            {
                Status = ajaxStatu,
                Message = message,
                BackUrl = backUrl,
                Data = data
            };

            return new JsonResult()
            {
                Data = ajaxMessage,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        //-----------------返回Ajax请求的各种快捷方法----------
        /// <summary>
        /// Ajax请求成功
        /// </summary>
        /// <param name="message"></param>
        /// <param name="backUrl"></param>
        /// <param name="data"></param>
        public JsonResult AjaxMessageOk(string message="操作成功",string backUrl="",object data=null)
        {
            return BackAjaxMessage(AjaxStatu.OK, message, backUrl, data);
        }
        /// <summary>
        /// Ajax请求失败
        /// </summary>
        /// <param name="message"></param>
        /// <param name="backUrl"></param>
        /// <param name="data"></param>
        public JsonResult AjaxMessageFaild(string message="操作失败",string backUrl="",object data =null)
        {
           return  BackAjaxMessage(AjaxStatu.Failed, message, backUrl, data);
        }

        /// <summary>
        /// Ajax请求发生异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="backUrl"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult AjaxMessageError(string message="操作异常",string backUrl="",object data=null)
        {
            return BackAjaxMessage(AjaxStatu.Error, message, backUrl, data);
        }
        /// <summary>
        /// Ajax请求发生异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="backUrl"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult AjaxMessageError(Exception ex,string backUrl="",object data=null)
        {
            return BackAjaxMessage(AjaxStatu.Error, ex.Message, backUrl, data);
        }

        /// <summary>
        /// Ajax请求没有获得登录权限
        /// </summary>
        /// <param name="backUrl"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult AjaxMessageNoLogin(string backUrl="/Admin/User/Login",object data=null)
        {
            return BackAjaxMessage(AjaxStatu.NoLogin, "尚未登录", backUrl, data);
        }

        /// <summary>
        /// Ajax请求无权访问页面
        /// </summary>
        /// <param name="strBackUrl"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult AjaxMsgNoPermission(string strBackUrl = "", object data = null)
        {
            return BackAjaxMessage(AjaxStatu.NoPower, "您没有访问此操作的权限~~~", strBackUrl, data);
        }



        public ContentResult JsMessage(string message,string backUrl)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder("<script>alert('"+message+"');");

            if (string.IsNullOrEmpty(backUrl) == false)
            {
                stringBuilder.Append("if(window.top!=window)window.top.location=\"").Append(backUrl).Append("\";");
                stringBuilder.Append("else window.location=\"").Append(backUrl).Append("\";");
            }
            stringBuilder.Append("</script>");
            return new ContentResult()
            {
                Content = stringBuilder.ToString()
            };

        }


        //-------------------------Json序列化--------------------
        System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        public string ToJson(object obj)
        {
            return javaScriptSerializer.Serialize(obj);
        }

    }
}
