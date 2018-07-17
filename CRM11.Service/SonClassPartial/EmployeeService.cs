using CRM11.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM11.Service
{
    public partial class EmployeeService: IService.IEmployeeService
    {
        /// <summary>
        /// 根据用户的id标识查询此用户拥有的权限
        /// </summary>
        /// <param name="uid">用户的id标识</param>
        /// <returns></returns>
        public List<Permission> GetUserPermission(int uid)
        {
            ///根据用户Id标识在EmpRoleRelation表中查出对应的erRId(角色Id)集合
            List<int> erRId = DBSession.EmpRoleRelation.Where(e => e.erUId == uid).Select(e => e.erRId).ToList();

            //根据用户对应的角色Id集合在RolePerRelationship中查出对应的权限Id
            List<int> erPerId = DBSession.RolePerRelationship.Where(r => erRId.Contains(r.rprRoleId)).Select(r => r.rprPerId).ToList();

            //根据用户Id标识在VipPermssion表中查出对应的vpPId(vip权限Id)集合
            List<int> vipPerId = DBSession.VipPermssion.Where(p => p.vpUId == uid).Select(p => p.vpPId).ToList();

            //将用户对应的vip权限Id集合和普通权限集合合并，获得此用户所拥有的所有权限Id集合
            vipPerId.ForEach(vipPerIdItem =>
            {
                //如果普通权限中没有vip权限，则将此vip权限加入普通权限中
                if (erPerId.Contains(vipPerIdItem) == false)
                {
                    erPerId.Add(vipPerIdItem);
                }

            });

            return DBSession.Permission.Where(p => erPerId.Contains(p.perId)).ToList().Select(p=>p.ToPOCO()).ToList();

        }
    }
}
