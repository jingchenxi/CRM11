using System.Web;
using System.Web.Mvc;

namespace CRM11.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new Filters.CheckPermissionAttribute());

            filters.Add(new HandleErrorAttribute());
        }
    }
}
