using System;
using System.Web.Mvc;

namespace GameApi
{
	public class FilterConfig
	{
		public FilterConfig()
		{
		}

		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}