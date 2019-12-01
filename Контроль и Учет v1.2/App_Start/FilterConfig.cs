using System.Web;
using System.Web.Mvc;

namespace Контроль_и_Учет_v1._2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
