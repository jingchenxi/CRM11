using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM11.UI.Helper
{
    public class BaseController:Controller
    {
        public OperationContext OperContext
        {
            get
            {
                return OperationContext.Current;
            }
        }
    }
}