using Game.Facade.Tools;
using System.Web.Mvc;

namespace Admin.Filters
{
	public class AntiSqlInjectionAttribute : FilterAttribute, IActionFilter
	{
		public void OnActionExecuted(ActionExecutedContext filterContext)
		{
		}

		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
			ParameterDescriptor[] parameters = filterContext.ActionDescriptor.GetParameters();
			ParameterDescriptor[] array = parameters;
			foreach (ParameterDescriptor parameterDescriptor in array)
			{
				if (parameterDescriptor.ParameterType == typeof(string) && filterContext.ActionParameters[parameterDescriptor.ParameterName] != null)
				{
					filterContext.ActionParameters[parameterDescriptor.ParameterName] = FiltUtil.GetSafeSQL(filterContext.ActionParameters[parameterDescriptor.ParameterName].ToString());
				}
			}
		}
	}
}
