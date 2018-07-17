using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM11.UI.Helper
{
    /// <summary>
    /// 通过EnumHelper管理自定义枚举数据
    /// 自定义枚举数据 ： 主要用来保存 数据库中没有保存 但 实际开发中使用的 枚举数据
    /// 如 权限表的请求方式：1-GET 2-POST 3-BOTH
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        ///权限的 请求方式
        /// </summary>
        public static class FormMethod
        {
            public static int GET = 1;
            public static int POST = 2;
            public static int BOTH = 3;

            static List<SelectListItem> _dropListData = null;
            public static List<SelectListItem> DropListData
            {
                get
                {
                    if (_dropListData == null)
                        _dropListData = new List<SelectListItem>() {
                             new SelectListItem{Value=GET.ToString(), Text="GET"},
                             new SelectListItem{Value=POST.ToString(),Text="POST" },
                             new SelectListItem{Value=BOTH.ToString(),Text="BOTH" }
                        };
                    return _dropListData;
                }
            }
        }

        /// <summary>
        /// 权限的 类型
        /// </summary>
        public static class OperationType
        {
            public static int MENU = 1;
            public static int BUTTON = 2;
            public static int AJAX = 3;

            static List<SelectListItem> _dropListData = null;
            /// <summary>
            /// 下拉框数据
            /// </summary>
            public static List<SelectListItem> DropListData
            {
                get
                {
                    if (_dropListData == null)
                        _dropListData = new List<System.Web.Mvc.SelectListItem>() {
                       new SelectListItem(){Value="1",Text="MENU" },
                       new SelectListItem(){Value="2",Text="BUTTON" },
                       new SelectListItem(){Value="3",Text="AJAX" }
                    };
                    return _dropListData;
                }
            }

        }

        /// <summary>
        /// 权限图标 类选择器名称
        /// </summary>
        public static class IconClassName
        {
            public static string IconAdd = "icon-add";
            public static string IconEdit = "icon-edit";
            public static string IconRemove = "icon-remove";
            public static string IconCut = "icon-cut";
            public static string IconSave = "icon-save";
            public static string IconOk = "icon-ok";
            public static string IconNo = "icon-no";
            public static string IconCancel = "icon-cancel";
            public static string IconSearch = "icon-search";
            public static string IconTip = "icon-tip";

            static List<SelectListItem> _dropListData = null;
            /// <summary>
            /// 下拉框数据
            /// </summary>
            public static List<SelectListItem> DropListData
            {
                get
                {
                    if (_dropListData == null)
                        _dropListData = new List<System.Web.Mvc.SelectListItem>() {
                       new SelectListItem(){Value="icon-add",Text="icon-add" },
                       new SelectListItem(){Value="icon-edit",Text="icon-edit" },
                       new SelectListItem(){Value="icon-remove",Text="icon-remove" },
                       new SelectListItem(){Value="icon-cut",Text="icon-cut" },
                       new SelectListItem(){Value="icon-save",Text="icon-save" },
                       new SelectListItem(){Value="icon-ok",Text="icon-ok" },
                       new SelectListItem(){Value="icon-no",Text="icon-no" },
                       new SelectListItem(){Value="icon-cancel",Text="icon-cancel" },
                       new SelectListItem(){Value="icon-search",Text="icon-search" },
                       new SelectListItem(){Value="icon-tip",Text="icon-tip" }
                    };
                    return _dropListData;
                }
            }
        }


    }
}