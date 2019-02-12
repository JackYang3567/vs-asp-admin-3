using Admin.Filters;
using Game.Facade;
using Game.Facade.Tools;
using Game.Kernel;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Text;
using System.Web.Mvc;

namespace Admin.Controllers
{
	public class RecordController : BaseController
	{
		public ActionResult Index()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult RecordGame()
		{
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetRecordGameList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["ServerID"]);
			string text = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string text2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			string text3 = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			int num3 = TypeUtil.ObjectToInt(base.Request["SearchType"]);
			string orderby = "Order BY InsertTime DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (num2 > 0)
			{
				stringBuilder.AppendFormat(" AND ServerID={0} ", num2);
			}
			DateTime now = DateTime.Now;
			switch (num)
			{
			case 1:
				if (text != "")
				{
					stringBuilder.AppendFormat(" AND InsertTime > '{0}'", Convert.ToDateTime(text));
				}
				if (text2 != "")
				{
					stringBuilder.AppendFormat(" AND InsertTime <= '{0}'", Convert.ToDateTime(text2));
				}
				if (text3 != "")
				{
					switch (num3)
					{
					case 1:
						stringBuilder.AppendFormat(" AND Accounts='{0}' ", text3);
						break;
					case 2:
						if (TypeUtil.ObjectToInt(text3) <= 0)
						{
							return Json(new
							{
								IsOk = false,
								Msg = "游戏ID格式错误"
							});
						}
						stringBuilder.AppendFormat(" AND GameID='{0}'", text3);
						break;
					case 4:
						stringBuilder.AppendFormat(" AND Spreader='{0}'", text3);
						break;
					case 5:
						stringBuilder.AppendFormat(" AND AgentAcc='{0}'", text3);
						break;
					}
				}
				break;
			case 2:
				text = Fetch.GetTodayTime().Split('$')[0].ToString();
				text2 = Fetch.GetTodayTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND InsertTime BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 3:
				text = Fetch.GetYesterdayTime().Split('$')[0].ToString();
				text2 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND InsertTime BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 4:
				text = Fetch.GetWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetWeekTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND InsertTime BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 5:
				text = Fetch.GetLastWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND InsertTime BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 6:
				text = now.ToString("yyyy-MM-01");
				text2 = now.ToString("yyyy-MM-dd 23:59:59");
				stringBuilder.AppendFormat(" AND InsertTime BETWEEN '{0}' AND '{1}'", text, text2);
				break;
			case 7:
				text = now.AddMonths(-1).ToString("yyyy-MM-01");
				text2 = now.ToString("yyyy-MM-01");
				stringBuilder.AppendFormat(" AND InsertTime BETWEEN '{0}' AND '{1}'", text, text2);
				break;
			}
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("View_PlayGameInfo", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			string sql = "Select Sum(Score) AS SumWinScore, Sum(Revenue) AS SumRevenue From View_PlayGameInfo" + stringBuilder.ToString();
			DataSet dataSetBySql = FacadeManage.aideTreasureFacade.GetDataSetBySql(sql);
			string msg = "";
			if (dataSetBySql.Tables[0].Rows.Count > 0)
			{
				DataRow dataRow = dataSetBySql.Tables[0].Rows[0];
				msg = JsonConvert.SerializeObject(new
				{
					SumWinScore = TypeUtil.FormatMoney(dataRow["SumWinScore"].ToString()),
					SumRevenue = dataRow["SumRevenue"].ToString()
				});
			}
			return Json(new
			{
				IsOk = true,
				Msg = msg,
				Total = list.RecordCount,
				Data = JsonConvert.SerializeObject(list.PageSet.Tables[0])
			});
		}
	}
}
