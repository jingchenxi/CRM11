using CRM11.UI.Extension;
using CRM11.UI.Helper;
using CRM11.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace CRM11.UI.Areas.Admin.Controllers
{
    public class PermissionController : BaseController
    {
        [HttpGet]
        // GET: Admin/Permission
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        /// <summary>
        /// 返回EasyUi中datagrid组件异步请求需要的数据,实现分页功能
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPermissionList(int id)
        {
            //var perList = OperContext.BllSession.Permission
            //.Where(o => o.perIsDel == false).OrderBy(o => o.perOrder).ToList().Select(o => o.ToPOCO());
            int pageindex = Request.Form["page"].AsInt();
            int pagesize = Request.Form["rows"].AsInt();
            var perList = OperContext.BllSession.Permission.WherePaged(pageindex, pagesize, o => o.perIsDel == false && o.perParent == id, o => o.perOrder);
            perList.rows = perList.rows.Select(o => o.ToPOCO()).ToList();

            return Content(OperContext.ToJson(perList));
        }

        [HttpGet]
        public ActionResult Add()
        {
            var parPers = OperContext.BllSession.Permission.WhereAndSort(o => o.perParent <= 1 && o.perIsDel == false && o.perIsShow == true, o => o.perOrder).ToList().Select(o => new SelectListItem()
            {
                Text = o.perParent == 0 ? o.perName : "--" + o.perName,
                Value = o.perId.ToString()
            });
            ViewBag.ParentPer = parPers;
            return View();
        }

        [HttpPost]
        public ActionResult Add(ModelView.PermissionView model)
        {

            if (ModelState.IsValid == false)
            {
                return OperContext.AjaxMessageFaild("请不要禁用浏览器js!");
            }
            else
            {
                OperContext.BllSession.Permission.Add(model.ToPOCO());
                OperContext.BllSession.SaveChanges();
                return OperContext.AjaxMessageOk();
            }
        }

        [HttpGet]
        public ActionResult Modify(int id)
        {
            var modifyModel = OperContext.BllSession.Permission.Single(o => o.perId == id);
            if (modifyModel == null) { throw new Exception("找不到要修改的权限"); }

            var parPers = OperContext.BllSession.Permission.WhereAndSort(o => o.perParent <= 1 && o.perIsDel == false && o.perIsShow == true, o => o.perOrder).ToList().Select(o => new SelectListItem
            {
                Value = o.perId.ToString(),
                Text = o.perParent == 0 ? o.perName : "--" + o.perName
            });
            ViewBag.ParentPer = parPers;

            return View("Add",modifyModel.ToViewModel());
        }

        [HttpPost]
        public ActionResult Modify(int id,ModelView.PermissionView viewmodel)
        {
            if (ModelState.IsValid)
            {
                viewmodel.perId = id;
                OperContext.BllSession.Permission.Update(viewmodel.ToPOCO(), new string[] { "perParent", "perName", "perRemark", "perAreaName", "perControllerName", "perActionName", "perFormMethod", "perOperationType", "perJsMethodName", "perIco", "perIsLink", "perOrder", "perIsShow" });
                OperContext.BllSession.SaveChanges();
                return OperContext.AjaxMessageOk();
            }
            return OperContext.AjaxMessageFaild("请不要禁用浏览器js");

        }

        [HttpPost]
        public ActionResult Remove()
        {
            try
            {
                int id = Request.Form["deleteId"].AsInt();
                var model = OperContext.BllSession.Permission.Single(o => o.perId == id);
                model.perIsDel = true;
                OperContext.BllSession.SaveChanges();
                
                return OperContext.AjaxMessageOk("删除成功");
            }
            catch (Exception ex)
            {

                return OperContext.AjaxMessageError(ex);
            }

            
            
            
        }


        [HttpGet]
        public ActionResult SonIndex(int id)
        {
            ViewBag.pid = id;
            return View();
        }

        /// <summary>
        /// 自动生成排序序号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string LoadOrderNumber()
        {
            int NewOrderNum = -1;

            int parentId = Request.Form["pid"].AsInt();

            var parentModel = OperContext.BllSession.Permission.Where(o => o.perId == parentId && o.perIsDel == false).FirstOrDefault();

            var maxOderSonModel = OperContext.BllSession.Permission.Where(o => o.perParent == parentId && o.perIsDel == false).OrderByDescending(o => o.perOrder).FirstOrDefault();

            int seed = 1;
            if (parentModel.perParent == 0)
            {
                seed = 100000;
            }

            if (maxOderSonModel != null)
                NewOrderNum = maxOderSonModel.perOrder + seed;
            else
                NewOrderNum = parentModel.perOrder + seed;

            return NewOrderNum.ToString();
        }
    }
}