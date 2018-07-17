using CRM11.UI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace CRM11.UI.Areas.Admin.Controllers
{
    public class RoleController : BaseController
    {
        // GET: Admin/Role
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection forms)
        {
            int pageindex = Request.Form["page"].AsInt();
            int pagesize = Request.Form["rows"].AsInt();

            var pageData = OperContext.BllSession.Role.WherePaged(pageindex, pagesize, o => o.roleIsDel == false, o => o.roleId, true, "Department");
            pageData.rows = pageData.rows.Select(o => o.ToPOCO(true)).ToList();

            return Content(OperContext.ToJson(pageData));

        }

        /// <summary>
        /// 显示权限树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SetRolePer(int id)
        {
            ModelView.SetRolePerViewModel setRolePerViewModel = new ModelView.SetRolePerViewModel();
            setRolePerViewModel.RoleId = id;
            setRolePerViewModel.AllPers = OperContext.BllSession.Permission.Where(p => p.perIsDel == false).ToList();

            setRolePerViewModel.RolePers = OperContext.BllSession.RolePerRelationship.Where(o => o.rprRoleId == id).Select(o => o.Permission).ToList();

            return View(setRolePerViewModel);
        }

        /// <summary>
        /// 设置权限
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetRolePer()
        {
            int roleId = Request.Form["roleId"].AsInt();
            List<int> newPerIds = Request.Form["newPerIds"].Split(new char[1] {','},StringSplitOptions.RemoveEmptyEntries).Select(o=>o.AsInt()).ToList();

            var oldPerList = OperContext.BllSession.RolePerRelationship.Where(o => o.rprRoleId == roleId).ToList();

            for (int i = oldPerList.Count-1; i>=0; i--)
            {
                var oldPer = oldPerList[i];
                if (newPerIds.Contains(oldPer.rprPerId))
                {
                    newPerIds.Remove(oldPer.rprPerId);
                    oldPerList.Remove(oldPer);
                }

            }

            newPerIds.ForEach(newPerId =>
            OperContext.BllSession.RolePerRelationship.Add(new MODEL.RolePerRelationship { rprPerId = newPerId, rprRoleId = roleId })
            );

            oldPerList.ForEach(oldPer =>
            OperContext.BllSession.RolePerRelationship.Remove(oldPer)
            );

            OperContext.BllSession.SaveChanges();
            return OperContext.AjaxMessageOk();
        }
    }
}