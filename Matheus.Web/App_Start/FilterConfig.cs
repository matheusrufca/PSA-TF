using Matheus.Web.Configs;
using Matheus.Web.Configs.Filters;
using System.Web;
using System.Web.Mvc;

namespace Matheus.Web
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
			//filters.Add(new JsonResponseActionFilterAttribute());
		}
	}
}
