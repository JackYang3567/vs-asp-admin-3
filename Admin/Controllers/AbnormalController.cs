using Admin.Filters;
using Game.Entity;
using Game.Facade;
using Game.Facade.Tools;
using Game.Kernel;
using Game.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Mvc;

namespace Admin.Controllers
{
	public class AbnormalController : BaseController
	{
		public ActionResult Index()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult FuList()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetFuList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY UserID DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE Score<0 OR InsureScore<0");
			PagerSet list = FacadeManage.aideAccountsFacade.GetList("View_UserInfo", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult ClearZero()
		{
			string value = TypeUtil.ObjectToString(base.Request["cid"]);
			if (string.IsNullOrEmpty(value))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有选择要操作的项"
				});
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["dwUserIDs"] = value;
			dictionary["dwOperater"] = user.Username;
			dictionary["strClientIP"] = GameRequest.GetUserIP();
			dictionary["strErr"] = "";
			Message message = FacadeManage.aideAccountsFacade.ExcuteByProce("RYTreasureDB..NET_PW_ZeroScore", dictionary);
			if (message.Success)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "清零成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = message.Content
			});
		}

		[CheckCustomer]
		public ActionResult TopList()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult GoldChart()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetTopList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY SwapScore DESC";
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("V_TodayRecordGold", pageIndex, pageSize, "", orderby);
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[CheckCustomer]
		public JsonResult ScoreTimeList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["uid"], 0);
			if (num < 1)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数错误！"
				});
			}
			DataTable o = FacadeManage.aideTreasureFacade.GetList("(SELECT SUM(PresentScore) SwapScore,DATEPART(Hour,CollectDate) CollectTime FROM  View_PresentInfo WHERE DateDiff(dd,CollectDate,GETDATE())=0 AND UserID=" + num + " GROUP BY DATEPART(Hour,CollectDate)) AS t", 1, 24, " ", " ORDER by CollectTime  ").PageSet.Tables[0];
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Data = JsonHelper.SerializeObject(o)
			});
		}

		[CheckCustomer]
		public ActionResult Setting()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult ExceptionList()
		{
			DateTime now = DateTime.Now;
			base.ViewBag.StartTime = now.ToString("yyyy-MM-dd");
			base.ViewBag.EndTime = now.ToString("yyyy-MM-dd");
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetExceptionList()
		{
			string text = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string text2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			if (text == "" || text2 == "")
			{
				return Json(new
				{
					IsOk = false,
					Msg = "查询起止时间必须输入"
				});
			}
			string text3 = TypeUtil.ObjectToString(base.Request["order"]);
			string str = TypeUtil.ObjectToString(base.Request["sort"]);
			string orderby = "ORDER BY ScoreSort DESC";
			if (text3 != "")
			{
				orderby = "ORDER BY " + text3 + " " + str;
			}
			PagerSet list = FacadeManage.aideAccountsFacade.GetList("Fn_GetUserGoldExcept('" + Convert.ToDateTime(text) + "','" + Convert.ToDateTime(text2).AddDays(1.0) + "')", 1, 100, "", orderby);
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		public ActionResult AccountScoreList()
		{
			return View();
		}

		public JsonResult GetAccountScoreList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int num2 = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num3 = TypeUtil.ObjectToInt(base.Request["DateType"]);
			string s = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string s2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			int num4 = TypeUtil.ObjectToInt(base.Request["sort"]);
			string safeSQL = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["KeyWord"]));
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			string text = "";
			string text2 = "";
			switch (num3)
			{
			case 0:
				if (!string.IsNullOrEmpty(safeSQL))
				{
					dictionary.Add("Acc", safeSQL);
				}
				else
				{
					dictionary.Add("Acc", "");
				}
				if (DateTime.TryParse(s, out result) && DateTime.TryParse(s2, out result2))
				{
					if (result > result2)
					{
						text = DateTime.Now.ToString("yyyy-MM-dd");
						text2 = DateTime.Now.ToString("yyyy-MM-dd");
						dictionary.Add("BTime", text);
						dictionary.Add("ETime", text2);
					}
					else
					{
						text = result.ToString("yyyy-MM-dd");
						text2 = result2.ToString("yyyy-MM-dd");
						dictionary.Add("BTime", text);
						dictionary.Add("ETime", text2);
					}
				}
				else
				{
					text = DateTime.Now.ToString("yyyy-MM-dd");
					text2 = DateTime.Now.ToString("yyyy-MM-dd");
					dictionary.Add("BTime", text);
					dictionary.Add("ETime", text2);
				}
				break;
			case 1:
				dictionary.Add("Acc", "");
				text = DateTime.Now.ToString("yyyy-MM-dd");
				text2 = DateTime.Now.ToString("yyyy-MM-dd");
				dictionary.Add("BTime", text);
				dictionary.Add("ETime", text2);
				break;
			case 2:
				dictionary.Add("Acc", "");
				text = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd");
				text2 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd");
				dictionary.Add("BTime", text);
				dictionary.Add("ETime", text2);
				break;
			case 3:
				dictionary.Add("Acc", "");
				text = DateTime.Now.AddDays((double)(-Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d")))).ToString("yyyy-MM-dd");
				text2 = DateTime.Now.AddDays((double)(-Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d")))).AddDays(6.0).ToString("yyyy-MM-dd");
				dictionary.Add("BTime", text);
				dictionary.Add("ETime", text2);
				break;
			case 4:
				dictionary.Add("Acc", "");
				text = DateTime.Now.AddDays((double)(-7 - Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d")))).ToString("yyyy-MM-dd");
				text2 = DateTime.Now.AddDays((double)(-7 - Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d")))).AddDays(6.0).ToString("yyyy-MM-dd");
				dictionary.Add("BTime", text);
				dictionary.Add("ETime", text2);
				break;
			}
			dictionary.Add("DescOrAsc", num4.ToString());
			dictionary.Add("PageSize", num2.ToString());
			dictionary.Add("PageIndex", num.ToString());
			dictionary.Add("PageCount|RecordCount", "");
			PagerSet pager = FacadeManage.aideTreasureFacade.GetPager(dictionary, "P_QueryUserRDScore");
			List<object> list = new List<object>();
			if (pager.PageSet.Tables[0] != null && pager.PageSet.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in pager.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						UserID = TypeUtil.ObjectToInt(row["UserID"]),
						Accounts = TypeUtil.ObjectToString(row["Accounts"]),
						NickName = TypeUtil.ObjectToString(row["NickName"]),
						Score = row["Score"],
						StartTime = text,
						EndTime = text2
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = pager.RecordCount,
				Data = list
			});
		}

		public ActionResult AccountScoreDetail()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			string text = TypeUtil.ObjectToString(base.Request["stTime"]);
			string text2 = TypeUtil.ObjectToString(base.Request["endTime"]);
			string text3 = TypeUtil.ObjectToString(base.Request["account"]);
			string text4 = TypeUtil.ObjectToString(base.Request["nick"]);
			base.ViewBag.Account = text3;
			base.ViewBag.NickName = text4;
			base.ViewBag.Times = text + "至" + text2;
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("UserID", num.ToString());
			if (DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2))
			{
				if (result > result2)
				{
					dictionary.Add("BTime", DateTime.Now.ToString("yyyy-MM-dd"));
					dictionary.Add("ETime", DateTime.Now.ToString("yyyy-MM-dd"));
				}
				else
				{
					dictionary.Add("BTime", result.ToString("yyyy-MM-dd"));
					dictionary.Add("ETime", result2.ToString("yyyy-MM-dd"));
				}
			}
			else
			{
				dictionary.Add("BTime", result.ToString("yyyy-MM-dd"));
				dictionary.Add("ETime", result2.ToString("yyyy-MM-dd"));
			}
			dictionary.Add("StrErr|", "");
			SQLResult table = FacadeManage.aideTreasureFacade.GetTable(dictionary, "P_QueryUserGameRDScore");
			DataTable data = table.Data;
			base.ViewData["data"] = data;
			return View();
		}

		public JsonResult GetAccountScoreDetail()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			string s = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string s2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("UserID", num.ToString());
			if (DateTime.TryParse(s, out result) && DateTime.TryParse(s2, out result2))
			{
				if (result > result2)
				{
					dictionary.Add("BTime", DateTime.Now.ToString("yyyy-MM-dd"));
					dictionary.Add("ETime", DateTime.Now.ToString("yyyy-MM-dd"));
				}
				else
				{
					dictionary.Add("BTime", result.ToString("yyyy-MM-dd"));
					dictionary.Add("ETime", result2.ToString("yyyy-MM-dd"));
				}
			}
			else
			{
				dictionary.Add("BTime", result.ToString("yyyy-MM-dd"));
				dictionary.Add("ETime", result2.ToString("yyyy-MM-dd"));
			}
			dictionary.Add("StrErr|", "");
			SQLResult table = FacadeManage.aideTreasureFacade.GetTable(dictionary, "P_QueryUserGameRDScore");
			return Json(new
			{
				IsOk = table.Success,
				Msg = table.Msg,
				Total = 0,
				Data = JsonConvert.SerializeObject(table.Data)
			});
		}

		public ActionResult AccountGameScore()
		{
			int id = TypeUtil.ObjectToInt(base.Request["param"]);
			string text = TypeUtil.ObjectToString(base.Request["Account"]);
			DataTable accountScoreById = FacadeManage.aideTreasureFacade.GetAccountScoreById(id);
			base.ViewBag.Account = text;
			base.ViewData["data"] = accountScoreById;
			return View();
		}
	}
}
