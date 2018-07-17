using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM11.IService
{
    public partial interface IServiceDbSession
    {
        int SaveChanges();
    }
}
