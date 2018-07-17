using CRM11.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM11.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CRM11.IService.IServiceDbSession serviceDbSession = DI.GetObject<IService.IServiceDbSession>("bllSession");

            var list = serviceDbSession.Employee.Where(d=>true);

            return View(list);
        }

       
    }
}