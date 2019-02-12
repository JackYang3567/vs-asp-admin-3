using Admin.Filters;
using Game.Entity.Accounts;
using Game.Facade;
using Game.Facade.Tools;
using Game.Kernel;
using Game.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
	public class DailiController : BaseController
	{
		[CheckCustomer]
		public ActionResult Index()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult Balance()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult AgentSet()
		{
			DataTable dataTable = FacadeManage.aideAccountsFacade.AgentCashInfo();
			if (dataTable.Rows.Count > 0)
			{
				return View(dataTable.Rows[0]);
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult AddInfo()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult EditInfo(int aid)
		{
			AgentInfo agentDetail = FacadeManage.aideAccountsFacade.GetAgentDetail(aid);
			return View(agentDetail);
		}

		[CheckCustomer]
		public ActionResult AllNextAgent(int aid)
		{
			base.ViewBag.aid = aid;
			return View();
		}

		[CheckCustomer]
		public ActionResult AllNextUser(int aid)
		{
			base.ViewBag.aid = aid;
			return View();
		}

		[CheckCustomer]
		public ActionResult AgentScoreChange(int aid)
		{
			base.ViewBag.aid = aid;
			return View();
		}

		[CheckCustomer]
		public ActionResult AgentBaseURL()
		{
			return View(FacadeManage.aideAccountsFacade.AgentBaseURL());
		}

		[CheckCustomer]
		public JsonResult Add()
		{
			string formString = GameRequest.GetFormString("Acc");
			string formString2 = GameRequest.GetFormString("Pwd");
			int formInt = GameRequest.GetFormInt("Rate", 0);
			string formString3 = GameRequest.GetFormString("RealName");
			string formString4 = GameRequest.GetFormString("QQ");
			string formString5 = GameRequest.GetFormString("Remarks");
			string formString6 = GameRequest.GetFormString("Domain");
			if (formString == "" || formString2 == "" || formString3 == "" || formString4 == "" || formInt < 0 || formInt > 100)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数错误"
				});
			}
			AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
			if (!adminPermission.GetPermission(2L))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有权限",
					Url = "/NoPower/Index"
				});
			}
			string value = TypeUtil.ObjectToString(base.Request["ShowName"]);
			string value2 = TypeUtil.ObjectToString(base.Request["WeChat"]);
			int num = TypeUtil.ObjectToInt(base.Request["IsClient"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["Sort"]);
			int formInt2 = GameRequest.GetFormInt("QueryRight", 0);
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["QueryRight"] = formInt2;
			dictionary["ParentID"] = 0;
			dictionary["agentAcc"] = formString;
			dictionary["Pwd"] = Utility.MD5(formString2);
			dictionary["SafePwd"] = "";
			dictionary["QQ"] = formString4;
			dictionary["ShowName"] = value;
			dictionary["ShowSort"] = num2;
			dictionary["WeChat"] = value2;
			dictionary["IsClient"] = num;
			dictionary["memo"] = formString5;
			dictionary["RealName"] = formString3;
			dictionary["agentRate"] = Convert.ToDouble(formInt) / 100.0;
			dictionary["agentDomain"] = formString6;
			dictionary["clientIP"] = GameRequest.GetUserIP();
			dictionary["strErr"] = "";
			Message message = FacadeManage.aideAccountsFacade.ExcuteByProce("RYAgentDB..P_Acc_AddAgentNew", dictionary);
			if (message.Success)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "添加成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = message.Content
			});
		}

		[CheckCustomer]
		public JsonResult Edit()
		{
			string formString = GameRequest.GetFormString("sData");
			if (!(formString == ""))
			{
				if (user != null)
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
				}
				AgentInfo agentInfo = JsonHelper.DeserializeJsonToObject<AgentInfo>(formString);
				if (agentInfo.Pwd != "")
				{
					agentInfo.Pwd = Utility.MD5(agentInfo.Pwd);
				}
				agentInfo.Operator = user.UserID;
				agentInfo.RegIP = GameRequest.GetUserIP();
				if (string.IsNullOrEmpty(agentInfo.WeChat))
				{
					agentInfo.WeChat = "";
				}
				if (string.IsNullOrEmpty(agentInfo.ShowName))
				{
					agentInfo.ShowName = "";
				}
				try
				{
					Message message = FacadeManage.aideAccountsFacade.UpdateAegnt(agentInfo);
					if (!message.Success)
					{
						return Json(new
						{
							IsOk = false,
							Msg = message.Content
						});
					}
					return Json(new
					{
						IsOk = true,
						Msg = "保存成功"
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
		public JsonResult Pay()
		{
			int formInt = GameRequest.GetFormInt("aid", 0);
			double num = Convert.ToDouble(base.Request.Form["score"]);
			if (formInt != 0 && num != 0.0)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary["AgentID"] = formInt;
				dictionary["Score"] = num;
				dictionary["opType"] = 0;
				dictionary["Operator"] = user.UserID;
				dictionary["ClientIP"] = GameRequest.GetUserIP();
				dictionary["strErr"] = "";
				try
				{
					Message message = FacadeManage.aideAccountsFacade.ExcuteByProce("RYAgentDB..P_Acc_AgentRecharge", dictionary);
					if (!message.Success)
					{
						return Json(new
						{
							IsOk = false,
							Msg = message.Content
						});
					}
					return Json(new
					{
						IsOk = true,
						Msg = "充值成功"
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
		public JsonResult ClearAgentPack()
		{
			int formInt = GameRequest.GetFormInt("aid", 0);
			if (formInt > 0)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary["dwAgentID"] = formInt;
				dictionary["strOperator"] = user.Username;
				dictionary["strErr"] = "";
				try
				{
					Message message = FacadeManage.aideAccountsFacade.ExcuteByProce("RYAgentDB.dbo.P_ClearAgentPack", dictionary);
					if (!message.Success)
					{
						return Json(new
						{
							IsOk = false,
							Msg = message.Content
						});
					}
					return Json(new
					{
						IsOk = true,
						Msg = "解绑成功"
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
		public JsonResult AegntList()
		{
			int formInt = GameRequest.GetFormInt("rate", 0);
			string formString = GameRequest.GetFormString("aname");
			string formString2 = GameRequest.GetFormString("qq");
			int @int = GameRequest.GetInt("pageIndex", 1);
			int int2 = GameRequest.GetInt("pageSize", 30);
			double num = Convert.ToDouble(formInt) / 100.0;
			string orderby = "ORDER BY AgentID DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (num > 0.0)
			{
				stringBuilder.AppendFormat(" AND AgentRate={0}", num);
			}
			if (formString != "")
			{
				stringBuilder.AppendFormat(" AND AgentAcc='{0}'", formString);
			}
			if (formString2 != "")
			{
				stringBuilder.AppendFormat(" AND QQ = '{0}'", formString2);
			}
			PagerSet list = FacadeManage.aideAccountsFacade.GetList("RYAgentDB.dbo.T_Acc_Agent", @int, int2, stringBuilder.ToString(), orderby);
			DataTable o = list.PageSet.Tables[0];
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(o)
			});
		}

		[CheckCustomer]
		public JsonResult MyNextAgent()
		{
			int formInt = GameRequest.GetFormInt("aid", 0);
			string formString = GameRequest.GetFormString("aname");
			int @int = GameRequest.GetInt("pageIndex", 1);
			int int2 = GameRequest.GetInt("pageSize", 30);
			string orderby = "ORDER BY AgentID DESC";
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(" WHERE AgentID IN(SELECT NodeID FROM RYAgentDB.dbo.Fn_GetAllNodes(1,{0}) WHERE NodeID<>{0})", formInt);
			if (formString != "")
			{
				stringBuilder.AppendFormat(" AND AgentAcc='{0}'", formString);
			}
			PagerSet list = FacadeManage.aideAccountsFacade.GetList("RYAgentDB..T_Acc_Agent", @int, int2, stringBuilder.ToString(), orderby);
			DataTable o = list.PageSet.Tables[0];
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(o)
			});
		}

		[CheckCustomer]
		public JsonResult MyNextUser()
		{
			int formInt = GameRequest.GetFormInt("aid", 0);
			string formString = GameRequest.GetFormString("aname");
			string formString2 = GameRequest.GetFormString("StartDate");
			string formString3 = GameRequest.GetFormString("EndDate");
			int @int = GameRequest.GetInt("pageIndex", 1);
			int int2 = GameRequest.GetInt("pageSize", 30);
			string orderby = "ORDER BY UserID DESC";
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(" WHERE ParentID IN(SELECT NodeID FROM RYAgentDB.dbo.Fn_GetAllNodes(1,{0}))", formInt);
			if (formString != "")
			{
				stringBuilder.AppendFormat(" AND Accounts='{0}'", formString);
			}
			if (formString2 != "")
			{
				stringBuilder.AppendFormat(" AND RegisterDate>'{0}'", Convert.ToDateTime(formString2));
			}
			if (formString3 != "")
			{
				stringBuilder.AppendFormat(" AND RegisterDate<'{0}'", Convert.ToDateTime(formString3).AddDays(1.0));
			}
			PagerSet list = FacadeManage.aideAccountsFacade.GetList("View_AllNextUser", @int, int2, stringBuilder.ToString(), orderby);
			DataTable o = list.PageSet.Tables[0];
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(o)
			});
		}

		[CheckCustomer]
		public JsonResult GetAgentScoreChange()
		{
			int formInt = GameRequest.GetFormInt("aid", 0);
			string formString = GameRequest.GetFormString("aname");
			string formString2 = GameRequest.GetFormString("StartDate");
			string formString3 = GameRequest.GetFormString("EndDate");
			int formInt2 = GameRequest.GetFormInt("cType", 0);
			int @int = GameRequest.GetInt("pageIndex", 1);
			int int2 = GameRequest.GetInt("pageSize", 30);
			if (formInt == 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数错误"
				});
			}
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (formInt > 0)
			{
				stringBuilder.AppendFormat(" AND AgentID={0}", formInt);
			}
			if (formInt2 > 0)
			{
				stringBuilder.AppendFormat(" AND LogType={0}", formInt2);
			}
			if (formString != "")
			{
				stringBuilder.AppendFormat(" AND SourceAcc='{0}'", formString);
			}
			if (formString2 != "")
			{
				stringBuilder.AppendFormat(" AND SwapDate>='{0}'", Convert.ToDateTime(formString2));
			}
			if (formString3 != "")
			{
				stringBuilder.AppendFormat(" AND SwapDate<'{0}'", Convert.ToDateTime(formString3));
			}
			PagerSet list = FacadeManage.aideAccountsFacade.GetList("RYAgentDB..View_AgentScoreLog", @int, int2, stringBuilder.ToString(), "ORDER BY ID DESC");
			DataTable dataTable = list.PageSet.Tables[0];
			decimal sum = 0m;
			if (dataTable.Rows.Count > 0)
			{
				sum = FacadeManage.aideTreasureFacade.TotalAgentScore(stringBuilder.ToString());
			}
			return Json(new
			{
				IsOk = true,
				Msg = new
				{
					Sum = sum
				},
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(dataTable)
			});
		}

		[CheckCustomer]
		public JsonResult UpdateAgentSet()
		{
			string formString = GameRequest.GetFormString("sData");
			if (!(formString == ""))
			{
				if (user != null)
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
				}
				AgentSetting model = JsonHelper.DeserializeJsonToObject<AgentSetting>(formString);
				try
				{
					FacadeManage.aideAccountsFacade.UpdateAgentSetting(model);
					return Json(new
					{
						IsOk = true,
						Msg = "设置成功"
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
		public JsonResult GetTradeList(int pageIndex, int pageSize, string OrderID, string KeyWord, string StartDate, string EndDate, int Type = 0, int TradeType = -1)
		{
			KeyWord = FiltUtil.GetSafeSQL(KeyWord);
			OrderID = FiltUtil.GetSafeSQL(OrderID);
			string orderby = "ORDER BY ID DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (!string.IsNullOrEmpty(KeyWord))
			{
				stringBuilder.AppendFormat(" AND AgentAcc='{0}'", KeyWord);
			}
			if (!string.IsNullOrEmpty(StartDate))
			{
				stringBuilder.AppendFormat(" AND ApplyTime >= '{0}'", Convert.ToDateTime(StartDate));
			}
			if (!string.IsNullOrEmpty(EndDate))
			{
				stringBuilder.AppendFormat(" AND ApplyTime < '{0}'", Convert.ToDateTime(EndDate));
			}
			if (!string.IsNullOrEmpty(OrderID))
			{
				stringBuilder.AppendFormat(" AND OrderID='{0}'", OrderID);
			}
			if (TradeType >= 0)
			{
				stringBuilder.AppendFormat(" AND DealStatus={0}", TradeType);
			}
			PagerSet list = FacadeManage.aideAccountsFacade.GetList("RYAgentDB..View_AgentDraw", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			DataTable o = list.PageSet.Tables[0];
			double sum = FacadeManage.aideAccountsFacade.SumAgentDraw(stringBuilder.ToString());
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

		[CheckCustomer]
		public JsonResult doTrade(int type, int id, string resaon)
		{
			if (id <= 0)
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
			if (user != null)
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
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["dwID"] = id;
			dictionary["strOrderID"] = "";
			dictionary["dwStatus"] = type;
			dictionary["strReason"] = resaon;
			dictionary["strOperator"] = user.Username;
			dictionary["strClientIP"] = GameRequest.GetUserIP();
			dictionary["strErr"] = "";
			Message message = FacadeManage.aideAccountsFacade.ExcuteByProce("RYAgentDB..P_Acc_AgentDrawRefused", dictionary);
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
		public JsonResult UpdateAgentBaseURL()
		{
			if (user != null)
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
			}
			string safeSQL = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request.Form["URL"]));
			string safeSQL2 = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request.Form["URL2"]));
			string safeSQL3 = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request.Form["URL3"]));
			string safeSQL4 = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request.Form["TGURL"]));
			string safeSQL5 = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request.Form["TGURL2"]));
			string safeSQL6 = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request.Form["TGURL3"]));
			AgentStatusInfo model = new AgentStatusInfo(safeSQL, safeSQL2, safeSQL3, safeSQL4, safeSQL5, safeSQL6);
			try
			{
				FacadeManage.aideAccountsFacade.UpdateAgentBaseURL(model);
				return Json(new
				{
					IsOk = true,
					Msg = "设置成功"
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
		public ActionResult NextPayList()
		{
			if (HttpRuntime.Cache["payrate"] == null)
			{
				SystemStatusInfo systemStatusInfo = FacadeManage.aideAccountsFacade.GetSystemStatusInfo("PayRate");
				if (systemStatusInfo != null)
				{
					CacheHelper.AddCache("payrate", systemStatusInfo.StatusValue);
				}
				else
				{
					CacheHelper.AddCache("payrate", 1);
				}
			}
			base.ViewBag.PayRate = HttpRuntime.Cache["payrate"];
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetNextPayList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["ShareID"]);
			string text = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string text2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			string text3 = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			int num3 = TypeUtil.ObjectToInt(base.Request["SearchType"]);
			int num4 = TypeUtil.ObjectToInt(base.Request["agentId"], 0);
			string orderby = "Order BY ApplyDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (num4 > 0)
			{
				stringBuilder.AppendFormat(" AND ParentID={0} ", num4);
			}
			if (num2 > 0)
			{
				stringBuilder.AppendFormat(" AND ShareID={0} ", num2);
			}
			DateTime now = DateTime.Now;
			switch (num)
			{
			case 1:
				if (text != "")
				{
					stringBuilder.AppendFormat(" AND ApplyDate > '{0}'", Convert.ToDateTime(text));
				}
				if (text2 != "")
				{
					stringBuilder.AppendFormat(" AND ApplyDate <= '{0}'", Convert.ToDateTime(text2));
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
					case 3:
						stringBuilder.AppendFormat(" AND OrderID='{0}' ", text3);
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
				stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", text, text2);
				break;
			case 3:
				text = Fetch.GetYesterdayTime().Split('$')[0].ToString();
				text2 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 4:
				text = Fetch.GetWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetWeekTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 5:
				text = Fetch.GetLastWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
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
			}
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("RYAgentDB.dbo.View_NextPayZj", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			string text4 = "SELECT ISNULL(SUM(PayAmount),0) FROM RYAgentDB.dbo.View_NextPayZj " + stringBuilder.ToString();
			decimal payAmount = Convert.ToDecimal(FacadeManage.aideTreasureFacade.GetScalarBySql(text4));
			decimal payAmount2 = Convert.ToDecimal(FacadeManage.aideTreasureFacade.GetScalarBySql(text4 + " AND AUShow=1"));
			return Json(new
			{
				IsOk = true,
				Msg = new
				{
					PayAmount = payAmount,
					PayAmount1 = payAmount2
				},
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[CheckCustomer]
		public ActionResult NextGameRecordList()
		{
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetNextGameList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["agentId"], 0);
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num2 = TypeUtil.ObjectToInt(base.Request["Type"]);
			int num3 = TypeUtil.ObjectToInt(base.Request["ServerID"]);
			string text = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string text2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			string text3 = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			int num4 = TypeUtil.ObjectToInt(base.Request["SearchType"]);
			string orderby = "Order BY InsertTime DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (num > 0)
			{
				stringBuilder.AppendFormat(" AND ParentID={0} ", num);
			}
			if (num3 > 0)
			{
				stringBuilder.AppendFormat(" AND ServerID={0} ", num3);
			}
			DateTime now = DateTime.Now;
			switch (num2)
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
					switch (num4)
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
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("RYAgentDB.dbo.View_PlayGameInfoZj", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			string sql = "Select Sum(Score) AS SumWinScore, Sum(Revenue) AS SumRevenue From RYAgentDB.dbo.View_PlayGameInfoZj" + stringBuilder.ToString();
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
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[CheckCustomer]
		public ActionResult NextDrawList()
		{
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetNextDrawList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["agentId"], 0);
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num2 = TypeUtil.ObjectToInt(base.Request["Type"]);
			string text = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string text2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			string text3 = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			int num3 = TypeUtil.ObjectToInt(base.Request["SearchType"]);
			string orderby = "Order BY ApplyDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (num > 0)
			{
				stringBuilder.AppendFormat(" AND ParentID={0} ", num);
			}
			DateTime now = DateTime.Now;
			switch (num2)
			{
			case 1:
				if (text != "")
				{
					stringBuilder.AppendFormat(" AND ApplyDate > '{0}'", Convert.ToDateTime(text));
				}
				if (text2 != "")
				{
					stringBuilder.AppendFormat(" AND ApplyDate <= '{0}'", Convert.ToDateTime(text2));
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
					}
				}
				break;
			case 2:
				text = Fetch.GetTodayTime().Split('$')[0].ToString();
				text2 = Fetch.GetTodayTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 3:
				text = Fetch.GetYesterdayTime().Split('$')[0].ToString();
				text2 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 4:
				text = Fetch.GetWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetWeekTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 5:
				text = Fetch.GetLastWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
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
			}
			PagerSet list = FacadeManage.aideAccountsFacade.GetList("RYAgentDB.dbo.View_ApplyOrderZj", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			string sql = "Select Sum(SellScore) AS SumSellScore From RYAgentDB.dbo.View_ApplyOrderZj" + stringBuilder.ToString();
			DataSet dataSetBySql = FacadeManage.aideAccountsFacade.GetDataSetBySql(sql);
			string msg = "";
			if (dataSetBySql.Tables[0].Rows.Count > 0)
			{
				DataRow dataRow = dataSetBySql.Tables[0].Rows[0];
				msg = JsonConvert.SerializeObject(new
				{
					SumSellScore = dataRow["SumSellScore"]
				});
			}
			return Json(new
			{
				IsOk = true,
				Msg = msg,
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[CheckCustomer]
		public ActionResult RechargeList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["agentId"]);
			base.ViewBag.AgentID = num;
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetRechargeList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string text = TypeUtil.ObjectToString(base.Request["StarDate"]);
			string text2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			int num = TypeUtil.ObjectToInt(base.Request["AgentID"]);
			string text3 = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["SearchType"]);
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			DateTime.TryParse(text, out result);
			DateTime.TryParse(text2, out result2);
			StringBuilder stringBuilder = new StringBuilder("WHERE AgentID=" + num);
			if (text != "")
			{
				stringBuilder.AppendFormat(" AND CollectDate >= '{0}'", result);
			}
			if (text2 != "")
			{
				stringBuilder.AppendFormat(" AND CollectDate < '{0}'", result2.AddDays(1.0));
			}
			if (text3 != "")
			{
				switch (num2)
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
				}
			}
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("RYAgentDB.dbo.View_OffLineOrderZj", pageIndex, pageSize, stringBuilder.ToString(), "ORDER BY ID DESC");
			string text4 = "SELECT SUM(PayAmount) FROM RYAgentDB.dbo.View_OffLineOrderZj " + stringBuilder.ToString();
			decimal sum = Convert.ToDecimal(FacadeManage.aideTreasureFacade.GetScalarBySql(text4));
			decimal sum2 = Convert.ToDecimal(FacadeManage.aideTreasureFacade.GetScalarBySql(text4 + " AND AUShow=1"));
			return Json(new
			{
				IsOk = true,
				Msg = new
				{
					Sum = sum,
					Sum1 = sum2
				},
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[CheckCustomer]
		public ActionResult ReportList()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetReportList()
		{
			int @int = GameRequest.GetInt("pageIndex", 1);
			int int2 = GameRequest.GetInt("pageSize", 10);
			int num = TypeUtil.ObjectToInt(base.Request["agentId"], 0);
			string orderby = "Order BY LastCountDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (num > 0)
			{
				stringBuilder.AppendFormat(" AND AgentID={0} ", num);
			}
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("RYAgentDB.dbo.View_FillAgentRpt", @int, int2, stringBuilder.ToString(), orderby);
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		public JsonResult SetRate()
		{
			int formInt = GameRequest.GetFormInt("ID", 0);
			int formInt2 = GameRequest.GetFormInt("FillRevRate", 0);
			int formInt3 = GameRequest.GetFormInt("FillRate", 0);
			int formInt4 = GameRequest.GetFormInt("DrawFee", 0);
			if (formInt <= 0 || formInt2 <= 0 || formInt3 <= 0 || formInt4 <= 0 || formInt2 > 100 || formInt3 > 100)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "验证失败，输入错误"
				});
			}
			string msg = "";
			if (FacadeManage.aideAccountsFacade.SetRate(formInt, formInt3, formInt4, formInt2, out msg))
			{
				return Json(new
				{
					IsOk = true,
					Msg = msg
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = msg
			});
		}

		public JsonResult UpdateState()
		{
			int formInt = GameRequest.GetFormInt("ID", 0);
			if (formInt <= 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "验证失败，输入错误"
				});
			}
			string msg = "";
			if (FacadeManage.aideAccountsFacade.UpdateState(formInt, 2, out msg))
			{
				return Json(new
				{
					IsOk = true,
					Msg = msg
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = msg
			});
		}

		[CheckCustomer]
		public ActionResult PayReportList()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetPayReportList()
		{
			int @int = GameRequest.GetInt("pageIndex", 1);
			int int2 = GameRequest.GetInt("pageSize", 10);
			string safeSQL = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["AgentAcc"]));
			string safeSQL2 = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["StartDate"]));
			string orderby = "Order BY LastCountDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (safeSQL != "")
			{
				stringBuilder.AppendFormat(" AND AgentAcc='{0}' ", safeSQL);
			}
			if (safeSQL2 != "")
			{
				stringBuilder.AppendFormat(" AND YearMonth='{0}' ", safeSQL2);
			}
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("RYAgentDB.dbo.View_FillAgentRpt", @int, int2, stringBuilder.ToString(), orderby);
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder2.Append("SELECT SUM(FillAmount) AS sumFillAmount,SUM(sFillAmount) AS sumsFillAmount,SUM(DrawAmount) AS sumDrawAmount");
			stringBuilder2.Append(",SUM(DrawTimes) AS sumDrawTimes,SUM(OffLineFillAmount) AS sumOffLineFillAmount,SUM(sOffLineFillAmount) AS sumsOffLineFillAmount");
			stringBuilder2.Append(",SUM(sFillAmount*FillRate/100) AS sumPayFee,SUM(DrawFee*DrawTimes) AS sumDrawFee");
			stringBuilder2.Append(" FROM RYAgentDB.dbo.View_FillAgentRpt " + stringBuilder.ToString());
			DataTable o = FacadeManage.aideTreasureFacade.GetDataSetBySql(stringBuilder2.ToString()).Tables[0];
			return Json(new
			{
				IsOk = true,
				Msg = JsonHelper.SerializeObject(o),
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[CheckCustomer]
		public ActionResult PlayerScoreList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["agentId"]);
			base.ViewBag.AgentID = num;
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetPlayerScoreList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int num2 = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num3 = TypeUtil.ObjectToInt(base.Request["agentID"]);
			string s = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string s2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			DateTime now = DateTime.Now;
			DateTime now2 = DateTime.Now;
			string value = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			int num4 = TypeUtil.ObjectToInt(base.Request["SearchType"]);
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			if (!DateTime.TryParse(s, out result))
			{
				result = DateTime.Now;
			}
			if (!DateTime.TryParse(s2, out result2))
			{
				result2 = DateTime.Now;
			}
			dictionary.Add("LogonAgentID ", num3);
			dictionary.Add("BeginTime", result.ToString("yyyy-MM-dd 00:00:00"));
			dictionary.Add("EndTime", result2.ToString("yyyy-MM-dd 23:59:59"));
			dictionary.Add("QueryType", num4);
			dictionary.Add("QueryWhere", value);
			dictionary.Add("DescOrAsc", 0);
			dictionary.Add("PageSize", num2);
			dictionary.Add("PageIndex", num);
			PagerSet pagerSet = FacadeManage.aideRecordFacade.QueryPlayerWinLost(dictionary);
			List<object> list = new List<object>();
			if (pagerSet != null && pagerSet.PageSet != null && pagerSet.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in pagerSet.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						UserID = TypeUtil.ObjectToInt(row["UserID"]),
						Accounts = TypeUtil.ObjectToString(row["Accounts"]),
						NickName = TypeUtil.ObjectToString(row["NickName"]),
						AgentAcc = TypeUtil.ObjectToString(row["AgentAcc"]),
						Score = TypeUtil.ObjectToDecimal(row["Score"]).ToString("0.00"),
						Time = result.ToString("yyyy-MM-dd") + "至" + result2.ToString("yyyy-MM-dd")
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = new
				{
					SumWinlost = TypeUtil.ObjectToDecimal(pagerSet.PageSet.Tables[1].Rows[0]["SumWinlost"])
				},
				Total = pagerSet.RecordCount,
				Data = list
			});
		}

		[CheckCustomer]
		public ActionResult PlayerScoreInfo()
		{
			return View();
		}

		public JsonResult GetPlayerScoreInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["agentID"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["userID"]);
			string s = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string s2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			if (!DateTime.TryParse(s, out result))
			{
				result = DateTime.Now;
			}
			if (!DateTime.TryParse(s2, out result2))
			{
				result2 = DateTime.Now;
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("LogonAgentID", num);
			dictionary.Add("UserID", num2);
			dictionary.Add("BTime", result.ToString("yyyy-MM-dd 00:00:00"));
			dictionary.Add("ETime", result2.ToString("yyyy-MM-dd 23:59:59"));
			DataTable dataTable = FacadeManage.aideRecordFacade.QueryUserGameWinLost(dictionary);
			List<object> list = new List<object>();
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				foreach (DataRow row in dataTable.Rows)
				{
					list.Add(new
					{
						KindName = TypeUtil.ObjectToString(row["KindName"]),
						Score = TypeUtil.ObjectToDecimal(row["Score"]).ToString("0.00"),
						Time = result.ToString("yyyy-MM-dd") + "至" + result2.ToString("yyyy-MM-dd")
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = 0,
				Data = list
			});
		}

		[CheckCustomer]
		public ActionResult AgentNewPlayerList()
		{
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetAgentNewPlayerList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string text = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string text2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			string text3 = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			new Dictionary<string, object>();
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			if (!DateTime.TryParse(text, out result))
			{
				result = DateTime.Now;
			}
			if (!DateTime.TryParse(text2, out result2))
			{
				result2 = DateTime.Now;
			}
			StringBuilder stringBuilder = new StringBuilder("where 1=1 ");
			if (text != "")
			{
				stringBuilder.AppendFormat(" and cDate>='{0}'", result);
			}
			if (text2 != "")
			{
				stringBuilder.AppendFormat(" and cDate<='{0}'", result2.AddDays(1.0));
			}
			if (!string.IsNullOrEmpty(text3))
			{
				stringBuilder.AppendFormat(" and AgentAcc ='{0}'", text3);
			}
			PagerSet list = FacadeManage.aideGameMatchFacade.GetList("RYAgentDB.dbo.Fn_GetAgentMsg(0)", pageIndex, pageSize, stringBuilder.ToString(), "order by cDate desc");
			List<object> list2 = new List<object>();
			if (list != null && list.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in list.PageSet.Tables[0].Rows)
				{
					list2.Add(new
					{
						AgentAcc = TypeUtil.ObjectToString(row["AgentAcc"]),
						AgentID = TypeUtil.ObjectToInt(row["AgentID"]),
						cDate = TypeUtil.ObjectToDateTime(row["cDate"]).ToString("yyyy-MM-dd HH:mm:ss"),
						NewNum = TypeUtil.ObjectToInt(row["NewNum"]),
						FirstFillUsers = TypeUtil.ObjectToInt(row["FirstFillUsers"]),
						FirstFillAmt = TypeUtil.ObjectToDecimal(row["FirstFillAmt"]),
						AllFillTimes = TypeUtil.ObjectToInt(row["AllFillTimes"]),
						AllFillAmt = TypeUtil.ObjectToDecimal(row["AllFillAmt"]),
						AllDrawAmt = TypeUtil.ObjectToDecimal(row["AllDrawAmt"]),
						OffFillAmt = TypeUtil.ObjectToDecimal(row["OffFillAmt"])
					});
				}
			}
			string sql = "SELECT SUM(NewNum),SUM(FirstFillUsers),SUM(FirstFillAmt),SUM(AllFillTimes),SUM(AllFillAmt),SUM(AllDrawAmt),SUM(OffFillAmt) FROM RYAgentDB.dbo.Fn_GetAgentMsg(0) " + stringBuilder.ToString();
			DataTable dataTable = FacadeManage.aideAccountsFacade.GetDataSetBySql(sql).Tables[0];
			object msg = "";
			if (dataTable.Rows.Count > 0)
			{
				msg = new
				{
					SumNewNum = dataTable.Rows[0][0],
					SumFirstFillUsers = dataTable.Rows[0][1],
					SumFirstFillAmt = dataTable.Rows[0][2],
					SumAllFillTimes = dataTable.Rows[0][3],
					SumAllFillAmt = dataTable.Rows[0][4],
					SumAllDrawAmt = dataTable.Rows[0][5],
					SumOffFillAmt = dataTable.Rows[0][6]
				};
			}
			return Json(new
			{
				IsOk = true,
				Msg = msg,
				Total = list.RecordCount,
				Data = list2
			});
		}

		[CheckCustomer]
		public ActionResult PaySetting()
		{
			int num = TypeUtil.ObjectToInt(base.Request["agentId"]);
			if (num <= 0)
			{
				return Content("参数错误");
			}
			base.ViewBag.AgentID = num;
			AUFillConfig fillConfig = FacadeManage.aideAccountsFacade.GetFillConfig(num);
			return View(fillConfig);
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult UpdatePaySet(AUFillConfig model)
		{
			if (model.AgentID <= 0 || model.Amount <= 0m || model.Times <= 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数错误"
				});
			}
			if (model.EndTime <= model.BeginTime)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "时间范围错误"
				});
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["AgentID"] = model.AgentID;
			dictionary["UserAllAmt"] = model.UserAllAmt;
			dictionary["BeginTime"] = model.BeginTime;
			dictionary["EndTime"] = model.EndTime;
			dictionary["Amount"] = model.Amount;
			dictionary["Times"] = model.Times;
			dictionary["Operator"] = user.Username;
			dictionary["ClientIP"] = GameRequest.GetUserIP();
			dictionary["strErr"] = "";
			Message message = FacadeManage.aideAccountsFacade.ExcuteByProce("RYAgentDB.dbo.P_AUFillConfig", dictionary);
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

		[CheckCustomer]
		public ActionResult TousuList()
		{
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetTousuList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string text = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string text2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			string text3 = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			int num = TypeUtil.ObjectToInt(base.Request["Status"], -1);
			string orderby = "Order BY Status ASC,CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (text != "")
			{
				stringBuilder.AppendFormat(" AND CollectDate > '{0}'", Convert.ToDateTime(text));
			}
			if (text2 != "")
			{
				stringBuilder.AppendFormat(" AND CollectDate <= '{0}'", Convert.ToDateTime(text2));
			}
			if (num > -1)
			{
				stringBuilder.AppendFormat(" AND Status={0}", num);
			}
			if (text3 != "")
			{
				switch (TypeUtil.ObjectToInt(base.Request["SearchType"]))
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
				case 3:
					stringBuilder.AppendFormat(" AND AgentAcc='{0}' ", text3);
					break;
				case 4:
					stringBuilder.AppendFormat(" AND WeChat='{0}' ", text3);
					break;
				case 5:
					stringBuilder.AppendFormat(" AND UserWx='{0}' ", text3);
					break;
				}
			}
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("RYAgentDB.dbo.View_UserAccuseYS", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			return Json(new
			{
				IsOk = true,
				Msg = new
				{

				},
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[CheckCustomer]
		public JsonResult UpdateCheckStatus()
		{
			int @int = GameRequest.GetInt("ID", 0);
			int int2 = GameRequest.GetInt("Status", 0);
			if (@int <= 0 || int2 <= 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数错误"
				});
			}
			string msg = "";
			if (FacadeManage.aideAccountsFacade.UpdateCheckState(@int, int2, user.Username, out msg))
			{
				return Json(new
				{
					IsOk = true,
					Msg = msg
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = msg
			});
		}

		public ActionResult CheckImg()
		{
			string str = ApplicationSettings.Get("webUrl");
			string @string = GameRequest.GetString("img");
			base.ViewBag.Img = str + "upload/tousu/" + @string + ".png";
			return View();
		}

		[CheckCustomer]
		public ActionResult DailiCheckList()
		{
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetDailiCheckList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string text = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string text2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			string text3 = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			int num = TypeUtil.ObjectToInt(base.Request["Status"], -1);
			string orderby = "Order BY YsStatus ASC,ApplyDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (text != "")
			{
				stringBuilder.AppendFormat(" AND ApplyDate > '{0}'", Convert.ToDateTime(text));
			}
			if (text2 != "")
			{
				stringBuilder.AppendFormat(" AND ApplyDate <= '{0}'", Convert.ToDateTime(text2));
			}
			if (num > -1)
			{
				stringBuilder.AppendFormat(" AND YsStatus={0}", num);
			}
			if (text3 != "")
			{
				switch (TypeUtil.ObjectToInt(base.Request["SearchType"]))
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
				case 3:
					stringBuilder.AppendFormat(" AND AgentAcc='{0}' ", text3);
					break;
				case 4:
					stringBuilder.AppendFormat(" AND ShowName='{0}' ", text3);
					break;
				case 5:
					stringBuilder.AppendFormat(" AND WeChat='{0}' ", text3);
					break;
				}
			}
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("RYAgentDB.dbo.VIEw_UserApplyYs", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			return Json(new
			{
				IsOk = true,
				Msg = new
				{

				},
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[CheckCustomer]
		public JsonResult UpdateDailiCheckStatus()
		{
			int @int = GameRequest.GetInt("ID", 0);
			int int2 = GameRequest.GetInt("Status", 0);
			if (@int <= 0 || int2 <= 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数错误"
				});
			}
			string msg = "";
			if (FacadeManage.aideAccountsFacade.UpdateDailiCheckState(@int, int2, user.Username, out msg))
			{
				return Json(new
				{
					IsOk = true,
					Msg = msg
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = msg
			});
		}
	}
}
