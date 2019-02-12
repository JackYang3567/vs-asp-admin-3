using Admin.Filters;
using Game.Entity.PlatformManager;
using Game.Facade;
using Game.Facade.Tools;
using Game.Kernel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Mvc;

namespace Admin.Controllers
{
	public class OperationLogController : BaseController
	{
		[CheckCustomer]
		public ActionResult Index()
		{
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult SystemSecurityList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string text = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			string text2 = TypeUtil.ObjectToString(base.Request["Remark"]);
			string text3 = TypeUtil.ObjectToString(base.Request["OpName"]);
			string orderby = "ORDER BY OperatingTime DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (!string.IsNullOrEmpty(text))
			{
				stringBuilder.AppendFormat(" AND OperatingAccounts='{0}'", text);
			}
			if (!string.IsNullOrEmpty(text3))
			{
				stringBuilder.AppendFormat(" AND OperatingName='{0}'", text3);
			}
			if (!string.IsNullOrEmpty(text2))
			{
				stringBuilder.AppendFormat(" AND Remark like '%{0}%'", text2);
			}
			PagerSet systemSecurityList = FacadeManage.aidePlatformManagerFacade.GetSystemSecurityList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (systemSecurityList != null && systemSecurityList.PageSet != null && systemSecurityList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in systemSecurityList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						RecordID = TypeUtil.ObjectToString(row["RecordID"]),
						OperatingTime = TypeUtil.ObjectToDateTime(row["OperatingTime"]).ToString("yyyy-MM-dd HH:mm:ss"),
						OperatingAccounts = TypeUtil.ObjectToString(row["OperatingAccounts"]),
						OperatingName = TypeUtil.ObjectToString(row["OperatingName"]),
						Address = TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(row["OperatingIP"])),
						OperatingIP = TypeUtil.ObjectToString(row["OperatingIP"]),
						Remark = TypeUtil.ObjectToString(row["Remark"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((systemSecurityList.PageSet != null && systemSecurityList.PageSet.Tables != null && systemSecurityList.PageSet.Tables[0].Rows.Count != 0) ? systemSecurityList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public ActionResult SystemSecurityInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["id"]);
			SystemSecurity value = new SystemSecurity();
			if (num > 0)
			{
				value = FacadeManage.aidePlatformManagerFacade.GetSystemSecurityById(num);
			}
			base.ViewData["data"] = value;
			return View();
		}

		[CheckCustomer]
		public ActionResult ActionLog()
		{
			DataTable logType = FacadeManage.aidePlatformManagerFacade.GetLogType();
			return View(logType);
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetActionLogs()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string text = TypeUtil.ObjectToString(base.Request["optype"]);
			string text2 = TypeUtil.ObjectToString(base.Request["lname"]);
			string s = TypeUtil.ObjectToString(base.Request["sDate"]);
			string s2 = TypeUtil.ObjectToString(base.Request["eDate"]);
			string text3 = TypeUtil.ObjectToString(base.Request["info"]);
			string text4 = TypeUtil.ObjectToString(base.Request["ip"]);
			string orderby = "ORDER BY RecordID DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (!string.IsNullOrEmpty(text) && text.Trim() != "全部")
			{
				stringBuilder.AppendFormat(" AND OperatingName='{0}'", text);
			}
			if (!string.IsNullOrEmpty(text2))
			{
				stringBuilder.AppendFormat(" AND OperatingAccounts = '{0}'", text2);
			}
			if (!string.IsNullOrEmpty(text4))
			{
				stringBuilder.AppendFormat(" AND OperatingIP ='{0}'", text4);
			}
			if (!string.IsNullOrEmpty(text3))
			{
				stringBuilder.AppendFormat(" AND Remark like '%{0}%'", text3);
			}
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			if (DateTime.TryParse(s, out result) && DateTime.TryParse(s2, out result2) && result <= result2)
			{
				stringBuilder.AppendFormat(" AND OperatingTime>='{0}' and OperatingTime<='{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).AddSeconds(-1.0).ToString("yyyy-MM-dd HH:mm:ss"));
			}
			PagerSet systemSecurityList = FacadeManage.aidePlatformManagerFacade.GetSystemSecurityList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = systemSecurityList.RecordCount,
				Data = JsonHelper.SerializeObject(systemSecurityList.PageSet.Tables[0])
			});
		}
	}
}
