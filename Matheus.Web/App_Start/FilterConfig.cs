using Matheus.Web.Config;
using Matheus.Web.Config.Filters;
using System.Web;
using System.Web.Mvc;

namespace Matheus.Web
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
