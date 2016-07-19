using System.Web;
using System.Web.Mvc;

namespace PPI.Core.Web
{
    public class FilterConfig
    {
        [Log]
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
