﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM11.Utility
{
    /// <summary>
    /// 自定义一个跳过权限特性，用于标识需要跳过权限验证的控制器和动作方法
    /// </summary>
    public class SkipPermissionAttribute:Attribute
    {
    }
}