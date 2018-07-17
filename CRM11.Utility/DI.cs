using Spring.Context;
using Spring.Context.Support;
using System;

namespace CRM11.Utility
{

    public class DI
    {
        [ThreadStatic]
        public static IApplicationContext objFactory = null;

        public static IApplicationContext GetFactory()
        {
            if (objFactory == null)
            {
                objFactory = ContextRegistry.GetContext();
            }
            return objFactory;
        }

        public static T GetObject<T>(string objectName) where T:class
        {
            return GetFactory().GetObject<T>(objectName);
        }
    }
}
