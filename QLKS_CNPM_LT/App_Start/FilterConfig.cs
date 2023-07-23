using System.Web;
using System.Web.Mvc;

namespace QLKS_CNPM_LT
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
