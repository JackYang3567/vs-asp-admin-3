using Admin.Filters;
using Game.Entity.Accounts;
using Game.Entity.NativeWeb;
using Game.Facade;
using Game.Facade.Tools;
using Game.Kernel;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Admin.Controllers
{
	public class TradeController : BaseController
	{
		[CheckCustomer]
		public ActionResult Index()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			string text = TypeUtil.ObjectToString(base.Request["Account"]);
			base.ViewBag.Account = text;
			base.ViewBag.UserID = num;
			return View();
		}

		[CheckCustomer]
		public ActionResult RevenueList()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult GameRoomRevenue()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult GameRoomRevenueByUser()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult TradeSet()
		{
			CashSetting model = FacadeManage.aideAccountsFacade.PlayerCashInfo(1);
			return View(model);
		}

		public JsonResult UpdateTradeSet()
		{
			string formString = GameRequest.GetFormString("sData");
			if (!(formString == ""))
			{
				CashSetting model = JsonHelper.DeserializeJsonToObject<CashSetting>(formString);
				try
				{
					FacadeManage.aideAccountsFacade.UpdatePlayerSetting(model);
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
		public JsonResult GetRevenueList(int pageIndex, int pageSize, string KeyWord, string StartDate = "", string EndDate = "", int kindID = 0)
		{
			KeyWord = FiltUtil.GetSafeSQL(KeyWord);
			string orderby = "ORDER BY InsertTime DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE Revenue>0");
			if (!string.IsNullOrEmpty(KeyWord))
			{
				stringBuilder.AppendFormat(" AND Accounts='{0}'", KeyWord);
			}
			if (!string.IsNullOrEmpty(StartDate))
			{
				stringBuilder.AppendFormat(" AND InsertTime >= '{0}'", Convert.ToDateTime(StartDate));
			}
			if (!string.IsNullOrEmpty(EndDate))
			{
				stringBuilder.AppendFormat(" AND InsertTime < '{0}'", Convert.ToDateTime(EndDate));
			}
			if (kindID > 0)
			{
				stringBuilder.AppendFormat(" AND KindID={0}", kindID);
			}
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("View_RecordDrawScore", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			DataTable o = list.PageSet.Tables[0];
			double sum = FacadeManage.aideTreasureFacade.SumRevenue(stringBuilder.ToString());
			DataSet dataSet = new DataSet();
			if (HttpRuntime.Cache.Get("SumRevenueAll") == null)
			{
				dataSet = FacadeManage.aideTreasureFacade.SumRevenueAll();
				HttpRuntime.Cache.Insert("SumRevenueAll", dataSet, null, DateTime.Now.AddMinutes(1.0), TimeSpan.Zero);
			}
			else
			{
				dataSet = (HttpRuntime.Cache.Get("SumRevenueAll") as DataSet);
			}
			double sumAll = 0.0;
			double sumAgent = 0.0;
			if (dataSet != null)
			{
				sumAll = ((dataSet.Tables[0].Rows[0][0] is DBNull) ? 0.0 : Convert.ToDouble(dataSet.Tables[0].Rows[0][0]));
				sumAgent = ((dataSet.Tables[1].Rows[0][0] is DBNull) ? 0.0 : Convert.ToDouble(dataSet.Tables[1].Rows[0][0]));
			}
			return Json(new
			{
				IsOk = true,
				Msg = new
				{
					Sum = sum,
					SumAll = sumAll,
					SumAgent = sumAgent
				},
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(o)
			});
		}

		[CheckCustomer]
		public JsonResult GetGameRevenue(string StartDate, string EndDate)
		{
			DataSet gameRevenue = FacadeManage.aideTreasureFacade.GetGameRevenue(StartDate, EndDate);
			DataTable dataTable = gameRevenue.Tables[0];
			double sum = Convert.ToDouble(gameRevenue.Tables[1].Rows[0][0]);
			return Json(new
			{
				IsOk = true,
				Msg = new
				{
					Sum = sum
				},
				Total = dataTable.Rows.Count,
				Data = JsonHelper.SerializeObject(dataTable)
			});
		}

		[CheckCustomer]
		public JsonResult RevenueDetailList(int pageIndex, int pageSize, string sTime, string eTime, int kid, int sid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(" WHERE ServerID = {0}", sid);
			if (!string.IsNullOrEmpty(sTime))
			{
				stringBuilder.AppendFormat(" AND InsertTime >= '{0}'", Convert.ToDateTime(sTime));
			}
			if (!string.IsNullOrEmpty(eTime))
			{
				stringBuilder.AppendFormat(" AND InsertTime <= '{0}'", Convert.ToDateTime(eTime));
			}
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("View_RecordDrawScore", pageIndex, pageSize, stringBuilder.ToString(), "ORDER BY InsertTime DESC");
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[CheckCustomer]
		public JsonResult GetTradeList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["SearchType"]);
			int num3 = TypeUtil.ObjectToInt(base.Request["TradeType"]);
			string safeSQL = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["KeyWord"]));
			string orderby = "ORDER BY ID DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (!string.IsNullOrEmpty(safeSQL))
			{
				switch (num2)
				{
				case 1:
					stringBuilder.AppendFormat(" AND Accounts='{0}'", safeSQL);
					break;
				case 2:
					stringBuilder.AppendFormat(" AND RealName='{0}'", safeSQL);
					break;
				case 3:
					stringBuilder.AppendFormat(" AND OrderID='{0}'", safeSQL);
					break;
				case 4:
					stringBuilder.AppendFormat(" AND SellScore={0}", Convert.ToInt32(safeSQL));
					break;
				}
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
			if (num3 > 0)
			{
				stringBuilder.AppendFormat(" AND Status={0}", num3);
			}
			PagerSet list = FacadeManage.aideAccountsFacade.GetList("View_ApplyOrder", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			DataTable o = list.PageSet.Tables[0];
			decimal sum = FacadeManage.aideAccountsFacade.SumPlayerDraw(stringBuilder.ToString());
			int successCount = FacadeManage.aideAccountsFacade.SuccessCount(stringBuilder.ToString());
			return Json(new
			{
				IsOk = true,
				Msg = new
				{
					Sum = sum,
					SuccessCount = successCount
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
			if (type == 3 && string.IsNullOrEmpty(resaon))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请输入拒绝理由！"
				});
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["id"] = 0;
			dictionary["orderid"] = orderid;
			dictionary["type"] = type;
			dictionary["msg"] = resaon;
			dictionary["strOperator"] = user.Username;
			dictionary["strClientIP"] = GameRequest.GetUserIP();
			dictionary["strErr"] = "";
			Message message = FacadeManage.aideAccountsFacade.ExcuteByProce("P_Acc_PlayerDrawRefused", dictionary);
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
		public JsonResult Buy(int type, int id)
		{
			if (id <= 0 || type <= 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数错误"
				});
			}
			int num = FacadeManage.aideAccountsFacade.TradeOper(type, id);
			if (num > 0)
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
				Msg = "操作失败"
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
				dictionary["id"] = 0;
				dictionary["orderid"] = orderId;
				dictionary["type"] = b;
				dictionary["msg"] = text4;
				dictionary["strOperator"] = user.Username;
				dictionary["strClientIP"] = GameRequest.GetUserIP();
				dictionary["strErr"] = "";
				Message message = FacadeManage.aideAccountsFacade.ExcuteByProce("P_Acc_PlayerDrawRefused", dictionary);
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
		public JsonResult DaiFuFailed(string orderId, string refreso)
		{
			if (string.IsNullOrEmpty(orderId))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数错误！"
				});
			}
			string a = DaiFu.Query_youmifu(orderId);
			if (a == "SUCCESS")
			{
				return Json(new
				{
					IsOk = false,
					Msg = "该订单交易成功或者已受理，无法拒绝"
				});
			}
			byte b = 3;
			string value = "";
			if (b == 3)
			{
				if (refreso == "")
				{
					return Json(new
					{
						IsOk = false,
						Msg = "请输入代付失败理由！"
					});
				}
				value = refreso;
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["id"] = 0;
			dictionary["orderid"] = orderId;
			dictionary["type"] = b;
			dictionary["msg"] = value;
			dictionary["strOperator"] = user.Username;
			dictionary["strClientIP"] = GameRequest.GetUserIP();
			dictionary["strErr"] = "";
			Message message = FacadeManage.aideAccountsFacade.ExcuteByProce("P_Acc_PlayerDrawRefused", dictionary);
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
		public ActionResult Tixian()
		{
			return View();
		}
        [CheckCustomer]
        public ActionResult LotteryBetDraw()
        {
            return View();
        }
		[CheckCustomer]
		public JsonResult SendCode()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(base.Server.MapPath("/App_Data/WebSite.xml"));
			string innerText = xmlDocument.SelectSingleNode("/root/TixianPhone").InnerText;
			string content = ApplicationSettings.Get("phoneContent");
			string text = TextUtility.CreateAuthStr(6, true);
			string text2 = CodeHelper.SendCode(innerText, text, content);
			if (text2 == "发送成功")
			{
				base.Session["txcode"] = text;
				return Json(new
				{
					IsOk = true,
					Msg = "发送成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = text2
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DaifuTixian(PlatformDraw model)
		{
			int @int = GameRequest.GetInt("platId", 0);
			if (@int <= 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请选择代付平台"
				});
			}
			if (model.RealName == "" || model.BankCode == "" || model.BankNo == "" || model.DrawAmt <= 0m || model.BankAddr == "" || model.Code == "")
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请输入完整信息"
				});
			}
			string @string = GameRequest.GetString("Province");
			string string2 = GameRequest.GetString("City");
			string flowid = "";
			string text = "";
			switch (@int)
			{
			case 1:
				model.OrderID = PayHelper.GetOrderIDByPrefix("txf");
				text = DaiFu.GateWayPement(model.OrderID, model.DrawAmt, model.RealName, model.BankNo, model.BankCode, model.BankAddr, out flowid);
				break;
			case 2:
				if (@string == "" || string2 == "")
				{
					return Json(new
					{
						IsOk = false,
						Msg = "请输入银行所在省市"
					});
				}
				model.OrderID = PayHelper.GetOrderIDByPrefix("45");
				text = DaiFu.Daifu_45(model.OrderID, model.DrawAmt, model.RealName, model.BankNo, model.BankName, model.BankAddr, string2, out flowid);
				break;
			case 3:
				if (@string == "" || string2 == "")
				{
					return Json(new
					{
						IsOk = false,
						Msg = "请输入银行所在省市"
					});
				}
				model.OrderID = PayHelper.GetOrderIDByPrefix("ymf");
				text = DaiFu.Daifu_youmifu(model.OrderID, model.DrawAmt, model.RealName, model.BankNo, model.BankCode, model.BankAddr, @string, string2, base.Request.Url.Host, out flowid);
				break;
			case 4:
				if (@string == "" || string2 == "")
				{
					return Json(new
					{
						IsOk = false,
						Msg = "请输入银行所在省市"
					});
				}
				model.OrderID = PayHelper.GetOrderIDByPrefix("ry");
				text = DaiFu.Daifu_ruyi(model.OrderID, model.DrawAmt, model.RealName, model.BankNo, model.BankCode, model.BankAddr, @string, string2, base.Request.Url.Host, out flowid);
				break;
			}
			string text2 = "";
			if (text.ToUpper() == "SUCCESS")
			{
				base.Session["code"] = null;
				base.Session["error"] = null;
				text2 = "提现请求提交成功，等待处理";
				model.OperateIP = GameRequest.GetUserIP();
				model.Operator = user.Username;
				model.FlowID = flowid;
				int num = FacadeManage.aideNativeWebFacade.AddPlatformDraw(model);
				if (num > 0)
				{
					return Json(new
					{
						IsOk = true,
						Msg = text2
					});
				}
				return Json(new
				{
					IsOk = false,
					Msg = text2 + "—记录数据错误"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = text
			});
		}

		[CheckCustomer]
		public JsonResult GetTixianList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string safeSQL = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["OrderID"]));
			string orderby = "ORDER BY ID DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			string value = base.Request["StartDate"];
			string value2 = base.Request["EndDate"];
			if (!string.IsNullOrEmpty(value))
			{
				stringBuilder.AppendFormat(" AND CollectDate >= '{0}'", Convert.ToDateTime(value));
			}
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.AppendFormat(" AND CollectDate < '{0}'", Convert.ToDateTime(value2));
			}
			if (!string.IsNullOrEmpty(safeSQL))
			{
				stringBuilder.AppendFormat(" AND OrderID='{0}'", safeSQL);
			}
			PagerSet list = FacadeManage.aideNativeWebFacade.GetList("T_PlatformDraw", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			DataTable o = list.PageSet.Tables[0];
			decimal sum = Convert.ToDecimal(FacadeManage.aideNativeWebFacade.SumPlayerDraw(stringBuilder.ToString()));
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
        public JsonResult GetLotteryBetDrawList()
        {
            int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
            int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
            string safeSQL = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["GameID"]));
            string orderby = "ORDER BY ID DESC";
            StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
            string value = base.Request["StartDate"];
            string value2 = base.Request["EndDate"];
            string iscomplete = base.Request["iscomplete"];
            
            if (!string.IsNullOrEmpty(value))
            {
                stringBuilder.AppendFormat(" AND inserttime >= '{0}'", Convert.ToDateTime(value));
            }
            if (!string.IsNullOrEmpty(value2))
            {
                stringBuilder.AppendFormat(" AND inserttime < '{0}'", Convert.ToDateTime(value2));
            }
            if (!string.IsNullOrEmpty(safeSQL))
            {
                stringBuilder.AppendFormat(" AND userid in (select userid from RYAccountsDB.dbo.accountsinfo where gameid like '%{0}%')", safeSQL);
            }
            iscomplete = iscomplete ?? "-1";
            stringBuilder.AppendFormat(" AND (iscomplete ={0} or -1={0})", iscomplete);
            PagerSet list = FacadeManage.aideTreasureFacade.GetList(@"(select a.*,b.gameid,isnull(bet.betscore,0) betscore1  from LotteryBetDraw a left join RYAccountsDB.dbo.accountsinfo b on a.userid = b.userid left join  (select sum(b.betscore) betscore,b.userid from (select userid,max(ApplyDate) ApplyDate from RYTreasureDB.dbo.OnLineOrder where OrderStatus=2 group by userid) a inner join 
(select  userid,betscore,inserttime from RYTreasureDB.dbo.LotteryBetDraw) b on a.userid = b.userid and b.inserttime >=a.ApplyDate
group by b.userid) bet on a.userid = bet.userid )  as tab ", pageIndex, pageSize, stringBuilder.ToString(), orderby);
            DataTable o = list.PageSet.Tables[0];
            decimal SumWinScore = Convert.ToDecimal(FacadeManage.aideTreasureFacade.SumWinScore(stringBuilder.ToString()));
            decimal Sumbetscore = Convert.ToDecimal(FacadeManage.aideTreasureFacade.Sumbetscore(stringBuilder.ToString()));
            return Json(new
            {
                IsOk = true,
                Msg = new
                {
                    SumWinScore = SumWinScore,
                    Sumbetscore = Sumbetscore
                },
                Total = list.RecordCount,
                Data = JsonHelper.SerializeObject(o)
            });
        }
		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult EditBankCard(BankCard entity)
		{
			if (entity == null)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有提交任何数据"
				});
			}
			if (entity.BankCity == null)
			{
				entity.BankCity = "";
			}
			if (entity.Province == null)
			{
				entity.Province = "";
			}
			if (FacadeManage.aideAccountsFacade.EditBankCard(entity) > 0)
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
		public JsonResult DelBankCard(int ID)
		{
			if (ID <= 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请选择银行卡号"
				});
			}
			if (FacadeManage.aideAccountsFacade.DelBankCard(ID) > 0)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "删除成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "删除失败"
			});
		}
	}
}
