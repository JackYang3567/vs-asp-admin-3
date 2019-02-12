using Admin.Filters;
using Game.Facade;
using Game.Facade.Tools;
using Game.Kernel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Admin.Controllers
{
	public class SuperAccountController : BaseController
	{
		[CheckCustomer]
		public ActionResult Index()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult Info()
		{
			int num = TypeUtil.ObjectToInt(base.Request["gameId"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["followGameID"]);
			if (num > 0)
			{
				base.ViewBag.GameID = num;
				base.ViewBag.FollowGameID = num2;
			}
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetSuperAccounts()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["GameId"], 0);
			int num2 = TypeUtil.ObjectToInt(base.Request["FollowGameID"], 0);
			StringBuilder stringBuilder = new StringBuilder("WHERE 1=1");
			if (num > 0)
			{
				stringBuilder.AppendFormat(" AND GameID={0}", num);
			}
			if (num2 > 0)
			{
				stringBuilder.AppendFormat(" AND FollowGameID={0}", num2);
			}
			PagerSet list = FacadeManage.aideAccountsFacade.GetList("View_SuperUser", pageIndex, pageSize, stringBuilder.ToString(), "Order by CollectDate DESC");
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonConvert.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[CheckCustomer]
		public JsonResult Edit()
		{
			int num = TypeUtil.ObjectToInt(base.Request["gameId"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["followGameID"]);
			if (num == 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "超权ID错误"
				});
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["dwGameID"] = num;
			dictionary["dwFollowGameID"] = num2;
			dictionary["strOperator"] = user.Username;
			dictionary["strErr"] = "";
			Message message = FacadeManage.aideAccountsFacade.ExcuteByProce("RYNativeWebDB.dbo.P_EditSuperUser", dictionary);
			if (message.Success)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "操作成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = message.Content
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult Cancel()
		{
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			if (string.IsNullOrEmpty(text))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有选择操作项"
				});
			}
			if (CheckHelper.CheckIds(text))
			{
				FacadeManage.aideAccountsFacade.QXSuperAccount(text, user.Username);
				return Json(new
				{
					IsOk = true,
					Msg = "取消成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "参数不正确"
			});
		}

		[CheckCustomer]
		public ActionResult Log()
		{
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetLog()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["searchType"], 0);
			string text = TypeUtil.ObjectToString(base.Request["search"]);
			StringBuilder stringBuilder = new StringBuilder("WHERE 1=1");
			if (text != "")
			{
				switch (num)
				{
				case 1:
					stringBuilder.AppendFormat(" AND MyFGameID={0}", Convert.ToInt32(text));
					break;
				case 2:
					stringBuilder.AppendFormat(" AND FGameID={0}", Convert.ToInt32(text));
					break;
				case 3:
					stringBuilder.AppendFormat(" AND Operator='{0}'", text);
					break;
				}
			}
			PagerSet list = FacadeManage.aideNativeWebFacade.GetList("T_SuperUserLog", pageIndex, pageSize, stringBuilder.ToString(), "Order by CollectDate DESC");
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}
	}
}
