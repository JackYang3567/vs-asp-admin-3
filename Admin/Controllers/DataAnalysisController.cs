using Admin.Filters;
using Admin.Models;
using Game.Entity.Accounts;
using Game.Facade;
using Game.Facade.Tools;
using Game.Kernel;
using Game.Utils;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;

namespace Admin.Controllers
{
	public class DataAnalysisController : BaseController
	{
		[CheckCustomer]
		public ActionResult Index()
		{
			string text = DateTime.Now.ToString("yyyy-MM-dd");
			string text2 = DateTime.Now.AddMonths(-1).AddDays((double)(1 - DateTime.Now.Day)).ToString("yyyy-MM-dd");
			base.ViewBag.StartTime = text2;
			base.ViewBag.EndTime = text;
			DataSet usersStat = FacadeManage.aideAccountsFacade.GetUsersStat();
			DataTable dataTable = usersStat.Tables[0];
			if (dataTable.Rows.Count > 0)
			{
				DataRow dataRow = dataTable.Rows[0];
				int num = 0;
				int value = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				int value2 = 0;
				int value3 = 0;
				int num6 = 0;
				int num7 = 0;
				int num8 = 0;
				int value4 = 0;
				int num9 = 0;
				long num10 = 0L;
				long num11 = 0L;
				int num12 = 1;
				int num13 = 1;
				decimal num14 = 0m;
				decimal num15 = 0m;
				num = Convert.ToInt32(dataRow["UserTotal"]);
				value = Convert.ToInt32(dataRow["CurrentMonthRegUserCounts"]);
				num2 = Convert.ToInt32(dataRow["MaxUserRegCounts"]);
				num3 = Convert.ToInt32(dataRow["UserAVGOnlineTime"]);
				num4 = Convert.ToInt32(dataRow["PayMaxAmount"]);
				num5 = Convert.ToInt32(dataRow["CurrentDateMaxAmount"]);
				Convert.ToInt32(dataRow["PayCurrencyAmount"]);
				value2 = Convert.ToInt32(dataRow["ActiveUserCounts"]);
				value3 = Convert.ToInt32(dataRow["LossUserCounts"]);
				num6 = Convert.ToInt32(dataRow["PayUserCounts"]);
				num7 = Convert.ToInt32(dataRow["PayTwoUserCounts"]);
				num8 = Convert.ToInt32(dataRow["PayTotalAmount"]);
				Convert.ToInt32(dataRow["MaxShareID"]);
				value4 = Convert.ToInt32(dataRow["PayUserOutflowTotal"]);
				num9 = Convert.ToInt32(dataRow["VIPPayUserTotal"]);
				num10 = Convert.ToInt32(dataRow["CurrencyTotal"]);
				num11 = Convert.ToInt64(dataRow["GoldTotal"]);
				num12 = Convert.ToInt32(dataRow["RMBRate"]);
				num13 = Convert.ToInt32(dataRow["CurrencyRate"]);
				num14 = Convert.ToDecimal(dataRow["UserAVGOnlineTime"]);
				num15 = Convert.ToDecimal(dataRow["GameTax"]);
				base.ViewBag.UserCounts = num.ToString();
				base.ViewBag.CurrentMonthRegUserCounts = value.ToString();
				dynamic viewBag = base.ViewBag;
				decimal num16;
				object obj;
				if (num != 0)
				{
					num16 = Convert.ToDecimal(value) / Convert.ToDecimal(num) * 100m;
					obj = num16.ToString("0.0") + "%";
				}
				else
				{
					obj = "0";
				}
				viewBag.NewRate = (string)obj;
				base.ViewBag.ActiveUserRate = ((num == 0) ? "0" : ((Convert.ToDecimal(value2) / Convert.ToDecimal(num) * 100m).ToString("0.0") + "%"));
				base.ViewBag.LossUserRate = ((num == 0) ? "0" : ((Convert.ToDecimal(value3) / Convert.ToDecimal(num) * 100m).ToString("0.0") + "%"));
				base.ViewBag.APRUUser = ((num == 0) ? "0" : (Convert.ToDecimal(num8) / Convert.ToDecimal(num)).ToString("0.00"));
				base.ViewBag.RegMaxCounts = num2.ToString();
				base.ViewBag.AVGOnlineTime = num3.ToString();
				base.ViewBag.ActiveUserCounts = value2.ToString();
				base.ViewBag.LossUserCounts = value3.ToString();
				base.ViewBag.PayUserOutflowTotal = num4.ToString();
				base.ViewBag.PayUserOutflowRate = num5.ToString();
				base.ViewBag.APRUPayUser = ((num6 == 0) ? "0" : (Convert.ToDecimal(num8) / Convert.ToDecimal(num6)).ToString("0.00"));
				base.ViewBag.PayUserCounts = num6.ToString();
				base.ViewBag.PayTwoUserCounts = num7.ToString();
				dynamic viewBag2 = base.ViewBag;
				object obj2;
				if (num != 0)
				{
					num16 = Convert.ToDecimal(num6) / Convert.ToDecimal(num) * 100m;
					obj2 = num16.ToString("0.0") + "%";
				}
				else
				{
					obj2 = "0";
				}
				viewBag2.PayRate = (string)obj2;
				base.ViewBag.LossPayUserCounts = value4.ToString();
				dynamic viewBag3 = base.ViewBag;
				object obj3;
				if (num6 != 0)
				{
					num16 = Convert.ToDecimal(value4) / Convert.ToDecimal(num6) * 100m;
					obj3 = num16.ToString("0.0") + "%";
				}
				else
				{
					obj3 = "0";
				}
				viewBag3.LossPayUserRate = (string)obj3;
				base.ViewBag.VIPPayUserTotal = num9.ToString();
				decimal num17 = value2;
				decimal num18 = num6;
				decimal num19 = num;
				decimal num20 = value4;
				decimal d = num8;
				decimal num21 = num14 / 3600m;
				decimal num22 = num15;
				decimal d2 = num19 - (decimal)value3 - num18 + num20;
				if (!(num17 == 0m) && !(num19 == 0m) && !(num18 == 0m) && !(num21 == 0m) && !(d == 0m) && !(num22 == 0m) && !(num20 == 0m))
				{
					decimal num23 = (num8 - num10 / num13) * num12 * num13 / num11;
					decimal d3 = num12 * num13;
					if (!(num23 * d3 == 0m))
					{
						decimal num24 = (num18 + d2 * num18 / num17 * (num17 / num19 * (1m - num20 / num18))) * num21 * num22 / (d / num23 * d3);
						base.ViewBag.PayDesire = num24.ToString();
					}
				}
			}
			return PartialView();
		}

		[HttpGet]
		public ActionResult UserStatisticsChart()
		{
			DateTime dateTime = TypeUtil.StringToDateTime(base.Request["startDate"]);
			DateTime date = TypeUtil.StringToDateTime(base.Request["endDate"]);
			int num = TypeUtil.ObjectToInt(base.Request["type"]);
			string key = "User_" + dateTime.ToString("yyyyMMdd") + "_" + date.ToString("yyyyMMdd") + "-" + num;
			System.Web.Helpers.Chart chart = System.Web.Helpers.Chart.GetFromCache(key);
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			if (chart == null)
			{
				new Random();
				int daysDate = TextUtility.GetDaysDate(dateTime);
				int daysDate2 = TextUtility.GetDaysDate(date);
				TypeUtil.ObjectToInt(TextUtility.GetDateTimeByDays(daysDate).ToString("yyyyMMdd"));
				TypeUtil.ObjectToInt(TextUtility.GetDateTimeByDays(daysDate2).ToString("yyyyMMdd"));
				if (date.AddMonths(-6) <= dateTime)
				{
					decimal num2 = 1m;
					decimal num3 = 1m;
					SystemStatusInfo systemStatusInfo = FacadeManage.aideAccountsFacade.GetSystemStatusInfo("");
					num2 = ((systemStatusInfo == null) ? 1m : systemStatusInfo.StatusValue);
					systemStatusInfo = FacadeManage.aideAccountsFacade.GetSystemStatusInfo("");
					num3 = ((systemStatusInfo == null) ? 1m : systemStatusInfo.StatusValue);
					string empty = string.Empty;
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendFormat(" WHERE DateID>{0} AND DateID<{1} ", daysDate, daysDate2);
					DataSet payDesireByDay = FacadeManage.aideRecordFacade.GetPayDesireByDay(daysDate, daysDate2);
					DataTable dataTable = payDesireByDay.Tables[0];
					string empty2 = string.Empty;
					string empty3 = string.Empty;
					decimal num4 = 0m;
					decimal num5 = 0m;
					decimal num6 = 0m;
					decimal num7 = 0m;
					decimal num8 = 0m;
					decimal num9 = 0m;
					decimal num10 = 0m;
					decimal num11 = 0m;
					decimal num12 = 0m;
					decimal num13 = 0m;
					decimal num14 = 0m;
					decimal d = num2 * num3;
					decimal num15 = 0m;
					decimal num16 = 0m;
					for (int i = daysDate; i <= daysDate2; i++)
					{
						empty3 = TextUtility.GetDateTimeByDays(i).ToString("yyyyMMdd");
						DataRow[] array = dataTable.Select("DateID=" + i);
						if (array.Length != 0)
						{
							num4 = Convert.ToDecimal(array[0]["ActiveUserTotal"]);
							num5 = Convert.ToDecimal(array[0]["PayUserTotal"]);
							num7 = Convert.ToDecimal(array[0]["UserTotal"]);
							num8 = Convert.ToDecimal(array[0]["LossPayUserTotal"]);
							num14 = Convert.ToDecimal(array[0]["LossUserTotal"]);
							num9 = Convert.ToDecimal(array[0]["PayAmountForCurrency"]);
							num10 = Convert.ToDecimal(array[0]["UserAVGOnlineTime"]) / 3600m;
							num11 = Convert.ToDecimal(array[0]["GameTaxTotal"]);
							num16 = Convert.ToDecimal(array[0]["GoldTotal"]);
							num15 = Convert.ToDecimal(array[0]["CurrencyTotal"]);
							empty = empty + "统计时间：" + empty3 + "\n";
							if (num4 == 0m || num7 == 0m || num5 == 0m || num10 == 0m || num9 == 0m || num11 == 0m || num12 * d == 0m)
							{
								empty2 = "0";
								empty += "用户付费欲望：0";
							}
							else
							{
								num6 = num7 - num14 - num5 + num8;
								num12 = (num9 - num15 / num2) * num2 * num3 / num16;
								num13 = (num5 + num6 * num5 / num4 * num4 / num7 * (1m - num8 / num5)) * num10 * num11 / (num9 / num12 * d);
								empty2 = num13.ToString("0.000000");
								empty = empty + "用户付费欲望：" + empty2 + "\n";
								empty = empty + "游戏币市场价值：" + num12.ToString("0.0000");
							}
						}
						else
						{
							empty2 = "0";
							empty = empty + "统计时间：" + empty3 + "\n";
							empty += "用户付费欲望：0";
						}
						list.Add(empty3);
						list2.Add(empty2);
						empty = string.Empty;
					}
				}
				chart = new System.Web.Helpers.Chart(1200, 300, "<Chart BackColor=\"#C9DC87\" BackGradientStyle=\"TopBottom\" BorderColor=\"181, 64, 1\" BorderWidth=\"2\" BorderlineDashStyle=\"Solid\" Palette=\"BrightPastel\">\r\n  <ChartAreas>\r\n    <ChartArea Name=\"Default\" _Template_=\"All\" BackColor=\"Transparent\" BackSecondaryColor=\"White\" BorderColor=\"64, 64, 64, 64\" BorderDashStyle=\"Solid\" ShadowColor=\"Transparent\">\r\n      <AxisY LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid Interval=\"Auto\" LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisY>\r\n      <AxisX LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisX>\r\n      <Area3DStyle Inclination=\"15\" IsClustered=\"False\" IsRightAngleAxes=\"False\" Perspective=\"10\" Rotation=\"10\" WallWidth=\"0\" />\r\n    </ChartArea>\r\n  </ChartAreas>\r\n  <Legends>\r\n    <Legend _Template_=\"All\" Alignment=\"Center\" BackColor=\"Transparent\" Docking=\"Bottom\" Font=\"Trebuchet MS, 8.25pt, style=Bold\" IsTextAutoFit =\"False\" LegendStyle=\"Row\">\r\n    </Legend>\r\n  </Legends>\r\n  <BorderSkin SkinStyle=\"Emboss\" />\r\n</Chart>", null).AddTitle("付费欲望", null).AddSeries(xValue: list, yValues: list2, markerStep: 1, name: "付费欲望", chartType: (num == 0) ? "line" : "Column", chartArea: null, axisLabel: "访问量", legend: null, xField: null, yFields: null).SetXAxis("日期", 0.0, double.NaN)
					.SetYAxis("付费欲望", 0.0, double.NaN);
				base.Session["xValue"] = list;
				base.Session["yValue"] = list2;
			}
			chart.SaveToCache(key, 5, true);
			return new ChartResult(chart, "png");
		}

		[CheckCustomer]
		public ActionResult PayStatGraph()
		{
			string text = DateTime.Now.ToString("yyyy-MM-dd");
			string text2 = DateTime.Now.AddMonths(-1).AddDays((double)(1 - DateTime.Now.Day)).ToString("yyyy-MM-dd");
			base.ViewBag.StartTime = text2;
			base.ViewBag.EndTime = text;
			DataSet payStat = FacadeManage.aideTreasureFacade.GetPayStat();
			DataTable dataTable = payStat.Tables[0];
			if (dataTable.Rows.Count > 0)
			{
				DataRow dataRow = dataTable.Rows[0];
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				ulong value = 0uL;
				int num4 = 0;
				int num5 = 0;
				string empty = string.Empty;
				int num6 = 0;
				int value2 = 0;
				int num7 = 0;
				num = Convert.ToInt32(dataRow["PayUserCounts"]);
				num2 = Convert.ToInt32(dataRow["PayTwoUserCounts"]);
				num3 = Convert.ToInt32(dataRow["PayMaxAmount"]);
				value = Convert.ToUInt32(dataRow["PayTotalAmount"]);
				num4 = Convert.ToInt32(dataRow["maxShareID"]);
				num5 = Convert.ToInt32(dataRow["CurrentDateMaxAmount"]);
				empty = (string.IsNullOrEmpty(dataRow["PayMaxDate"].ToString()) ? "无充值" : dataRow["PayMaxDate"].ToString());
				num6 = Convert.ToInt32(dataRow["UserTotal"]);
				num7 = Convert.ToInt32(dataRow["VIPpayUserTotal"]);
				base.ViewBag.PayUserCounts = num.ToString();
				base.ViewBag.PayTwoUserCounts = num2.ToString();
				base.ViewBag.PayMaxAmount = num3.ToString();
				base.ViewBag.PayTotalAmount = value.ToString();
				base.ViewBag.MaxShareName = ((num4 == 0) ? "无充值" : TypeUtil.GetShareName(num4).ToString());
				base.ViewBag.CurrentDateMaxAmount = num5.ToString();
				base.ViewBag.PayMaxDate = empty;
				base.ViewBag.PayUserRateWill = "0";
				base.ViewBag.PayUserOutflowTotal = value2.ToString();
				base.ViewBag.APRUPay = ((num == 0) ? "0" : (Convert.ToDouble(value) / Convert.ToDouble(num)).ToString("0.00"));
				base.ViewBag.APRUReg = ((num6 == 0) ? "0" : (Convert.ToDouble(value) / Convert.ToDouble(num6)).ToString("0.00"));
				base.ViewBag.PayUserRate = ((num6 == 0) ? "0" : ((Convert.ToDouble(num) / Convert.ToDouble(num6) * 100.0).ToString("0.00") + "%"));
				base.ViewBag.PayUserOutflowRate = ((num == 0) ? "0" : ((Convert.ToDouble(value2) / Convert.ToDouble(num) * 100.0).ToString("0.00") + "%"));
				base.ViewBag.VIPPayUserRate = num7.ToString();
			}
			return PartialView();
		}

		[HttpGet]
		[CheckCustomer]
		public ActionResult PayStatisticsChart()
		{
			DateTime dateTime = TypeUtil.StringToDateTime(base.Request["startDate"]);
			DateTime dateTime2 = TypeUtil.StringToDateTime(base.Request["endDate"]);
			int num = TypeUtil.ObjectToInt(base.Request["type"]);
			string key = "Pay_" + dateTime.ToString("yyyyMMdd") + "_" + dateTime2.ToString("yyyyMMdd") + "-" + num;
			System.Web.Helpers.Chart chart = System.Web.Helpers.Chart.GetFromCache(key);
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			if (chart == null)
			{
				new Random();
				int daysDate = TextUtility.GetDaysDate(dateTime);
				int daysDate2 = TextUtility.GetDaysDate(dateTime2);
				TypeUtil.ObjectToInt(TextUtility.GetDateTimeByDays(daysDate).ToString("yyyyMMdd"));
				TypeUtil.ObjectToInt(TextUtility.GetDateTimeByDays(daysDate2).ToString("yyyyMMdd"));
				int num2 = daysDate2 - daysDate;
				if (dateTime2.AddMonths(-6) <= dateTime && num2 > 1)
				{
					DataSet payStatByDay = FacadeManage.aideTreasureFacade.GetPayStatByDay(dateTime.ToString(), Fetch.GetEndTime(Convert.ToDateTime(dateTime2)));
					DataTable dataTable = payStatByDay.Tables[0];
					string empty = string.Empty;
					int num3 = 0;
					string empty2 = string.Empty;
					for (int i = daysDate; i <= daysDate2; i++)
					{
						empty2 = TextUtility.GetDateTimeByDays(i).ToString("yyyy-MM-dd");
						DataRow[] array = dataTable.Select("ApplyDate='" + empty2 + "'");
						if (array.Length != 0)
						{
							num3 = Convert.ToInt32(array[0]["PayAmount"]);
							empty = empty + "统计时间：" + empty2 + "\n";
							empty = empty + "充值：" + array[0]["PayAmount"].ToString() + "\n";
						}
						else
						{
							num3 = 0;
							empty = empty + "统计时间：" + empty2 + "\n";
							empty += "充值：0\n";
						}
						list.Add(empty2);
						list2.Add(num3.ToString());
						empty = string.Empty;
					}
				}
				chart = new System.Web.Helpers.Chart(1200, 300, "<Chart BackColor=\"#C9DC87\" BackGradientStyle=\"TopBottom\" BorderColor=\"181, 64, 1\" BorderWidth=\"2\" BorderlineDashStyle=\"Solid\" Palette=\"BrightPastel\">\r\n  <ChartAreas>\r\n    <ChartArea Name=\"Default\" _Template_=\"All\" BackColor=\"Transparent\" BackSecondaryColor=\"White\" BorderColor=\"64, 64, 64, 64\" BorderDashStyle=\"Solid\" ShadowColor=\"Transparent\">\r\n      <AxisY LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid Interval=\"Auto\" LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisY>\r\n      <AxisX LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisX>\r\n      <Area3DStyle Inclination=\"15\" IsClustered=\"False\" IsRightAngleAxes=\"False\" Perspective=\"10\" Rotation=\"10\" WallWidth=\"0\" />\r\n    </ChartArea>\r\n  </ChartAreas>\r\n  <Legends>\r\n    <Legend _Template_=\"All\" Alignment=\"Center\" BackColor=\"Transparent\" Docking=\"Bottom\" Font=\"Trebuchet MS, 8.25pt, style=Bold\" IsTextAutoFit =\"False\" LegendStyle=\"Row\">\r\n    </Legend>\r\n  </Legends>\r\n  <BorderSkin SkinStyle=\"Emboss\" />\r\n</Chart>", null).AddTitle("充值统计", null).AddSeries(xValue: list, yValues: list2, markerStep: 1, name: "充值统计", chartType: (num == 0) ? "line" : "Column", chartArea: null, axisLabel: "访问量", legend: null, xField: null, yFields: null).SetXAxis("日期", 0.0, double.NaN)
					.SetYAxis("充值金额", 0.0, double.NaN);
				base.Session["xValue"] = list;
				base.Session["yValue"] = list2;
			}
			chart.SaveToCache(key, 5, true);
			return new ChartResult(chart, "png");
		}

		[CheckCustomer]
		public ActionResult UserRegStat()
		{
			string text = DateTime.Now.ToString("yyyy-MM-dd");
			string text2 = DateTime.Now.AddMonths(-1).AddDays((double)(1 - DateTime.Now.Day)).ToString("yyyy-MM-dd");
			base.ViewBag.StartTime = text2;
			base.ViewBag.EndTime = text;
			DataSet usersNumber = FacadeManage.aideAccountsFacade.GetUsersNumber();
			DataTable dataTable = usersNumber.Tables[0];
			if (dataTable.Rows.Count > 0)
			{
				DataRow dataRow = dataTable.Rows[0];
				int num = 0;
				int value = 0;
				int num2 = 0;
				num = Convert.ToInt32(dataRow["UserTotal"]);
				value = Convert.ToInt32(dataRow["CurrentMonthRegUserCounts"]);
				num2 = Convert.ToInt32(dataRow["MaxUserRegCounts"]);
				base.ViewBag.UserCounts = num.ToString();
				base.ViewBag.CurrentMonthRegUserCounts = value.ToString();
				base.ViewBag.NewRate = ((num == 0) ? "0" : ((Convert.ToDecimal(value) / Convert.ToDecimal(num) * 100m).ToString("0.0") + "%"));
				base.ViewBag.RegMaxCounts = num2.ToString();
			}
			return PartialView();
		}

		[CheckCustomer]
		[HttpGet]
		public ActionResult UserRegDayStatisticsChart()
		{
			DateTime dateTime = TypeUtil.StringToDateTime(base.Request["startDate"]);
			DateTime dateTime2 = TypeUtil.StringToDateTime(base.Request["endDate"]);
			int num = TypeUtil.ObjectToInt(base.Request["type"]);
			string key = "UserRegDay_" + dateTime.ToString("yyyyMMdd") + "_" + dateTime2.ToString("yyyyMMdd") + "-" + num;
			System.Web.Helpers.Chart chart = System.Web.Helpers.Chart.GetFromCache(key);
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			if (chart == null)
			{
				int days = (dateTime2 - dateTime).Days;
				if (dateTime2.AddMonths(-6) <= dateTime && days >= 0)
				{
					if (days > 0)
					{
						DataSet regUserByDays = FacadeManage.aideAccountsFacade.GetRegUserByDays(dateTime.ToString("yyyy-MM-dd"), dateTime2.ToString("yyyy-MM-dd"));
						int num2 = 0;
						for (int i = 0; i <= days; i++)
						{
							string item = dateTime.AddDays((double)i).ToString("yyyyMMdd");
							string dateID = Fetch.GetDateID(dateTime.AddDays((double)i));
							DataRow[] array = regUserByDays.Tables[0].Select("DateID=" + dateID);
							num2 = ((array.Length != 0) ? Convert.ToInt32(array[0]["RegisterCount"]) : 0);
							list.Add(item);
							list2.Add(num2.ToString());
						}
					}
					else
					{
						DataSet regUserByHours = FacadeManage.aideAccountsFacade.GetRegUserByHours(Fetch.GetStartTime(dateTime), Fetch.GetEndTime(dateTime2));
						int num3 = 0;
						string empty = string.Empty;
						for (int j = 0; j <= 23; j++)
						{
							empty = j.ToString() + ":00";
							DataRow[] array2 = regUserByHours.Tables[0].Select("StatDate='" + j + "'");
							num3 = ((array2.Length != 0) ? Convert.ToInt32(array2[0]["RegisterCount"]) : 0);
							list.Add(empty);
							list2.Add(num3.ToString());
						}
					}
				}
				chart = new System.Web.Helpers.Chart(1200, 250, "<Chart BackColor=\"#C9DC87\" BackGradientStyle=\"TopBottom\" BorderColor=\"181, 64, 1\" BorderWidth=\"2\" BorderlineDashStyle=\"Solid\" Palette=\"BrightPastel\">\r\n  <ChartAreas>\r\n    <ChartArea Name=\"Default\" _Template_=\"All\" BackColor=\"Transparent\" BackSecondaryColor=\"White\" BorderColor=\"64, 64, 64, 64\" BorderDashStyle=\"Solid\" ShadowColor=\"Transparent\">\r\n      <AxisY LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid Interval=\"Auto\" LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisY>\r\n      <AxisX LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisX>\r\n      <Area3DStyle Inclination=\"15\" IsClustered=\"False\" IsRightAngleAxes=\"False\" Perspective=\"10\" Rotation=\"10\" WallWidth=\"0\" />\r\n    </ChartArea>\r\n  </ChartAreas>\r\n  <Legends>\r\n    <Legend _Template_=\"All\" Alignment=\"Center\" BackColor=\"Transparent\" Docking=\"Bottom\" Font=\"Trebuchet MS, 8.25pt, style=Bold\" IsTextAutoFit =\"False\" LegendStyle=\"Row\">\r\n    </Legend>\r\n  </Legends>\r\n  <BorderSkin SkinStyle=\"Emboss\" />\r\n</Chart>", null).AddTitle("【注册日统计图】", null).AddSeries(xValue: list, yValues: list2, markerStep: 1, name: "注册日统计", chartType: (num == 0) ? "line" : "Column", chartArea: null, axisLabel: "注册日统计", legend: null, xField: null, yFields: null).SetXAxis("日期", 0.0, double.NaN)
					.SetYAxis("每日注册人数", 0.0, double.NaN);
				base.Session["xValue"] = list;
				base.Session["yValue"] = list2;
			}
			chart.SaveToCache(key, 5, true);
			return new ChartResult(chart, "png");
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetUserRegDayStatistcsData()
		{
			DateTime dateTime = TypeUtil.StringToDateTime(base.Request["startDate"]);
			DateTime dateTime2 = TypeUtil.StringToDateTime(base.Request["endDate"]);
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			int days = (dateTime2 - dateTime).Days;
			if (dateTime2.AddMonths(-6) <= dateTime)
			{
				if (days > 0)
				{
					DataSet regUserByDays = FacadeManage.aideAccountsFacade.GetRegUserByDays(dateTime.ToString("yyyy-MM-dd"), dateTime2.ToString("yyyy-MM-dd"));
					int num = 0;
					for (int i = 0; i <= days; i++)
					{
						string key = dateTime.AddDays((double)i).ToString("yyyyMMdd");
						string dateID = Fetch.GetDateID(dateTime.AddDays((double)i));
						DataRow[] array = regUserByDays.Tables[0].Select("DateID=" + dateID);
						num = ((array.Length != 0) ? Convert.ToInt32(array[0]["RegisterCount"]) : 0);
						dictionary.Add(key, num);
					}
				}
				else
				{
					DataSet regUserByHours = FacadeManage.aideAccountsFacade.GetRegUserByHours(Fetch.GetStartTime(dateTime), Fetch.GetEndTime(dateTime2));
					int num2 = 0;
					string empty = string.Empty;
					for (int j = 0; j <= 23; j++)
					{
						empty = j.ToString() + ":00";
						DataRow[] array2 = regUserByHours.Tables[0].Select("StatDate='" + j + "'");
						num2 = ((array2.Length != 0) ? Convert.ToInt32(array2[0]["RegisterCount"]) : 0);
						dictionary.Add(empty, num2);
					}
				}
			}
			List<object> list = new List<object>();
			if (dictionary != null && dictionary.Keys.Count > 0)
			{
				foreach (string key2 in dictionary.Keys)
				{
					list.Add(new
					{
						key = key2,
						vaule = TypeUtil.ObjectToString(dictionary[key2])
					});
				}
				return Json(new
				{
					IsOk = true,
					Data = JsonConvert.SerializeObject(list)
				});
			}
			return Json(new
			{
				IsOk = true,
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		[HttpGet]
		public ActionResult UserRegMonthStatisticsChart()
		{
			string key = "UserRegMonth_" + DateTime.Now.ToString("yyyyMMdd");
			System.Web.Helpers.Chart chart = System.Web.Helpers.Chart.GetFromCache(key);
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			if (chart == null)
			{
				DataSet regUserByMonth = FacadeManage.aideAccountsFacade.GetRegUserByMonth();
				DataTable dataTable = regUserByMonth.Tables[0];
				DateTime t = default(DateTime);
				DateTime t2 = default(DateTime);
				if (dataTable.Rows.Count > 0)
				{
					DataRow dataRow = (from r in dataTable.AsEnumerable()
					orderby r.Field<string>("StatDate") descending
					select r).FirstOrDefault();
					DataRow dataRow2 = (from r in dataTable.AsEnumerable()
					orderby r.Field<string>("StatDate")
					select r).FirstOrDefault();
					t = Convert.ToDateTime(dataRow["StatDate"]);
					t2 = Convert.ToDateTime(dataRow2["StatDate"]);
				}
				int num = 0;
				int num2 = 0;
				while (t2 <= t)
				{
					DataRow[] array = dataTable.Select("StatDate='" + t2.ToString("yyyy-MM") + "'");
					num = ((array.Length != 0) ? Convert.ToInt32(array[0]["RegisterCount"]) : 0);
					list.Add(t2.ToString("yyyyMM"));
					list2.Add(num.ToString());
					num2++;
					t2 = t2.AddMonths(1);
				}
				System.Web.Helpers.Chart chart2 = new System.Web.Helpers.Chart(1200, 250, "<Chart BackColor=\"#C9DC87\" BackGradientStyle=\"TopBottom\" BorderColor=\"181, 64, 1\" BorderWidth=\"2\" BorderlineDashStyle=\"Solid\" Palette=\"BrightPastel\">\r\n  <ChartAreas>\r\n    <ChartArea Name=\"Default\" _Template_=\"All\" BackColor=\"Transparent\" BackSecondaryColor=\"White\" BorderColor=\"64, 64, 64, 64\" BorderDashStyle=\"Solid\" ShadowColor=\"Transparent\">\r\n      <AxisY LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid Interval=\"Auto\" LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisY>\r\n      <AxisX LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisX>\r\n      <Area3DStyle Inclination=\"15\" IsClustered=\"False\" IsRightAngleAxes=\"False\" Perspective=\"10\" Rotation=\"10\" WallWidth=\"0\" />\r\n    </ChartArea>\r\n  </ChartAreas>\r\n  <Legends>\r\n    <Legend _Template_=\"All\" Alignment=\"Center\" BackColor=\"Transparent\" Docking=\"Bottom\" Font=\"Trebuchet MS, 8.25pt, style=Bold\" IsTextAutoFit =\"False\" LegendStyle=\"Row\">\r\n    </Legend>\r\n  </Legends>\r\n  <BorderSkin SkinStyle=\"Emboss\" />\r\n</Chart>", null).AddTitle("【注册月统计图】", null);
				IEnumerable xValue = list;
				IEnumerable yValues = list2;
				int markerStep = 1;
				chart = chart2.AddSeries("注册月统计", "line", null, "注册月统计", null, markerStep, xValue, null, yValues, null).SetXAxis("月份", 0.0, double.NaN).SetYAxis("每月注册人数", 0.0, double.NaN);
				base.Session["xValue"] = list;
				base.Session["yValue"] = list2;
			}
			chart.SaveToCache(key, 5, true);
			return new ChartResult(chart, "png");
		}

		[CheckCustomer]
		public ActionResult StatOnline()
		{
			string text = TypeUtil.ObjectToString(base.Request["Time"]);
			string text2 = DateTime.Now.Year.ToString();
			string text3 = DateTime.Now.Month.ToString().PadLeft(2, '0');
			string text4 = DateTime.Now.ToString("dd");
			if (!string.IsNullOrEmpty(text))
			{
				if (text.IndexOf('-') == -1)
				{
					text2 = text;
					text3 = "-1";
					text4 = "-1";
				}
				else if (text.Split('-').Length == 2)
				{
					text2 = text.Split('-')[0].ToString().Trim();
					text3 = text.Split('-')[1].ToString().Trim();
					text4 = "-1";
				}
				else
				{
					text2 = text.Split('-')[0].ToString().Trim();
					text3 = text.Split('-')[1].ToString().Trim();
					text4 = text.Split('-')[2].ToString().Trim();
				}
			}
			base.ViewBag.Year = text2;
			base.ViewBag.Month = text3;
			base.ViewBag.Day = text4;
			DateTime dateTime = DateTime.Now.AddDays(-1.0);
			base.ViewBag.Year2 = dateTime.Year.ToString();
			base.ViewBag.Month2 = dateTime.Month.ToString().PadLeft(2, '0');
			base.ViewBag.Day2 = dateTime.ToString("dd");
			return PartialView();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetOnline()
		{
			string text = TypeUtil.ObjectToString(base.Request["year"]);
			string text2 = TypeUtil.ObjectToString(base.Request["month"]);
			string text3 = TypeUtil.ObjectToString(base.Request["day"]);
			List<object> list = new List<object>();
			if (text2 != "-1" && text3 != "-1")
			{
				DataSet onLineStreamInfoList = FacadeManage.aidePlatformFacade.GetOnLineStreamInfoList(text, text2, text3);
				DataTable sourceDt = onLineStreamInfoList.Tables[0];
				DataTable dataTable = TypeUtil.FormatTable(sourceDt, text, text2, text3);
				if (dataTable.Rows.Count > 0)
				{
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						list.Add(new
						{
							InsertDateTime = text + "-" + text2 + "-" + text3 + "-" + TypeUtil.ObjectToString(dataTable.Rows[i]["InsertDateTime"]) + ":00 - " + TypeUtil.ObjectToString(dataTable.Rows[i]["InsertDateTime"]) + ":59",
							MaxCount = TypeUtil.ObjectToInt(dataTable.Rows[i]["MaxCount"]),
							MinCount = TypeUtil.ObjectToInt(dataTable.Rows[i]["MinCount"]),
							AvgCount = TypeUtil.ObjectToInt(dataTable.Rows[i]["AvgCount"])
						});
					}
				}
			}
			return Json(new
			{
				IsOk = true,
				Data = JsonConvert.SerializeObject(list)
			});
		}

		public JsonResult GetDays()
		{
			int num = TypeUtil.ObjectToInt(base.Request["year"]);
			string text = TypeUtil.ObjectToString(base.Request["month"]);
			string text2 = TypeUtil.ObjectToString(base.Request["day"]);
			DateTime d = Convert.ToDateTime(num + "-" + text + "-01");
			DateTime d2 = Convert.ToDateTime(num + "-" + text + "-" + text2);
			DateTime d3 = d.AddMonths(1);
			List<object> list = new List<object>();
			while (d != d3)
			{
				list.Add(new
				{
					vaule = d.ToString("dd"),
					text = d.ToString("dd") + "日",
					isSelected = ((d == d2) ? 1 : 0)
				});
				d = d.AddDays(1.0);
			}
			return Json(new
			{
				IsOk = true,
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[HttpGet]
		[CheckCustomer]
		public ActionResult OnlineStatisticsChart()
		{
			string text = TypeUtil.ObjectToString(base.Request["year"]);
			string text2 = TypeUtil.ObjectToString(base.Request["month"]);
			string text3 = TypeUtil.ObjectToString(base.Request["day"]);
			string key = "Online_" + text + text2 + text3;
			System.Web.Helpers.Chart chart = System.Web.Helpers.Chart.GetFromCache(key);
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			if (chart == null && text2 != "-1" && text3 != "-1")
			{
				DataSet onLineStreamInfoList = FacadeManage.aidePlatformFacade.GetOnLineStreamInfoList(text, text2, text3);
				DataTable sourceDt = onLineStreamInfoList.Tables[0];
				DataTable dataTable = TypeUtil.FormatTable(sourceDt, text, text2, text3);
				if (dataTable.Rows.Count > 0)
				{
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						list.Add(dataTable.Rows[i]["InsertDateTime"].ToString());
						list2.Add(dataTable.Rows[i]["MaxCount"].ToString());
					}
				}
				System.Web.Helpers.Chart chart2 = new System.Web.Helpers.Chart(1200, 250, "<Chart BackColor=\"#C9DC87\" BackGradientStyle=\"TopBottom\" BorderColor=\"181, 64, 1\" BorderWidth=\"2\" BorderlineDashStyle=\"Solid\" Palette=\"BrightPastel\">\r\n  <ChartAreas>\r\n    <ChartArea Name=\"Default\" _Template_=\"All\" BackColor=\"Transparent\" BackSecondaryColor=\"White\" BorderColor=\"64, 64, 64, 64\" BorderDashStyle=\"Solid\" ShadowColor=\"Transparent\">\r\n      <AxisY LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid Interval=\"Auto\" LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisY>\r\n      <AxisX LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisX>\r\n      <Area3DStyle Inclination=\"15\" IsClustered=\"False\" IsRightAngleAxes=\"False\" Perspective=\"10\" Rotation=\"10\" WallWidth=\"0\" />\r\n    </ChartArea>\r\n  </ChartAreas>\r\n  <Legends>\r\n    <Legend _Template_=\"All\" Alignment=\"Center\" BackColor=\"Transparent\" Docking=\"Bottom\" Font=\"Trebuchet MS, 8.25pt, style=Bold\" IsTextAutoFit =\"False\" LegendStyle=\"Row\">\r\n    </Legend>\r\n  </Legends>\r\n  <BorderSkin SkinStyle=\"Emboss\" />\r\n</Chart>", null).AddTitle("【在线人数统计图】", null);
				IEnumerable xValue = list;
				IEnumerable yValues = list2;
				int markerStep = 1;
				chart = chart2.AddSeries("在线人数统计", "line", null, "在线人数统计", null, markerStep, xValue, null, yValues, null).SetXAxis("", 0.0, double.NaN).SetYAxis("最高在线人数", 0.0, double.NaN);
				base.Session["xValue"] = list;
				base.Session["yValue"] = list2;
			}
			chart.SaveToCache(key, 5, true);
			return new ChartResult(chart, "png");
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public ActionResult StatOnlineInfo()
		{
			string text = TypeUtil.ObjectToString(base.Request["Time"]);
			base.ViewBag.TimeStr = text;
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetStatOnlineInfo()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string text = TypeUtil.ObjectToString(base.Request["TimeStr"]);
			string orderby = "ORDER BY InsertDateTime DESC";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			StringBuilder stringBuilder = new StringBuilder("");
			if (text.Split('-').Length == 2)
			{
				text2 = text.Split('-')[0].ToString().Trim();
				text3 = text.Split('-')[1].ToString().Trim();
				DateTime dateTime = Convert.ToDateTime(text2 + "-" + text3 + "-01");
				DateTime dateTime2 = dateTime.AddMonths(1);
				stringBuilder.AppendFormat(" where InsertDateTime>'{0}' and InsertDateTime<'{1}'", dateTime.ToString(), dateTime2.ToString());
			}
			else
			{
				text2 = text.Split('-')[0].ToString().Trim();
				text3 = text.Split('-')[1].ToString().Trim();
				text4 = text.Split('-')[2].ToString().Trim();
				string arg = text2 + "-" + text3 + "-" + text4;
				stringBuilder.AppendFormat(" where convert(varchar(10),InsertDateTime,120)='{0}'", arg);
			}
			PagerSet onLineStreamInfoList = FacadeManage.aidePlatformFacade.GetOnLineStreamInfoList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (onLineStreamInfoList != null && onLineStreamInfoList.PageSet != null && onLineStreamInfoList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in onLineStreamInfoList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						InsertDateTime = TypeUtil.ObjectToString(row["InsertDateTime"]),
						OnLineCountSum = TypeUtil.ObjectToString(row["OnLineCountSum"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = onLineStreamInfoList.RecordCount,
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public ActionResult UserPlaying()
		{
			return View();
		}

		public JsonResult GetUserPlaying()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY CollectDate ASC";
			string text = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["ServerID"]);
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1 ");
			if (!string.IsNullOrEmpty(text))
			{
				int num3 = 0;
				AccountsInfo accountsInfo = null;
				switch (num)
				{
				case 1:
					accountsInfo = FacadeManage.aideAccountsFacade.GetAccountInfoByAccount(text);
					break;
				case 2:
					accountsInfo = FacadeManage.aideAccountsFacade.GetAccountInfoByNickname(text);
					break;
				case 3:
					if (TypeUtil.ObjectToInt(text) > 0)
					{
						accountsInfo = FacadeManage.aideAccountsFacade.GetAccountInfoByGameID(Convert.ToInt32(text));
					}
					break;
				case 4:
					if (TypeUtil.ObjectToInt(text) > 0)
					{
						num3 = Convert.ToInt32(text);
					}
					break;
				}
				num3 = ((accountsInfo != null) ? accountsInfo.UserID : 0);
				if (num3 > 0)
				{
					stringBuilder.AppendFormat(" AND UserID={0}", num3);
				}
			}
			if (num2 > 0)
			{
				stringBuilder.AppendFormat(" AND ServerID={0}", num2);
			}
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("GameScoreLocker", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list2 = new List<object>();
			if (list != null && list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in list.PageSet.Tables[0].Rows)
				{
					list2.Add(new
					{
						UserID = TypeUtil.ObjectToInt(row["UserID"]),
						Accounts = TypeUtil.GetAccounts(TypeUtil.ObjectToInt(row["UserID"])),
						NickNameByUserID = TypeUtil.GetNickNameByUserID(TypeUtil.ObjectToInt(row["UserID"])),
						GameID = TypeUtil.GetGameID(TypeUtil.ObjectToInt(row["UserID"])),
						GameNameByServerID = TypeUtil.GameGameNameByServerID(TypeUtil.ObjectToInt(row["ServerID"])),
						GameRoomName = TypeUtil.GetGameRoomName(TypeUtil.ObjectToInt(row["ServerID"])),
						EnterIP = TypeUtil.ObjectToInt(row["EnterIP"]),
						CollectDate = TypeUtil.ObjectToInt(row["CollectDate"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonConvert.SerializeObject(list2)
			});
		}

		[CheckCustomer]
		public ActionResult GoldDistribution()
		{
			DataSet goldDistribution = FacadeManage.aideTreasureFacade.GetGoldDistribution();
			base.ViewBag.Label1 = "0";
			base.ViewBag.Label2 = "0";
			base.ViewBag.Label3 = "0";
			base.ViewBag.Label4 = "0";
			base.ViewBag.Label5 = "0";
			base.ViewBag.Label6 = "0";
			base.ViewBag.Label7 = "0";
			base.ViewBag.Label8 = "0";
			if (goldDistribution.Tables[0].Rows.Count > 0)
			{
				DataRow dataRow = goldDistribution.Tables[0].Rows[0];
				base.ViewBag.Label1 = dataRow[0].ToString();
				base.ViewBag.Label2 = dataRow[1].ToString();
				base.ViewBag.Label3 = dataRow[2].ToString();
				base.ViewBag.Label4 = dataRow[3].ToString();
				base.ViewBag.Label5 = dataRow[4].ToString();
				base.ViewBag.Label6 = dataRow[5].ToString();
				base.ViewBag.Label7 = dataRow[6].ToString();
				base.ViewBag.Label8 = dataRow[7].ToString();
			}
			base.ViewBag.GoldTotal = "0";
			base.ViewBag.GoldRate = "0";
			base.ViewBag.GoldRate2 = "0";
			base.ViewBag.GoldTrueValue = "0";
			base.ViewBag.ExpansionRate = "";
			base.ViewBag.GoldEstimatedValue = "";
			if (goldDistribution.Tables[1].Rows.Count > 0)
			{
				DataRow dataRow2 = goldDistribution.Tables[1].Rows[0];
				decimal num = Convert.ToDecimal(dataRow2["GoldTotal"]);
				base.ViewBag.GoldTotal = num.ToString("N0");
				decimal d = Convert.ToDecimal(dataRow2["PayAmountTotal"]);
				decimal d2 = Convert.ToDecimal(dataRow2["CurrencyTotal"]);
				decimal num2 = 0.0m;
				decimal num3 = Convert.ToDecimal(dataRow2["CurrencyRate"]);
				base.ViewBag.GoldRate = (num2 * num3).ToString();
				base.ViewBag.GoldRate2 = (num2 * num3).ToString();
				decimal num4 = 0m;
				if (num != 0m && num3 != 0m)
				{
					num4 = (d - d2 / num3) * num2 * num3 / num;
					base.ViewBag.GoldTrueValue = num4.ToString("0.00");
				}
				decimal d3 = (num4 == 0m) ? 0m : (1m / num4);
				if (d3 <= 2.5m)
				{
					base.ViewBag.ExpansionRate = d3.ToString("0.00");
				}
				else
				{
					base.ViewBag.ExpansionRate = d3.ToString("0.00") + "<span style='color:red;'>     警告！通胀率大于2.5<span>";
				}
				base.ViewBag.GoldEstimatedValue = (num4 * num4 * 2.5m).ToString("0.00");
			}
			return PartialView();
		}

		[HttpGet]
		public ActionResult GoldDistributionChart()
		{
			System.Web.UI.DataVisualization.Charting.Chart chart = new System.Web.UI.DataVisualization.Charting.Chart();
			chart.ChartAreas.Add(new ChartArea("ChartArea1"));
			chart.Width = 800;
			chart.Height = 350;
			chart.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
			chart.BorderSkin.SkinStyle = BorderSkinStyle.None;
			chart.BorderlineDashStyle = ChartDashStyle.Dash;
			chart.BorderlineWidth = 1;
			chart.Series.Add(new Series("Series1"));
			chart.Series[0].ChartType = SeriesChartType.Pie;
			chart.Series[0]["PieLabelStyle"] = "Outside";
			chart.Series[0]["PieLineColor"] = "Black";
			DataSet goldDistribution = FacadeManage.aideTreasureFacade.GetGoldDistribution();
			if (goldDistribution.Tables[0].Rows.Count > 0)
			{
				DataRow dataRow = goldDistribution.Tables[0].Rows[0];
				string[] array = new string[8]
				{
					"1万以下",
					"1万-10万",
					"10万-50万",
					"50万-100万",
					"100万-500万",
					"500万-1000万",
					"1000万-3000万",
					"3000万以上"
				};
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				int value = Convert.ToInt32(dataRow[8]);
				int num = 0;
				for (int i = 0; i < 8; i++)
				{
					num = Convert.ToInt32(dataRow[i]);
					if (num > 0)
					{
						dictionary.Add(array[i] + "(" + (Convert.ToDouble(num) / Convert.ToDouble(value) * 100.0).ToString("0.00") + "%)", Convert.ToInt32(dataRow[i]));
					}
				}
				chart.Series[0].Points.DataBindXY(dictionary.Keys, dictionary.Values);
			}
			MemoryStream memoryStream = new MemoryStream();
			chart.SaveImage(memoryStream, ChartImageFormat.Png);
			return File(memoryStream.ToArray(), "image/png");
		}

		[CheckCustomer]
		public ActionResult UserLossStat()
		{
			string text = DateTime.Now.ToString("yyyy-MM-dd");
			string text2 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd");
			base.ViewBag.StartTime = text2;
			base.ViewBag.EndTime = text;
			DataSet usersNumber = FacadeManage.aideAccountsFacade.GetUsersNumber();
			base.ViewBag.LossUserCounts = "0";
			base.ViewBag.LossUserRate = "0";
			DataTable dataTable = usersNumber.Tables[0];
			if (dataTable.Rows.Count > 0)
			{
				DataRow dataRow = dataTable.Rows[0];
				int num = 0;
				int value = 0;
				num = Convert.ToInt32(dataRow["UserTotal"]);
				value = Convert.ToInt32(dataRow["LossUserCounts"]);
				base.ViewBag.LossUserCounts = value.ToString();
				base.ViewBag.LossUserRate = ((num == 0) ? "0" : ((Convert.ToDecimal(value) / Convert.ToDecimal(num) * 100m).ToString("0.0") + "%"));
			}
			return PartialView();
		}

		[CheckCustomer]
		[HttpGet]
		[AntiSqlInjection]
		public ActionResult DayUserLossChart()
		{
			DateTime dateTime = TypeUtil.StringToDateTime(base.Request["startDate"]);
			DateTime value = TypeUtil.StringToDateTime(base.Request["endDate"]);
			int num = TypeUtil.ObjectToInt(base.Request["type"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["UserType"]);
			string key = "DayUserLos_" + dateTime.ToString("yyyyMMdd") + "_" + value.ToString("yyyyMMdd") + "-" + num + "-" + num2;
			System.Web.Helpers.Chart chart = System.Web.Helpers.Chart.GetFromCache(key);
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			if (chart == null)
			{
				int daysDate = TextUtility.GetDaysDate(Convert.ToDateTime(dateTime));
				int daysDate2 = TextUtility.GetDaysDate(Convert.ToDateTime(value));
				int num3 = daysDate2 - daysDate;
				DataSet dataSet = new DataSet();
				if (value.AddMonths(-6) <= dateTime && num3 > 0)
				{
					dataSet = FacadeManage.aideRecordFacade.GetLossUserByDay(daysDate, daysDate2);
					DataTable dataTable = dataSet.Tables[0];
					string empty = string.Empty;
					int num4 = 0;
					string empty2 = string.Empty;
					for (int i = daysDate; i <= daysDate2; i++)
					{
						empty2 = TextUtility.GetDateTimeByDays(i).ToString("yyyyMMdd");
						DataRow[] array = dataTable.Select("DateID=" + i);
						if (array.Length != 0)
						{
							num4 = ((num2 != 0) ? Convert.ToInt32(array[0]["LossPayUser"]) : Convert.ToInt32(array[0]["LossUser"]));
							empty = empty + "统计时间：" + empty2 + "\n";
							object obj = empty;
							empty = obj + "流失用户数：" + num4 + "\n";
						}
						else
						{
							num4 = 0;
							empty = empty + "统计时间：" + empty2 + "\n";
							empty += "流失用户数：0\n";
						}
						list.Add(empty2);
						list2.Add(num4.ToString());
						empty = string.Empty;
					}
				}
				chart = new System.Web.Helpers.Chart(1200, 250, "<Chart BackColor=\"#C9DC87\" BackGradientStyle=\"TopBottom\" BorderColor=\"181, 64, 1\" BorderWidth=\"2\" BorderlineDashStyle=\"Solid\" Palette=\"BrightPastel\">\r\n  <ChartAreas>\r\n    <ChartArea Name=\"Default\" _Template_=\"All\" BackColor=\"Transparent\" BackSecondaryColor=\"White\" BorderColor=\"64, 64, 64, 64\" BorderDashStyle=\"Solid\" ShadowColor=\"Transparent\">\r\n      <AxisY LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid Interval=\"Auto\" LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisY>\r\n      <AxisX LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisX>\r\n      <Area3DStyle Inclination=\"15\" IsClustered=\"False\" IsRightAngleAxes=\"False\" Perspective=\"10\" Rotation=\"10\" WallWidth=\"0\" />\r\n    </ChartArea>\r\n  </ChartAreas>\r\n  <Legends>\r\n    <Legend _Template_=\"All\" Alignment=\"Center\" BackColor=\"Transparent\" Docking=\"Bottom\" Font=\"Trebuchet MS, 8.25pt, style=Bold\" IsTextAutoFit =\"False\" LegendStyle=\"Row\">\r\n    </Legend>\r\n  </Legends>\r\n  <BorderSkin SkinStyle=\"Emboss\" />\r\n</Chart>", null).AddTitle("【日流失玩家数—连续30天内未登陆的用户为流失玩家】", null).AddSeries(xValue: list, yValues: list2, markerStep: 1, name: "日流失玩家数统计", chartType: (num == 0) ? "line" : "Column", chartArea: null, axisLabel: "日流失玩家数统计", legend: null, xField: null, yFields: null).SetXAxis("日期", 0.0, double.NaN)
					.SetYAxis("人数", 0.0, double.NaN);
				base.Session["xValue"] = list;
				base.Session["yValue"] = list2;
			}
			chart.SaveToCache(key, 5, true);
			return new ChartResult(chart, "png");
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetUserLossStat()
		{
			DateTime dateTime = TypeUtil.StringToDateTime(base.Request["startDate"]);
			DateTime value = TypeUtil.StringToDateTime(base.Request["endDate"]);
			TypeUtil.ObjectToInt(base.Request["type"]);
			int num = TypeUtil.ObjectToInt(base.Request["UserType"]);
			int daysDate = TextUtility.GetDaysDate(Convert.ToDateTime(dateTime));
			int daysDate2 = TextUtility.GetDaysDate(Convert.ToDateTime(value));
			int num2 = daysDate2 - daysDate;
			DataSet dataSet = new DataSet();
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (value.AddMonths(-6) <= dateTime && num2 > 0)
			{
				dataSet = FacadeManage.aideRecordFacade.GetLossUserByDay(daysDate, daysDate2);
				DataTable dataTable = dataSet.Tables[0];
				int num3 = 0;
				string empty = string.Empty;
				for (int i = daysDate; i <= daysDate2; i++)
				{
					empty = TextUtility.GetDateTimeByDays(i).ToString("yyyyMMdd");
					DataRow[] array = dataTable.Select("DateID=" + i);
					num3 = ((array.Length != 0) ? ((num != 0) ? Convert.ToInt32(array[0]["LossPayUser"]) : Convert.ToInt32(array[0]["LossUser"])) : 0);
					dictionary.Add(empty, num3);
				}
			}
			List<object> list = new List<object>();
			if (dictionary != null && dictionary.Keys.Count > 0)
			{
				foreach (string key in dictionary.Keys)
				{
					list.Add(new
					{
						key = key,
						vaule = TypeUtil.ObjectToString(dictionary[key])
					});
				}
				return Json(new
				{
					IsOk = true,
					Data = JsonConvert.SerializeObject(list)
				});
			}
			return Json(new
			{
				IsOk = true,
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		[HttpGet]
		public ActionResult MatchUserLossChart()
		{
			int num = TypeUtil.ObjectToInt(base.Request["UserType"]);
			string key = "MatchUserLoss_" + DateTime.Now.ToString("yyyyMMdd") + "-" + num;
			System.Web.Helpers.Chart chart = System.Web.Helpers.Chart.GetFromCache(key);
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			if (chart == null)
			{
				DataSet lossUserByMonth = FacadeManage.aideRecordFacade.GetLossUserByMonth();
				DataTable dataTable = lossUserByMonth.Tables[0];
				DateTime dateTime = default(DateTime);
				DateTime t = default(DateTime);
				if (dataTable.Rows.Count > 0)
				{
					DataRow dataRow = (from r in dataTable.AsEnumerable()
					orderby r.Field<string>("CollectDate") descending
					select r).FirstOrDefault();
					DataRow dataRow2 = (from r in dataTable.AsEnumerable()
					orderby r.Field<string>("CollectDate")
					select r).FirstOrDefault();
					dateTime = Convert.ToDateTime(dataRow["CollectDate"]);
					t = Convert.ToDateTime(dataRow2["CollectDate"]);
					string empty = string.Empty;
					int num2 = 0;
					int num3 = 0;
					while (t <= dateTime)
					{
						DataRow[] array = dataTable.Select("CollectDate='" + t.ToString("yyyy-MM") + "'");
						num2 = ((array.Length != 0) ? ((num != 0) ? Convert.ToInt32(array[0]["LossPayUserTotal"]) : Convert.ToInt32(array[0]["LossUserTotal"])) : 0);
						empty = empty + "统计时间：" + t.AddMonths(-1).ToString("yyyyMM") + "\n";
						object obj = empty;
						empty = obj + "流失数：" + num2 + "\n";
						list.Add(t.AddMonths(-1).ToString("yyyyMM"));
						list2.Add(num2.ToString());
						empty = string.Empty;
						num3++;
						t = t.AddMonths(1);
					}
				}
				System.Web.Helpers.Chart chart2 = new System.Web.Helpers.Chart(1200, 250, "<Chart BackColor=\"#C9DC87\" BackGradientStyle=\"TopBottom\" BorderColor=\"181, 64, 1\" BorderWidth=\"2\" BorderlineDashStyle=\"Solid\" Palette=\"BrightPastel\">\r\n  <ChartAreas>\r\n    <ChartArea Name=\"Default\" _Template_=\"All\" BackColor=\"Transparent\" BackSecondaryColor=\"White\" BorderColor=\"64, 64, 64, 64\" BorderDashStyle=\"Solid\" ShadowColor=\"Transparent\">\r\n      <AxisY LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid Interval=\"Auto\" LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisY>\r\n      <AxisX LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisX>\r\n      <Area3DStyle Inclination=\"15\" IsClustered=\"False\" IsRightAngleAxes=\"False\" Perspective=\"10\" Rotation=\"10\" WallWidth=\"0\" />\r\n    </ChartArea>\r\n  </ChartAreas>\r\n  <Legends>\r\n    <Legend _Template_=\"All\" Alignment=\"Center\" BackColor=\"Transparent\" Docking=\"Bottom\" Font=\"Trebuchet MS, 8.25pt, style=Bold\" IsTextAutoFit =\"False\" LegendStyle=\"Row\">\r\n    </Legend>\r\n  </Legends>\r\n  <BorderSkin SkinStyle=\"Emboss\" />\r\n</Chart>", null).AddTitle("【月平均每天流失玩家总数】", null);
				IEnumerable xValue = list;
				IEnumerable yValues = list2;
				int markerStep = 1;
				chart = chart2.AddSeries("月平均每天流失玩家总数统计", "Column", null, "月平均每天流失玩家总数统计", null, markerStep, xValue, null, yValues, null).SetXAxis("月", 0.0, double.NaN).SetYAxis("人数", 0.0, double.NaN);
				base.Session["xValue"] = list;
				base.Session["yValue"] = list2;
			}
			chart.SaveToCache(key, 5, true);
			return new ChartResult(chart, "png");
		}

		[CheckCustomer]
		public ActionResult UserActiveStat()
		{
			string text = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd");
			string text2 = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
			base.ViewBag.StartTime = text2;
			base.ViewBag.EndTime = text;
			base.ViewBag.ActiveUserRate = "0";
			base.ViewBag.AVGOnlineTime = "";
			base.ViewBag.ActiveUserCounts = "0";
			DataSet usersNumber = FacadeManage.aideAccountsFacade.GetUsersNumber();
			DataTable dataTable = usersNumber.Tables[0];
			if (dataTable.Rows.Count > 0)
			{
				DataRow dataRow = dataTable.Rows[0];
				int num = 0;
				int value = 0;
				int num2 = 0;
				num = Convert.ToInt32(dataRow["UserTotal"]);
				num2 = Convert.ToInt32(dataRow["UserAVGOnlineTime"]);
				value = Convert.ToInt32(dataRow["ActiveUserCounts"]);
				base.ViewBag.ActiveUserRate = ((num == 0) ? "0" : ((Convert.ToDecimal(value) / Convert.ToDecimal(num) * 100m).ToString("0.0") + "%"));
				base.ViewBag.AVGOnlineTime = num2.ToString();
				base.ViewBag.ActiveUserCounts = value.ToString();
			}
			return PartialView();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetUserActiveStat()
		{
			DateTime dateTime = TypeUtil.StringToDateTime(base.Request["startDate"]);
			DateTime dateTime2 = TypeUtil.StringToDateTime(base.Request["endDate"]);
			TypeUtil.ObjectToInt(base.Request["type"]);
			int i = Convert.ToInt32(Fetch.GetDateID(dateTime));
			int num = Convert.ToInt32(Fetch.GetDateID(dateTime2));
			int num2 = num - i;
			DataSet dataSet = new DataSet();
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (dateTime2.AddMonths(-6) <= dateTime && num2 > 0)
			{
				dataSet = FacadeManage.aideTreasureFacade.GetActiveUserByDay(i, num);
				DataTable dataTable = dataSet.Tables[0];
				int num3 = 0;
				int num4 = 0;
				for (; i <= num; i++)
				{
					DataRow[] array = dataTable.Select("DateID=" + i);
					num3 = ((array.Length != 0) ? array.Count() : 0);
					dictionary.Add(Fetch.ShowDate(i), num3);
					num4++;
				}
			}
			List<object> list = new List<object>();
			if (dictionary != null && dictionary.Keys.Count > 0)
			{
				foreach (string key in dictionary.Keys)
				{
					list.Add(new
					{
						key = key,
						vaule = TypeUtil.ObjectToString(dictionary[key])
					});
				}
				return Json(new
				{
					IsOk = true,
					Data = JsonConvert.SerializeObject(list)
				});
			}
			return Json(new
			{
				IsOk = true,
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[HttpGet]
		[AntiSqlInjection]
		[CheckCustomer]
		public ActionResult DayUserActiveStatChart()
		{
			DateTime dateTime = TypeUtil.StringToDateTime(base.Request["startDate"]);
			DateTime dateTime2 = TypeUtil.StringToDateTime(base.Request["endDate"]);
			int num = TypeUtil.ObjectToInt(base.Request["type"]);
			int i = Convert.ToInt32(Fetch.GetDateID(dateTime));
			int num2 = Convert.ToInt32(Fetch.GetDateID(dateTime2));
			int num3 = num2 - i;
			string key = "DayUserActiveStat_" + dateTime.ToString("yyyyMMdd") + "-" + dateTime2.ToString("yyyyMMdd") + num;
			System.Web.Helpers.Chart chart = System.Web.Helpers.Chart.GetFromCache(key);
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			if (chart == null && dateTime2.AddMonths(-6) <= dateTime && num3 > 0)
			{
				DataSet activeUserByDay = FacadeManage.aideTreasureFacade.GetActiveUserByDay(i, num2);
				DataTable dataTable = activeUserByDay.Tables[0];
				int num4 = 0;
				int num5 = 0;
				for (; i <= num2; i++)
				{
					DataRow[] array = dataTable.Select("DateID=" + i);
					num4 = ((array.Length != 0) ? array.Count() : 0);
					list.Add(Fetch.ShowDate(i));
					list2.Add(num4.ToString());
					num5++;
				}
				chart = new System.Web.Helpers.Chart(1200, 250, "<Chart BackColor=\"#C9DC87\" BackGradientStyle=\"TopBottom\" BorderColor=\"181, 64, 1\" BorderWidth=\"2\" BorderlineDashStyle=\"Solid\" Palette=\"BrightPastel\">\r\n  <ChartAreas>\r\n    <ChartArea Name=\"Default\" _Template_=\"All\" BackColor=\"Transparent\" BackSecondaryColor=\"White\" BorderColor=\"64, 64, 64, 64\" BorderDashStyle=\"Solid\" ShadowColor=\"Transparent\">\r\n      <AxisY LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid Interval=\"Auto\" LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisY>\r\n      <AxisX LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisX>\r\n      <Area3DStyle Inclination=\"15\" IsClustered=\"False\" IsRightAngleAxes=\"False\" Perspective=\"10\" Rotation=\"10\" WallWidth=\"0\" />\r\n    </ChartArea>\r\n  </ChartAreas>\r\n  <Legends>\r\n    <Legend _Template_=\"All\" Alignment=\"Center\" BackColor=\"Transparent\" Docking=\"Bottom\" Font=\"Trebuchet MS, 8.25pt, style=Bold\" IsTextAutoFit =\"False\" LegendStyle=\"Row\">\r\n    </Legend>\r\n  </Legends>\r\n  <BorderSkin SkinStyle=\"Emboss\" />\r\n</Chart>", null).AddTitle("【每天活跃玩家数—当天在线时长大于1个小时的玩家】", null).AddSeries(xValue: list, yValues: list2, markerStep: 1, name: "每天活跃玩家数统计", chartType: (num == 0) ? "line" : "Column", chartArea: null, axisLabel: "每天活跃玩家数统计", legend: null, xField: null, yFields: null).SetXAxis("月", 0.0, double.NaN)
					.SetYAxis("活跃玩家数", 0.0, double.NaN);
				base.Session["xValue"] = list;
				base.Session["yValue"] = list2;
			}
			chart.SaveToCache(key, 5, true);
			return new ChartResult(chart, "png");
		}

		[HttpGet]
		[CheckCustomer]
		public ActionResult MonthUserActiveStatChart()
		{
			string key = "MonthUserActiveStat_" + DateTime.Now.ToString("yyyyMMdd");
			System.Web.Helpers.Chart chart = System.Web.Helpers.Chart.GetFromCache(key);
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			if (chart == null)
			{
				DataSet activieUserByMonth = FacadeManage.aideTreasureFacade.GetActivieUserByMonth();
				DataTable dataTable = activieUserByMonth.Tables[0];
				DateTime dateTime = default(DateTime);
				DateTime t = default(DateTime);
				if (dataTable.Rows.Count > 0)
				{
					DataRow dataRow = (from r in dataTable.AsEnumerable()
					orderby r.Field<string>("StatDate") descending
					select r).FirstOrDefault();
					DataRow dataRow2 = (from r in dataTable.AsEnumerable()
					orderby r.Field<string>("StatDate")
					select r).FirstOrDefault();
					dateTime = Convert.ToDateTime(dataRow["StatDate"]);
					t = Convert.ToDateTime(dataRow2["StatDate"]);
					int num = 0;
					int num2 = 0;
					while (t <= dateTime)
					{
						DataRow[] array = dataTable.Select("StatDate='" + t.ToString("yyyy-MM") + "'");
						num = ((array.Length != 0) ? array.Count() : 0);
						list.Add(t.ToString("yyyy-MM"));
						list2.Add(num.ToString());
						num2++;
						t = t.AddMonths(1);
					}
				}
				System.Web.Helpers.Chart chart2 = new System.Web.Helpers.Chart(1200, 250, "<Chart BackColor=\"#C9DC87\" BackGradientStyle=\"TopBottom\" BorderColor=\"181, 64, 1\" BorderWidth=\"2\" BorderlineDashStyle=\"Solid\" Palette=\"BrightPastel\">\r\n  <ChartAreas>\r\n    <ChartArea Name=\"Default\" _Template_=\"All\" BackColor=\"Transparent\" BackSecondaryColor=\"White\" BorderColor=\"64, 64, 64, 64\" BorderDashStyle=\"Solid\" ShadowColor=\"Transparent\">\r\n      <AxisY LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid Interval=\"Auto\" LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisY>\r\n      <AxisX LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisX>\r\n      <Area3DStyle Inclination=\"15\" IsClustered=\"False\" IsRightAngleAxes=\"False\" Perspective=\"10\" Rotation=\"10\" WallWidth=\"0\" />\r\n    </ChartArea>\r\n  </ChartAreas>\r\n  <Legends>\r\n    <Legend _Template_=\"All\" Alignment=\"Center\" BackColor=\"Transparent\" Docking=\"Bottom\" Font=\"Trebuchet MS, 8.25pt, style=Bold\" IsTextAutoFit =\"False\" LegendStyle=\"Row\">\r\n    </Legend>\r\n  </Legends>\r\n  <BorderSkin SkinStyle=\"Emboss\" />\r\n</Chart>", null).AddTitle("【每个月活跃玩家数—本月在线时长大于40个小时的玩家】", null);
				IEnumerable xValue = list;
				IEnumerable yValues = list2;
				int markerStep = 1;
				chart = chart2.AddSeries("每个月活跃玩家数统计", "Column", null, "每个月活跃玩家数统计", null, markerStep, xValue, null, yValues, null).SetXAxis("月", 0.0, double.NaN).SetYAxis("活跃玩家数", 0.0, double.NaN);
				base.Session["xValue"] = list;
				base.Session["yValue"] = list2;
			}
			chart.SaveToCache(key, 5, true);
			return new ChartResult(chart, "png");
		}

		[CheckCustomer]
		public ActionResult SystemStat()
		{
			base.ViewBag.OnLineCount = "";
			base.ViewBag.DisenableCount = "";
			base.ViewBag.AllCount = "";
			base.ViewBag.Score = "";
			base.ViewBag.InsureScore = "";
			base.ViewBag.AllScore = "";
			base.ViewBag.RegPresent = "";
			base.ViewBag.AgentRegPresent = "";
			base.ViewBag.DBPresent = "";
			base.ViewBag.QDPresent = "";
			base.ViewBag.YBPresent = "";
			base.ViewBag.MLPresent = "";
			base.ViewBag.QDPresent = "";
			base.ViewBag.YBPresent = "";
			base.ViewBag.MLPresent = "";
			base.ViewBag.RWPresent = "";
			base.ViewBag.OnlinePresent = "";
			base.ViewBag.SMPresent = "";
			base.ViewBag.DayPresent = "";
			base.ViewBag.MatchPresent = "";
			base.ViewBag.DJPresent = "";
			base.ViewBag.SharePresent = "";
			base.ViewBag.LotteryPresent = "";
			base.ViewBag.WebPresent = "";
			base.ViewBag.LoveLiness = "";
			base.ViewBag.Present = "";
			base.ViewBag.RemainLove = "";
			base.ViewBag.ConvertPresent = "";
			base.ViewBag.Revenue = "";
			base.ViewBag.TransferRevenue = "";
			base.ViewBag.Waste = "";
			base.ViewBag.CardCount = "";
			base.ViewBag.CardGold = "";
			base.ViewBag.CardPrice = "";
			base.ViewBag.CardPayCount = "";
			base.ViewBag.CardPayGold = "";
			base.ViewBag.CardPayPrice = "";
			base.ViewBag.MemberCardCount = "0";
			DataSet statInfo = FacadeManage.aideTreasureFacade.GetStatInfo();
			DataTable dataTable = statInfo.Tables[0];
			if (dataTable.Rows.Count > 0)
			{
				base.ViewBag.OnLineCount = TypeUtil.FormatMoney(dataTable.Rows[0]["OnLineCount"].ToString());
				base.ViewBag.DisenableCount = TypeUtil.FormatMoney(dataTable.Rows[0]["DisenableCount"].ToString());
				base.ViewBag.AllCount = TypeUtil.FormatMoney(dataTable.Rows[0]["AllCount"].ToString());
				base.ViewBag.Score = TypeUtil.FormatMoney(dataTable.Rows[0]["Score"].ToString());
				base.ViewBag.InsureScore = TypeUtil.FormatMoney(dataTable.Rows[0]["InsureScore"].ToString());
				base.ViewBag.AllScore = TypeUtil.FormatMoney(dataTable.Rows[0]["AllScore"].ToString());
				base.ViewBag.RegPresent = TypeUtil.FormatMoney(dataTable.Rows[0]["RegPresent"].ToString());
				base.ViewBag.AgentRegPresent = TypeUtil.FormatMoney(dataTable.Rows[0]["AgentRegPresent"].ToString());
				base.ViewBag.DBPresent = TypeUtil.FormatMoney(dataTable.Rows[0]["DBPresent"].ToString());
				base.ViewBag.QDPresent = TypeUtil.FormatMoney(dataTable.Rows[0]["QDPresent"].ToString());
				base.ViewBag.YBPresent = TypeUtil.FormatMoney(dataTable.Rows[0]["YBPresent"].ToString());
				base.ViewBag.MLPresent = TypeUtil.FormatMoney(dataTable.Rows[0]["MLPresent"].ToString());
				base.ViewBag.OnlinePresent = TypeUtil.FormatMoney(dataTable.Rows[0]["OnlinePresent"].ToString());
				base.ViewBag.RWPresent = TypeUtil.FormatMoney(dataTable.Rows[0]["RWPresent"].ToString());
				base.ViewBag.SMPresent = TypeUtil.FormatMoney(dataTable.Rows[0]["SMPresent"].ToString());
				base.ViewBag.DayPresent = TypeUtil.FormatMoney(dataTable.Rows[0]["DayPresent"].ToString());
				base.ViewBag.MatchPresent = TypeUtil.FormatMoney(dataTable.Rows[0]["MatchPresent"].ToString());
				base.ViewBag.DJPresent = TypeUtil.FormatMoney(dataTable.Rows[0]["DJPresent"].ToString());
				base.ViewBag.SharePresent = TypeUtil.FormatMoney(dataTable.Rows[0]["SharePresent"].ToString());
				base.ViewBag.LotteryPresent = TypeUtil.FormatMoney(dataTable.Rows[0]["LotteryPresent"].ToString());
				base.ViewBag.WebPresent = TypeUtil.FormatMoney(dataTable.Rows[0]["WebPresent"].ToString());
				base.ViewBag.LoveLiness = TypeUtil.FormatMoney(dataTable.Rows[0]["LoveLiness"].ToString());
				base.ViewBag.Present = TypeUtil.FormatMoney(dataTable.Rows[0]["Present"].ToString());
				base.ViewBag.RemainLove = TypeUtil.FormatMoney(dataTable.Rows[0]["RemainLove"].ToString());
				base.ViewBag.ConvertPresent = TypeUtil.FormatMoney(dataTable.Rows[0]["ConvertPresent"].ToString());
				base.ViewBag.Revenue = TypeUtil.FormatMoney(dataTable.Rows[0]["Revenue"].ToString());
				base.ViewBag.TransferRevenue = TypeUtil.FormatMoney(dataTable.Rows[0]["TransferRevenue"].ToString());
				base.ViewBag.Waste = TypeUtil.FormatMoney(dataTable.Rows[0]["Waste"].ToString());
				base.ViewBag.CardCount = TypeUtil.FormatMoney(dataTable.Rows[0]["CardCount"].ToString());
				base.ViewBag.CardGold = TypeUtil.FormatMoney(dataTable.Rows[0]["CardGold"].ToString());
				base.ViewBag.CardPrice = TypeUtil.FormatMoney(dataTable.Rows[0]["CardPrice"].ToString());
				base.ViewBag.CardPayCount = TypeUtil.FormatMoney(dataTable.Rows[0]["CardPayCount"].ToString());
				base.ViewBag.CardPayGold = TypeUtil.FormatMoney(dataTable.Rows[0]["CardPayGold"].ToString());
				base.ViewBag.CardPayPrice = TypeUtil.FormatMoney(dataTable.Rows[0]["CardPayPrice"].ToString());
				base.ViewBag.MemberCardCount = TypeUtil.FormatMoney(dataTable.Rows[0]["MemberCardCount"].ToString());
			}
			DataTable gameRevenue = FacadeManage.aideRecordFacade.GetGameRevenue();
			base.ViewData["GameTax"] = gameRevenue;
			DataTable roomRevenue = FacadeManage.aideRecordFacade.GetRoomRevenue();
			base.ViewData["RoomTax"] = roomRevenue;
			DataTable gameWaste = FacadeManage.aideRecordFacade.GetGameWaste();
			base.ViewData["GameWast"] = gameWaste;
			DataTable roomWaste = FacadeManage.aideRecordFacade.GetRoomWaste();
			base.ViewData["RoomWast"] = roomWaste;
			return View();
		}

		public ActionResult GameRecord()
		{
			base.ViewBag.StartDate = DateTime.Now.ToString("yyyy-MM-") + "01";
			base.ViewBag.EndDate = DateTime.Now.AddDays(1.0).ToString("yyyy-MM-dd");
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetGameRecord()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["DateType"]);
			string orderby = "ORDER BY DrawID DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (num > 0)
			{
				stringBuilder.AppendFormat(" AND ServerID = {0} ", num);
			}
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			switch (num2)
			{
			case 1:
			{
				string text = Fetch.GetTodayTime().Split('$')[0].ToString();
				string text2 = Fetch.GetTodayTime().Split('$')[1].ToString();
				if (text != "" && text2 != "")
				{
					stringBuilder.AppendFormat(" AND ConcludeTime >= '{0}' AND ConcludeTime < '{1}'", text, Convert.ToDateTime(text2).AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			}
			case 2:
			{
				string text = Fetch.GetYesterdayTime().Split('$')[0].ToString();
				string text2 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
				if (text != "" && text2 != "")
				{
					stringBuilder.AppendFormat(" AND ConcludeTime >= '{0}' AND ConcludeTime < '{1}'", text, Convert.ToDateTime(text2).AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			}
			case 3:
			{
				string text = Fetch.GetWeekTime().Split('$')[0].ToString();
				string text2 = Fetch.GetWeekTime().Split('$')[1].ToString();
				if (text != "" && text2 != "")
				{
					stringBuilder.AppendFormat(" AND ConcludeTime >= '{0}' AND ConcludeTime < '{1}'", text, Convert.ToDateTime(text2).AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			}
			case 4:
			{
				string text = Fetch.GetLastWeekTime().Split('$')[0].ToString();
				string text2 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
				if (text != "" && text2 != "")
				{
					stringBuilder.AppendFormat(" AND ConcludeTime >= '{0}' AND ConcludeTime < '{1}'", text, Convert.ToDateTime(text2).AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			}
			case 5:
				if (DateTime.TryParse(base.Request["StartDate"].ToString(), out result))
				{
					stringBuilder.AppendFormat(" AND ConcludeTime >= '{0}' ", result);
				}
				if (DateTime.TryParse(base.Request["EndDate"].ToString(), out result2))
				{
					stringBuilder.AppendFormat(" AND  ConcludeTime < '{0}'", result2);
				}
				break;
			}
			PagerSet recordDrawInfoList = FacadeManage.aideTreasureFacade.GetRecordDrawInfoList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (recordDrawInfoList != null && recordDrawInfoList.PageSet != null && recordDrawInfoList.PageSet.Tables.Count > 0)
			{
				for (int i = 0; i < recordDrawInfoList.PageSet.Tables[0].Rows.Count; i++)
				{
					DataRow dataRow = recordDrawInfoList.PageSet.Tables[0].Rows[i];
					list.Add(new
					{
						InsertTime = TypeUtil.ObjectToDateTime(dataRow["InsertTime"]).ToString("yyyy-MM-dd HH:mm:ss"),
						DrawID = TypeUtil.ObjectToInt(dataRow["DrawID"]),
						GameKindName = TypeUtil.GetGameKindName(TypeUtil.ObjectToInt(dataRow["KindID"])),
						GameRoomName = TypeUtil.GetGameRoomName(TypeUtil.ObjectToInt(dataRow["ServerID"])),
						TableID = TypeUtil.ObjectToString(dataRow["TableID"]),
						UserCount = TypeUtil.ObjectToString(dataRow["UserCount"]),
						AndroidCount = TypeUtil.ObjectToString(dataRow["AndroidCount"]),
						Waste = TypeUtil.ObjectToInt(dataRow["Waste"]).ToString("N0"),
						Revenue = TypeUtil.ObjectToInt(dataRow["Revenue"]).ToString("N0"),
						UserMedal = TypeUtil.ObjectToString(dataRow["UserMedal"]),
						StartTime = TypeUtil.ObjectToDateTime(dataRow["StartTime"]).ToString("yyyy-MM-dd HH:mm:ss"),
						ConcludeTime = TypeUtil.ObjectToDateTime(dataRow["ConcludeTime"]).ToString("yyyy-MM-dd HH:mm:ss")
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = recordDrawInfoList.RecordCount,
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetUserGameRecord()
		{
			if (user != null)
			{
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(1L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
			}
			int num = TypeUtil.ObjectToInt(base.Request["drawID"]);
			if (num < 1)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数出错"
				});
			}
			List<object> list = new List<object>();
			DataSet recordDrawScoreByDrawID = FacadeManage.aideTreasureFacade.GetRecordDrawScoreByDrawID(num);
			DataTable dataTable = new DataTable();
			if (recordDrawScoreByDrawID.Tables[0].Rows.Count > 0)
			{
				dataTable = recordDrawScoreByDrawID.Tables[0].Clone();
				dataTable.Columns["IsAndroid"].DataType = typeof(string);
				dataTable.Columns["Score"].DataType = typeof(decimal);
				dataTable.Columns["Revenue"].DataType = typeof(decimal);
				for (int i = 0; i <= recordDrawScoreByDrawID.Tables[0].Rows.Count - 1; i++)
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow = recordDrawScoreByDrawID.Tables[0].Rows[i];
					dataTable.Rows.Add(dataRow.ItemArray);
					if (Convert.ToInt32(dataTable.Rows[i]["IsAndroid"]) == 0)
					{
						dataTable.Rows[i]["IsAndroid"] = "否";
					}
					else
					{
						dataTable.Rows[i]["IsAndroid"] = "是";
					}
					dataTable.Rows[i]["Score"] = Convert.ToDecimal(dataTable.Rows[i]["Score"]).ToString("N");
					dataTable.Rows[i]["Revenue"] = Convert.ToDecimal(dataTable.Rows[i]["Revenue"]).ToString("N");
				}
			}
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				foreach (DataRow row in dataTable.Rows)
				{
					list.Add(new
					{
						Accounts = TypeUtil.ObjectToString(row["Accounts"]),
						NickName = TypeUtil.ObjectToString(row["NickName"]),
						GameID = TypeUtil.ObjectToString(row["GameID"]),
						IsAndroid = TypeUtil.ObjectToString(row["IsAndroid"]),
						ChairID = TypeUtil.ObjectToString(row["ChairID"]),
						Score = TypeUtil.ObjectToDecimal(row["Score"]).ToString("N"),
						Revenue = TypeUtil.ObjectToDecimal(row["Revenue"]).ToString("N"),
						UserMedal = TypeUtil.ObjectToString(row["UserMedal"]),
						PlayTimeCount = TypeUtil.ObjectToString(row["PlayTimeCount"]),
						InsertTime = TypeUtil.ObjectToString(row["InsertTime"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public ActionResult BankTaxStat()
		{
			base.ViewBag.StartTime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-01";
			base.ViewBag.EndTime = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd");
			return PartialView();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetBankTaxStat()
		{
			DateTime dateTime = TypeUtil.StringToDateTime(base.Request["startDate"]);
			DateTime dateTime2 = TypeUtil.StringToDateTime(base.Request["endDate"]);
			TypeUtil.ObjectToInt(base.Request["type"]);
			int num = Convert.ToInt32((dateTime2 - dateTime).TotalDays);
			DataSet dataSet = new DataSet();
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (dateTime2.AddMonths(-6) <= dateTime && num > 0)
			{
				dataSet = FacadeManage.aideRecordFacade.GetBankTaxByDay(Convert.ToInt32(Fetch.GetDateID(dateTime)), Convert.ToInt32(Fetch.GetDateID(dateTime2)));
				long num2 = 0L;
				for (int i = 0; i <= num; i++)
				{
					string key = dateTime.AddDays((double)i).ToString("yyyyMMdd");
					string dateID = Fetch.GetDateID(dateTime.AddDays((double)i));
					DataRow[] array = dataSet.Tables[0].Select("DateID=" + dateID);
					num2 = ((array.Length == 0) ? 0 : Convert.ToInt64(array[0]["BankTax"]));
					dictionary.Add(key, num2);
				}
			}
			List<object> list = new List<object>();
			if (dictionary != null && dictionary.Keys.Count > 0)
			{
				foreach (string key2 in dictionary.Keys)
				{
					list.Add(new
					{
						key = key2,
						vaule = TypeUtil.ObjectToString(dictionary[key2])
					});
				}
				return Json(new
				{
					IsOk = true,
					Data = JsonConvert.SerializeObject(list)
				});
			}
			return Json(new
			{
				IsOk = true,
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[HttpGet]
		[CheckCustomer]
		[AntiSqlInjection]
		public ActionResult DayBankTaxStatChart()
		{
			DateTime dateTime = TypeUtil.StringToDateTime(base.Request["startDate"]);
			DateTime dateTime2 = TypeUtil.StringToDateTime(base.Request["endDate"]);
			int num = TypeUtil.ObjectToInt(base.Request["type"]);
			int num2 = Convert.ToInt32((dateTime2 - dateTime).TotalDays);
			string key = "DayBankTaxStat_" + dateTime.ToString("yyyyMMdd") + "-" + dateTime2.ToString("yyyyMMdd") + num;
			System.Web.Helpers.Chart chart = System.Web.Helpers.Chart.GetFromCache(key);
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			if (chart == null)
			{
				if (dateTime2.AddMonths(-6) <= dateTime && num2 > 0)
				{
					DataSet bankTaxByDay = FacadeManage.aideRecordFacade.GetBankTaxByDay(Convert.ToInt32(Fetch.GetDateID(dateTime)), Convert.ToInt32(Fetch.GetDateID(dateTime2)));
					long num3 = 0L;
					for (int i = 0; i <= num2; i++)
					{
						string item = dateTime.AddDays((double)i).ToString("yyyyMMdd");
						string dateID = Fetch.GetDateID(dateTime.AddDays((double)i));
						DataRow[] array = bankTaxByDay.Tables[0].Select("DateID=" + dateID);
						num3 = ((array.Length == 0) ? 0 : Convert.ToInt64(array[0]["BankTax"]));
						list.Add(item);
						list2.Add(num3.ToString());
					}
				}
				chart = new System.Web.Helpers.Chart(1200, 300, "<Chart BackColor=\"#C9DC87\" BackGradientStyle=\"TopBottom\" BorderColor=\"181, 64, 1\" BorderWidth=\"2\" BorderlineDashStyle=\"Solid\" Palette=\"BrightPastel\">\r\n  <ChartAreas>\r\n    <ChartArea Name=\"Default\" _Template_=\"All\" BackColor=\"Transparent\" BackSecondaryColor=\"White\" BorderColor=\"64, 64, 64, 64\" BorderDashStyle=\"Solid\" ShadowColor=\"Transparent\">\r\n      <AxisY LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid Interval=\"Auto\" LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisY>\r\n      <AxisX LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisX>\r\n      <Area3DStyle Inclination=\"15\" IsClustered=\"False\" IsRightAngleAxes=\"False\" Perspective=\"10\" Rotation=\"10\" WallWidth=\"0\" />\r\n    </ChartArea>\r\n  </ChartAreas>\r\n  <Legends>\r\n    <Legend _Template_=\"All\" Alignment=\"Center\" BackColor=\"Transparent\" Docking=\"Bottom\" Font=\"Trebuchet MS, 8.25pt, style=Bold\" IsTextAutoFit =\"False\" LegendStyle=\"Row\">\r\n    </Legend>\r\n  </Legends>\r\n  <BorderSkin SkinStyle=\"Emboss\" />\r\n</Chart>", null).AddTitle("【保险柜税收日统计图】", null).AddSeries(xValue: list, yValues: list2, markerStep: 1, name: "", chartType: (num == 0) ? "line" : "Column", chartArea: null, axisLabel: "", legend: null, xField: null, yFields: null).SetXAxis("日期", 0.0, double.NaN)
					.SetYAxis("每日保险柜税收", 0.0, double.NaN);
				base.Session["xValue"] = list;
				base.Session["yValue"] = list2;
			}
			chart.SaveToCache(key, 5, true);
			return new ChartResult(chart, "png");
		}

		[HttpGet]
		[CheckCustomer]
		public ActionResult MonthBankTaxStatChart()
		{
			string key = "MonthBankTaxStat_" + DateTime.Now.ToString("yyyyMMdd");
			System.Web.Helpers.Chart chart = System.Web.Helpers.Chart.GetFromCache(key);
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			if (chart == null)
			{
				DataSet bankTaxByMonth = FacadeManage.aideRecordFacade.GetBankTaxByMonth();
				DataTable dataTable = bankTaxByMonth.Tables[0];
				DateTime dateTime = default(DateTime);
				DateTime t = default(DateTime);
				if (dataTable.Rows.Count > 0)
				{
					DataRow dataRow = (from r in dataTable.AsEnumerable()
					orderby r.Field<string>("StatDate") descending
					select r).FirstOrDefault();
					DataRow dataRow2 = (from r in dataTable.AsEnumerable()
					orderby r.Field<string>("StatDate")
					select r).FirstOrDefault();
					dateTime = Convert.ToDateTime(dataRow["StatDate"]);
					t = Convert.ToDateTime(dataRow2["StatDate"]);
					long num = 0L;
					int num2 = 0;
					while (t <= dateTime)
					{
						DataRow[] array = dataTable.Select("StatDate='" + t.ToString("yyyy-MM") + "'");
						num = ((array.Length == 0) ? 0 : Convert.ToInt64(array[0]["BankTax"]));
						list.Add(t.ToString("yyyyMM"));
						list2.Add(num.ToString());
						num2++;
						t = t.AddMonths(1);
					}
				}
				System.Web.Helpers.Chart chart2 = new System.Web.Helpers.Chart(1200, 250, "<Chart BackColor=\"#C9DC87\" BackGradientStyle=\"TopBottom\" BorderColor=\"181, 64, 1\" BorderWidth=\"2\" BorderlineDashStyle=\"Solid\" Palette=\"BrightPastel\">\r\n  <ChartAreas>\r\n    <ChartArea Name=\"Default\" _Template_=\"All\" BackColor=\"Transparent\" BackSecondaryColor=\"White\" BorderColor=\"64, 64, 64, 64\" BorderDashStyle=\"Solid\" ShadowColor=\"Transparent\">\r\n      <AxisY LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid Interval=\"Auto\" LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisY>\r\n      <AxisX LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisX>\r\n      <Area3DStyle Inclination=\"15\" IsClustered=\"False\" IsRightAngleAxes=\"False\" Perspective=\"10\" Rotation=\"10\" WallWidth=\"0\" />\r\n    </ChartArea>\r\n  </ChartAreas>\r\n  <Legends>\r\n    <Legend _Template_=\"All\" Alignment=\"Center\" BackColor=\"Transparent\" Docking=\"Bottom\" Font=\"Trebuchet MS, 8.25pt, style=Bold\" IsTextAutoFit =\"False\" LegendStyle=\"Row\">\r\n    </Legend>\r\n  </Legends>\r\n  <BorderSkin SkinStyle=\"Emboss\" />\r\n</Chart>", null).AddTitle("【保险柜税收月统计图】", null);
				IEnumerable xValue = list;
				IEnumerable yValues = list2;
				int markerStep = 1;
				chart = chart2.AddSeries("", "line", null, "", null, markerStep, xValue, null, yValues, null).SetXAxis("月份", 0.0, double.NaN).SetYAxis("每月保险柜税收", 0.0, double.NaN);
				base.Session["xValue"] = list;
				base.Session["yValue"] = list2;
			}
			chart.SaveToCache(key, 5, true);
			return new ChartResult(chart, "png");
		}

		[CheckCustomer]
		public ActionResult GameTaxStat()
		{
			base.ViewBag.StartTime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-01";
			base.ViewBag.EndTime = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd");
			base.ViewBag.startDateID = Convert.ToInt32(Fetch.GetDateID(Convert.ToDateTime(base.ViewBag.StartTime)));
			base.ViewBag.endDateID = Convert.ToInt32(Fetch.GetDateID(Convert.ToDateTime(base.ViewBag.EndTime)));
			return PartialView();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetGameTaxStat()
		{
			DateTime dateTime = TypeUtil.StringToDateTime(base.Request["startDate"]);
			DateTime dateTime2 = TypeUtil.StringToDateTime(base.Request["endDate"]);
			TypeUtil.ObjectToInt(base.Request["type"]);
			int num = Convert.ToInt32((dateTime2 - dateTime).TotalDays);
			DataSet dataSet = new DataSet();
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (dateTime2.AddMonths(-6) <= dateTime && num > 0)
			{
				dataSet = FacadeManage.aideRecordFacade.GetGameTaxByDay(Convert.ToInt32(Fetch.GetDateID(dateTime)), Convert.ToInt32(Fetch.GetDateID(dateTime2)));
				int num2 = 0;
				for (int i = 0; i <= num; i++)
				{
					string key = dateTime.AddDays((double)i).ToString("yyyyMMdd");
					string dateID = Fetch.GetDateID(dateTime.AddDays((double)i));
					DataRow[] array = dataSet.Tables[0].Select("DateID=" + dateID);
					num2 = ((array.Length != 0) ? Convert.ToInt32(array[0]["GameTax"]) : 0);
					dictionary.Add(key, num2);
				}
			}
			List<object> list = new List<object>();
			if (dictionary != null && dictionary.Keys.Count > 0)
			{
				foreach (string key2 in dictionary.Keys)
				{
					list.Add(new
					{
						key = key2,
						vaule = TypeUtil.ObjectToString(dictionary[key2])
					});
				}
				return Json(new
				{
					IsOk = true,
					Data = JsonConvert.SerializeObject(list)
				});
			}
			return Json(new
			{
				IsOk = true,
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		[HttpGet]
		public ActionResult DayGameTaxStatChart()
		{
			DateTime dateTime = TypeUtil.StringToDateTime(base.Request["startDate"]);
			DateTime dateTime2 = TypeUtil.StringToDateTime(base.Request["endDate"]);
			int num = TypeUtil.ObjectToInt(base.Request["type"]);
			int num2 = Convert.ToInt32((dateTime2 - dateTime).TotalDays);
			string key = "DayGameTaxStat_" + dateTime.ToString("yyyyMMdd") + "-" + dateTime2.ToString("yyyyMMdd") + num;
			System.Web.Helpers.Chart chart = System.Web.Helpers.Chart.GetFromCache(key);
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			if (chart == null)
			{
				if (dateTime2.AddMonths(-6) <= dateTime && num2 > 0)
				{
					DataSet gameTaxByDay = FacadeManage.aideRecordFacade.GetGameTaxByDay(Convert.ToInt32(Fetch.GetDateID(dateTime)), Convert.ToInt32(Fetch.GetDateID(dateTime2)));
					int num3 = 0;
					for (int i = 0; i <= num2; i++)
					{
						string item = dateTime.AddDays((double)i).ToString("yyyyMMdd");
						string dateID = Fetch.GetDateID(dateTime.AddDays((double)i));
						DataRow[] array = gameTaxByDay.Tables[0].Select("DateID=" + dateID);
						num3 = ((array.Length != 0) ? Convert.ToInt32(array[0]["GameTax"]) : 0);
						list.Add(item);
						list2.Add(num3.ToString());
					}
				}
				chart = new System.Web.Helpers.Chart(1200, 300, "<Chart BackColor=\"#C9DC87\" BackGradientStyle=\"TopBottom\" BorderColor=\"181, 64, 1\" BorderWidth=\"2\" BorderlineDashStyle=\"Solid\" Palette=\"BrightPastel\">\r\n  <ChartAreas>\r\n    <ChartArea Name=\"Default\" _Template_=\"All\" BackColor=\"Transparent\" BackSecondaryColor=\"White\" BorderColor=\"64, 64, 64, 64\" BorderDashStyle=\"Solid\" ShadowColor=\"Transparent\">\r\n      <AxisY LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid Interval=\"Auto\" LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisY>\r\n      <AxisX LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisX>\r\n      <Area3DStyle Inclination=\"15\" IsClustered=\"False\" IsRightAngleAxes=\"False\" Perspective=\"10\" Rotation=\"10\" WallWidth=\"0\" />\r\n    </ChartArea>\r\n  </ChartAreas>\r\n  <Legends>\r\n    <Legend _Template_=\"All\" Alignment=\"Center\" BackColor=\"Transparent\" Docking=\"Bottom\" Font=\"Trebuchet MS, 8.25pt, style=Bold\" IsTextAutoFit =\"False\" LegendStyle=\"Row\">\r\n    </Legend>\r\n  </Legends>\r\n  <BorderSkin SkinStyle=\"Emboss\" />\r\n</Chart>", null).AddTitle("【游戏税收日统计图】", null).AddSeries(xValue: list, yValues: list2, markerStep: 1, name: "", chartType: (num == 0) ? "line" : "Column", chartArea: null, axisLabel: "", legend: null, xField: null, yFields: null).SetXAxis("日期", 0.0, double.NaN)
					.SetYAxis("每日游戏税收", 0.0, double.NaN);
				base.Session["xValue"] = list;
				base.Session["yValue"] = list2;
			}
			chart.SaveToCache(key, 5, true);
			return new ChartResult(chart, "png");
		}

		[CheckCustomer]
		[HttpGet]
		public ActionResult MonthGameTaxStatChart()
		{
			string key = "MonthGameTaxStat_" + DateTime.Now.ToString("yyyyMMdd");
			System.Web.Helpers.Chart chart = System.Web.Helpers.Chart.GetFromCache(key);
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			if (chart == null)
			{
				DataSet gameTaxByMonth = FacadeManage.aideRecordFacade.GetGameTaxByMonth();
				DataTable dataTable = gameTaxByMonth.Tables[0];
				DateTime dateTime = default(DateTime);
				DateTime t = default(DateTime);
				if (dataTable.Rows.Count > 0)
				{
					DataRow dataRow = (from r in dataTable.AsEnumerable()
					orderby r.Field<string>("StatDate") descending
					select r).FirstOrDefault();
					DataRow dataRow2 = (from r in dataTable.AsEnumerable()
					orderby r.Field<string>("StatDate")
					select r).FirstOrDefault();
					dateTime = Convert.ToDateTime(dataRow["StatDate"]);
					t = Convert.ToDateTime(dataRow2["StatDate"]);
					int num = 0;
					int num2 = 0;
					while (t <= dateTime)
					{
						DataRow[] array = dataTable.Select("StatDate='" + t.ToString("yyyy-MM") + "'");
						num = ((array.Length != 0) ? Convert.ToInt32(array[0]["GameTax"]) : 0);
						list.Add(t.ToString("yyyyMM"));
						list2.Add(num.ToString());
						num2++;
						t = t.AddMonths(1);
					}
				}
				System.Web.Helpers.Chart chart2 = new System.Web.Helpers.Chart(1200, 250, "<Chart BackColor=\"#C9DC87\" BackGradientStyle=\"TopBottom\" BorderColor=\"181, 64, 1\" BorderWidth=\"2\" BorderlineDashStyle=\"Solid\" Palette=\"BrightPastel\">\r\n  <ChartAreas>\r\n    <ChartArea Name=\"Default\" _Template_=\"All\" BackColor=\"Transparent\" BackSecondaryColor=\"White\" BorderColor=\"64, 64, 64, 64\" BorderDashStyle=\"Solid\" ShadowColor=\"Transparent\">\r\n      <AxisY LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid Interval=\"Auto\" LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisY>\r\n      <AxisX LineColor=\"64, 64, 64, 64\">\r\n        <MajorGrid LineColor=\"64, 64, 64, 64\" />\r\n        <LabelStyle Font=\"Trebuchet MS, 8.25pt, style=Bold\" />\r\n      </AxisX>\r\n      <Area3DStyle Inclination=\"15\" IsClustered=\"False\" IsRightAngleAxes=\"False\" Perspective=\"10\" Rotation=\"10\" WallWidth=\"0\" />\r\n    </ChartArea>\r\n  </ChartAreas>\r\n  <Legends>\r\n    <Legend _Template_=\"All\" Alignment=\"Center\" BackColor=\"Transparent\" Docking=\"Bottom\" Font=\"Trebuchet MS, 8.25pt, style=Bold\" IsTextAutoFit =\"False\" LegendStyle=\"Row\">\r\n    </Legend>\r\n  </Legends>\r\n  <BorderSkin SkinStyle=\"Emboss\" />\r\n</Chart>", null).AddTitle("【游戏税收月统计图】", null);
				IEnumerable xValue = list;
				IEnumerable yValues = list2;
				int markerStep = 1;
				chart = chart2.AddSeries("", "line", null, "", null, markerStep, xValue, null, yValues, null).SetXAxis("月份", 0.0, double.NaN).SetYAxis("每月游戏税收", 0.0, double.NaN);
				base.Session["xValue"] = list;
				base.Session["yValue"] = list2;
			}
			chart.SaveToCache(key, 5, true);
			return new ChartResult(chart, "png");
		}

		[CheckCustomer]
		public ActionResult GameTaxList()
		{
			int num = Convert.ToInt32(Fetch.GetDateID(Convert.ToDateTime(base.Request["startDateID"])));
			int num2 = Convert.ToInt32(Fetch.GetDateID(Convert.ToDateTime(base.Request["endDateID"])));
			base.ViewBag.StartDateID = num;
			base.ViewBag.EndDateID = num2;
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetGameTaxList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["StartDateID"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["EndDateID"]);
			string orderby = "ORDER BY DateID DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (num > 0 && num2 > 0)
			{
				stringBuilder.AppendFormat(" AND DateID>={0} AND DateID<={1} ", num, num2);
			}
			PagerSet list = FacadeManage.aideRecordFacade.GetList("RecordEveryDayData", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list2 = new List<object>();
			if (list != null && list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				for (int i = 0; i < list.PageSet.Tables[0].Rows.Count; i++)
				{
					DataRow dataRow = list.PageSet.Tables[0].Rows[i];
					list2.Add(new
					{
						ShowDate = Fetch.ShowDate(TypeUtil.ObjectToInt(dataRow["DateID"])),
						DateID = TypeUtil.ObjectToInt(dataRow["DateID"]),
						GameTax = TypeUtil.ObjectToString(dataRow["GameTax"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonConvert.SerializeObject(list2)
			});
		}

		[CheckCustomer]
		public ActionResult GameKindTaxList()
		{
			int dateID = TypeUtil.ObjectToInt(base.Request["dateID"]);
			base.ViewData["data"] = null;
			DataSet gameTaxListByDateID = FacadeManage.aideRecordFacade.GetGameTaxListByDateID(dateID);
			if (gameTaxListByDateID != null && gameTaxListByDateID.Tables.Count > 0)
			{
				base.ViewData["data"] = gameTaxListByDateID.Tables[0];
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult ClearTableData()
		{
			DataSet dataSet = new DataSet();
			dataSet = FacadeManage.aideTreasureFacade.StatRecordTable();
			base.ViewData["data"] = null;
			if (dataSet != null && dataSet.Tables.Count > 0)
			{
				base.ViewData["data"] = dataSet.Tables[0];
			}
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DoClearTableData()
		{
			if (user != null)
			{
				int moduleID = 812;
				AdminPermission adminPermission = new AdminPermission(user, moduleID);
				if (!adminPermission.GetPermission(8L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
			}
			int num = TypeUtil.StringToInt(base.Request["ID"]);
			string text = TypeUtil.ObjectToString(base.Request["DateTime"]);
			if (num < 1)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数出错"
				});
			}
			if (string.IsNullOrEmpty(text))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "清理记录截止时间不能为空"
				});
			}
			DateTime result = DateTime.Now;
			if (!DateTime.TryParse(text, out result))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "清理记录截止时间不能为空"
				});
			}
			if (DateTime.Now.AddDays(-14.0) <= result)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "截止时间至少保留14天数据"
				});
			}
			int num2 = 0;
			switch (num)
			{
			case 1:
				num2 = FacadeManage.aideTreasureFacade.DeleteRecordUserInoutByTime(result);
				break;
			case 2:
				num2 = FacadeManage.aideTreasureFacade.DeleteRecordDrawInfoByTime(result);
				break;
			case 3:
				num2 = FacadeManage.aideTreasureFacade.DeleteRecordDrawScoreByTime(result);
				break;
			case 4:
				num2 = FacadeManage.aideTreasureFacade.DeleteRecordInsureByTime(result);
				break;
			case 5:
				num2 = FacadeManage.aideTreasureFacade.DeletePresentByTime(result);
				break;
			case 6:
				num2 = FacadeManage.aideTreasureFacade.DeleteSpreadByTime(result);
				break;
			case 7:
				num2 = FacadeManage.aideTreasureFacade.DeleteAgentScoreByTime(result);
				break;
			case 8:
				num2 = FacadeManage.aideTreasureFacade.DeleteAgentChangeByTime(result);
				break;
			case 9:
				num2 = FacadeManage.aideTreasureFacade.DeleteSystemByTime(result);
				break;
			}
			return Json(new
			{
				IsOk = true,
				Msg = "操作成功，共删除了" + num2 + "条记录"
			});
		}
	}
}
