using CRM11.IRespository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM11.Respository
{
    public partial class DbSession:IDbSession
    {
      
        /// <summary>
        /// 调用EF容器 批量执行sql 语句
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            //从线程中获取DbContext对象
            return EFFactory.GetEntityContext().SaveChanges();

        }

    }
}
