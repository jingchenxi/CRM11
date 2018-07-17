using CRM11.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM11.Service
{
    public partial class ServiceDbSession:CRM11.IService.IServiceDbSession
    {
        public int SaveChanges()
        {
            IRespository.IDbSession dbSession =  System.Runtime.Remoting.Messaging.CallContext.GetData(typeof(IRespository.IDbSession).FullName) as IRespository.IDbSession;
            if (dbSession == null)
            {
                dbSession = DI.GetObject<IRespository.IDbSession>("dalSession");
                System.Runtime.Remoting.Messaging.CallContext.SetData(typeof(IRespository.IDbSession).FullName, dbSession);
            }

            return  dbSession.SaveChanges();

            
        }
    }
}
