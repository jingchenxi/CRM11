using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM11.IRespository
{
    public partial interface IDbSession
    {
        /// <summary>
        /// 1.0 调用EF容器 批量 执行 sql语句
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}
