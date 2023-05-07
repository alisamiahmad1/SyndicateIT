using SyndicateIT.Filters;
using System.Web;
using System.Web.Mvc;

namespace SyndicateIT
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
           // filters.Add(new InitializeSimpleMembershipAttribute());
        }
    }
}
