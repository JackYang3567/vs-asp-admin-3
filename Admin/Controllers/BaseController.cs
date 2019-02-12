using Game.Entity.PlatformManager;
using Game.Facade;
using Game.Facade.Tools;
using System.Web.Mvc;
using System.Web.Routing;

namespace Admin.Controllers
{
	public class BaseController : Controller
	{
		public Base_Users user;

		protected override void Initialize(RequestContext requestContext)
		{
			base.Initialize(requestContext);
			user = FacadeManage.aidePlatformManagerFacade.GetUserInfoFromCache();
			if (user != null && user.UserID > 0 && (user.UserID == 1 || user.RoleID > 0))
			{
				if (TypeUtil.ObjectToInt(base.Request["ModuleID"]) > 0)
				{
					user.MoudleID = TypeUtil.ObjectToInt(base.Request["ModuleID"]);
				}
				FacadeManage.aidePlatformManagerFacade.SaveUserCache(user);
			}
		}

		protected override void OnException(ExceptionContext filterContext)
		{
			base.OnException(filterContext);
		}
	}
}
