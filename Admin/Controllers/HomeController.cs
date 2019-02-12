using Admin.Filters;
using Game.Facade;
using Game.Facade.Tools;
using Game.Utils;
using System.Data;
using System.Web.Mvc;

namespace Admin.Controllers
{
	public class HomeController : BaseController
	{
		[CheckCustomer]
		public ActionResult Index()
		{
			base.ViewData["munetb"] = null;
			base.ViewData["muneitemtb"] = null;
			base.ViewData["user"] = null;
			base.ViewBag.RoleName = "";
			base.ViewBag.Username = "";
			base.ViewBag.IsBand = 0;
			base.ViewBag.ID = 0;
			DataSet dataSet = null;
			if (user != null && user.UserID > 0)
			{
				if (user != null && user.UserID > 0)
				{
					base.ViewBag.Username = user.Username;
					base.ViewBag.IsBand = user.IsBand;
					if (user.UserID == 1 || user.RoleID == 1)
					{
						base.ViewBag.RoleName = "超级管理员";
						user.RoleID = 1;
					}
					else
					{
						PlatformManagerFacade platformManagerFacade = new PlatformManagerFacade();
						base.ViewBag.RoleName = platformManagerFacade.GetRolenameByRoleID(user.RoleID);
					}
					dataSet = FacadeManage.aidePlatformManagerFacade.GetMenuByRoleID(user.RoleID);
					if (dataSet != null && dataSet.Tables.Count > 0)
					{
						DataView dataView = new DataView(dataSet.Tables[0]);
						dataView.Sort = "OrderNo asc";
						dataView.ToTable();
						base.ViewData["muneitemtb"] = dataView.ToTable();
					}
				}
				base.ViewBag.ID = user.UserID;
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult Welcome()
		{
			base.ViewBag.UserName = "";
			base.ViewBag.PreLogonDate = "";
			base.ViewBag.PreLogonip = "";
			base.ViewBag.PreLogonTimes = 0;
			base.ViewBag.PreLogonAddress = "";
			base.ViewBag.RoleName = "";
			base.ViewData["data"] = null;
			base.ViewBag.RoleID = user.RoleID;
			if (user != null && user.UserID > 0 && user.RoleID == 1)
			{
				user = FacadeManage.aidePlatformManagerFacade.GetUserByUserID(user.UserID);
				base.ViewBag.UserName = user.Username;
				base.ViewBag.PreLogonDate = user.PreLogintime.ToString();
				base.ViewBag.PreLogonip = user.LastLoginIP;
				base.ViewBag.PreLogonTimes = user.LoginTimes;
				base.ViewBag.PreLogonAddress = "";
				DataTable dataTable = FacadeManage.aideAccountsFacade.SystemCount();
				if (dataTable != null && dataTable.Rows.Count > 0)
				{
					base.ViewData["data"] = dataTable;
				}
				return View();
			}
			return Content("欢迎您：" + user.Username);
		}

		public ActionResult OutLogin()
		{
			FacadeManage.aidePlatformManagerFacade.UserLogout();
			return RedirectToAction("Index", "Login");
		}

		public JsonResult GetNewMessageAndNewOrder()
		{
			return Json(new
			{
				IsOk = true,
				Msg = "登录成功"
			});
		}

		[CheckCustomer]
		public JsonResult BindIp()
		{
			int num = TypeUtil.ObjectToInt(base.Request["isbind"]);
			if (num >= 0 && user.UserID > 0)
			{
				if (user == null)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "操作失败",
						Url = "/Login/index"
					});
				}
				user.BandIP = GameRequest.GetUserIP();
				user.IsBand = num;
				FacadeManage.aidePlatformManagerFacade.BindIP(user);
				FacadeManage.aidePlatformManagerFacade.SaveUserCache(user);
				return Json(new
				{
					IsOk = true,
					Msg = "操作成功",
					IsBind = num
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "参数出错"
			});
		}

		[AntiSqlInjection]
		public JsonResult GetUndoTradeInfo(int doType)
		{
			int undoApplyOrderCount = FacadeManage.aideAccountsFacade.GetUndoApplyOrderCount();
			int undoAgentDrawCount = FacadeManage.aideAccountsFacade.GetUndoAgentDrawCount();
			int unDoGameFeedbackInfoCount = FacadeManage.aideNativeWebFacade.GetUnDoGameFeedbackInfoCount();
			return Json(new
			{
				applyordercount = undoApplyOrderCount,
				agentdrawcount = undoAgentDrawCount,
				UndoMessageCount = unDoGameFeedbackInfoCount
			});
		}
	}
}
