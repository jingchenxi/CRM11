using CRM11.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM11.UI.Extension
{
    public static class HtmlExtension
    {
        /// <summary>
        ///  1.0 获取 当前登录用户 拥有的受访页面的 按钮
        /// </summary>
        /// <returns></returns>
        public static MvcHtmlString GetSonBtnJs(this HtmlHelper htmlhelper)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder(300);

            foreach (var item in htmlhelper.ViewBag.sonBtnData as List<MODEL.Permission>)
            {
                stringBuilder.Append("{");
                stringBuilder.Append("iconCls:'" + item.perIco + "',");
                stringBuilder.Append("text:'" + item.perName + "',");
                stringBuilder.Append("handler:" + item.perJsMethodName + "");
                stringBuilder.Append("},'-',");
            }

            return new MvcHtmlString(stringBuilder.ToString());
        }


        public static bool IsBtnJsMethodExist(this HtmlHelper htmlHelper,string jsMethodName)
        {
           var model = (htmlHelper.ViewBag.sonBtnData as List<MODEL.Permission>).FirstOrDefault(o => o.perJsMethodName.IsSame(jsMethodName));

            return model != null;
        }
    }
}