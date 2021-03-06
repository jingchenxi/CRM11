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
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.EmpRoleRelation = new HashSet<EmpRoleRelation>();
            this.VipPermssion = new HashSet<VipPermssion>();
        }
    
        public int empId { get; set; }
        public Nullable<int> empDepId { get; set; }
        public string empCnName { get; set; }
        public string empLoginName { get; set; }
        public string empLoginPwd { get; set; }
        public bool empSex { get; set; }
        public short empAge { get; set; }
        public string empCellPhone { get; set; }
        public string empPhone { get; set; }
        public string empAddress { get; set; }
        public bool empIsDel { get; set; }
        public System.DateTime empAddTime { get; set; }
    
        public virtual Department Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmpRoleRelation> EmpRoleRelation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VipPermssion> VipPermssion { get; set; }
    }
}
