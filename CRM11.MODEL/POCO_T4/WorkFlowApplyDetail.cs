//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

  namespace CRM11.MODEL
{
using System;
using System.Collections.Generic;

public partial class WorkFlowApplyDetail
{
      public WorkFlowApplyDetail ToPOCO(){
			return new WorkFlowApplyDetail(){
								wfadId=this.wfadId,
				wfadApplyId=this.wfadApplyId,
				wfadCurNodeId=this.wfadCurNodeId,
				wfadUserId=this.wfadUserId,
				wfadContent=this.wfadContent,
				wfadOperation=this.wfadOperation,
				wfadAddTime=this.wfadAddTime,
				wfadIsDel=this.wfadIsDel,
			};


       }


}
}