﻿//------------------------------------------------------------------------------
// <auto-generated>
// 数据仓储接口，作用：
// 1.调用EF容器 批量 执行 sql语句
// 2.方便通过 子接口属性直接 获取 对应数据表的操作接口对象
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRM11.IService
{
    
    public partial interface IServiceDbSession
    {       
         
           CRM11.IService.IDepartmentService Department 
    	  { 
             get;        
    	  }
    
         
           CRM11.IService.IEmployeeService Employee 
    	  { 
             get;        
    	  }
    
         
           CRM11.IService.IEmpRoleRelationService EmpRoleRelation 
    	  { 
             get;        
    	  }
    
         
           CRM11.IService.IPermissionService Permission 
    	  { 
             get;        
    	  }
    
         
           CRM11.IService.IRoleService Role 
    	  { 
             get;        
    	  }
    
         
           CRM11.IService.IRolePerRelationshipService RolePerRelationship 
    	  { 
             get;        
    	  }
    
         
           CRM11.IService.IVipPermssionService VipPermssion 
    	  { 
             get;        
    	  }
    
         
           CRM11.IService.IWorkFLowService WorkFLow 
    	  { 
             get;        
    	  }
    
         
           CRM11.IService.IWorkFlowApplyService WorkFlowApply 
    	  { 
             get;        
    	  }
    
         
           CRM11.IService.IWorkFlowApplyDetailService WorkFlowApplyDetail 
    	  { 
             get;        
    	  }
    
         
           CRM11.IService.IWorkFlowBLLBranchNodeService WorkFlowBLLBranchNode 
    	  { 
             get;        
    	  }
    
         
           CRM11.IService.IWorkFlowNodeService WorkFlowNode 
    	  { 
             get;        
    	  }
    
         
           CRM11.IService.IWorkFlowNodeRoleService WorkFlowNodeRole 
    	  { 
             get;        
    	  }
    
    }
}
