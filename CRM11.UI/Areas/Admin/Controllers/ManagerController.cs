using CRM11.MODEL;
using CRM11.UI.Helper;
using CRM11.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM11.UI.Areas.Admin.Controllers
{
    public class ManagerController : BaseController
    {
        [HttpGet]
        // GET: Admin/Manager
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost,SkipPermission]
        /// <summary>
        /// 生成菜单树
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMenu()
        {
            //从Session中获取当前用户所拥有的权限,并在其中筛选出生成菜单树需要的权限
            //perOperationType为1的是生成菜单的权限，为2的是生成按钮的权限
            var menulist = OperContext.UserNowPermission.FindAll(p => p.perOperationType == 1);

            //创建根节点
            TreeNode rootNode = OperContext.BllSession.Permission.Where(p => p.perParent == 0 && p.perOperationType == 1)
                .SingleOrDefault().ConvertToTreeNode();

            rootNode.children = FindSonTreeNodes(menulist, rootNode.id);

            return Content("[" + OperContext.ToJson(rootNode) + "]");
        }

        /// <summary>
        /// 递归生成所有的菜单树子节点
        /// </summary>
        /// <param name="menulist">生成菜单树需要的权限</param>
        /// <param name="id">父节点的id</param>
        /// <returns></returns>
        private List<TreeNode> FindSonTreeNodes(List<Permission> menulist, int id)
        {
            List<TreeNode> SonNodes = null;

            foreach (var item in menulist)
            {
                //如果传入的父节点的Id等于当前遍历节点的perParent
                if (item.perParent == id)
                {
                    //如果子节点集合SonNodes为空，则实例化
                    if (SonNodes == null) SonNodes = new List<TreeNode>();
                    //将当前遍历Permission转换为TreeNode对象
                    TreeNode sonNode = item.ConvertToTreeNode();
                    //将转换成功的sonNode加入子节点集合
                    SonNodes.Add(sonNode);
                    //递归为当前子节点 查找 属于他的子节点
                    sonNode.children =  FindSonTreeNodes(menulist, sonNode.id);

                }
            }

            return SonNodes;
        }
    }
}