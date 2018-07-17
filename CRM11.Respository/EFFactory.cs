using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace CRM11.Respository
{
    internal class EFFactory
    {

        //private static DbContext context = new NewCRMEntities();

        public static DbContext  GetEntityContext()
        {
            object context = CallContext.GetData(typeof(NewCRMEntities).FullName);

            if (context == null)
            {
                context = new NewCRMEntities();
                CallContext.SetData(typeof(NewCRMEntities).FullName, context);
            }

            return context as DbContext;
        }
    }
}
