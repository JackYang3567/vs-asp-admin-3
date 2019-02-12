using Admin.Filters;
using Game.Entity.Accounts;
using Game.Entity.Record;
using Game.Entity.Treasure;
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
	public class PromoterController : BaseController
	{
		[CheckCustomer]
		public ActionResult Index()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult PromoterInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			string text = TypeUtil.ObjectToString(base.Request["uname"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["gameid"]);
			if (num == 0 || text == "" || num2 == 0)
			{
				return Content("参数错误");
			}
			base.ViewBag.UserID = num;
			base.ViewBag.GameID = num2;
			base.ViewBag.Accounts = text;
			DataTable dataTable = FacadeManage.aideAccountsFacade.MyOwnSpreadCount(num);
			return View(dataTable.Rows[0]);
		}

		[CheckCustomer]
		public ActionResult PromoterManager()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			string text = TypeUtil.ObjectToString(base.Request["uname"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["gameid"]);
			if (num == 0 || text == "" || num2 == 0)
			{
				return Content("参数错误");
			}
			base.ViewBag.UserID = num;
			base.ViewBag.GameID = num2;
			base.ViewBag.Accounts = text;
			DataTable dataTable = FacadeManage.aideAccountsFacade.MySpreadCntInfo(num, 0);
			return View(dataTable.Rows[0]);
		}

		[CheckCustomer]
		public ActionResult MyUserOnline()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			if (num == 0)
			{
				return Content("参数错误");
			}
			base.ViewBag.UserID = num;
			return View();
		}

		[CheckCustomer]
		public ActionResult MyUserRev()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			if (num == 0)
			{
				return Content("参数错误");
			}
			base.ViewBag.UserID = num;
			return View();
		}

		[CheckCustomer]
		public ActionResult MyUser()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			if (num == 0)
			{
				return Content("参数错误");
			}
			base.ViewBag.UserID = num;
			return View();
		}

		[CheckCustomer]
		public ActionResult MyUserAll()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			if (num == 0)
			{
				return Content("参数错误");
			}
			base.ViewBag.UserID = num;
			return View();
		}

		[CheckCustomer]
		public ActionResult PromoterReport()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			if (num == 0)
			{
				return Content("参数错误");
			}
			base.ViewBag.UserID = num;
			base.ViewBag.StartDate = DateTime.Now.ToString("yyyy-MM-dd");
			return View();
		}

		[CheckCustomer]
		public ActionResult AdvertiserList()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult Advertiser()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			DataTable value = null;
			base.ViewBag.OP = "添加";
			base.ViewBag.ID = num;
			if (num > 0)
			{
				base.ViewBag.OP = "更新";
				string where = "where ID=" + num;
				value = FacadeManage.aideRecordFacade.GetAdvertisers(" ID,Advertiser,AdKey,Url,PcUrl,Descr,StatusT ", where);
			}
			base.ViewData["dt"] = value;
			return View();
		}

		[CheckCustomer]
		public ActionResult AdvertTaskList()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult AdvertTask()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			AdvertTask advertTask = new AdvertTask();
			if (num > 0)
			{
				base.ViewBag.OP = "编辑";
				DataTable advertTask2 = FacadeManage.aideRecordFacade.GetAdvertTask(" ID,AdvertiserID,TaskName,StatusT,AdID,BeginTime,EndTime,AddTime ", "where ID=" + num);
				if (advertTask2 != null && advertTask2.Rows.Count > 0)
				{
					advertTask.ID = TypeUtil.ObjectToInt(advertTask2.Rows[0]["ID"]);
					advertTask.AdvertiserID = TypeUtil.ObjectToInt(advertTask2.Rows[0]["AdvertiserID"]);
					advertTask.TaskName = TypeUtil.ObjectToString(advertTask2.Rows[0]["TaskName"]);
					advertTask.StatusT = (short)TypeUtil.ObjectToInt(advertTask2.Rows[0]["StatusT"]);
					advertTask.AdID = TypeUtil.ObjectToString(advertTask2.Rows[0]["AdID"]);
					advertTask.BeginTime = TypeUtil.ObjectToDateTime(advertTask2.Rows[0]["BeginTime"]).ToString("yyyy-MM-dd HH:mm:ss");
					advertTask.EndTime = TypeUtil.ObjectToDateTime(advertTask2.Rows[0]["EndTime"]).ToString("yyyy-MM-dd HH:mm:ss");
					advertTask.AddTime = TypeUtil.ObjectToDateTime(advertTask2.Rows[0]["AddTime"]).ToString("yyyy-MM-dd HH:mm:ss");
				}
				if (advertTask != null && advertTask.AdvertiserID > 0)
				{
					DataTable advertisers = FacadeManage.aideRecordFacade.GetAdvertisers(" ID,Advertiser", "where ID=" + advertTask.AdvertiserID);
					if (advertisers != null && advertisers.Rows.Count > 0)
					{
						advertTask.Advertiser = TypeUtil.ObjectToString(advertisers.Rows[0]["Advertiser"]);
					}
				}
				base.ViewData["data"] = advertTask;
			}
			else
			{
				DataTable advertisers2 = FacadeManage.aideRecordFacade.GetAdvertisers(" ID,Advertiser", "");
				base.ViewData["tb"] = advertisers2;
				base.ViewBag.OP = "添加";
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult RankingList()
		{
			DataTable advertisers = FacadeManage.aideRecordFacade.GetAdvertisers(" ID,Advertiser ", "");
			base.ViewData["adves"] = advertisers;
			return View();
		}

		[CheckCustomer]
		public ActionResult EditAdvertTaskKinds()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			string text = TypeUtil.ObjectToString(base.Request["task"]);
			base.ViewBag.TaxkName = text;
			DataTable advertTaskKinds = FacadeManage.aideRecordFacade.GetAdvertTaskKinds(num);
			base.ViewData["tb"] = advertTaskKinds;
			return View();
		}

		[CheckCustomer]
		public ActionResult PromoterSet()
		{
			GlobalSpreadInfo globalSpreadInfo = FacadeManage.aideTreasureFacade.GetGlobalSpreadInfo(1);
			return View(globalSpreadInfo);
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetList()
		{
			int num = Convert.ToInt32(base.Request.Form["wType"]);
			string formString = GameRequest.GetFormString("wValue");
			int @int = GameRequest.GetInt("pageIndex", 1);
			int int2 = GameRequest.GetInt("pageSize", 30);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(" WHERE 1=1");
			if (formString != "")
			{
				switch (num)
				{
				case 1:
					if (!Validate.IsPositiveInt(formString))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "用户ID格式错误！"
						});
					}
					stringBuilder.AppendFormat(" AND GameID={0}", formString);
					break;
				case 2:
					stringBuilder.AppendFormat(" AND Accounts='{0}'", formString.Trim());
					break;
				case 3:
					stringBuilder.AppendFormat(" AND SpreadAcc='{0}'", formString.Trim());
					break;
				default:
					if (Validate.IsPositiveInt(formString))
					{
						stringBuilder.AppendFormat(" AND (GameID={0} OR Accounts = '{0}'  OR SpreadAcc = '{0}' )", formString.Trim());
					}
					else
					{
						stringBuilder.AppendFormat(" AND Accounts = '{0}' OR SpreadAcc = '{0}' ", formString.Trim());
					}
					break;
				}
			}
			string text = TypeUtil.ObjectToString(base.Request["order"]);
			string str = TypeUtil.ObjectToString(base.Request["sort"]);
			string orderby = "ORDER BY SpreadScore+OutScore DESC";
			if (text != "")
			{
				orderby = "ORDER BY " + text + " " + str;
			}
			try
			{
				PagerSet list = FacadeManage.aideAccountsFacade.GetList("RYAgentDB..View_Spread", @int, int2, stringBuilder.ToString(), orderby);
				DataTable o = list.PageSet.Tables[0];
				string sql = "Select ISNULL(Sum(OutScore),0) AS OutScore,ISNULL(Sum(SpreadScore),0) AS SpreadScore From RYAgentDB..View_Spread" + stringBuilder.ToString();
				DataSet dataSetBySql = FacadeManage.aideTreasureFacade.GetDataSetBySql(sql);
				string msg = "";
				if (dataSetBySql.Tables[0].Rows.Count > 0)
				{
					DataRow dataRow = dataSetBySql.Tables[0].Rows[0];
					msg = JsonConvert.SerializeObject(new
					{
						SumOutScore = dataRow["OutScore"],
						SumSpreadScore = dataRow["SpreadScore"]
					});
				}
				return Json(new
				{
					IsOk = true,
					Msg = msg,
					Total = list.RecordCount,
					Data = JsonHelper.SerializeObject(o)
				});
			}
			catch (Exception ex)
			{
				return Json(new
				{
					IsOk = false,
					Msg = ex.Message
				});
			}
		}

		[CheckCustomer]
		public JsonResult UserSpreadInfo()
		{
			int uid = TypeUtil.ObjectToInt(base.Request.Form["uid"]);
			int dateType = TypeUtil.ObjectToInt(base.Request.Form["dtype"]);
			DataTable o = FacadeManage.aideAccountsFacade.MySpreadInfo(uid, dateType);
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Data = JsonHelper.SerializeObject(o)
			});
		}

		[CheckCustomer]
		public JsonResult GetMyUserOnline()
		{
			int num = TypeUtil.ObjectToInt(base.Request.Form["uid"]);
			if (num > 0)
			{
				int @int = GameRequest.GetInt("pageIndex", 1);
				int int2 = GameRequest.GetInt("pageSize", 10);
				try
				{
					StringBuilder stringBuilder = new StringBuilder(" WHERE ServerID>-1");
					stringBuilder.AppendFormat(" AND myuserid={0} AND UserID<>{0}", num);
					string[] fields = new string[7]
					{
						"Accounts",
						"RealName",
						"WinCount",
						"LostCount",
						"PlayTimeCount",
						"RegisterDate",
						"LastLogonDate"
					};
					PagerSet list = FacadeManage.aideAccountsFacade.GetList("RYAgentDB..View_spreaders", @int, int2, stringBuilder.ToString(), "ORDER BY UserID DESC", fields);
					DataTable o = list.PageSet.Tables[0];
					return Json(new
					{
						IsOk = true,
						Msg = "",
						Total = list.RecordCount,
						Data = JsonHelper.SerializeObject(o)
					});
				}
				catch (Exception ex)
				{
					return Json(new
					{
						IsOk = false,
						Msg = ex.Message
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "参数错误"
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetMyUserRev()
		{
			int num = TypeUtil.ObjectToInt(base.Request.Form["uid"]);
			if (num > 0)
			{
				int @int = GameRequest.GetInt("pageIndex", 1);
				int int2 = GameRequest.GetInt("pageSize", 10);
				string text = TypeUtil.ObjectToString(base.Request["order"]);
				string str = TypeUtil.ObjectToString(base.Request["sort"]);
				string orderby = "ORDER BY RevGx DESC";
				if (text != "")
				{
					orderby = "ORDER BY " + text + " " + str;
				}
				try
				{
					StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
					stringBuilder.AppendFormat(" AND myuserid={0} AND UserID<>{0}", num);
					string[] fields = new string[5]
					{
						"Accounts",
						"TotalRev",
						"Rate",
						"RevGx",
						"AllGx"
					};
					PagerSet list = FacadeManage.aideAccountsFacade.GetList("RYAgentDB..View_spreaders", @int, int2, stringBuilder.ToString(), orderby, fields);
					DataTable o = list.PageSet.Tables[0];
					return Json(new
					{
						IsOk = true,
						Msg = "",
						Total = list.RecordCount,
						Data = JsonHelper.SerializeObject(o)
					});
				}
				catch (Exception ex)
				{
					return Json(new
					{
						IsOk = false,
						Msg = ex.Message
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "参数错误"
			});
		}

		[CheckCustomer]
		public JsonResult GetMyUser()
		{
			int num = TypeUtil.ObjectToInt(base.Request.Form["uid"]);
			if (num > 0)
			{
				int @int = GameRequest.GetInt("pageIndex", 1);
				int int2 = GameRequest.GetInt("pageSize", 10);
				try
				{
					StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
					stringBuilder.AppendFormat(" AND myuserid={0} AND SpreaderID={0}", num);
					string[] fields = new string[2]
					{
						"Accounts",
						"RegisterDate"
					};
					PagerSet list = FacadeManage.aideAccountsFacade.GetList("RYAgentDB..View_spreaders", @int, int2, stringBuilder.ToString(), "ORDER BY UserID DESC", fields);
					DataTable o = list.PageSet.Tables[0];
					return Json(new
					{
						IsOk = true,
						Msg = "",
						Total = list.RecordCount,
						Data = JsonHelper.SerializeObject(o)
					});
				}
				catch (Exception ex)
				{
					return Json(new
					{
						IsOk = false,
						Msg = ex.Message
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "参数错误"
			});
		}

		[CheckCustomer]
		public JsonResult GetMyUserAll()
		{
			int num = TypeUtil.ObjectToInt(base.Request.Form["uid"]);
			if (num > 0)
			{
				int @int = GameRequest.GetInt("pageIndex", 1);
				int int2 = GameRequest.GetInt("pageSize", 10);
				try
				{
					StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
					stringBuilder.AppendFormat(" AND myuserid={0} AND UserID<>{0}", num);
					string[] fields = new string[2]
					{
						"Accounts",
						"RegisterDate"
					};
					PagerSet list = FacadeManage.aideAccountsFacade.GetList("RYAgentDB..View_spreaders", @int, int2, stringBuilder.ToString(), "ORDER BY UserID DESC", fields);
					DataTable o = list.PageSet.Tables[0];
					return Json(new
					{
						IsOk = true,
						Msg = "",
						Total = list.RecordCount,
						Data = JsonHelper.SerializeObject(o)
					});
				}
				catch (Exception ex)
				{
					return Json(new
					{
						IsOk = false,
						Msg = ex.Message
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "参数错误"
			});
		}

		[CheckCustomer]
		public JsonResult GetPromoterReport()
		{
			int num = TypeUtil.ObjectToInt(base.Request.Form["uid"]);
			if (num > 0)
			{
				string account = TypeUtil.ObjectToString(base.Request.Form["KeyWord"]);
				int lev = TypeUtil.ObjectToInt(base.Request.Form["Lev"]);
				DateTime bTime = TypeUtil.ObjectToDateTime(base.Request.Form["StartDate"]);
				DateTime eTime = TypeUtil.ObjectToDateTime(base.Request.Form["EndDate"]).AddDays(1.0);
				int @int = GameRequest.GetInt("pageIndex", 1);
				int int2 = GameRequest.GetInt("pageSize", 10);
				try
				{
					PagerSet pagerSet = FacadeManage.aideAccountsFacade.QuerySpreadDailyRpt(num, account, lev, bTime, eTime, @int, int2);
					DataTable o = pagerSet.PageSet.Tables[0];
					return Json(new
					{
						IsOk = true,
						Msg = "",
						Total = pagerSet.RecordCount,
						Data = JsonHelper.SerializeObject(o)
					});
				}
				catch (Exception ex)
				{
					return Json(new
					{
						IsOk = false,
						Msg = ex.Message
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "参数错误"
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetRankingList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["pageIndex"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["pageSize"]);
			int num3 = TypeUtil.ObjectToInt(base.Request["TaskID"]);
			string value = TypeUtil.ObjectToString(base.Request["bTime"]);
			string value2 = TypeUtil.ObjectToString(base.Request["eTime"]);
			string text = TypeUtil.ObjectToString(base.Request["account"]);
			string value3 = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("TaskID", num3.ToString());
			dictionary.Add("bTime", value);
			dictionary.Add("eTime", value2);
			dictionary.Add("AccOrNick", value3);
			dictionary.Add("PageSize", string.IsNullOrEmpty(text) ? num2.ToString() : "0");
			dictionary.Add("PageIndex", num.ToString());
			dictionary.Add("output|PageCount", "0");
			dictionary.Add("output|RecordCount", "0");
			DataSet ranks = FacadeManage.aideRecordFacade.GetRanks(dictionary, "P_Rec_QueryAdvertRankNew", text);
			int total = 0;
			List<object> list = new List<object>();
			if (ranks != null && ranks.Tables.Count > 0 && ranks.Tables[0].Rows.Count > 0)
			{
				total = TypeUtil.ObjectToInt(ranks.Tables[1].Rows[0]["RecordCount"]);
				foreach (DataRow row in ranks.Tables[0].Rows)
				{
					list.Add(new
					{
						rnk = TypeUtil.ObjectToInt(row["rnk"]),
						Accounts = TypeUtil.ObjectToString(row["Accounts"]),
						NickName = TypeUtil.ObjectToString(row["NickName"]),
						ADID = TypeUtil.ObjectToString(row["ADID"]),
						KSScore = TypeUtil.ObjectToLong(row["KSScore"]),
						Score = TypeUtil.ObjectToLong(row["Score"]),
						MyScore = TypeUtil.ObjectToLong(row["MyScore"]),
						PlayTimeCount = TypeUtil.LongToTimeStr(TypeUtil.ObjectToLong(row["PlayTimeCount"]))
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = total,
				Data = JsonHelper.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public JsonResult GetAdvertTasks(int AdvertiserID)
		{
			if (AdvertiserID <= 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数错误"
				});
			}
			DataTable advertTask = FacadeManage.aideRecordFacade.GetAdvertTask(" ID,TaskName,BeginTime,EndTime ", "where AdvertiserID=" + AdvertiserID);
			List<object> list = new List<object>();
			if (advertTask != null && advertTask.Rows.Count > 0)
			{
				foreach (DataRow row in advertTask.Rows)
				{
					list.Add(new
					{
						ID = TypeUtil.ObjectToInt(row["ID"]),
						TaskName = TypeUtil.ObjectToString(row["TaskName"]),
						BeginTime = TypeUtil.ObjectToDateTime(row["BeginTime"]).ToString("yyyy-MM-dd"),
						EndTime = TypeUtil.ObjectToDateTime(row["EndTime"]).ToString("yyyy-MM-dd")
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Data = JsonHelper.SerializeObject(list)
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetAdvertiserList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"]);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"]);
			PagerSet list = FacadeManage.aideRecordFacade.GetList("T_Rec_Advertiser", pageIndex, pageSize, "", "ORDER BY ID DESC");
			List<object> list2 = new List<object>();
			if (list != null && list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in list.PageSet.Tables[0].Rows)
				{
					list2.Add(new
					{
						ID = TypeUtil.ObjectToInt(row["ID"]),
						Advertiser = TypeUtil.ObjectToString(row["Advertiser"]),
						AdKey = TypeUtil.ObjectToString(row["AdKey"]),
						Url = TypeUtil.ObjectToString(row["Url"]),
						PcUrl = TypeUtil.ObjectToString(row["PcUrl"]),
						Descr = TypeUtil.ObjectToString(row["Descr"]),
						StatusT = TypeUtil.ObjectToInt(row["StatusT"]),
						AddTime = TypeUtil.ObjectToString(row["AddTime"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list2)
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DoAdvertiser(TAdvertiser entity)
		{
			if (entity == null)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有提交任何数据"
				});
			}
			entity.Operator = user.Username;
			entity.ClientIP = GameRequest.GetUserIP();
			Message message = FacadeManage.aideRecordFacade.EditAdvertiser(entity);
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
				Msg = "操作失败"
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DoAdvertTask(AdvertTask entity)
		{
			if (entity == null)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有提交任何数据"
				});
			}
			Message message = FacadeManage.aideRecordFacade.EditAdvertTask(entity);
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
				Msg = "操作失败"
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DelAdvertiser(int ID)
		{
			if (ID < 1)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有删除的项"
				});
			}
			if (FacadeManage.aideRecordFacade.DelAdvertiser(ID))
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
				Msg = "操作失败"
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetAdvertTaskList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"]);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"]);
			PagerSet list = FacadeManage.aideRecordFacade.GetList("View_AdTask", pageIndex, pageSize, "", "ORDER BY ID DESC");
			List<object> list2 = new List<object>();
			if (list != null && list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in list.PageSet.Tables[0].Rows)
				{
					list2.Add(new
					{
						ID = TypeUtil.ObjectToInt(row["ID"]),
						Advertiser = TypeUtil.ObjectToString(row["Advertiser"]),
						TaskName = TypeUtil.ObjectToString(row["TaskName"]),
						AdID = TypeUtil.ObjectToString(row["AdID"]),
						StatusT = TypeUtil.ObjectToInt(row["StatusT"]),
						BeginTime = TypeUtil.ObjectToDateTime(row["BeginTime"]).ToString("yyyy-MM-dd HH:mm:ss"),
						EndTime = TypeUtil.ObjectToDateTime(row["EndTime"]).ToString("yyyy-MM-dd HH:mm:ss"),
						AddTime = TypeUtil.ObjectToDateTime(row["AddTime"]).ToString("yyyy-MM-dd HH:mm:ss")
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list2)
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DoAdvertTaskKinds()
		{
			string value = TypeUtil.ObjectToString(base.Request["list"]);
			if (string.IsNullOrEmpty(value))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有提交任何数据"
				});
			}
			List<T_Rec_AdvertTaskKindsEntity> list = JsonConvert.DeserializeObject<List<T_Rec_AdvertTaskKindsEntity>>(value);
			if (list == null || list.Count < 1)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "提交数据格式错误"
				});
			}
			if (FacadeManage.aideRecordFacade.EidterAdvertTaskKinds(list))
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
				Msg = "操作失败"
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult UpdatePromoterSet()
		{
			AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
			if (!adminPermission.GetPermission(4L))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有权限",
					Url = "/NoPower/Index"
				});
			}
			int formInt = GameRequest.GetFormInt("rate1", 0);
			int formInt2 = GameRequest.GetFormInt("rate2", 0);
			int formInt3 = GameRequest.GetFormInt("rate3", 0);
			int formInt4 = GameRequest.GetFormInt("rate4", 0);
			int formInt5 = GameRequest.GetFormInt("rate5", 0);
			if (formInt < 0 || formInt2 < 0 || formInt3 < 0 || formInt4 < 0 || formInt5 < 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "抽成比例不能为负数"
				});
			}
			if (formInt + formInt2 + formInt3 + formInt4 + formInt5 <= 100)
			{
				GlobalSpreadInfo globalSpreadInfo = new GlobalSpreadInfo();
				globalSpreadInfo.To1UperRate = formInt;
				globalSpreadInfo.To2UperRate = formInt2;
				globalSpreadInfo.To3UperRate = formInt3;
				globalSpreadInfo.To4UperRate = formInt4;
				globalSpreadInfo.To5UperRate = formInt5;
				globalSpreadInfo.ID = 1;
				try
				{
					FacadeManage.aideTreasureFacade.UpdatePromoterSet(globalSpreadInfo);
					return Json(new
					{
						IsOk = true,
						Msg = "操作成功"
					});
				}
				catch (Exception ex)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "操作失败：" + ex.Message
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "抽成比例总和不能超过100！"
			});
		}

		[CheckCustomer]
		public ActionResult NewIndex()
		{
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetNewSpreadInfoes()
		{
			int num = TypeUtil.ObjectToInt(base.Request["wType"]);
			string text = TypeUtil.ObjectToString(base.Request["wValue"]);
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"]);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"]);
			StringBuilder stringBuilder = new StringBuilder();
			if (text != "")
			{
				switch (num)
				{
				case 1:
				{
					int num2 = TypeUtil.ObjectToInt(text);
					stringBuilder.AppendFormat("where GameID={0}", num2);
					break;
				}
				case 2:
					stringBuilder.AppendFormat("where Accounts ='{0}'", text);
					break;
				default:
				{
					int result = 0;
					if (int.TryParse(text, out result))
					{
						stringBuilder.AppendFormat("where GameID={0} OR Accounts ='{0}'", text);
					}
					else
					{
						stringBuilder.AppendFormat("where Accounts ='{0}'", text);
					}
					break;
				}
				}
			}
			PagerSet list = FacadeManage.aideRecordFacade.GetList("RYAgentDB.dbo.View_YjSpread", pageIndex, pageSize, stringBuilder.ToString(), "ORDER BY UserID DESC");
			List<object> list2 = new List<object>();
			if (list != null && list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in list.PageSet.Tables[0].Rows)
				{
					list2.Add(new
					{
						UserID = TypeUtil.ObjectToInt(row["UserID"]),
						Accounts = TypeUtil.ObjectToString(row["Accounts"]),
						Compellation = TypeUtil.ObjectToString(row["Compellation"]),
						Score = TypeUtil.ObjectToString(row["Score"]),
						InsureScore = TypeUtil.ObjectToString(row["InsureScore"]),
						TotalYj = TypeUtil.ObjectToString(row["TotalYj"]),
						NowYj = TypeUtil.ObjectToString(row["NowYj"]),
						GameID = TypeUtil.ObjectToInt(row["GameID"]),
						Lev = TypeUtil.ObjectToInt(row["Lev"]),
						UserType = row["UserType"]
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list2)
			});
		}

		[CheckCustomer]
		public ActionResult NewSpreadManager()
		{
			int uid = TypeUtil.ObjectToInt(base.Request["param"]);
			DataTable mySpread = FacadeManage.aideTreasureFacade.GetMySpread(uid);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			decimal num7 = 0.0m;
			decimal num8 = 0.0m;
			decimal num9 = 0.0m;
			decimal num10 = 0.0m;
			decimal num11 = 0.0m;
			decimal num12 = 0.0m;
			decimal num13 = 0.0m;
			decimal num14 = 0.0m;
			decimal num15 = 0.0m;
			if (mySpread != null && mySpread.Rows.Count > 0)
			{
				num = TypeUtil.ObjectToInt(mySpread.Rows[0]["hyWeekNew"]);
				num2 = TypeUtil.ObjectToInt(mySpread.Rows[0]["hyMonthNew"]);
				num3 = TypeUtil.ObjectToInt(mySpread.Rows[0]["hyAll"]);
				num4 = TypeUtil.ObjectToInt(mySpread.Rows[0]["tgWeekNew"]);
				num5 = TypeUtil.ObjectToInt(mySpread.Rows[0]["tgMonthNew"]);
				num6 = TypeUtil.ObjectToInt(mySpread.Rows[0]["tgAll"]);
				num7 = TypeUtil.ObjectToDecimal(mySpread.Rows[0]["hyYj"]);
				num8 = TypeUtil.ObjectToDecimal(mySpread.Rows[0]["tgYj"]);
				num9 = TypeUtil.ObjectToDecimal(mySpread.Rows[0]["MyYj"]);
				num10 = TypeUtil.ObjectToDecimal(mySpread.Rows[0]["NowYj"]);
				num11 = TypeUtil.ObjectToDecimal(mySpread.Rows[0]["hyRtnFee"]);
				num12 = TypeUtil.ObjectToDecimal(mySpread.Rows[0]["tgRtnFee"]);
				num13 = TypeUtil.ObjectToDecimal(mySpread.Rows[0]["MyRtnFee"]);
				num14 = TypeUtil.ObjectToDecimal(mySpread.Rows[0]["NowRtnFee"]);
				num15 = TypeUtil.ObjectToDecimal(mySpread.Rows[0]["RtnFee"]);
			}
			base.ViewBag.hyWeekNew = num;
			base.ViewBag.hyMonthNew = num2;
			base.ViewBag.hyAll = num3;
			base.ViewBag.tgWeekNew = num4;
			base.ViewBag.tgMonthNew = num5;
			base.ViewBag.tgAll = num6;
			base.ViewBag.hyYj = num7;
			base.ViewBag.tgYj = num8;
			base.ViewBag.MyYj = num9;
			base.ViewBag.NowYj = num10;
			base.ViewBag.hyRtnFee = num11;
			base.ViewBag.tgRtnFee = num12;
			base.ViewBag.MyRtnFee = num13;
			base.ViewBag.NowRtnFee = num14;
			base.ViewBag.RtnFee = num15;
			return View();
		}

		[CheckCustomer]
		public ActionResult Commision()
		{
			DataSet dataSetBySql = FacadeManage.aideAccountsFacade.GetDataSetBySql("select LName,MinYJ,MaxYj,RtnAmt from RYAgentDB..View_SpreadLevCfg order by Lev asc");
			base.ViewData["data"] = dataSetBySql.Tables[0];
			return View();
		}

		[CheckCustomer]
		public ActionResult Withdraw()
		{
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetWithdraws()
		{
			int num = TypeUtil.ObjectToInt(base.Request["UserID"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["Type"]);
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"]);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"]);
			string text = TypeUtil.ObjectToString(base.Request["TimeStr"]);
			string text2 = TypeUtil.ObjectToString(base.Request["TimeEnd"]);
			StringBuilder stringBuilder = new StringBuilder("where UserID=" + num);
			if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
			{
				DateTime result = DateTime.Now;
				DateTime result2 = DateTime.Now;
				if (DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result < result2)
				{
					stringBuilder.AppendFormat(" and CollectDate>='{0}' and CollectDate<='{1}'", result, result2);
				}
			}
			if (num2 > 0)
			{
				stringBuilder.AppendFormat(" and TypeID={0} ", num2);
			}
			PagerSet list = FacadeManage.aideRecordFacade.GetList("RYAgentDB..View_RecordSpreadRtnFee", pageIndex, pageSize, stringBuilder.ToString(), "ORDER BY RecordID DESC");
			List<object> list2 = new List<object>();
			if (list != null && list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in list.PageSet.Tables[0].Rows)
				{
					list2.Add(new
					{
						RecordID = TypeUtil.ObjectToInt(row["RecordID"]),
						GameID = TypeUtil.ObjectToInt(row["GameID"]),
						TypeName = TypeUtil.ObjectToString(row["TypeName"]),
						PreFee = TypeUtil.ObjectToString(row["PreFee"]),
						SwapFee = TypeUtil.ObjectToString(row["SwapFee"]),
						CollectNote = TypeUtil.ObjectToString(row["CollectNote"]),
						CollectDate = TypeUtil.ObjectToDateTime(row["CollectDate"]).ToString("yyyy-MM-dd HH:MM:ss")
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list2)
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult EditCommision()
		{
			string value = TypeUtil.ObjectToString(base.Request["Data"]);
			if (string.IsNullOrEmpty(value))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数不能为空"
				});
			}
			List<T_SpreadLevCfg> list = JsonConvert.DeserializeObject<List<T_SpreadLevCfg>>(value);
			if (list == null || list.Count < 1)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数错误"
				});
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (T_SpreadLevCfg item in list)
			{
				stringBuilder.AppendFormat("update RYAgentDB.dbo.T_SpreadLevCfg set LName='{0}',MinYJ={1},RtnAmt={2} where Lev={3} and (LName<>'{0}' or MinYJ<>{1} or RtnAmt<>{2}) ;", item.LName, item.MinYJ, item.RtnAmt, item.Lev);
			}
			if (FacadeManage.aideAccountsFacade.ExecuteSql(stringBuilder.ToString()) > 0)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "操作成功："
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "操作失败："
			});
		}

		[CheckCustomer]
		public ActionResult Insurances()
		{
			base.ViewBag.Title = TypeUtil.ObjectToString(base.Request["uname"]) + "的推广员";
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetInsurances()
		{
			int num = TypeUtil.ObjectToInt(base.Request["UserID"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["wType"]);
			string text = TypeUtil.ObjectToString(base.Request["wValue"]);
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"]);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"]);
			StringBuilder stringBuilder = new StringBuilder("where 1=1");
			if (num2 > 0 && !string.IsNullOrEmpty(text))
			{
				switch (num2)
				{
				case 1:
				{
					int num3 = TypeUtil.ObjectToInt(text);
					stringBuilder.AppendFormat(" And GameID={0}", num3);
					break;
				}
				case 2:
					stringBuilder.AppendFormat(" And  Accounts ='{0}'", text);
					break;
				}
			}
			PagerSet list = FacadeManage.aideRecordFacade.GetList("RYAgentDB..Fn_GetSpreadDowns(" + num + ",1)", pageIndex, pageSize, stringBuilder.ToString(), "ORDER BY UserID DESC");
			string sql = "SELECT ISNULL(SUM(FillAmt),0) AS SumFillAmt,ISNULL(SUM(DrawMoney),0) AS SumDrawMoney FROM RYAgentDB..Fn_GetSpreadDowns(" + num + ",1) " + stringBuilder.ToString();
			DataTable dataTable = FacadeManage.aideAccountsFacade.GetDataSetBySql(sql).Tables[0];
			return Json(new
			{
				IsOk = true,
				Msg = new
				{
					SumFillAmt = dataTable.Rows[0][0],
					SumDrawMoney = dataTable.Rows[0][1]
				},
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[CheckCustomer]
		public ActionResult Understrappers()
		{
			base.ViewBag.Title = TypeUtil.ObjectToString(base.Request["uname"]) + "的下属玩家";
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetUnderstrappers()
		{
			int num = TypeUtil.ObjectToInt(base.Request["UserID"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["wType"]);
			string text = TypeUtil.ObjectToString(base.Request["wValue"]);
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"]);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"]);
			StringBuilder stringBuilder = new StringBuilder("where 1=1");
			if (num2 > 0 && !string.IsNullOrEmpty(text))
			{
				switch (num2)
				{
				case 1:
				{
					int num3 = TypeUtil.ObjectToInt(text);
					stringBuilder.AppendFormat(" And GameID={0}", num3);
					break;
				}
				case 2:
					stringBuilder.AppendFormat(" And Accounts ='{0}'", text);
					break;
				}
			}
			PagerSet list = FacadeManage.aideRecordFacade.GetList("RYAgentDB..Fn_GetSpreadDowns(" + num + ",0)", pageIndex, pageSize, stringBuilder.ToString(), "ORDER BY UserID DESC");
			string sql = "SELECT ISNULL(SUM(FillAmt),0) AS SumFillAmt,ISNULL(SUM(DrawMoney),0) AS SumDrawMoney FROM RYAgentDB..Fn_GetSpreadDowns(" + num + ",0) " + stringBuilder.ToString();
			DataTable dataTable = FacadeManage.aideAccountsFacade.GetDataSetBySql(sql).Tables[0];
			return Json(new
			{
				IsOk = true,
				Msg = new
				{
					SumFillAmt = dataTable.Rows[0][0],
					SumDrawMoney = dataTable.Rows[0][1]
				},
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[CheckCustomer]
		public ActionResult TradeManager()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetTradeList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["TradeType"]);
			string safeSQL = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["OrderID"]));
			string safeSQL2 = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["KeyWord"]));
			string orderby = "ORDER BY ID DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (!string.IsNullOrEmpty(safeSQL2))
			{
				stringBuilder.AppendFormat(" AND Accounts='{0}'", safeSQL2);
			}
			string text = base.Request["StartDate"];
			string text2 = base.Request["EndDate"];
			DateTime now = DateTime.Now;
			switch (num)
			{
			case 2:
				text = Fetch.GetTodayTime().Split('$')[0].ToString();
				text2 = Fetch.GetTodayTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", text, text2);
				break;
			case 3:
				text = Fetch.GetYesterdayTime().Split('$')[0].ToString();
				text2 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", text, text2);
				break;
			case 4:
				text = Fetch.GetWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetWeekTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", text, text2);
				break;
			case 5:
				text = Fetch.GetLastWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", text, text2);
				break;
			case 6:
				text = now.ToString("yyyy-MM-01");
				text2 = now.ToString("yyyy-MM-dd 23:59:59");
				stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", text, text2);
				break;
			case 7:
				text = now.AddMonths(-1).ToString("yyyy-MM-01");
				text2 = now.ToString("yyyy-MM-01");
				stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", text, text2);
				break;
			default:
				if (text != "")
				{
					stringBuilder.AppendFormat(" AND ApplyDate > '{0}'", Convert.ToDateTime(text));
				}
				if (text2 != "")
				{
					stringBuilder.AppendFormat(" AND ApplyDate <= '{0}'", Convert.ToDateTime(text2));
				}
				break;
			}
			if (!string.IsNullOrEmpty(safeSQL))
			{
				stringBuilder.AppendFormat(" AND OrderID='{0}'", safeSQL);
			}
			if (num2 > 0)
			{
				stringBuilder.AppendFormat(" AND DealStatus={0}", num2);
			}
			PagerSet list = FacadeManage.aideAccountsFacade.GetList("RYAgentDB.dbo.View_SpreadBalance", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			DataTable o = list.PageSet.Tables[0];
			decimal sum = FacadeManage.aideAccountsFacade.SumSpreadTrade(stringBuilder.ToString());
			return Json(new
			{
				IsOk = true,
				Msg = new
				{
					Sum = sum
				},
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(o)
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult doTrade(int type, string orderid, string resaon)
		{
			if (string.IsNullOrEmpty(orderid))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数错误"
				});
			}
			if (type == 1 && string.IsNullOrEmpty(resaon))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请输入拒绝理由！"
				});
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["strOrderID"] = orderid;
			dictionary["dwType"] = type;
			dictionary["strMsg"] = resaon;
			dictionary["strOperator"] = user.Username;
			dictionary["strErr"] = "";
			Message message = FacadeManage.aideAccountsFacade.ExcuteByProce("RYAgentDB.dbo.P_DealSpreadBalance", dictionary);
			if (message.Success)
			{
				return Json(new
				{
					IsOk = true,
					Msg = ""
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = message.Content
			});
		}

		[CheckCustomer]
		public JsonResult Daifu(int tradeId, string orderId, string bankAccount, string bankAccountCode, decimal bankAmount, string selectBankCode)
		{
			if (string.IsNullOrEmpty(orderId) || string.IsNullOrEmpty(bankAccount) || string.IsNullOrEmpty(bankAccountCode) || bankAmount <= 0m || string.IsNullOrEmpty(selectBankCode))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "订单号或者银行信息有一项未填写"
				});
			}
			string formString = GameRequest.GetFormString("bankAddress");
			if (formString == "")
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请输入开户行地址"
				});
			}
			string flowid = "";
			string text = GameRequest.GetString("Province");
			string text2 = GameRequest.GetString("City");
			if (text == "")
			{
				text = "江西";
			}
			if (text2 == "")
			{
				text2 = "南昌";
			}
			string text3 = DaiFu.Daifu_youmifu(orderId, bankAmount, bankAccount, bankAccountCode, selectBankCode, formString, text, text2, base.Request.Url.Host, out flowid);
			byte b = 3;
			string text4 = "";
			if (text3.ToUpper() == "SUCCESS")
			{
				b = 2;
				text4 = "代付成功";
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary["strOrderID"] = orderId;
				dictionary["dwType"] = b;
				dictionary["strMsg"] = text4;
				dictionary["strOperator"] = user.Username;
				dictionary["strErr"] = "";
				Message message = FacadeManage.aideAccountsFacade.ExcuteByProce("RYAgentDB.dbo.P_DealSpreadBalance", dictionary);
				if (message.Success)
				{
					return Json(new
					{
						IsOk = true,
						Msg = text4
					});
				}
				return Json(new
				{
					IsOk = false,
					Msg = text4 + "  数据处理失败，" + message.Content
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = text3
			});
		}

		[CheckCustomer]
		public ActionResult TradeSet()
		{
			CashSetting model = FacadeManage.aideAccountsFacade.PlayerCashInfo(2);
			return View(model);
		}
	}
}
