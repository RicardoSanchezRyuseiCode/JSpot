using System.Web;
using System.Web.Mvc;

namespace Ryusei.JSpot.Auth.WebApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
