using Admin.Filters;
using Game.Entity.Accounts;
using Game.Entity.FrontEntity;
using Game.Entity.NativeWeb;
using Game.Entity.PlatformManager;
using Game.Entity.Record;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Facade.Aide;
using Game.Facade.Mail;
using Game.Facade.Tools;
using Game.Kernel;
using Game.Utils;
using Game.Utils.Cache;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Xml;

namespace Admin.Controllers
{
	public class AccountController : BaseController
	{
		[CheckCustomer]
		public ActionResult Index()
		{
			base.ViewBag.ModuleID = user.MoudleID;
			base.ViewBag.UserID = user.RoleID;
			return View();
		}

		[CheckCustomer]
		public ActionResult OnlineList()
		{
			base.ViewBag.UserID = user.RoleID;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (HttpRuntime.Cache["website"] == null)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(base.Server.MapPath("/App_Data/WebSite.xml"));
				dictionary["TotalPay"] = xmlDocument.SelectSingleNode("/root/TotalPay").InnerText;
				dictionary["TotalWin"] = xmlDocument.SelectSingleNode("/root/TotalWin").InnerText;
				dictionary["TotalWinMax"] = xmlDocument.SelectSingleNode("/root/TotalWinMax").InnerText;
				CacheHelper.AddCache("website", dictionary);
			}
			else
			{
				dictionary = (HttpRuntime.Cache["website"] as Dictionary<string, string>);
			}
			base.ViewBag.TotalPay = dictionary["TotalPay"];
			base.ViewBag.TotalWin = dictionary["TotalWin"];
			base.ViewBag.TotalWinMax = dictionary["TotalWinMax"];
			return View();
		}

		[CheckCustomer]
		public ActionResult Online()
		{
			base.ViewBag.UserID = user.RoleID;
			return View();
		}

		[CheckCustomer]
		public ActionResult AddAccount()
		{
			base.ViewBag.ModuleID = user.MoudleID;
			base.ViewBag.UserID = user.RoleID;
			return View();
		}

		[CheckCustomer]
		public ActionResult UserFaceList()
		{
			base.ViewBag.Avatar = "";
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < 200; i++)
			{
				stringBuilder.Append("<a id=\"lnkFaceID" + i + "\" href=\"javascript:void(0);\" onclick=\"javascript:PI(" + i + ",'../Content/images/gamepic/Avatar" + i + ".png',1);\" hidefocus=\"true\"><img src=\"../Content/images/gamepic/Avatar" + i + ".png\" alt='' /></a>");
			}
			base.ViewBag.Avatar = stringBuilder.ToString();
			return View();
		}

		[CheckCustomer]
		public ActionResult IpAddress()
		{
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			base.ViewBag.Paras = text;
			return View();
		}

		[CheckCustomer]
		public ActionResult GrantTreasure()
		{
			base.ViewBag.ModuleID = user.MoudleID;
			base.ViewBag.RoleID = user.RoleID;
			base.ViewBag.Paras = "";
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			base.ViewBag.Paras = text;
			int result;
			if (int.TryParse(text, out result))
			{
				DataTable money = FacadeManage.aideTreasureFacade.GetMoney(result);
				if (money.Rows.Count > 0)
				{
					base.ViewBag.Score = Convert.ToInt64(money.Rows[0]["Score"]).ToString();
					base.ViewBag.InsureScore = Convert.ToInt64(money.Rows[0]["InsureScore"]);
				}
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult GrantMember()
		{
			base.ViewBag.ModuleID = user.MoudleID;
			base.ViewBag.UserID = user.RoleID;
			base.ViewBag.Paras = "";
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			base.ViewBag.Paras = text;
			return View();
		}

		[CheckCustomer]
		public ActionResult RecordUserScoreInoutList()
		{
			base.ViewBag.Paras = "";
			string text = TypeUtil.ObjectToString(base.Request["param"]);
			base.ViewBag.ID = text;
			return View();
		}

		[CheckCustomer]
		public ActionResult GrantExperience()
		{
			base.ViewBag.ModuleID = user.MoudleID;
			base.ViewBag.UserID = user.UserID;
			base.ViewBag.Paras = "";
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			base.ViewBag.Paras = text;
			return View();
		}

		[CheckCustomer]
		public ActionResult AccountsInsureTop()
		{
			DataSet userTransferTop = FacadeManage.aideTreasureFacade.GetUserTransferTop100();
			base.ViewData["data"] = ((userTransferTop != null) ? userTransferTop.Tables[0] : null);
			return View();
		}

		[CheckCustomer]
		public ActionResult GrantScore()
		{
			base.ViewBag.ModuleID = user.MoudleID;
			base.ViewBag.UserID = user.UserID;
			base.ViewBag.Paras = "";
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			base.ViewBag.Paras = text;
			return View();
		}

		[CheckCustomer]
		public ActionResult GrantClearScore()
		{
			base.ViewBag.ModuleID = user.MoudleID;
			base.ViewBag.UserID = user.UserID;
			base.ViewBag.Paras = "";
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			base.ViewBag.Paras = text;
			return View();
		}

		[CheckCustomer]
		public ActionResult GrantFlee()
		{
			base.ViewBag.ModuleID = user.MoudleID;
			base.ViewBag.UserID = user.UserID;
			base.ViewBag.Paras = "";
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			base.ViewBag.Paras = text;
			return View();
		}

		[CheckCustomer]
		public ActionResult AccountsGoldList()
		{
			base.ViewBag.ModuleID = ((user != null) ? user.MoudleID : 0);
			base.ViewBag.UserID = user.UserID;
			return View();
		}

		[CheckCustomer]
		public ActionResult JiechuDailiList()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult RecordGrantTreasureList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			return View();
		}

		[CheckCustomer]
		public ActionResult RecordGrantClearFlee()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			return View();
		}

		[CheckCustomer]
		public ActionResult RecordConvertPresentList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			return View();
		}

		[CheckCustomer]
		public ActionResult RecordInsureList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			return View();
		}

		[CheckCustomer]
		public ActionResult RecordGrantExperienceList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			return View();
		}

		[CheckCustomer]
		public ActionResult RecordGrantMemberList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			return View();
		}

		[CheckCustomer]
		public ActionResult AccountsCurrencyList()
		{
			base.ViewBag.ModuleID = ((user != null) ? user.MoudleID : 0);
			return View();
		}

		[CheckCustomer]
		public ActionResult AccountsScoreList()
		{
			base.ViewBag.ModuleID = ((user != null) ? user.MoudleID : 0);
			return View();
		}

		[CheckCustomer]
		public ActionResult AccountsGoldInfo()
		{
			base.ViewBag.Paras = "";
			string text = TypeUtil.ObjectToString(base.Request["param"]);
			base.ViewBag.Paras = text;
			base.ViewBag.NavActivated = "C";
			AccountsInfo accountInfoByUserID = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(TypeUtil.ObjectToInt(text));
			if (accountInfoByUserID != null)
			{
				base.ViewBag.GameID = accountInfoByUserID.GameID;
				base.ViewBag.Accounts = accountInfoByUserID.Accounts.Trim();
				base.ViewBag.UserMedal = accountInfoByUserID.UserMedal.ToString("N0");
				base.ViewBag.Love = accountInfoByUserID.LoveLiness.ToString("N0");
				base.ViewBag.LastLogonIP = accountInfoByUserID.LastLogonIP.ToString();
				base.ViewBag.LogonIPInfo = accountInfoByUserID.LastLogonIPAddress;
			}
			UserCurrencyInfo userCurrencyInfo = FacadeManage.aideTreasureFacade.GetUserCurrencyInfo(TypeUtil.ObjectToInt(text));
			if (userCurrencyInfo != null)
			{
				base.ViewBag.Currency = userCurrencyInfo.Currency.ToString("NO");
			}
			GameScoreInfo gameScoreInfoByUserID = FacadeManage.aideTreasureFacade.GetGameScoreInfoByUserID(TypeUtil.ObjectToInt(text));
			if (gameScoreInfoByUserID != null)
			{
				base.ViewBag.Score = gameScoreInfoByUserID.Score.ToString("NO");
				base.ViewBag.InsureScore = gameScoreInfoByUserID.InsureScore.ToString("NO");
				base.ViewBag.WinCount = gameScoreInfoByUserID.WinCount.ToString();
				base.ViewBag.LostCount = gameScoreInfoByUserID.LostCount.ToString();
				base.ViewBag.DrawCount = gameScoreInfoByUserID.DrawCount.ToString();
				base.ViewBag.FleeCount = gameScoreInfoByUserID.FleeCount.ToString();
				base.ViewBag.Revenue = gameScoreInfoByUserID.Revenue.ToString("N0");
				base.ViewBag.AllLogonTimes = gameScoreInfoByUserID.AllLogonTimes.ToString();
				base.ViewBag.OnLineTimeCount = gameScoreInfoByUserID.OnLineTimeCount.ToString();
				base.ViewBag.PlayTimeCount = gameScoreInfoByUserID.PlayTimeCount.ToString();
				base.ViewBag.LastLogonDate = ((gameScoreInfoByUserID.AllLogonTimes == 0) ? "从未登陆房间" : gameScoreInfoByUserID.LastLogonDate.ToString("yyyy-MM-dd HH:mm:ss"));
				base.ViewBag.LogonSpacingTime = ((gameScoreInfoByUserID.AllLogonTimes == 0) ? "" : (Fetch.GetTimeSpan(Convert.ToDateTime(gameScoreInfoByUserID.LastLogonDate), DateTime.Now) + " 前"));
				base.ViewBag.LastLogonMachine = gameScoreInfoByUserID.LastLogonMachine.ToString();
				base.ViewBag.RegisterDate = gameScoreInfoByUserID.RegisterDate.ToString("yyyy-MM-dd HH:mm:ss");
				base.ViewBag.RegisterIP = gameScoreInfoByUserID.RegisterIP.ToString();
				try
				{
					base.ViewBag.RegIPInfo = IPQuery.GetAddressWithIP(gameScoreInfoByUserID.RegisterIP.ToString());
				}
				catch
				{
					base.ViewBag.RegIPInfo = "";
				}
				base.ViewBag.RegisterMachine = gameScoreInfoByUserID.RegisterMachine.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Where UserID={0}", TypeUtil.ObjectToInt(text));
			string orderby = "ORDER BY CollectDate DESC";
			PagerSet gameScoreLockerList = FacadeManage.aideTreasureFacade.GetGameScoreLockerList(1, 100, stringBuilder.ToString(), orderby);
			if (gameScoreLockerList.PageSet.Tables[0].Rows.Count > 0)
			{
				base.ViewBag.Data = gameScoreLockerList.PageSet.Tables[0];
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult AccountsInfo()
		{
			base.ViewBag.ModuleID = user.MoudleID;
			base.ViewBag.UserID = user.UserID;
			base.ViewBag.TagIndex = TypeUtil.ObjectToInt(base.Request["tagindex"]);
			string text = TypeUtil.ObjectToString(base.Request["param"]);
			base.ViewBag.Param = text;
			int userID = TypeUtil.ObjectToInt(text);
			AccountsInfo accountInfoByUserID = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(userID);
			IndividualDatum accountDetailByUserID = FacadeManage.aideAccountsFacade.GetAccountDetailByUserID(userID);
			if (accountDetailByUserID != null)
			{
				base.ViewBag.QQ = accountDetailByUserID.QQ.ToString();
				base.ViewBag.EMail = accountDetailByUserID.EMail.ToString();
				base.ViewBag.SeatPhone = accountDetailByUserID.SeatPhone.ToString();
				base.ViewBag.MobilePhone = accountDetailByUserID.MobilePhone.ToString();
				base.ViewBag.PostalCode = accountDetailByUserID.PostalCode.ToString();
				base.ViewBag.UserNote = accountDetailByUserID.UserNote;
				base.ViewBag.DwellingPlace = accountDetailByUserID.DwellingPlace;
				base.ViewBag.BankNO = accountDetailByUserID.BankNO;
				base.ViewBag.BankName = accountDetailByUserID.BankName;
				base.ViewBag.BankAddress = accountDetailByUserID.BankAddress;
				base.ViewBag.Compellation1 = accountDetailByUserID.Compellation;
			}
			if (accountInfoByUserID != null)
			{
				base.ViewBag.UserModel = accountInfoByUserID.UserMedal.ToString();
				base.ViewBag.GameID = accountInfoByUserID.GameID;
				base.ViewBag.RegAccounts = accountInfoByUserID.RegAccounts.Trim();
				base.ViewBag.Accounts = accountInfoByUserID.Accounts.Trim();
				base.ViewBag.NickName = accountInfoByUserID.NickName.Trim();
				base.ViewBag.Compellation = accountInfoByUserID.Compellation.Trim();
				base.ViewBag.UnderWrite = accountInfoByUserID.UnderWrite.Trim();
				base.ViewBag.Nullity = accountInfoByUserID.Nullity;
				base.ViewBag.StunDown = accountInfoByUserID.StunDown;
				base.ViewBag.Experience = accountInfoByUserID.Experience;
				base.ViewBag.Present = accountInfoByUserID.Present;
				base.ViewBag.LoveLiness = accountInfoByUserID.LoveLiness;
				base.ViewBag.RegisterMobile = accountInfoByUserID.RegisterMobile;
				base.ViewBag.ProtectID = ((accountInfoByUserID.ProtectID > 0) ? ("<span style=\"font-weight: bold;\">已申请</span>&nbsp;<a href=\"javascript:openWindow('" + base.Server.MapPath("~/") + "Account/AccountsProtectInfo?param=" + accountInfoByUserID.ProtectID + "',580,320)\" class=\"l1\">点击查看</a>") : "未申请");
				dynamic viewBag = base.ViewBag;
				string memberName = TypeUtil.GetMemberName(accountInfoByUserID.MemberOrder);
				DateTime dateTime;
				object str;
				if (accountInfoByUserID.MemberOrder != 0)
				{
					dateTime = accountInfoByUserID.MemberSwitchDate;
					str = "&nbsp;&nbsp;&nbsp;&nbsp;到期时间：" + dateTime.ToString("yyyy-MM-dd HH:mm:ss");
				}
				else
				{
					str = "";
				}
				viewBag.MemberInfo = memberName + (string)str;
				if (accountInfoByUserID.SpreaderID != 0)
				{
					AccountsAgent accountAgentByUserID = FacadeManage.aideAccountsFacade.GetAccountAgentByUserID(accountInfoByUserID.SpreaderID);
					if (accountAgentByUserID.UserID != 0)
					{
						base.ViewBag.SName = "代理商";
					}
					else
					{
						base.ViewBag.SName = "推广人";
					}
					base.ViewBag.Spreader = TypeUtil.GetAccounts(accountInfoByUserID.SpreaderID);
				}
				if (accountInfoByUserID.MemberOrder != 0)
				{
					base.ViewBag.MemberOrder = accountInfoByUserID.MemberOrder;
				}
				base.ViewBag.FaceUrl = FacadeManage.aideAccountsFacade.GetUserFaceUrl(accountInfoByUserID.FaceID, accountInfoByUserID.CustomID);
				if (accountInfoByUserID.CustomID != 0 && FacadeManage.aideAccountsFacade.GetAccountsFace(accountInfoByUserID.CustomID) != null)
				{
					base.ViewBag.FaceId = accountInfoByUserID.CustomID;
					base.ViewBag.FaceType = 2;
				}
				else
				{
					base.ViewBag.FaceId = accountInfoByUserID.FaceID;
					base.ViewBag.FaceType = 1;
				}
				base.ViewBag.Gender = accountInfoByUserID.Gender;
				base.ViewBag.MoorMachine = accountInfoByUserID.MoorMachine;
				base.ViewBag.UserRight = accountInfoByUserID.UserRight;
				base.ViewBag.MasterOrder = accountInfoByUserID.MasterOrder;
				base.ViewBag.MasterRight = accountInfoByUserID.MasterRight;
				base.ViewBag.IsAndroid = accountInfoByUserID.IsAndroid;
				base.ViewBag.WebLogonTimes = accountInfoByUserID.WebLogonTimes;
				base.ViewBag.GameLogonTimes = accountInfoByUserID.GameLogonTimes;
				base.ViewBag.LastLogonDate = accountInfoByUserID.LastLogonDate.ToString("yyyy-MM-dd HH:mm:ss");
				base.ViewBag.LogonSpacingTime = Fetch.GetTimeSpan(Convert.ToDateTime(accountInfoByUserID.LastLogonDate), DateTime.Now);
				base.ViewBag.LastLogonIP = accountInfoByUserID.LastLogonIP;
				base.ViewBag.LogonIPInfo = accountInfoByUserID.LastLogonIPAddress;
				base.ViewBag.LastLogonMachine = accountInfoByUserID.LastLogonMachine;
				dynamic viewBag2 = base.ViewBag;
				dateTime = accountInfoByUserID.RegisterDate;
				viewBag2.RegisterDate = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
				base.ViewBag.RegisterIP = accountInfoByUserID.RegisterIP;
				base.ViewBag.RegIPInfo = accountInfoByUserID.LastLogonIPAddress;
				base.ViewBag.RegisterMachine = accountInfoByUserID.RegisterMachine;
				base.ViewBag.RegisterOrigin = TypeUtil.GetRegisterOrigin(accountInfoByUserID.RegisterOrigin);
				base.ViewBag.OnLineTimeCount = Fetch.ConverTimeToDHMS(accountInfoByUserID.OnLineTimeCount);
				base.ViewBag.PlayTimeCount = Fetch.ConverTimeToDHMS(accountInfoByUserID.PlayTimeCount);
				base.ViewBag.PasswordID = accountInfoByUserID.PasswordID;
				base.ViewBag.CardNum = accountInfoByUserID.PassPortID;
				base.ViewBag.PasswordCard = ((accountInfoByUserID.PasswordID != 0) ? "<span style=\"font-weight: bold;\">已绑定</span>" : "");
				base.ViewBag.UserType = accountInfoByUserID.UserType;
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult AccountsDetailInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.Param = num;
			AccountsInfo accountInfoByUserID = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(num);
			IndividualDatum accountDetailByUserID = FacadeManage.aideAccountsFacade.GetAccountDetailByUserID(num);
			if (accountDetailByUserID != null)
			{
				base.ViewBag.BankNO = accountDetailByUserID.BankNO;
				base.ViewBag.BankName = accountDetailByUserID.BankName;
				base.ViewBag.BankAddress = accountDetailByUserID.BankAddress;
				base.ViewBag.QQ = accountDetailByUserID.QQ;
				base.ViewBag.EMail = accountDetailByUserID.EMail;
				base.ViewBag.SeatPhone = accountDetailByUserID.SeatPhone;
				base.ViewBag.MobilePhone = accountDetailByUserID.MobilePhone;
				base.ViewBag.PostalCode = accountDetailByUserID.PostalCode;
				base.ViewBag.DwellingPlace = accountDetailByUserID.DwellingPlace;
				base.ViewBag.UserNote = accountDetailByUserID.UserNote;
				base.ViewBag.Compellation = accountDetailByUserID.Compellation;
			}
			if (accountInfoByUserID != null)
			{
				base.ViewBag.GameID = accountInfoByUserID.GameID;
				base.ViewBag.Accounts = accountInfoByUserID.Accounts.Trim();
				base.ViewBag.CardNum = accountInfoByUserID.PassPortID;
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult AccountsTreasureInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.Param = num;
			AccountsInfo accountInfoByUserID = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(num);
			if (accountInfoByUserID != null)
			{
				base.ViewBag.GameID = accountInfoByUserID.GameID;
				base.ViewBag.Accounts = accountInfoByUserID.Accounts;
				base.ViewBag.UserModel = accountInfoByUserID.UserMedal.ToString();
				base.ViewBag.LoveLiness = accountInfoByUserID.LoveLiness;
				base.ViewBag.GameLogonTimes = accountInfoByUserID.GameLogonTimes;
				base.ViewBag.OnLineTimeCount = accountInfoByUserID.OnLineTimeCount;
				base.ViewBag.PlayTimeCount = accountInfoByUserID.PlayTimeCount;
				base.ViewBag.LastLogonMachine = accountInfoByUserID.LastLogonMachine;
				base.ViewBag.RegisterMachine = accountInfoByUserID.RegisterMachine;
				base.ViewBag.LastLogonDate = accountInfoByUserID.LastLogonDate;
				base.ViewBag.LastLogonIP = accountInfoByUserID.LastLogonIP;
				base.ViewBag.RegisterDate = accountInfoByUserID.RegisterDate;
				base.ViewBag.RegisterIP = accountInfoByUserID.RegisterIP;
			}
			UserCurrencyInfo userCurrencyInfo = FacadeManage.aideTreasureFacade.GetUserCurrencyInfo(num);
			if (userCurrencyInfo != null)
			{
				base.ViewBag.Currency = userCurrencyInfo.Currency.ToString("N0");
			}
			GameScoreInfo gameScoreInfoByUserID = FacadeManage.aideTreasureFacade.GetGameScoreInfoByUserID(num);
			if (gameScoreInfoByUserID != null)
			{
				base.ViewBag.Score = gameScoreInfoByUserID.Score.ToString();
				base.ViewBag.InsureScore = gameScoreInfoByUserID.InsureScore.ToString();
				base.ViewBag.WinCount = gameScoreInfoByUserID.WinCount.ToString();
				base.ViewBag.LostCount = gameScoreInfoByUserID.LostCount;
				base.ViewBag.Revenue = gameScoreInfoByUserID.Revenue.ToString();
				base.ViewBag.FleeCount = gameScoreInfoByUserID.FleeCount;
				base.ViewBag.DrawCount = gameScoreInfoByUserID.DrawCount;
				base.ViewBag.LastLogonMachine = gameScoreInfoByUserID.LastLogonMachine;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Where UserID={0}", num);
			string orderby = "ORDER BY CollectDate DESC";
			PagerSet gameScoreLockerList = FacadeManage.aideTreasureFacade.GetGameScoreLockerList(1, 100, stringBuilder.ToString(), orderby);
			if (gameScoreLockerList != null && gameScoreLockerList.PageSet != null && gameScoreLockerList.PageSet.Tables.Count > 0)
			{
				base.ViewData["kxdt"] = gameScoreLockerList.PageSet.Tables[0];
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult AccountsRecordInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.Param = num;
			AccountsInfo accountInfoByUserID = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(num);
			if (accountInfoByUserID != null)
			{
				base.ViewBag.GameID = accountInfoByUserID.GameID;
				base.ViewBag.Accounts = accountInfoByUserID.Accounts;
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult AccountPasswordCard()
		{
			base.ViewBag.Paras = "";
			string text = TypeUtil.ObjectToString(base.Request["param"]);
			base.ViewBag.Paras = text;
			return View();
		}

		[CheckCustomer]
		public ActionResult AccountsMemberList()
		{
			base.ViewBag.Paras = "";
			string text = TypeUtil.ObjectToString(base.Request["param"]);
			base.ViewBag.Paras = text;
			return View();
		}

		[CheckCustomer]
		public ActionResult RecordGrantScoreList()
		{
			base.ViewBag.Paras = "";
			string text = TypeUtil.ObjectToString(base.Request["param"]);
			base.ViewBag.Paras = text;
			return View();
		}

		[CheckCustomer]
		public ActionResult AccountsProtectInfo()
		{
			base.ViewBag.Paras = "";
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.Paras = num;
			Protection protection = new Protection(TextUtility.GetRealPath("/App_Data/protection.xml"));
			List<string> protectionQuestions = protection.GetProtectionQuestions();
			base.ViewData["protect"] = protectionQuestions;
			AccountsProtect accountsProtectByPID = FacadeManage.aideAccountsFacade.GetAccountsProtectByPID(num);
			if (accountsProtectByPID == null)
			{
				base.ViewBag.StrTitle = "用户密保信息不存在";
				base.ViewBag.Enabled = false;
			}
			else
			{
				AccountsInfo accountInfoByUserID = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(accountsProtectByPID.UserID);
				if (accountInfoByUserID != null)
				{
					base.ViewBag.StrTitle = "玩家-" + accountInfoByUserID.Accounts + "-密保信息";
					base.ViewBag.Accounts = accountInfoByUserID.Accounts.Trim();
					base.ViewBag.SafeEmail = accountsProtectByPID.SafeEmail.Trim();
					base.ViewBag.Question1 = accountsProtectByPID.Question1;
					base.ViewBag.Question2 = accountsProtectByPID.Question2;
					base.ViewBag.Question3 = accountsProtectByPID.Question3;
					base.ViewBag.Response1 = accountsProtectByPID.Response1.Trim();
					base.ViewBag.Response2 = accountsProtectByPID.Response2.Trim();
					base.ViewBag.Response3 = accountsProtectByPID.Response3.Trim();
					base.ViewBag.PassportID = accountsProtectByPID.PassportID.Trim();
					base.ViewBag.PassportType = TypeUtil.GetPassPortType(accountsProtectByPID.PassportType);
					base.ViewBag.CreateIP = accountsProtectByPID.CreateIP.Trim();
					base.ViewBag.ModifyIP = accountsProtectByPID.ModifyIP.Trim();
					base.ViewBag.CreateDate = accountsProtectByPID.CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
					base.ViewBag.ModifyDate = accountsProtectByPID.ModifyDate.ToString("yyyy-MM-dd HH:mm:ss");
				}
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult GrantGameID()
		{
			base.ViewBag.ModuleID = user.MoudleID;
			base.ViewBag.RoleID = user.RoleID;
			base.ViewBag.Paras = "";
			string text = TypeUtil.ObjectToString(base.Request["param"]);
			string text2 = TypeUtil.ObjectToString(base.Request["accounts"]);
			base.ViewBag.Paras = text;
			base.ViewBag.Accounts = text2;
			return View();
		}

		[CheckCustomer]
		public ActionResult AccountsScoreInfo()
		{
			base.ViewBag.Paras = "";
			string text = TypeUtil.ObjectToString(base.Request["param"]);
			base.ViewBag.Paras = text;
			AccountsInfo accountInfoByUserID = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(TypeUtil.ObjectToInt(text));
			if (accountInfoByUserID != null)
			{
				base.ViewBag.GameID = accountInfoByUserID.GameID.ToString();
				base.ViewBag.Accounts = accountInfoByUserID.Accounts.Trim();
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult AccountsConvertPresentList()
		{
			base.ViewBag.ModuleID = ((user != null) ? user.MoudleID : 0);
			return View();
		}

		[CheckCustomer]
		public ActionResult RecordConvertMedalList()
		{
			base.ViewBag.ModuleID = ((user != null) ? user.MoudleID : 0);
			return View();
		}

		[CheckCustomer]
		public ActionResult AccountsGrantTreasureList()
		{
			base.ViewBag.ModuleID = ((user != null) ? user.MoudleID : 0);
			return View();
		}

		[CheckCustomer]
		public ActionResult RecordUsePropertyList()
		{
			base.ViewBag.ModuleID = ((user != null) ? user.MoudleID : 0);
			return View();
		}

		[CheckCustomer]
		public ActionResult RecordBuyPropertyList()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult AccountsInsureList()
		{
			base.ViewBag.ModuleID = ((user != null) ? user.MoudleID : 0);
			return View();
		}

		[CheckCustomer]
		public ActionResult RecordUserInoutList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			return View();
		}

		[CheckCustomer]
		public ActionResult RecordGrantGameIDList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			return View();
		}

		[CheckCustomer]
		public ActionResult RecordGrantClearScoreList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			return View();
		}

		[CheckCustomer]
		public ActionResult ConfineContentList()
		{
			base.ViewBag.ModuleID = ((user != null) ? user.MoudleID : 0);
			return View();
		}

		[CheckCustomer]
		public ActionResult ConfineContenInfo()
		{
			base.ViewBag.Paras = "";
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.Paras = num;
			if (user != null)
			{
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(1L))
				{
					base.Response.Redirect("/NoPower/Index", true);
				}
				ConfineContent confineContentByContentID = FacadeManage.aideAccountsFacade.GetConfineContentByContentID(num);
				if (confineContentByContentID != null)
				{
					base.ViewBag.String = confineContentByContentID.String;
					base.ViewBag.EnjoinOverDate = ((!confineContentByContentID.EnjoinOverDate.HasValue) ? "" : Convert.ToDateTime(confineContentByContentID.EnjoinOverDate).ToString("yyyy-MM-dd"));
				}
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult ConfineAddressList()
		{
			base.ViewBag.ModuleID = ((user != null) ? user.MoudleID : 0);
			if (user != null)
			{
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(1L))
				{
					base.Response.Redirect("/NoPower/Index", true);
				}
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult RecordDrawInfoList()
		{
			if (user != null)
			{
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(1L))
				{
					base.Response.Redirect("/NoPower/Index", true);
				}
			}
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			return View();
		}

		[CheckCustomer]
		public ActionResult ConfineAddressInfo()
		{
			base.ViewBag.Paras = "";
			string text = TypeUtil.ObjectToString(base.Request["param"]);
			base.ViewBag.Paras = text;
			Game.Entity.Accounts.ConfineAddress confineAddressByAddress = FacadeManage.aideAccountsFacade.GetConfineAddressByAddress(text);
			if (confineAddressByAddress != null)
			{
				base.ViewBag.String = confineAddressByAddress.AddrString;
				base.ViewBag.EnjoinLogon = confineAddressByAddress.EnjoinLogon;
				base.ViewBag.EnjoinRegister = confineAddressByAddress.EnjoinRegister;
				base.ViewBag.EnjoinOverDate = ((!confineAddressByAddress.EnjoinOverDate.HasValue) ? "" : Convert.ToDateTime(confineAddressByAddress.EnjoinOverDate).ToString("yyyy-MM-dd"));
				base.ViewBag.CollectNote = confineAddressByAddress.CollectNote;
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult ConfineAddressTop()
		{
			DataSet iPRegisterTop = FacadeManage.aideAccountsFacade.GetIPRegisterTop100();
			if (iPRegisterTop != null && iPRegisterTop.Tables.Count > 0)
			{
				base.ViewBag.Data = iPRegisterTop.Tables[0];
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult ConfineMachineList()
		{
			base.ViewBag.ModuleID = 0;
			if (user != null)
			{
				base.ViewBag.ModuleID = user.MoudleID;
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(1L))
				{
					base.Response.Redirect("/NoPower/Index", true);
				}
			}
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public ActionResult ConfineMachineInfo()
		{
			if (user != null)
			{
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(1L))
				{
					base.Response.Redirect("/NoPower/Index", true);
				}
			}
			base.ViewBag.Paras = "";
			string text = TypeUtil.ObjectToString(base.Request["param"]);
			base.ViewBag.Paras = text;
			Game.Entity.Accounts.ConfineMachine confineMachineBySerial = FacadeManage.aideAccountsFacade.GetConfineMachineBySerial(text);
			if (confineMachineBySerial != null)
			{
				base.ViewBag.String = confineMachineBySerial.MachineSerial;
				base.ViewBag.EnjoinLogon = confineMachineBySerial.EnjoinLogon;
				base.ViewBag.EnjoinRegister = confineMachineBySerial.EnjoinRegister;
				base.ViewBag.EnjoinOverDate = ((!confineMachineBySerial.EnjoinOverDate.HasValue) ? "" : Convert.ToDateTime(confineMachineBySerial.EnjoinOverDate).ToString("yyyy-MM-dd"));
				base.ViewBag.CollectNote = confineMachineBySerial.CollectNote;
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult ConfineMachineTop()
		{
			DataSet machineRegisterTop = FacadeManage.aideAccountsFacade.GetMachineRegisterTop100();
			if (machineRegisterTop != null && machineRegisterTop.Tables.Count > 0)
			{
				base.ViewBag.Data = machineRegisterTop.Tables[0];
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult AccountsLossReportList()
		{
			if (user != null)
			{
				base.ViewBag.ModuleID = user.MoudleID;
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult AccountsLossReportInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ReportId = num;
			if (num != 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				LossReport lossReport = FacadeManage.aideNativeWebFacade.GetLossReport(num);
				AccountsInfo accountInfoByUserID = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(lossReport.UserID);
				IndividualDatum accountDetailByUserID = FacadeManage.aideAccountsFacade.GetAccountDetailByUserID(lossReport.UserID);
				AccountsProtect accountsProtectByUserID = FacadeManage.aideAccountsFacade.GetAccountsProtectByUserID(lossReport.UserID);
				base.ViewBag.UserID = lossReport.UserID;
				base.ViewBag.Accounts = lossReport.Accounts;
				string text = "（&nbsp;<font class=\"lanse fontTip\"><strong>√</strong></font>&nbsp;）";
				string text2 = "（&nbsp;<font class=\"hong fontTip\"><strong>×</strong></font>&nbsp;）";
				int num2 = 0;
				int num3 = 0;
				if (string.IsNullOrEmpty(lossReport.PassportID))
				{
					num3++;
					base.ViewBag.PassportID = "";
					stringBuilder.Append("身份证号（未填写）<br/>");
				}
				else if (lossReport.PassportID == accountInfoByUserID.PassPortID)
				{
					base.ViewBag.PassportID = text + lossReport.PassportID;
					num2++;
					stringBuilder.Append("身份证号（正确）<br/>");
				}
				else
				{
					base.ViewBag.PassportID = text2 + lossReport.PassportID;
					stringBuilder.Append("身份证号（错误）<br/>");
				}
				if (string.IsNullOrEmpty(lossReport.MobilePhone))
				{
					num3++;
					base.ViewBag.LinkPhone = "";
					stringBuilder.Append("移动电话（未填写）<br/>");
				}
				else if (accountDetailByUserID == null || lossReport.MobilePhone != accountDetailByUserID.MobilePhone)
				{
					base.ViewBag.LinkPhone = text2 + lossReport.MobilePhone;
					stringBuilder.Append("移动电话（错误）<br/>");
				}
				else
				{
					base.ViewBag.LinkPhone = text + lossReport.MobilePhone;
					num2++;
					stringBuilder.Append("移动电话（正确）<br/>");
				}
				if (string.IsNullOrEmpty(lossReport.Compellation))
				{
					num3++;
					base.ViewBag.Compellation = "";
					stringBuilder.Append("移动电话（未填写）<br/>");
				}
				else if (lossReport.Compellation == accountInfoByUserID.Compellation)
				{
					base.ViewBag.Compellation = text + lossReport.Compellation;
					num2++;
					stringBuilder.Append("移动电话（正确）<br/>");
				}
				else
				{
					base.ViewBag.Compellation = text2 + lossReport.Compellation;
					stringBuilder.Append("移动电话（错误）<br/>");
				}
				DateTime dateTime;
				if (string.IsNullOrEmpty(lossReport.RegisterDate))
				{
					num3++;
					base.ViewBag.RegisterTime = "";
					stringBuilder.Append("注册时间（未填写）<br/>");
				}
				else
				{
					dateTime = Convert.ToDateTime(lossReport.RegisterDate);
					if (dateTime.Date == accountInfoByUserID.RegisterDate.Date)
					{
						base.ViewBag.RegisterTime = text + lossReport.RegisterDate;
						num2++;
						stringBuilder.Append("注册时间（正确）<br/>");
					}
					else
					{
						base.ViewBag.RegisterTime = text2 + lossReport.RegisterDate;
						stringBuilder.Append("注册时间（错误）<br/>");
					}
				}
				if (string.IsNullOrEmpty(lossReport.OldQuestion1) && string.IsNullOrEmpty(lossReport.OldResponse1))
				{
					num3++;
					base.ViewBag.OldProtect1 = "";
					stringBuilder.Append("申诉密保1（未填写）<br/>");
				}
				else if ((lossReport.OldQuestion1 == accountsProtectByUserID.Question1 && lossReport.OldResponse1 == accountsProtectByUserID.Response1) || (lossReport.OldQuestion1 == accountsProtectByUserID.Question2 && lossReport.OldResponse1 == accountsProtectByUserID.Response2) || (lossReport.OldQuestion1 == accountsProtectByUserID.Question3 && lossReport.OldResponse1 == accountsProtectByUserID.Response3))
				{
					base.ViewBag.OldProtect1 = text + "问：" + lossReport.OldQuestion1 + " 答：" + lossReport.OldResponse1;
					num2++;
					stringBuilder.Append("申诉密保1（正确）<br/>");
				}
				else
				{
					base.ViewBag.OldProtect1 = text2 + "问：" + lossReport.OldQuestion1 + " 答：" + lossReport.OldResponse1;
					stringBuilder.Append("申诉密保1（错误）<br/>");
				}
				if (string.IsNullOrEmpty(lossReport.OldQuestion2) && string.IsNullOrEmpty(lossReport.OldResponse2))
				{
					num3++;
					base.ViewBag.OldProtect2 = "";
					stringBuilder.Append("申诉密保2（未填写）<br/>");
				}
				else if ((lossReport.OldQuestion2 == accountsProtectByUserID.Question1 && lossReport.OldResponse2 == accountsProtectByUserID.Response1) || (lossReport.OldQuestion2 == accountsProtectByUserID.Question2 && lossReport.OldResponse2 == accountsProtectByUserID.Response2) || (lossReport.OldQuestion2 == accountsProtectByUserID.Question3 && lossReport.OldResponse2 == accountsProtectByUserID.Response3))
				{
					base.ViewBag.OldProtect2 = text + "问：" + lossReport.OldQuestion2 + " 答：" + lossReport.OldResponse2;
					num2++;
					stringBuilder.Append("申诉密保2（正确）<br/>");
				}
				else
				{
					base.ViewBag.OldProtect2 = text2 + "问：" + lossReport.OldQuestion2 + " 答：" + lossReport.OldResponse2;
					stringBuilder.Append("申诉密保2（错误）<br/>");
				}
				if (string.IsNullOrEmpty(lossReport.OldQuestion3) && string.IsNullOrEmpty(lossReport.OldResponse3))
				{
					num3++;
					base.ViewBag.OldProtect3 = "";
					stringBuilder.Append("申诉密保3（未填写）<br/>");
				}
				else if ((lossReport.OldQuestion3 == accountsProtectByUserID.Question1 && lossReport.OldResponse3 == accountsProtectByUserID.Response1) || (lossReport.OldQuestion3 == accountsProtectByUserID.Question2 && lossReport.OldResponse3 == accountsProtectByUserID.Response2) || (lossReport.OldQuestion3 == accountsProtectByUserID.Question3 && lossReport.OldResponse3 == accountsProtectByUserID.Response3))
				{
					base.ViewBag.OldProtect3 = text + "问：" + lossReport.OldQuestion3 + " 答：" + lossReport.OldResponse3;
					num2++;
					stringBuilder.Append("申诉密保3（正确）<br/>");
				}
				else
				{
					base.ViewBag.OldProtect3 = text2 + "问：" + lossReport.OldQuestion3 + " 答：" + lossReport.OldResponse3;
					stringBuilder.Append("申诉密保3（错误）<br/>");
				}
				Dictionary<int, string> oldNickNameOrAccountsList = FacadeManage.aideRecordFacade.GetOldNickNameOrAccountsList(lossReport.UserID, 1);
				if (string.IsNullOrEmpty(lossReport.OldNickName1))
				{
					num3++;
					base.ViewBag.OldNickName1 = "";
					stringBuilder.Append("绑定历史昵称1（未填写）<br/>");
				}
				else if (oldNickNameOrAccountsList.ContainsValue(lossReport.OldNickName1))
				{
					base.ViewBag.OldNickName1 = text + lossReport.OldNickName1;
					num2++;
					stringBuilder.Append("绑定历史昵称1（正确）<br/>");
				}
				else
				{
					base.ViewBag.OldNickName1 = text2 + lossReport.OldNickName1;
					stringBuilder.Append("绑定历史昵称1（错误）<br/>");
				}
				if (string.IsNullOrEmpty(lossReport.OldNickName2))
				{
					num3++;
					base.ViewBag.OldNickName2 = "";
					stringBuilder.Append("绑定历史昵称2（未填写）<br/>");
				}
				else if (oldNickNameOrAccountsList.ContainsValue(lossReport.OldNickName2))
				{
					base.ViewBag.OldNickName2 = text + lossReport.OldNickName2;
					num2++;
					stringBuilder.Append("绑定历史昵称2（正确）<br/>");
				}
				else
				{
					base.ViewBag.OldNickName2 = text2 + lossReport.OldNickName2;
					stringBuilder.Append("绑定历史昵称2（错误）<br/>");
				}
				if (string.IsNullOrEmpty(lossReport.OldNickName3))
				{
					num3++;
					base.ViewBag.OldNickName3 = "";
					stringBuilder.Append("绑定历史昵称3（未填写）<br/>");
				}
				else if (oldNickNameOrAccountsList.ContainsValue(lossReport.OldNickName3))
				{
					base.ViewBag.OldNickName3 = text + lossReport.OldNickName3;
					num2++;
					stringBuilder.Append("绑定历史昵称3（正确）<br/>");
				}
				else
				{
					base.ViewBag.OldNickName3 = text2 + lossReport.OldNickName3;
					stringBuilder.Append("绑定历史昵称3（错误）<br/>");
				}
				oldNickNameOrAccountsList = FacadeManage.aideRecordFacade.GetOldLogonPassList(lossReport.UserID);
				if (string.IsNullOrEmpty(lossReport.OldLogonPass1))
				{
					num3++;
					base.ViewBag.OldLogonPass1 = "";
					stringBuilder.Append("绑定历史登录密码1（未填写）<br/>");
				}
				else if (oldNickNameOrAccountsList.ContainsValue(lossReport.OldLogonPass1))
				{
					base.ViewBag.OldLogonPass1 = text + lossReport.OldLogonPass1;
					num2++;
					stringBuilder.Append("绑定历史登录密码1（正确）<br/>");
				}
				else
				{
					base.ViewBag.OldLogonPass1 = text2 + lossReport.OldLogonPass1;
					stringBuilder.Append("绑定历史登录密码1（错误）<br/>");
				}
				if (string.IsNullOrEmpty(lossReport.OldLogonPass2))
				{
					num3++;
					base.ViewBag.OldLogonPass2 = "";
					stringBuilder.Append("绑定历史登录密码2（未填写）<br/>");
				}
				else if (oldNickNameOrAccountsList.ContainsValue(lossReport.OldLogonPass2))
				{
					base.ViewBag.OldLogonPass2 = text + lossReport.OldLogonPass2;
					num2++;
					stringBuilder.Append("绑定历史登录密码2（正确）<br/>");
				}
				else
				{
					base.ViewBag.OldLogonPass2 = text2 + lossReport.OldLogonPass2;
					stringBuilder.Append("绑定历史登录密码2（错误）<br/>");
				}
				if (string.IsNullOrEmpty(lossReport.OldLogonPass3))
				{
					num3++;
					base.ViewBag.OldLogonPass3 = "";
					stringBuilder.Append("绑定历史登录密码3（未填写）<br/>");
				}
				else if (oldNickNameOrAccountsList.ContainsValue(lossReport.OldLogonPass3))
				{
					base.ViewBag.OldLogonPass3 = text + lossReport.OldLogonPass3;
					num2++;
					stringBuilder.Append("绑定历史登录密码3（正确）<br/>");
				}
				else
				{
					base.ViewBag.OldLogonPass3 = text2 + lossReport.OldLogonPass3;
					stringBuilder.Append("绑定历史登录密码3（错误）<br/>");
				}
				dynamic viewBag = base.ViewBag;
				dateTime = lossReport.ReportDate;
				viewBag.ReportDate = dateTime.ToString();
				base.ViewBag.ReportIP = lossReport.ReportIP;
				base.ViewBag.ReportNo = lossReport.ReportNo;
				base.ViewBag.SuppInfo = lossReport.SuppInfo;
				string text3 = (accountsProtectByUserID != null) ? string.Format("<a href=\"#\" class=\"l\" onclick=\"javascript:openWindowOwn('AccountsProtectInfo.aspx?param={0}','{1}',660,320);\">已申请密码保护</a>", accountInfoByUserID.ProtectID, "密码保护") : "<font class=\"hong\">未申请密码保护</font>";
				base.ViewBag.PassProtect = text3;
				base.ViewBag.ProtectID = accountInfoByUserID.ProtectID;
				base.ViewBag.CardNum = accountInfoByUserID.PassPortID;
				string str = "<div class=\"reasonSS\"><strong>在您提供的17笔证明材料中:</strong><br/>其中," + (14 - num3 - num2) + "项（错误），" + num3 + "项（未填写），" + num2 + "项（正确）<br/>";
				str = str + stringBuilder.ToString() + "</div>";
				base.ViewBag.CheckInfo = str;
				WHCache.Default.Save<SessionCache>("reason", str, 5);
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult AccountsUpdatePass()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.UserID = num;
			return View();
		}

		[CheckCustomer]
		public ActionResult RecordAccountsExpendList()
		{
			switch (TypeUtil.ObjectToInt(base.Request["type"]))
			{
			case 0:
				base.ViewBag.Type = "帐号";
				break;
			case 1:
				base.ViewBag.Type = "昵称";
				break;
			}
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.UserID = num;
			return View();
		}

		[CheckCustomer]
		public ActionResult RecordPasswdExpendList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.UserID = num;
			return View();
		}

		[CheckCustomer]
		public ActionResult RecordPasswdExpendConfirm()
		{
			int num = TypeUtil.ObjectToInt(base.Request["type"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.IntType = num;
			base.ViewBag.RecordID = num2;
			RecordPasswdExpend recordPasswdExpendByRid = FacadeManage.aideRecordFacade.GetRecordPasswdExpendByRid(num2);
			if (recordPasswdExpendByRid != null)
			{
				AccountsInfo accountInfoByUserID = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(recordPasswdExpendByRid.UserID);
				if (accountInfoByUserID != null)
				{
					base.ViewBag.UserID = accountInfoByUserID.UserID.ToString();
					base.ViewBag.Accounts = accountInfoByUserID.Accounts.Trim();
				}
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult SendLossReportInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ReportId = num;
			return View();
		}

		[CheckCustomer]
		public ActionResult ShareDetailList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetAccounts()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["userType"]);
			StringBuilder stringBuilder = new StringBuilder(" WHERE IsAndroid=0 ");
			if (num == 0)
			{
				int num3 = TypeUtil.ObjectToInt(base.Request["IsLike"]);
				string safeSQL = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["KeyWord"]));
				if (!string.IsNullOrEmpty(safeSQL))
				{
					if (num3 == 1)
					{
						if (TypeUtil.IsInteger(safeSQL))
						{
							stringBuilder.AppendFormat(" AND (UserID ={0} OR GameID = {0} OR Accounts LIKE '%{0}%' OR NickName LIKE '%{0}%')", safeSQL);
						}
						else
						{
							stringBuilder.AppendFormat(" AND (Accounts LIKE '%{0}%' OR NickName LIKE '%{0}%')", safeSQL);
						}
					}
					else if (TypeUtil.IsInteger(safeSQL))
					{
						stringBuilder.AppendFormat(" AND (Accounts='{0}' OR NickName='{0}' OR UserID={0} OR GameID={0})", safeSQL);
					}
					else
					{
						stringBuilder.AppendFormat(" AND (Accounts='{0}' OR NickName='{0}')", safeSQL);
					}
				}
			}
			else
			{
				string text = TypeUtil.ObjectToString(base.Request["AccountStu"]);
				string s = TypeUtil.ObjectToString(base.Request["StartDate"]);
				string s2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
				string s3 = TypeUtil.ObjectToString(base.Request["LoStartDate"]);
				string s4 = TypeUtil.ObjectToString(base.Request["LoEndDate"]);
				string safeSQL2 = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["RegIP"]));
				string safeSQL3 = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["RegMachine"]));
				string safeSQL4 = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["LogIP"]));
				string safeSQL5 = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["LogMachine"]));
				string text2 = TypeUtil.ObjectToString(base.Request["Phone"]);
				string text3 = TypeUtil.ObjectToString(base.Request["TrueName"]);
				string text4 = TypeUtil.ObjectToString(base.Request["Agent"]);
				if (text2 != "")
				{
					stringBuilder.AppendFormat(" AND RegisterMobile = '{0}'", text2);
				}
				if (text3 != "")
				{
					stringBuilder.AppendFormat(" AND Compellation = '{0}'", text3);
				}
				DateTime result = DateTime.Now;
				DateTime result2 = DateTime.Now;
				if (DateTime.TryParse(s, out result))
				{
					stringBuilder.AppendFormat(" AND RegisterDate>='{0}'", result);
				}
				if (DateTime.TryParse(s2, out result2))
				{
					stringBuilder.AppendFormat(" AND RegisterDate<'{0}'", result2);
				}
				if (DateTime.TryParse(s3, out result))
				{
					stringBuilder.AppendFormat(" AND LastLogonDate>='{0}'", result);
				}
				if (DateTime.TryParse(s4, out result2))
				{
					stringBuilder.AppendFormat(" AND LastLogonDate<'{0}'", result2);
				}
				if (!string.IsNullOrEmpty(safeSQL2))
				{
					stringBuilder.AppendFormat(" AND RegisterIP = '{0}' ", safeSQL2);
				}
				if (!string.IsNullOrEmpty(safeSQL3))
				{
					stringBuilder.AppendFormat(" AND RegisterMachine = '{0}' ", safeSQL3);
				}
				if (!string.IsNullOrEmpty(safeSQL4))
				{
					stringBuilder.AppendFormat(" AND LastLogonIP = '{0}' ", safeSQL4);
				}
				if (!string.IsNullOrEmpty(safeSQL5))
				{
					stringBuilder.AppendFormat(" AND LastLogonMachine = '{0}' ", safeSQL5);
				}
				if (!string.IsNullOrWhiteSpace(text4))
				{
					stringBuilder.AppendFormat(" AND Superior like '%{0}%' ", text4);
				}
				if (!string.IsNullOrEmpty(text))
				{
					string[] array = text.Split(',');
					if (array.Count() > 0)
					{
						string[] array2 = array;
						foreach (string o in array2)
						{
							switch (TypeUtil.ObjectToInt(o))
							{
							case 3:
								stringBuilder.Append(" AND Nullity = 1 ");
								break;
							}
						}
					}
				}
			}
			if (num2 > 0)
			{
				stringBuilder.AppendFormat(" AND UserType = {0} ", num2);
			}
			string orderby = "ORDER BY RegisterDate DESC";
			PagerSet list = FacadeManage.aideAccountsFacade.GetList("View_UserInfo", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			DataTable o2 = list.PageSet.Tables[0];
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(o2)
			});
		}

		[CheckCustomer]
		public JsonResult GetMemberList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int num2 = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int userID = TypeUtil.ObjectToInt(base.Request["param"]);
			AccountsInfo accountInfoByUserID = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(userID);
			if (accountInfoByUserID != null)
			{
				StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
				stringBuilder.Append(" AND UserID= " + userID.ToString());
				stringBuilder.Append(" AND MemberOverDate > '" + DateTime.Now.ToString() + "'");
				PagerSet list = FacadeManage.aideAccountsFacade.GetList("AccountsMember", num, num2, stringBuilder.ToString(), "ORDER BY MemberOverDate DESC");
				List<object> list2 = new List<object>();
				if (list.PageSet != null && list.PageSet.Tables.Count > 0)
				{
					for (int i = 0; i < list.PageSet.Tables[0].Rows.Count; i++)
					{
						DataRow dataRow = list.PageSet.Tables[0].Rows[i];
						list2.Add(new
						{
							SNO = num2 * (num - 1) + (i + 1),
							MemberName = TypeUtil.GetMemberName((byte)dataRow["MemberOrder"]),
							MemberOverDate = dataRow["MemberOverDate"].ToString()
						});
					}
				}
				return Json(new
				{
					IsOk = true,
					Msg = "操作成功",
					Total = list.RecordCount,
					Data = JsonConvert.SerializeObject(list2)
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "没有任何信息",
				Total = 0,
				Data = ""
			});
		}

		[CheckCustomer]
		public JsonResult AddNewAccount(AccountsEntity entity)
		{
			if (user != null && !TypeUtil.IsPower(user.MoudleID, user.RoleID, "添加"))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有权限",
					Url = "/NoPower/Index"
				});
			}
			if (entity != null)
			{
				string text = TextFilter.FilterAll(entity.Accounts);
				string text2 = TextFilter.FilterAll(entity.NickName);
				int num = Encoding.Default.GetBytes(text).Length;
				if (num > 32 || num < 6)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "游戏帐号的长度为6-32位，中文算两位"
					});
				}
				if (string.IsNullOrEmpty(text2))
				{
					text2 = text;
				}
				else
				{
					int num2 = Encoding.Default.GetBytes(text2).Length;
					if (num2 > 32 || num2 < 6)
					{
						return Json(new
						{
							IsOk = false,
							Msg = "昵称的的长度为6-32位，中文算两位"
						});
					}
				}
				AccountsInfo accountsInfo = new AccountsInfo();
				IndividualDatum individualDatum = new IndividualDatum();
				accountsInfo.Accounts = text;
				accountsInfo.NickName = text2;
				accountsInfo.LogonPass = Utility.MD5(entity.LogonPass);
				accountsInfo.InsurePass = (string.IsNullOrEmpty(entity.InsurePass) ? Utility.MD5(entity.LogonPass) : Utility.MD5(entity.InsurePass));
				accountsInfo.DynamicPass = TextUtility.CreateRandom(32, 1, 1, 1, 0, "");
				accountsInfo.UnderWrite = TextFilter.FilterAll((entity.UnderWrite == null) ? "" : entity.UnderWrite);
				accountsInfo.Present = entity.Present;
				accountsInfo.LoveLiness = entity.LoveLiness;
				accountsInfo.Experience = entity.Experience;
				accountsInfo.Gender = entity.Gender;
				accountsInfo.FaceID = entity.FaceID;
				accountsInfo.Nullity = entity.Nullity;
				accountsInfo.StunDown = entity.StunDown;
				accountsInfo.MoorMachine = entity.MoorMachine;
				accountsInfo.IsAndroid = entity.IsAndroid;
				accountsInfo.UserRight = entity.UserRight;
				accountsInfo.MasterRight = entity.MasterRight;
				accountsInfo.MasterOrder = entity.MasterOrder;
				accountsInfo.Compellation = TextFilter.FilterAll((entity.Compellation == null) ? "" : entity.Compellation);
				accountsInfo.MemberOrder = entity.MemberOrder;
				accountsInfo.MemberOverDate = DateTime.Now;
				accountsInfo.MemberSwitchDate = DateTime.Now;
				accountsInfo.RegisterIP = GameRequest.GetUserIP();
				individualDatum.QQ = TextFilter.FilterAll((entity.QQ == null) ? "" : entity.QQ);
				individualDatum.EMail = TextFilter.FilterAll((entity.EMail == null) ? "" : entity.EMail);
				individualDatum.SeatPhone = TextFilter.FilterAll((entity.SeatPhone == null) ? "" : entity.SeatPhone);
				individualDatum.MobilePhone = TextFilter.FilterAll((entity.MobilePhone == null) ? "" : entity.MobilePhone);
				individualDatum.PostalCode = TextFilter.FilterAll((entity.PostalCode == null) ? "" : entity.PostalCode);
				individualDatum.DwellingPlace = TextFilter.FilterAll((entity.DwellingPlace == null) ? "" : entity.DwellingPlace);
				individualDatum.UserNote = TextFilter.FilterAll((entity.UserNote == null) ? "" : entity.UserNote);
				Message message = FacadeManage.aideAccountsFacade.AddAccount(accountsInfo, individualDatum);
				if (message.Success)
				{
					return Json(new
					{
						IsOk = true,
						Msg = message.Content
					});
				}
				return Json(new
				{
					IsOk = false,
					Msg = message.Content
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "参数为空"
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult UpdateNewAccount(AccountsEntity entity)
		{
			if (entity != null)
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
				string text = TextFilter.FilterAll(entity.Accounts);
				string text2 = TextFilter.FilterAll(entity.NickName);
				int num = Encoding.Default.GetBytes(text).Length;
				if (num > 32 || num < 6)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "游戏帐号的长度为6-32位，中文算两位"
					});
				}
				if (string.IsNullOrEmpty(text2))
				{
					text2 = text;
				}
				else
				{
					int num2 = Encoding.Default.GetBytes(text2).Length;
					if (num2 > 32 || num2 < 6)
					{
						return Json(new
						{
							IsOk = false,
							Msg = "昵称的的长度为6-32位，中文算两位"
						});
					}
				}
				if (!string.IsNullOrEmpty(entity.LogonPass) && (entity.LogonPass.Length > 32 || entity.LogonPass.Length < 6))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "登陆密码的长度为6-32个字符"
					});
				}
				if (!string.IsNullOrEmpty(entity.InsurePass) && (entity.InsurePass.Length > 32 || entity.InsurePass.Length < 6))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "保险柜密码的长度为6-32个字符"
					});
				}
				AccountsInfo accountsInfo = new AccountsInfo();
				accountsInfo = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(entity.UserID);
				if (accountsInfo == null)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有找到该账户"
					});
				}
				string account = accountsInfo.Accounts;
				string nickName = accountsInfo.NickName;
				string logonPass = accountsInfo.LogonPass;
				string insurePass = accountsInfo.InsurePass;
				accountsInfo.Accounts = text;
				accountsInfo.NickName = text2;
				accountsInfo.LogonPass = (string.IsNullOrEmpty(entity.LogonPass) ? accountsInfo.LogonPass : Utility.MD5(entity.LogonPass));
				accountsInfo.InsurePass = (string.IsNullOrEmpty(entity.InsurePass) ? accountsInfo.InsurePass : Utility.MD5(entity.InsurePass));
				accountsInfo.UnderWrite = ((entity.UnderWrite == null) ? "" : entity.UnderWrite);
				accountsInfo.Experience = entity.Experience;
				accountsInfo.Present = entity.Present;
				accountsInfo.LoveLiness = entity.LoveLiness;
				accountsInfo.Gender = entity.Gender;
				accountsInfo.FaceID = entity.FaceID;
				accountsInfo.Nullity = entity.Nullity;
				accountsInfo.StunDown = entity.StunDown;
				accountsInfo.MoorMachine = entity.MoorMachine;
				accountsInfo.IsAndroid = entity.IsAndroid;
				accountsInfo.UserRight = entity.UserRight;
				accountsInfo.MasterRight = entity.MasterRight;
				accountsInfo.MasterOrder = entity.MasterOrder;
				accountsInfo.RegisterMobile = ((entity.MobilePhone == null) ? "" : entity.MobilePhone);
				int faceType = entity.FaceType;
				if (faceType == 1)
				{
					accountsInfo.FaceID = entity.FaceID;
					accountsInfo.CustomID = 0;
				}
				else
				{
					accountsInfo.CustomID = entity.FaceID;
				}
				accountsInfo.UserType = entity.UserType;
				Message message = new Message();
				message = FacadeManage.aideAccountsFacade.UpdateAccount(accountsInfo, user.UserID, GameRequest.GetUserIP());
				if (message.Success)
				{
					return Json(new
					{
						IsOk = true,
						Msg = message.Content
					});
				}
				return Json(new
				{
					IsOk = false,
					Msg = "操作成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "参数为空"
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult UpdateIndividualDatum()
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
			int num = TypeUtil.ObjectToInt(base.Request["UserID"]);
			TypeUtil.ObjectToString(base.Request["Accounts"]);
			string value = TypeUtil.ObjectToString(base.Request["QQ"]);
			string value2 = TypeUtil.ObjectToString(base.Request["EMail"]);
			string value3 = TypeUtil.ObjectToString(base.Request["SeatPhone"]);
			string value4 = TypeUtil.ObjectToString(base.Request["MobilePhone"]);
			string value5 = TypeUtil.ObjectToString(base.Request["DwellingPlace"]);
			string value6 = TypeUtil.ObjectToString(base.Request["UserNote"]);
			string value7 = TypeUtil.ObjectToString(base.Request["Compellation"]);
			string value8 = TypeUtil.ObjectToString(base.Request["BankNO"]);
			string value9 = TypeUtil.ObjectToString(base.Request["BankName"]);
			string value10 = TypeUtil.ObjectToString(base.Request["BankAddress"]);
			string value11 = TypeUtil.ObjectToString(base.Request["PostalCode"]);
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["dwUserID"] = num;
			dictionary["strQQ"] = value;
			dictionary["strEMail"] = value2;
			dictionary["strSeatPhone"] = value3;
			dictionary["strMobilePhone"] = value4;
			dictionary["strPostalCode"] = value11;
			dictionary["strDwellingPlace"] = value5;
			dictionary["strUserNote"] = value6;
			dictionary["strCompellation"] = value7;
			dictionary["strBankNO"] = value8;
			dictionary["strBankName"] = value9;
			dictionary["strBankAddress"] = value10;
			dictionary["operator"] = user.Username;
			dictionary["strClientIP"] = GameRequest.GetUserIP();
			dictionary["strErr"] = "";
			Message message = FacadeManage.aideAccountsFacade.ExcuteByProce("P_EditPlayerDetail", dictionary);
			if (message.Success)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "信息修改成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = message.Content
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DissolveTable()
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
			if (user != null && !TypeUtil.IsPower(user.MoudleID, user.RoleID, "冻/解"))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有权限",
					Url = "/NoPower/Index"
				});
			}
			string[] array = text.Trim().Split(',');
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			string text2 = "";
			bool flag = true;
			RequestMessage requestMessage = new RequestMessage(1);
			string[] array2 = array;
			foreach (string text3 in array2)
			{
				int result = 0;
				if (int.TryParse(text3, out result))
				{
					stringBuilder.Append(text3 + ",");
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					dictionary["dwUserID"] = text3;
					dictionary["Operator"] = user.Username;
					dictionary["strClientIP"] = GameRequest.GetUserIP();
					dictionary["strErr"] = "";
					Message message = FacadeManage.aideAccountsFacade.ExcuteByProce("RYTreasureDB..P_UnlockPlayer", dictionary);
					if (message.Success)
					{
						if (flag)
						{
							requestMessage.AddDataItem("userid", result);
							requestMessage.AddDataItem("serverid", num);
							flag = false;
						}
						else
						{
							requestMessage.SetDataItem("userid", result);
							requestMessage.SetDataItem("serverid", num);
						}
						string text4 = requestMessage.Post();
						text2 = ((!text4.Contains("OK")) ? (text2 + text4 + "\n") : (text2 + text3 + "解散成功\n"));
					}
					else
					{
						text2 = text2 + message.Content + "\n";
					}
				}
			}
			if (string.IsNullOrEmpty(stringBuilder.ToString()))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数不正确"
				});
			}
			return Json(new
			{
				IsOk = true,
				Msg = text2
			});
		}

		[CheckCustomer]
		public JsonResult JiechuDaili()
		{
			if (!TypeUtil.IsPower(user.MoudleID, user.RoleID, "冻/解"))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有权限",
					Url = "/NoPower/Index"
				});
			}
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			if (text == "")
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有选择操作项"
				});
			}
			string[] array = text.Split(',');
			int result = 0;
			string text2 = "";
			string[] array2 = array;
			foreach (string s in array2)
			{
				if (int.TryParse(s, out result))
				{
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					dictionary["dwUserID"] = result;
					dictionary["strAdmin"] = user.Username;
					dictionary["strClientIP"] = GameRequest.GetUserIP();
					dictionary["strErr"] = "";
					Message message = FacadeManage.aideAccountsFacade.ExcuteByProce("RYAgentDB.dbo.P_UnrelAgentUser", dictionary);
					text2 = ((!message.Success) ? (text2 + result + "解除失败\n") : (text2 + result + "解除成功\n"));
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = text2
			});
		}

		public JsonResult SetSite()
		{
			int formInt = GameRequest.GetFormInt("totalPay", 0);
			int formInt2 = GameRequest.GetFormInt("totalWin", 0);
			int formInt3 = GameRequest.GetFormInt("totalWinMax", 0);
			if (formInt != 0 && formInt2 != 0)
			{
				XmlDocument xmlDocument = new XmlDocument();
				string filename = base.Server.MapPath("/App_Data/WebSite.xml");
				try
				{
					xmlDocument.Load(filename);
					xmlDocument.SelectSingleNode("/root/TotalPay").InnerText = formInt.ToString();
					xmlDocument.SelectSingleNode("/root/TotalWin").InnerText = formInt2.ToString();
					xmlDocument.SelectSingleNode("/root/TotalWinMax").InnerText = formInt3.ToString();
					xmlDocument.Save(filename);
					HttpRuntime.Cache.Remove("website");
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
				Msg = "输入错误"
			});
		}

		[CheckCustomer]
		public JsonResult SetTeshu()
		{
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			int type = TypeUtil.ObjectToInt(base.Request["type"]);
			if (text == "")
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有选择操作项"
				});
			}
			if (CheckHelper.CheckIds(text))
			{
				string sqlQuery = " WHERE UserID in (" + text + ")";
				try
				{
					FacadeManage.aideAccountsFacade.SetTeshu(type, sqlQuery);
					return Json(new
					{
						IsOk = true,
						Msg = "操作成功"
					});
				}
				catch
				{
					return Json(new
					{
						IsOk = false,
						Msg = "操作失败"
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "参数不正确"
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult TiDT()
		{
			string value = TypeUtil.ObjectToString(base.Request["cid"]);
			if (string.IsNullOrEmpty(value))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有选择操作项"
				});
			}
			if (user != null && !TypeUtil.IsPower(user.MoudleID, user.RoleID, "冻/解"))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有权限",
					Url = "/NoPower/Index"
				});
			}
			string text = "";
			RequestMessage requestMessage = new RequestMessage(6);
			requestMessage.AddDataItem("userid", value);
			string text2 = requestMessage.Post();
			text = ((!text2.Contains("OK")) ? text2 : "操作成功");
			return Json(new
			{
				IsOk = true,
				Msg = text
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult FreezeAccount()
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
			if (user != null && !TypeUtil.IsPower(user.MoudleID, user.RoleID, "冻/解"))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有权限",
					Url = "/NoPower/Index"
				});
			}
			string[] array = text.Trim().Split(',');
			StringBuilder stringBuilder = new StringBuilder();
			string[] array2 = array;
			foreach (string text2 in array2)
			{
				int result = 0;
				if (int.TryParse(text2, out result))
				{
					stringBuilder.Append(text2 + ",");
				}
			}
			if (!string.IsNullOrEmpty(stringBuilder.ToString()))
			{
				string sqlQuery = "WHERE UserID in (" + stringBuilder.ToString().TrimEnd(',') + ")";
				try
				{
					FacadeManage.aideAccountsFacade.DongjieAccount(sqlQuery);
					return Json(new
					{
						IsOk = true,
						Msg = "冻结成功"
					});
				}
				catch
				{
					return Json(new
					{
						IsOk = false,
						Msg = "冻结失败"
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "参数不正确"
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult UnfreezeAccount()
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
			if (user != null && !TypeUtil.IsPower(user.MoudleID, user.RoleID, "冻/解"))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有权限",
					Url = "/NoPower/Index"
				});
			}
			string[] array = text.Trim().Split(',');
			StringBuilder stringBuilder = new StringBuilder();
			string[] array2 = array;
			foreach (string text2 in array2)
			{
				int result = 0;
				if (int.TryParse(text2, out result))
				{
					stringBuilder.Append(text2 + ",");
				}
			}
			if (!string.IsNullOrEmpty(stringBuilder.ToString()))
			{
				string sqlQuery = "WHERE UserID in (" + stringBuilder.ToString().TrimEnd(',') + ")";
				try
				{
					FacadeManage.aideAccountsFacade.JieDongAccount(sqlQuery);
					return Json(new
					{
						IsOk = true,
						Msg = "解冻成功"
					});
				}
				catch
				{
					return Json(new
					{
						IsOk = false,
						Msg = "解冻失败"
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "参数不正确"
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult SettingAndroid()
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
			if (user != null && !TypeUtil.IsPower(user.MoudleID, user.RoleID, "设置/取消机器人"))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有权限",
					Url = "/NoPower/Index"
				});
			}
			string[] array = text.Trim().Split(',');
			StringBuilder stringBuilder = new StringBuilder();
			string[] array2 = array;
			foreach (string text2 in array2)
			{
				int result = 0;
				if (int.TryParse(text2, out result))
				{
					stringBuilder.Append(text2 + ",");
				}
			}
			if (!string.IsNullOrEmpty(stringBuilder.ToString()))
			{
				string sqlQuery = "WHERE UserID in (" + stringBuilder.ToString().TrimEnd(',') + ")";
				try
				{
					FacadeManage.aideAccountsFacade.SettingAndroid(sqlQuery);
					return Json(new
					{
						IsOk = true,
						Msg = "解冻成功"
					});
				}
				catch
				{
					return Json(new
					{
						IsOk = false,
						Msg = "解冻失败"
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "参数不正确"
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult CancleAndroid()
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
			if (user != null && !TypeUtil.IsPower(user.MoudleID, user.RoleID, "设置/取消机器人"))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有权限",
					Url = "/NoPower/Index"
				});
			}
			string[] array = text.Trim().Split(',');
			StringBuilder stringBuilder = new StringBuilder();
			string[] array2 = array;
			foreach (string text2 in array2)
			{
				int result = 0;
				if (int.TryParse(text2, out result))
				{
					stringBuilder.Append(text2 + ",");
				}
			}
			if (!string.IsNullOrEmpty(stringBuilder.ToString()))
			{
				string sqlQuery = "WHERE UserID in (" + stringBuilder.ToString().TrimEnd(',') + ")";
				try
				{
					FacadeManage.aideAccountsFacade.CancleAndroid(sqlQuery);
					return Json(new
					{
						IsOk = true,
						Msg = "解冻成功"
					});
				}
				catch
				{
					return Json(new
					{
						IsOk = false,
						Msg = "解冻失败"
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "参数不正确"
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult ExcuteClearScore()
		{
			if (user != null && !TypeUtil.IsPower(user.MoudleID, user.UserID, "清零积分"))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有权限",
					Url = "/NoPower/Index"
				});
			}
			string text = TypeUtil.ObjectToString(base.Request["Reason"]);
			int num = TypeUtil.ObjectToInt(base.Request["kindID"]);
			string text2 = TypeUtil.ObjectToString(base.Request["Param"]);
			if (num <= 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请选择游戏"
				});
			}
			if (string.IsNullOrEmpty(text))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "清零原因不能为空"
				});
			}
			AccountsInfo accountsInfo = new AccountsInfo();
			string[] array = text2.Split(',');
			int num2 = 0;
			string[] array2 = array;
			foreach (string s in array2)
			{
				int result = 0;
				if (int.TryParse(s, out result))
				{
					accountsInfo = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(int.Parse(s));
					if (accountsInfo != null)
					{
						TreasureFacade treasureFacade = new TreasureFacade(num);
						Message message = treasureFacade.GrantClearScore(int.Parse(s), num, user.UserID, text, GameRequest.GetUserIP());
						if (message.Success)
						{
							num2++;
						}
					}
				}
			}
			if (num2 > 0)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "所选用户共有" + num2 + "个用户积分为负，全部清除成功！"
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
		public JsonResult ExcuteScore()
		{
			if (user != null && TypeUtil.IsPower(user.MoudleID, user.RoleID, "赠送积分"))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有权限",
					Url = "/NoPower/Index"
				});
			}
			int num = TypeUtil.ObjectToInt(base.Request["Score"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["kindID"]);
			string text = TypeUtil.ObjectToString(base.Request["Param"]);
			string text2 = TypeUtil.ObjectToString(base.Request["Reason"]);
			if (num2 <= 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请选择游戏"
				});
			}
			if (num == 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "赠送积分不能为零"
				});
			}
			if (string.IsNullOrEmpty(text2))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "赠送原因不能为空"
				});
			}
			AccountsInfo accountsInfo = new AccountsInfo();
			string[] array = text.Split(',');
			int num3 = 0;
			string[] array2 = array;
			foreach (string s in array2)
			{
				int result = 0;
				if (int.TryParse(s, out result))
				{
					accountsInfo = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(int.Parse(s));
					if (accountsInfo != null)
					{
						TreasureFacade treasureFacade = new TreasureFacade(num2);
						Message message = treasureFacade.GrantScore(int.Parse(s), num2, num, user.UserID, text2, GameRequest.GetUserIP());
						if (message.Success)
						{
							num3++;
						}
					}
				}
			}
			if (num3 > 0)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "所选用户共有" + num3 + "个用户赠送积分为" + num + "，全部赠送成功！"
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
		public JsonResult ExcuteExperience()
		{
			if (user != null && !TypeUtil.IsPower(user.MoudleID, user.RoleID, "赠送经验"))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有权限",
					Url = "/NoPower/Index"
				});
			}
			int num = TypeUtil.ObjectToInt(base.Request["Experience"]);
			string text = TypeUtil.ObjectToString(base.Request["Param"]);
			string text2 = TypeUtil.ObjectToString(base.Request["Reason"]);
			if (num == 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "赠送经验数目不能为零"
				});
			}
			if (string.IsNullOrEmpty(text2))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "赠送原因不能为空"
				});
			}
			RecordGrantExperience recordGrantExperience = new RecordGrantExperience();
			AccountsInfo accountsInfo = new AccountsInfo();
			recordGrantExperience.ClientIP = GameRequest.GetUserIP();
			recordGrantExperience.MasterID = user.UserID;
			recordGrantExperience.AddExperience = num;
			recordGrantExperience.Reason = text2;
			string[] array = text.Split(',');
			int num2 = 0;
			string[] array2 = array;
			foreach (string s in array2)
			{
				int result = 0;
				if (int.TryParse(s, out result))
				{
					recordGrantExperience.UserID = int.Parse(s);
					recordGrantExperience.CurExperience = FacadeManage.aideAccountsFacade.GetExperienceByUserID(int.Parse(s));
					accountsInfo = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(int.Parse(s));
					if (accountsInfo != null)
					{
						accountsInfo.Experience = recordGrantExperience.CurExperience + num;
						Message message = FacadeManage.aideAccountsFacade.UpdateAccount(accountsInfo, user.UserID, GameRequest.GetUserIP());
						FacadeManage.aideRecordFacade.InsertRecordGrantExperience(recordGrantExperience);
						if (message.Success)
						{
							num2++;
						}
					}
				}
			}
			if (num2 > 0)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "所选用户共有" + num2 + "个用户赠送经验数为" + num + "，全部赠送成功！"
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
		public JsonResult UpdateIpAddress()
		{
			string text = TypeUtil.ObjectToString(base.Request["Param"]);
			string value = TypeUtil.ObjectToString(base.Request["IpAddress"]);
			if (text == "")
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数非法"
				});
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["dwUserIDs"] = text;
			dictionary["strClientIP"] = value;
			dictionary["strErr"] = "";
			Message message = FacadeManage.aideAccountsFacade.ExcuteByProce("P_ChangeIP", dictionary);
			if (message.Success)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "修改成功，重新登录游戏生效"
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
		public JsonResult ExcuteTreasure()
		{
			if (user != null && !TypeUtil.IsPower(user.MoudleID, user.RoleID, "赠送金币"))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有权限",
					Url = "/NoPower/Index"
				});
			}
			decimal num = TypeUtil.ObjectToDecimal(base.Request["Gold"]);
			string strUserIdList = TypeUtil.ObjectToString(base.Request["Param"]);
			string text = TypeUtil.ObjectToString(base.Request["Reason"]);
			if (num == 0m)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "赠送送游戏币不能为零"
				});
			}
			if (string.IsNullOrEmpty(text))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "赠送原因不能为空"
				});
			}
			int num2 = TypeUtil.ObjectToInt(base.Request["Type"]);
			if (num2 == 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请选择类型"
				});
			}
			Message message = FacadeManage.aideTreasureFacade.GrantTreasure(num2, strUserIdList, num, user.UserID, text, GameRequest.GetUserIP());
			if (message.Success)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "确认成功"
				});
			}
			string text2 = "";
			switch (message.MessageID)
			{
			case -4:
				text2 = "选中的玩家正在游戏，不能扣除！";
				break;
			case -3:
				text2 = "未选中赠送对象！";
				break;
			case -2:
				text2 = "赠送金额不能为零！";
				break;
			case -1:
				text2 = "抱歉，未知服务器错误！";
				break;
			default:
				text2 = "抱歉，未知服务器错误！";
				break;
			}
			return Json(new
			{
				IsOk = false,
				Msg = text2
			});
		}

        

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult ExcuteMember()
		{
			if (!TypeUtil.IsPower(user.MoudleID, user.UserID, "赠送会员"))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有权限",
					Url = "/NoPower/Index"
				});
			}
			int num = TypeUtil.ObjectToInt(base.Request["MemberType"]);
			string text = TypeUtil.ObjectToString(base.Request["Param"]);
			string text2 = TypeUtil.ObjectToString(base.Request["Reason"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["MemberDays"]);
			if (num == 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "会员类别不能为空"
				});
			}
			if (num2 < 1)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "赠送天数必须为大于零的正整数！"
				});
			}
			if (string.IsNullOrEmpty(text2))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "赠送原因不能为空"
				});
			}
			AccountsInfo accountsInfo = new AccountsInfo();
			string[] array = text.Split(',');
			string[] array2 = array;
			foreach (string s in array2)
			{
				int result = 0;
				if (int.TryParse(s, out result))
				{
					accountsInfo = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(int.Parse(s));
					if (accountsInfo != null)
					{
						FacadeManage.aideRecordFacade.GrantMember(int.Parse(s), num, num2, user.UserID, text2, GameRequest.GetUserIP());
					}
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "操作成功"
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult ExcuteFlee()
		{
			if (user != null && TypeUtil.IsPower(user.MoudleID, user.RoleID, "清零逃率"))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有权限",
					Url = "/NoPower/Index"
				});
			}
			int num = TypeUtil.ObjectToInt(base.Request["kindID"]);
			string text = TypeUtil.ObjectToString(base.Request["Param"]);
			string text2 = TypeUtil.ObjectToString(base.Request["Reason"]);
			if (num <= 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请选择游戏"
				});
			}
			if (string.IsNullOrEmpty(text2))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "赠送原因不能为空"
				});
			}
			AccountsInfo accountsInfo = new AccountsInfo();
			string[] array = text.Split(',');
			int num2 = 0;
			string[] array2 = array;
			foreach (string s in array2)
			{
				int result = 0;
				if (int.TryParse(s, out result))
				{
					accountsInfo = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(int.Parse(s));
					if (accountsInfo != null)
					{
						TreasureFacade treasureFacade = new TreasureFacade(num);
						Message message = treasureFacade.GrantFlee(int.Parse(s), num, user.UserID, text2, GameRequest.GetUserIP());
						if (message.Success)
						{
							num2++;
						}
					}
				}
			}
			if (num2 > 0)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "所选用户共有" + num2 + "个用户逃跑记录，全部清除成功！"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "所选用户没有有逃跑记录的用户"
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult ExcuteGameID()
		{
			if (user != null && !TypeUtil.IsPower(user.MoudleID, user.RoleID, "赠送靓号"))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有权限",
					Url = "/NoPower/Index"
				});
			}
			int num = TypeUtil.ObjectToInt(base.Request["gameID"]);
			string o = TypeUtil.ObjectToString(base.Request["Param"]);
			string text = TypeUtil.ObjectToString(base.Request["Reason"]);
			if (num <= 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "靓号ID不正确"
				});
			}
			if (string.IsNullOrEmpty(text))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "赠送原因不能为空"
				});
			}
			int userID = TypeUtil.ObjectToInt(o);
			Message message = FacadeManage.aideRecordFacade.GrantGameID(userID, num, user.UserID, text, GameRequest.GetUserIP());
			if (message.Success)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "操作成功"
				});
			}
			string text2 = "";
			switch (message.MessageID)
			{
			case 2:
				text2 = "抱歉，赠送的靓号已被使用，请更换！";
				break;
			case 1:
				text2 = "抱歉，赠送的靓号不存在！";
				break;
			default:
				text2 = "抱歉，未知服务器错误！";
				break;
			}
			return Json(new
			{
				IsOk = false,
				Msg = text2
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult ExcutePasswordCard()
		{
			string o = TypeUtil.ObjectToString(base.Request["Param"]);
			int num = TypeUtil.ObjectToInt(o);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("UPDATE {0} SET PasswordID = '0' WHERE PasswordID != '0' AND UserID = {1}", "AccountsInfo", num);
			int num2 = FacadeManage.aideAccountsFacade.ExecuteSql(stringBuilder.ToString());
			if (num2 > 0)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "取消密保卡绑定成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "取消失败"
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult ExcuteProtectInfo(AccountsProtectEntity entity)
		{
			if (entity != null)
			{
				AccountsProtect accountsProtect = new AccountsProtect();
				accountsProtect.ProtectID = entity.ProtectID;
				accountsProtect.Question1 = entity.Question1;
				accountsProtect.Question2 = entity.Question2;
				accountsProtect.Question3 = entity.Question3;
				accountsProtect.Response1 = entity.Response1;
				accountsProtect.Response2 = entity.Response2;
				accountsProtect.Response3 = entity.Response3;
				accountsProtect.SafeEmail = entity.SafeEmail;
				Message message = new Message();
				message = FacadeManage.aideAccountsFacade.UpdateAccountsProtect(accountsProtect);
				if (message.Success)
				{
					return Json(new
					{
						IsOk = true,
						Msg = "密保信息修改成功"
					});
				}
				return Json(new
				{
					IsOk = false,
					Msg = message.Content
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "参数为空"
			});
		}

		[CheckCustomer]
		public JsonResult ExcuteDelProtectInfo()
		{
			int pid = TypeUtil.ObjectToInt(base.Request["ProtectID"]);
			FacadeManage.aideAccountsFacade.DeleteAccountsProtect(pid);
			return Json(new
			{
				IsOk = true,
				Msg = "操作成功"
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetAccountsGoldList()
		{
			if (user != null && !TypeUtil.IsPower(user.MoudleID, user.RoleID, "查看"))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有权限",
					Url = "/NoPower/Index"
				});
			}
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			string text = TypeUtil.ObjectToString(base.Request["Sort"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["SortDrc"]);
			string text2 = "";
			text2 = ((!string.IsNullOrEmpty(text)) ? ("ORDER BY " + text + " " + ((num2 == 0) ? "DESC" : "ASC")) : "ORDER BY UserID DESC");
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			string text3 = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			if (!string.IsNullOrEmpty(text3))
			{
				int result = 0;
				if (num > 1 && !int.TryParse(text3, out result))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "查询格式错误"
					});
				}
				switch (num)
				{
				case 1:
					stringBuilder.AppendFormat(" AND Accounts='{0}'", text3);
					break;
				case 2:
					stringBuilder.AppendFormat(" AND GameID={0}", result);
					break;
				case 3:
					stringBuilder.AppendFormat(" AND Score>={0}", result);
					break;
				case 4:
					stringBuilder.AppendFormat(" AND Score<={0}", result);
					break;
				case 5:
					stringBuilder.AppendFormat(" AND InsureScore>={0}", result);
					break;
				case 6:
					stringBuilder.AppendFormat(" AND InsureScore<={0}", result);
					break;
				case 7:
					stringBuilder.AppendFormat(" AND Revenue>={0}", result);
					break;
				case 8:
					stringBuilder.AppendFormat(" AND Revenue<={0}", result);
					break;
				}
			}
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("View_GameScoreInfo", pageIndex, pageSize, stringBuilder.ToString(), text2);
			List<object> list2 = new List<object>();
			if (list != null && list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in list.PageSet.Tables[0].Rows)
				{
					list2.Add(new
					{
						UserID = TypeUtil.ObjectToInt(row["UserID"]),
						GameID = row["GameID"],
						Accounts = row["Accounts"],
						NickName = row["NickName"],
						Score = TypeUtil.ObjectToDecimal(row["Score"]).ToString("N"),
						InsureScore = TypeUtil.ObjectToDecimal(row["InsureScore"]).ToString("N"),
						TotalScore = (TypeUtil.ObjectToDecimal(row["Score"]) + TypeUtil.ObjectToDecimal(row["InsureScore"])).ToString("N"),
						UserMedal = row["UserMedal"],
						Present = row["Present"],
						LoveLiness = row["LoveLiness"],
						Experience = row["LoveLiness"],
						Revenue = TypeUtil.ObjectToDecimal(row["Revenue"]).ToString("N"),
						WinCount = TypeUtil.ObjectToInt(row["WinCount"]),
						LostCount = TypeUtil.ObjectToInt(row["LostCount"]),
						DrawCount = TypeUtil.ObjectToInt(row["DrawCount"]),
						FleeCount = TypeUtil.ObjectToInt(row["FleeCount"]),
						TotalCount = (TypeUtil.ObjectToInt(row["WinCount"]) + TypeUtil.ObjectToInt(row["LostCount"]) + TypeUtil.ObjectToInt(row["DrawCount"]) + TypeUtil.ObjectToInt(row["FleeCount"])).ToString(),
						AllLogonTimes = TypeUtil.ObjectToString(row["AllLogonTimes"]),
						PlayTimeCount = TypeUtil.ObjectToString(row["PlayTimeCount"]),
						OnLineTimeCount = TypeUtil.ObjectToString(row["OnLineTimeCount"]),
						LastLogonDate = TypeUtil.ObjectToString(row["LastLogonDate"]),
						Address = TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(row["LastLogonIP"])),
						LastLogonIP = TypeUtil.ObjectToString(row["LastLogonIP"])
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

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetJiechuDailiList()
		{
			if (user != null && !TypeUtil.IsPower(user.MoudleID, user.RoleID, "查看"))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有权限",
					Url = "/NoPower/Index"
				});
			}
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			string text = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			if (text != "")
			{
				stringBuilder.AppendFormat(" AND Accounts='{0}'", text);
			}
			string text2 = TypeUtil.ObjectToString(base.Request["AgentAcc"]);
			if (text2 != "")
			{
				stringBuilder.AppendFormat(" AND AgentAcc='{0}'", text2);
			}
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("RYAgentDB.dbo.View_AgentUnrelUser", pageIndex, pageSize, stringBuilder.ToString(), "Order By UnRelTime DESC");
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetRecordUserInoutList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["ID"]);
			string orderby = "ORDER BY EnterTime DESC";
			int num3 = TypeUtil.ObjectToInt(base.Request["op"]);
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			string text = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string text2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			stringBuilder.AppendFormat(" AND UserID={0}", num2);
			switch (num)
			{
			case 1:
				text = Fetch.GetTodayTime().Split('$')[0].ToString();
				text2 = Fetch.GetTodayTime().Split('$')[1].ToString();
				break;
			case 2:
				text = Fetch.GetYesterdayTime().Split('$')[0].ToString();
				text2 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
				break;
			case 3:
				text = Fetch.GetWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetWeekTime().Split('$')[1].ToString();
				break;
			case 4:
				text = Fetch.GetLastWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
				break;
			}
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
			{
				if (DateTime.TryParse(text, out result))
				{
					switch (num3)
					{
					case 1:
						stringBuilder.AppendFormat(" AND EnterTime>='{0}'", result);
						break;
					case 2:
						stringBuilder.AppendFormat(" AND LeaveTime>='{0}'", result);
						break;
					case 3:
						stringBuilder.AppendFormat(" AND EnterTime<='{0}'", result);
						break;
					default:
						stringBuilder.AppendFormat(" AND EnterTime>='{0}'", result);
						break;
					}
				}
				if (DateTime.TryParse(text2, out result2))
				{
					switch (num3)
					{
					case 1:
						stringBuilder.AppendFormat(" AND EnterTime<'{0}'", result2);
						break;
					case 2:
						stringBuilder.AppendFormat(" AND LeaveTime<'{0}'", result2);
						break;
					case 3:
						stringBuilder.AppendFormat(" AND LeaveTime<='{0}'", result2);
						break;
					default:
						stringBuilder.AppendFormat(" AND EnterTime<'{0}'", result2);
						break;
					}
				}
			}
			List<object> list = new List<object>();
			PagerSet recordUserInoutList = FacadeManage.aideTreasureFacade.GetRecordUserInoutList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			if (recordUserInoutList != null && recordUserInoutList.PageSet != null && recordUserInoutList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in recordUserInoutList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						EnterTime = TypeUtil.ObjectToString(row["EnterTime"]),
						AddressWithIP = TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(row["EnterClientIP"])),
						EnterClientIP = TypeUtil.ObjectToString(row["EnterClientIP"]),
						EnterMachine = TypeUtil.ObjectToString(row["EnterMachine"]),
						GameKindName = TypeUtil.GetGameKindName(TypeUtil.ObjectToInt(row["KindID"])),
						GameRoomName = TypeUtil.GetGameRoomName(TypeUtil.ObjectToInt(row["ServerID"])),
						EnterScore = TypeUtil.ObjectToString(row["EnterScore"]),
						EnterUserMedal = TypeUtil.ObjectToString(row["EnterUserMedal"]),
						EnterLoveliness = TypeUtil.ObjectToString(row["EnterLoveliness"]),
						LeaveTime = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "正在游戏中" : TypeUtil.ObjectToString(row["LeaveTime"])),
						LeaveReason = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : ((TypeUtil.ObjectToInt(row["LeaveReason"]) == 0) ? "常规离开" : ((TypeUtil.ObjectToInt(row["LeaveReason"]) == 1) ? "系统原因" : ((TypeUtil.ObjectToInt(row["LeaveReason"]) == 2) ? "用户冲突" : ((TypeUtil.ObjectToInt(row["LeaveReason"]) == 3) ? "网络原因" : "人满为患"))))),
						LeaveAddress = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(row["LeaveClientIP"]))),
						LeaveClientIP = TypeUtil.ObjectToString(row["LeaveClientIP"]),
						Score = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : TypeUtil.ObjectToString(row["Score"])),
						Insure = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : TypeUtil.ObjectToString(row["Insure"])),
						UserMedal = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : TypeUtil.ObjectToString(row["UserMedal"])),
						LoveLiness = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : TypeUtil.ObjectToString(row["LoveLiness"])),
						Experience = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : TypeUtil.ObjectToString(row["Experience"])),
						Revenue = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : TypeUtil.ObjectToString(row["Revenue"])),
						PlayTimeCount = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : TypeUtil.ObjectToString(row["PlayTimeCount"])),
						OnLineTimeCount = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : TypeUtil.ObjectToString(row["OnLineTimeCount"])),
						Total = ((!(TypeUtil.ObjectToString(row["LeaveTime"]) == "")) ? (TypeUtil.ObjectToInt(row["WinCount"]) + TypeUtil.ObjectToInt(row["LostCount"]) + TypeUtil.ObjectToInt(row["DrawCount"]) + TypeUtil.ObjectToInt(row["FleeCount"])) : 0),
						WinCount = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : TypeUtil.ObjectToString(row["WinCount"])),
						LostCount = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : TypeUtil.ObjectToString(row["LostCount"])),
						DrawCount = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : TypeUtil.ObjectToString(row["DrawCount"])),
						FleeCount = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : TypeUtil.ObjectToString(row["FleeCount"]))
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((recordUserInoutList.PageSet != null && recordUserInoutList.PageSet.Tables != null && recordUserInoutList.PageSet.Tables[0].Rows.Count != 0) ? recordUserInoutList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetRecordGrantMemberList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["UserID"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			stringBuilder.AppendFormat(" AND UserID={0}", num);
			List<object> list = new List<object>();
			PagerSet recordGrantMemberList = FacadeManage.aideRecordFacade.GetRecordGrantMemberList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			if (recordGrantMemberList != null && recordGrantMemberList.PageSet != null && recordGrantMemberList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in recordGrantMemberList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						CollectDate = TypeUtil.ObjectToDateTime(row["CollectDate"]).ToString("yyyy-MM-dd hh:mm:ss"),
						AddressWithIP = TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(row["ClientIP"])),
						MemberName = TypeUtil.GetMemberName((byte)row["GrantCardType"]),
						MemberDays = TypeUtil.ObjectToString(row["MemberDays"]),
						MasterName = TypeUtil.GetMasterName(TypeUtil.ObjectToInt(row["MasterID"])),
						Reason = TypeUtil.ObjectToString(row["Reason"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((recordGrantMemberList.PageSet != null && recordGrantMemberList.PageSet.Tables != null && recordGrantMemberList.PageSet.Tables[0].Rows.Count != 0) ? recordGrantMemberList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetRecordConvertPresentList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int num2 = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num3 = TypeUtil.ObjectToInt(base.Request["UserID"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			stringBuilder.AppendFormat(" AND UserID={0}", num3);
			int num4 = TypeUtil.ObjectToInt(base.Request["Type"]);
			string text = "";
			string text2 = "";
			switch (num4)
			{
			case 1:
				text = Fetch.GetTodayTime().Split('$')[0].ToString();
				text2 = Fetch.GetTodayTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND CollectDate>='{0}' AND CollectDate<='{1}'", text, text2);
				break;
			case 2:
				text = Fetch.GetYesterdayTime().Split('$')[0].ToString();
				text2 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND CollectDate>='{0}' AND CollectDate<='{1}'", text, text2);
				break;
			case 3:
				text = Fetch.GetWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetWeekTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND CollectDate>='{0}' AND CollectDate<='{1}'", text, text2);
				break;
			case 4:
				text = Fetch.GetLastWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND CollectDate>='{0}' AND CollectDate<='{1}'", text, text2);
				break;
			default:
			{
				DateTime result = DateTime.Now;
				DateTime result2 = DateTime.Now;
				if (DateTime.TryParse(base.Request["StartDate"].ToString(), out result) && DateTime.TryParse(base.Request["EndDate"].ToString(), out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND CollectDate>='{0}' AND CollectDate<='{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			}
			case 0:
				break;
			}
			List<object> list = new List<object>();
			PagerSet recordConvertPresentList = FacadeManage.aideRecordFacade.GetRecordConvertPresentList(num, num2, stringBuilder.ToString(), orderby);
			if (recordConvertPresentList != null && recordConvertPresentList.PageSet != null && recordConvertPresentList.PageSet.Tables.Count > 0)
			{
				int num5 = 0;
				foreach (DataRow row in recordConvertPresentList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						SNO = (num2 * (num - 1) + (num5 + 1)).ToString(),
						GameKindName = TypeUtil.GetGameKindName(TypeUtil.ObjectToInt(row["KindID"])),
						GameRoomName = TypeUtil.GetGameRoomName(TypeUtil.ObjectToInt(row["ServerID"])),
						CurInsureScore = TypeUtil.ObjectToLong(row["CurInsureScore"]).ToString("NO"),
						Score = (TypeUtil.ObjectToLong(row["CurInsureScore"]) + TypeUtil.ObjectToLong(row["ConvertPresent"]) + TypeUtil.ObjectToLong(row["ConvertRate"])).ToString("NO"),
						CurPresent = TypeUtil.ObjectToLong(row["CurPresent"]).ToString("NO"),
						ConvertPresent = TypeUtil.ObjectToLong(row["ConvertPresent"]).ToString("NO"),
						ConvertRate = TypeUtil.ObjectToString(row["ConvertRate"]),
						ExchangePlace = TypeUtil.GetExchangePlace(TypeUtil.ObjectToInt(row["IsGamePlaza"])),
						AddressWithIP = TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(row["ClientIP"])),
						CollectDate = TypeUtil.ObjectToDateTime(row["CollectDate"]).ToString("yyyy-MM-dd hh:mm:ss")
					});
					num5++;
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((recordConvertPresentList.PageSet != null && recordConvertPresentList.PageSet.Tables != null && recordConvertPresentList.PageSet.Tables[0].Rows.Count != 0) ? recordConvertPresentList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetRecordGrantTreasureList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["UserID"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			stringBuilder.AppendFormat(" AND UserID={0}", num);
			List<object> list = new List<object>();
			PagerSet recordGrantTreasureList = FacadeManage.aideRecordFacade.GetRecordGrantTreasureList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			if (recordGrantTreasureList != null && recordGrantTreasureList.PageSet != null && recordGrantTreasureList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in recordGrantTreasureList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						CollectDate = TypeUtil.ObjectToDateTime(row["CollectDate"]).ToString("yyyy-MM-dd hh:mm:ss"),
						ClientIP = TypeUtil.ObjectToString(row["ClientIP"]),
						AddressWithIP = TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(row["ClientIP"])),
						CurGold = TypeUtil.ObjectToString(row["CurGold"]),
						AddGold = TypeUtil.ObjectToString(row["AddGold"]),
						Score = ((TypeUtil.ObjectToInt(row["CurGold"]) + TypeUtil.ObjectToInt(row["AddGold"]) < 0) ? "0" : (TypeUtil.ObjectToInt(row["CurGold"]) + TypeUtil.ObjectToInt(row["AddGold"])).ToString()),
						MasterName = TypeUtil.GetMasterName(TypeUtil.ObjectToInt(row["MasterID"])),
						Reason = TypeUtil.ObjectToString(row["Reason"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((recordGrantTreasureList.PageSet != null && recordGrantTreasureList.PageSet.Tables != null && recordGrantTreasureList.PageSet.Tables[0].Rows.Count != 0) ? recordGrantTreasureList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetRecordGrantExperienceList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["UserID"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			stringBuilder.AppendFormat(" AND UserID={0}", num);
			List<object> list = new List<object>();
			PagerSet recordGrantExperienceList = FacadeManage.aideRecordFacade.GetRecordGrantExperienceList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			if (recordGrantExperienceList != null && recordGrantExperienceList.PageSet != null && recordGrantExperienceList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in recordGrantExperienceList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						CollectDate = TypeUtil.ObjectToDateTime(row["CollectDate"]).ToString("yyyy-MM-dd hh:mm:ss"),
						ClientIP = TypeUtil.ObjectToString(row["ClientIP"]),
						AddressWithIP = TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(row["ClientIP"])),
						CurExperience = TypeUtil.ObjectToString(row["CurExperience"]),
						AddExperience = TypeUtil.ObjectToString(row["AddExperience"]),
						Score = TypeUtil.ObjectToLong(row["CurExperience"]) + TypeUtil.ObjectToLong(row["AddExperience"]),
						MasterName = TypeUtil.GetMasterName(TypeUtil.ObjectToInt(row["MasterID"])),
						Reason = TypeUtil.ObjectToString(row["Reason"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((recordGrantExperienceList.PageSet != null && recordGrantExperienceList.PageSet.Tables != null && recordGrantExperienceList.PageSet.Tables[0].Rows.Count != 0) ? recordGrantExperienceList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetRecordGrantScoreList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["UserID"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			stringBuilder.AppendFormat(" AND UserID={0}", num);
			List<object> list = new List<object>();
			PagerSet recordGrantGameScoreList = FacadeManage.aideRecordFacade.GetRecordGrantGameScoreList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			if (recordGrantGameScoreList != null && recordGrantGameScoreList.PageSet != null && recordGrantGameScoreList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in recordGrantGameScoreList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						CollectDate = TypeUtil.ObjectToDateTime(row["CollectDate"]).ToString("yyyy-MM-dd hh:mm:ss"),
						ClientIP = TypeUtil.ObjectToString(row["ClientIP"]),
						AddressWithIP = TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(row["ClientIP"])),
						GameKindName = TypeUtil.GetGameKindName(TypeUtil.ObjectToInt(row["KindID"])),
						CurScore = TypeUtil.ObjectToString(row["CurScore"]),
						AddScore = TypeUtil.ObjectToString(row["AddScore"]),
						MasterName = TypeUtil.GetMasterName(TypeUtil.ObjectToInt(row["MasterID"])),
						Reason = TypeUtil.ObjectToString(row["Reason"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((recordGrantGameScoreList.PageSet != null && recordGrantGameScoreList.PageSet.Tables != null && recordGrantGameScoreList.PageSet.Tables[0].Rows.Count != 0) ? recordGrantGameScoreList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetRecordGrantClearScoreList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["UserID"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			stringBuilder.AppendFormat(" AND UserID={0}", num);
			List<object> list = new List<object>();
			PagerSet recordGrantClearScoreList = FacadeManage.aideRecordFacade.GetRecordGrantClearScoreList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			if (recordGrantClearScoreList != null && recordGrantClearScoreList.PageSet != null && recordGrantClearScoreList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in recordGrantClearScoreList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						CollectDate = TypeUtil.ObjectToDateTime(row["CollectDate"]).ToString("yyyy-MM-dd hh:mm:ss"),
						ClientIP = TypeUtil.ObjectToString(row["ClientIP"]),
						AddressWithIP = TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(row["ClientIP"])),
						KindID = TypeUtil.ObjectToString(row["KindID"]),
						CurFlee = TypeUtil.ObjectToString(row["CurFlee"]),
						MasterName = TypeUtil.GetMasterName(TypeUtil.ObjectToInt(row["MasterID"])),
						Reason = TypeUtil.ObjectToString(row["Reason"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((recordGrantClearScoreList.PageSet != null && recordGrantClearScoreList.PageSet.Tables != null && recordGrantClearScoreList.PageSet.Tables[0].Rows.Count != 0) ? recordGrantClearScoreList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetRecordGrantClearFlee()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["UserID"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			stringBuilder.AppendFormat(" AND UserID={0}", num);
			List<object> list = new List<object>();
			PagerSet recordGrantClearFleeList = FacadeManage.aideRecordFacade.GetRecordGrantClearFleeList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			if (recordGrantClearFleeList != null && recordGrantClearFleeList.PageSet != null && recordGrantClearFleeList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in recordGrantClearFleeList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						CollectDate = TypeUtil.ObjectToDateTime(row["CollectDate"]).ToString("yyyy-MM-dd hh:mm:ss"),
						ClientIP = TypeUtil.ObjectToString(row["ClientIP"]),
						KindID = TypeUtil.ObjectToString(row["KindID"]),
						CurScore = TypeUtil.ObjectToString(row["CurScore"]),
						MasterName = TypeUtil.GetMasterName(TypeUtil.ObjectToInt(row["MasterID"])),
						Reason = TypeUtil.ObjectToString(row["Reason"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((recordGrantClearFleeList.PageSet != null && recordGrantClearFleeList.PageSet.Tables != null && recordGrantClearFleeList.PageSet.Tables[0].Rows.Count != 0) ? recordGrantClearFleeList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetRecordGrantGameIDList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["UserID"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			stringBuilder.AppendFormat(" AND UserID={0}", num);
			List<object> list = new List<object>();
			PagerSet recordGrantGameIDList = FacadeManage.aideRecordFacade.GetRecordGrantGameIDList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			if (recordGrantGameIDList != null && recordGrantGameIDList.PageSet != null && recordGrantGameIDList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in recordGrantGameIDList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						CollectDate = TypeUtil.ObjectToDateTime(row["CollectDate"]).ToString("yyyy-MM-dd hh:mm:ss"),
						ClientIP = TypeUtil.ObjectToString(row["ClientIP"]),
						CurGameID = TypeUtil.ObjectToString(row["CurGameID"]),
						ReGameID = TypeUtil.ObjectToString(row["ReGameID"]),
						IDLevel = TypeUtil.ObjectToString(row["IDLevel"]),
						MasterName = TypeUtil.GetMasterName(TypeUtil.ObjectToInt(row["MasterID"])),
						Reason = TypeUtil.ObjectToString(row["Reason"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((recordGrantGameIDList.PageSet != null && recordGrantGameIDList.PageSet.Tables != null && recordGrantGameIDList.PageSet.Tables[0].Rows.Count != 0) ? recordGrantGameIDList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetRecordDrawInfoList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["ID"]);
			string orderby = "ORDER BY DrawID DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			string text = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string text2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			stringBuilder.AppendFormat(" AND DrawID IN (SELECT DrawID FROM  RecordDrawScore WHERE UserID={0})", num2);
			switch (num)
			{
			case 1:
				text = Fetch.GetTodayTime().Split('$')[0].ToString();
				text2 = Fetch.GetTodayTime().Split('$')[1].ToString();
				break;
			case 2:
				text = Fetch.GetYesterdayTime().Split('$')[0].ToString();
				text2 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
				break;
			case 3:
				text = Fetch.GetWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetWeekTime().Split('$')[1].ToString();
				break;
			case 4:
				text = Fetch.GetLastWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
				break;
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			if (text != "")
			{
				stringBuilder.AppendFormat(" AND ConcludeTime>='{0}'", Convert.ToDateTime(text));
				stringBuilder2.AppendFormat(" AND InsertTime>='{0}'", Convert.ToDateTime(text));
			}
			if (text2 != "")
			{
				stringBuilder.AppendFormat(" AND ConcludeTime<='{0}'", Convert.ToDateTime(text2));
				stringBuilder2.AppendFormat(" AND InsertTime<='{0}'", Convert.ToDateTime(text2));
			}
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("View_RecordDrawInfo", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			string sql = "SELECT ISNULL(SUM(Score),0) FROM RecordDrawScore WHERE UserID=" + num2 + stringBuilder2.ToString();
			decimal sumWinlose = Convert.ToDecimal(FacadeManage.aideTreasureFacade.GetScalarBySql(sql));
			return Json(new
			{
				IsOk = true,
				Msg = new
				{
					SumWinlose = sumWinlose
				},
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetSubRecordDrawInfoList()
		{
			string orderby = "ORDER BY DrawID DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			int num = TypeUtil.ObjectToInt(base.Request["DrawID"]);
			stringBuilder.AppendFormat(" AND DrawID={0}", num);
			List<object> list = new List<object>();
			PagerSet recordDrawScoreList = FacadeManage.aideTreasureFacade.GetRecordDrawScoreList(1, 1000, stringBuilder.ToString(), orderby);
			if (recordDrawScoreList != null && recordDrawScoreList.PageSet != null && recordDrawScoreList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in recordDrawScoreList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						Accounts = TypeUtil.GetAccounts(TypeUtil.ObjectToInt(row["UserID"])),
						IsAndroid = (TypeUtil.IsAndroid(TypeUtil.ObjectToInt(row["UserID"])) ? "是" : "否"),
						ChairID = TypeUtil.ObjectToString(row["ChairID"]),
						Score = TypeUtil.ObjectToString(row["Score"]),
						Revenue = TypeUtil.ObjectToString(row["Revenue"]),
						UserMedal = TypeUtil.ObjectToString(row["UserMedal"]),
						PlayTimeCount = TypeUtil.ObjectToString(row["PlayTimeCount"]),
						InsertTime = TypeUtil.ObjectToString(row["InsertTime"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((recordDrawScoreList.PageSet != null && recordDrawScoreList.PageSet.Tables != null && recordDrawScoreList.PageSet.Tables[0].Rows.Count != 0) ? recordDrawScoreList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetRecordInsureList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["ID"]);
			int num3 = TypeUtil.ObjectToInt(base.Request["DateType"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			string text = "";
			string text2 = "";
			switch (num3)
			{
			case 1:
				text = Fetch.GetTodayTime().Split('$')[0].ToString();
				text2 = Fetch.GetTodayTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND CollectDate>='{0}' AND CollectDate<='{1}'", text, text2);
				break;
			case 2:
				text = Fetch.GetYesterdayTime().Split('$')[0].ToString();
				text2 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND CollectDate>='{0}' AND CollectDate<='{1}'", text, text2);
				break;
			case 3:
				text = Fetch.GetWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetWeekTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND CollectDate>='{0}' AND CollectDate<='{1}'", text, text2);
				break;
			case 4:
				text = Fetch.GetLastWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND CollectDate>='{0}' AND CollectDate<='{1}'", text, text2);
				break;
			default:
			{
				DateTime result = DateTime.Now;
				DateTime result2 = DateTime.Now;
				if (DateTime.TryParse(TypeUtil.ObjectToString(base.Request["StartDate"]), out result))
				{
					stringBuilder.AppendFormat(" AND CollectDate>='{0}'", result);
				}
				if (DateTime.TryParse(TypeUtil.ObjectToString(base.Request["EndDate"]), out result2))
				{
					stringBuilder.AppendFormat(" AND CollectDate<'{0}'", result2);
				}
				break;
			}
			}
			switch (num)
			{
			case 1:
				stringBuilder.Append(" AND TradeType=1 AND  SourceUserID= " + num2);
				break;
			case 2:
				stringBuilder.Append(" AND TradeType=2 AND  SourceUserID= " + num2);
				break;
			case 3:
				stringBuilder.Append(" AND TradeType=3 AND TargetUserID=" + num2);
				break;
			case 4:
				stringBuilder.Append(" AND TradeType=3 AND SourceUserID=" + num2);
				break;
			default:
				stringBuilder.Append(" AND (SourceUserID= " + num2 + " OR TargetUserID =" + num2 + ")");
				break;
			}
			List<object> list = new List<object>();
			PagerSet recordInsureList = FacadeManage.aideTreasureFacade.GetRecordInsureList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			if (recordInsureList != null && recordInsureList.PageSet != null && recordInsureList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in recordInsureList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						CollectDate = TypeUtil.ObjectToString(row["CollectDate"]),
						Accounts = TypeUtil.GetAccounts(TypeUtil.ObjectToInt(row["SourceUserID"])),
						TargetUser = ((TypeUtil.ObjectToInt(row["TradeType"]) == 3) ? TypeUtil.GetAccounts(TypeUtil.ObjectToInt(row["TargetUserID"])) : ""),
						TradeType = ((TypeUtil.ObjectToInt(row["TradeType"]) == 1) ? "存款" : ((TypeUtil.ObjectToInt(row["TradeType"]) == 2) ? "取款" : ((TypeUtil.ObjectToInt(row["SourceUserID"]) == num2) ? "转出" : "转入"))),
						SourceGold = TypeUtil.ObjectToString(row["SourceGold"]),
						SourceBank = TypeUtil.ObjectToString(row["SourceBank"]),
						TargetGold = ((TypeUtil.ObjectToInt(row["TradeType"]) == 3) ? TypeUtil.ObjectToString(row["TargetGold"]) : ""),
						TargetBank = ((TypeUtil.ObjectToInt(row["TradeType"]) == 3) ? TypeUtil.ObjectToString(row["TargetBank"]) : ""),
						SwapScore = TypeUtil.ObjectToString(row["SwapScore"]),
						Revenue = TypeUtil.ObjectToString(row["Revenue"]),
						IsGamePlaza = ((TypeUtil.ObjectToInt(row["IsGamePlaza"]) == 0) ? "大厅" : "网页"),
						ClientIP = TypeUtil.ObjectToString(row["ClientIP"]),
						AddressWithIP = TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(row["ClientIP"])),
						GameKindName = TypeUtil.GetGameKindName(TypeUtil.ObjectToInt(row["KindID"])),
						GameRoomName = TypeUtil.GetGameRoomName(TypeUtil.ObjectToInt(row["ServerID"])),
						CollectNote = TypeUtil.ObjectToString(row["CollectNote"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((recordInsureList.PageSet != null && recordInsureList.PageSet.Tables != null && recordInsureList.PageSet.Tables[0].Rows.Count != 0) ? recordInsureList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetRecordUserScoreInoutList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["ID"]);
			int num3 = TypeUtil.ObjectToInt(base.Request["DateType"]);
			int num4 = TypeUtil.ObjectToInt(base.Request["GameID"]);
			string orderby = "ORDER BY EnterTime DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			stringBuilder.AppendFormat(" AND UserID={0}", num2);
			if (num4 > 0)
			{
				stringBuilder.AppendFormat(" AND KindID={0}", num4);
			}
			string arg = "";
			string arg2 = "";
			switch (num3)
			{
			case 1:
				arg = Fetch.GetTodayTime().Split('$')[0].ToString();
				arg2 = Fetch.GetTodayTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND EnterTime>='{0}' AND EnterTime<='{1}'", arg, arg2);
				break;
			case 2:
				arg = Fetch.GetYesterdayTime().Split('$')[0].ToString();
				arg2 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
				break;
			case 3:
				arg = Fetch.GetWeekTime().Split('$')[0].ToString();
				arg2 = Fetch.GetWeekTime().Split('$')[1].ToString();
				break;
			case 4:
				arg = Fetch.GetLastWeekTime().Split('$')[0].ToString();
				arg2 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
				break;
			}
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			if (DateTime.TryParse(base.Request["StartDate"].ToString(), out result) && DateTime.TryParse(base.Request["EndDate"].ToString(), out result2) && result <= result2)
			{
				switch (num)
				{
				case 1:
					stringBuilder.AppendFormat(" AND EnterTime>='{0}' AND EnterTime<='{1}'", arg, arg2);
					break;
				case 2:
					stringBuilder.AppendFormat(" AND LeaveTime>='{0}' AND LeaveTime<='{1}'", arg, arg2);
					break;
				case 3:
					stringBuilder.AppendFormat(" AND EnterTime>='{0}' AND EnterTime<='{1}'", arg, arg2);
					break;
				default:
					stringBuilder.AppendFormat(" AND EnterTime>='{0}' AND EnterTime<='{1}'", arg, arg2);
					break;
				}
			}
			List<object> list = new List<object>();
			PagerSet recordUserInoutList = FacadeManage.aideTreasureFacade.GetRecordUserInoutList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			if (recordUserInoutList != null && recordUserInoutList.PageSet != null && recordUserInoutList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in recordUserInoutList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						EnterTime = TypeUtil.ObjectToString(row["EnterTime"]),
						EnterClientIP = TypeUtil.ObjectToString(row["EnterClientIP"]),
						AddressWithIP = TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(row["EnterClientIP"])),
						GameKindName = TypeUtil.GetGameKindName(TypeUtil.ObjectToInt(row["KindID"])),
						GameRoomName = TypeUtil.GetGameRoomName(TypeUtil.ObjectToInt(row["ServerID"])),
						EnterScore = TypeUtil.ObjectToString(row["EnterScore"]),
						LeaveTime = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "正在游戏中" : TypeUtil.ObjectToString(row["LeaveTime"])),
						LeaveClientIP = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(row["LeaveClientIP"])) : TypeUtil.ObjectToString(row["LeaveClientIP"])),
						LeaveReason = ((TypeUtil.ObjectToInt(row["LeaveReason"]) == 0) ? "常规离开" : ((TypeUtil.ObjectToInt(row["LeaveReason"]) == 1) ? "系统原因" : ((TypeUtil.ObjectToInt(row["LeaveReason"]) == 2) ? "用户冲突" : ((TypeUtil.ObjectToInt(row["LeaveReason"]) == 3) ? "网络原因" : "人满为患")))),
						Score = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : TypeUtil.ObjectToString(row["Score"])),
						Experience = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : TypeUtil.ObjectToString(row["Experience"])),
						PlayTimeCount = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : TypeUtil.ObjectToString(row["PlayTimeCount"])),
						OnLineTimeCount = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : TypeUtil.ObjectToString(row["OnLineTimeCount"])),
						WinCount = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : TypeUtil.ObjectToString(row["WinCount"])),
						LostCount = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : TypeUtil.ObjectToString(row["LostCount"])),
						DrawCount = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : TypeUtil.ObjectToString(row["DrawCount"])),
						FleeCount = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : TypeUtil.ObjectToString(row["FleeCount"])),
						LeaveMachine = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : TypeUtil.ObjectToString(row["LeaveMachine"])),
						EnterMachine = TypeUtil.ObjectToString(row["EnterMachine"]),
						Total = ((TypeUtil.ObjectToString(row["LeaveTime"]) == "") ? "" : (TypeUtil.ObjectToInt(row["WinCount"]) + TypeUtil.ObjectToInt(row["LostCount"]) + TypeUtil.ObjectToInt(row["DrawCount"]) + TypeUtil.ObjectToInt(row["FleeCount"])).ToString())
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((recordUserInoutList.PageSet != null && recordUserInoutList.PageSet.Tables != null && recordUserInoutList.PageSet.Tables[0].Rows.Count != 0) ? recordUserInoutList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetShareDetailList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			TypeUtil.ObjectToInt(base.Request["Type"]);
			int num = TypeUtil.ObjectToInt(base.Request["ID"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["DateType"]);
			int num3 = TypeUtil.ObjectToInt(base.Request["ShareID"]);
			int num4 = TypeUtil.ObjectToInt(base.Request["CardType"]);
			string orderby = "ApplyDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1 ");
			stringBuilder.AppendFormat(" AND acc.UserID={0}", num);
			string text = TypeUtil.StringToDateTime(base.Request["StartDate"]).ToString("yyyy-MM-dd");
			string text2 = TypeUtil.StringToDateTime(base.Request["EndDate"]).AddDays(1.0).ToString("yyyy-MM-dd");
			switch (num2)
			{
			case 1:
				text = Fetch.GetTodayTime().Split('$')[0].ToString();
				text2 = Fetch.GetTodayTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND ApplyDate>='{0}' AND ApplyDate<='{1}'", text, text2);
				break;
			case 2:
				text = Fetch.GetYesterdayTime().Split('$')[0].ToString();
				text2 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND ApplyDate>='{0}' AND ApplyDate<='{1}'", text, text2);
				break;
			case 3:
				text = Fetch.GetWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetWeekTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND ApplyDate>='{0}' AND ApplyDate<='{1}'", text, text2);
				break;
			case 4:
				text = Fetch.GetLastWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND ApplyDate>='{0}' AND ApplyDate<='{1}'", text, text2);
				break;
			default:
			{
				DateTime result = DateTime.Now;
				DateTime result2 = DateTime.Now;
				if (DateTime.TryParse(TypeUtil.ObjectToString(base.Request["StartDate"]), out result))
				{
					stringBuilder.AppendFormat(" AND ApplyDate>='{0}'", result);
				}
				if (DateTime.TryParse(TypeUtil.ObjectToString(base.Request["EndDate"]), out result2))
				{
					stringBuilder.AppendFormat(" AND ApplyDate<'{0}'", result2);
				}
				break;
			}
			}
			if (num3 > 0)
			{
				stringBuilder.AppendFormat(" AND ShareID={0}", num3);
			}
			if (num4 > 0)
			{
				stringBuilder.AppendFormat(" AND CardTypeID={0}", num4);
			}
			List<object> list = new List<object>();
			PagerSet shareDetailList = FacadeManage.aideTreasureFacade.GetShareDetailList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			if (shareDetailList != null && shareDetailList.PageSet != null && shareDetailList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in shareDetailList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						ApplyDate = TypeUtil.ObjectToString(row["ApplyDate"]),
						ShareName = TypeUtil.GetShareName(TypeUtil.ObjectToInt(row["ShareID"])),
						Accounts = TypeUtil.ObjectToString(row["Accounts"]),
						GameID = TypeUtil.ObjectToString(row["GameID"]),
						OrderID = TypeUtil.ObjectToString(row["OrderID"]),
						SerialID = TypeUtil.ObjectToString(row["SerialID"]),
						CardTypeName = TypeUtil.GetCardTypeName(TypeUtil.ObjectToInt(row["CardTypeID"])),
						Currency = TypeUtil.ObjectToString(row["Currency"]),
						BeforeCurrency = TypeUtil.ObjectToString(row["BeforeCurrency"]),
						OrderAmount = TypeUtil.ObjectToString(row["OrderAmount"]),
						IPAddress = TypeUtil.ObjectToString(row["IPAddress"]),
						PayAmount = TypeUtil.ObjectToString(row["PayAmount"]),
						AddressWithIP = TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(row["IPAddress"])),
						OperUserID = TypeUtil.GetAccounts(TypeUtil.ObjectToInt(row["OperUserID"]))
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((shareDetailList.PageSet != null && shareDetailList.PageSet.Tables != null && shareDetailList.PageSet.Tables[0].Rows.Count != 0) ? shareDetailList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public JsonResult GetAccountsCurrencyList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string text = TypeUtil.ObjectToString(base.Request["Sort"]);
			int num = TypeUtil.ObjectToInt(base.Request["SortDrc"]);
			string text2 = "";
			text2 = ((!string.IsNullOrEmpty(text)) ? ("ORDER BY " + text + " " + ((num == 0) ? "DESC" : "ASC")) : "ORDER BY UserID DESC");
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			string safeSQL = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["KeyWord"]));
			if (!string.IsNullOrEmpty(safeSQL))
			{
				int userIDByAccount = TypeUtil.GetUserIDByAccount(safeSQL);
				stringBuilder.AppendFormat(" AND UserID={0}", userIDByAccount);
			}
			List<object> list = new List<object>();
			PagerSet list2 = FacadeManage.aideTreasureFacade.GetList("UserCurrencyInfo", pageIndex, pageSize, stringBuilder.ToString(), text2);
			if (list2 != null && list2.PageSet != null && list2.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in list2.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						UserID = TypeUtil.ObjectToInt(row["UserID"]),
						GameID = TypeUtil.GetGameID(TypeUtil.ObjectToInt(row["UserID"])),
						Accounts = TypeUtil.GetAccounts(TypeUtil.ObjectToInt(row["UserID"])),
						Currency = TypeUtil.ObjectToString(row["Currency"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((list2.PageSet != null && list2.PageSet.Tables != null && list2.PageSet.Tables[0].Rows.Count != 0) ? list2.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public JsonResult GetAccountsScoreList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["Types"]);
			TypeUtil.ObjectToInt(base.Request["kindID"]);
			string orderby = "ORDER BY UserID DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			string safeSQL = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["KeyWord"]));
			int result = 0;
			if (!string.IsNullOrEmpty(safeSQL))
			{
				switch (num)
				{
				case 1:
					stringBuilder.AppendFormat(" AND UserID={0}", TypeUtil.GetUserIDByAccount(safeSQL));
					break;
				case 2:
					if (int.TryParse(safeSQL, out result))
					{
						stringBuilder.AppendFormat(" AND UserID={0}", result);
					}
					else
					{
						stringBuilder.Append(" AND 1=2 ");
					}
					break;
				case 3:
					if (int.TryParse(safeSQL, out result))
					{
						stringBuilder.AppendFormat(" AND Score>={0}", result);
					}
					else
					{
						stringBuilder.Append(" AND 1=2 ");
					}
					break;
				case 4:
					if (int.TryParse(safeSQL, out result))
					{
						stringBuilder.AppendFormat(" AND Score<={0}", result);
					}
					else
					{
						stringBuilder.Append(" AND 1=2 ");
					}
					break;
				}
			}
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("GameScoreInfo", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list2 = new List<object>();
			if (list != null && list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in list.PageSet.Tables[0].Rows)
				{
					list2.Add(new
					{
						UserID = TypeUtil.ObjectToInt(row["UserID"]),
						GameID = TypeUtil.GetGameID(TypeUtil.ObjectToInt(row["UserID"])),
						Accounts = TypeUtil.GetAccounts(TypeUtil.ObjectToInt(row["UserID"])),
						Score = TypeUtil.ObjectToString(row["Score"]),
						WinCount = TypeUtil.ObjectToString(row["WinCount"]),
						LostCount = TypeUtil.ObjectToString(row["LostCount"]),
						DrawCount = TypeUtil.ObjectToString(row["DrawCount"]),
						FleeCount = TypeUtil.ObjectToString(row["FleeCount"]),
						AllLogonTimes = TypeUtil.ObjectToString(row["AllLogonTimes"]),
						PlayTimeCount = TypeUtil.ObjectToString(row["PlayTimeCount"]),
						OnLineTimeCount = TypeUtil.ObjectToString(row["OnLineTimeCount"]),
						LastLogonDate = TypeUtil.ObjectToString(row["LastLogonDate"]),
						Address = TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(row["LastLogonIP"])),
						SubAddress = TextUtility.CutString(TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(row["LastLogonIP"])), 0, 6)
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
		public JsonResult CleanGoldInfo()
		{
			if (user != null)
			{
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(8192L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
			}
			int userID = TypeUtil.ObjectToInt(base.Request["IntParam"]);
			FacadeManage.aideTreasureFacade.DeleteGameScoreLockerByUserID(userID);
			return Json(new
			{
				IsOk = true,
				Msg = "操作成功"
			});
		}

		[CheckCustomer]
		public JsonResult GetScoreInfoByKindIDAndUserID()
		{
			int kindID = TypeUtil.ObjectToInt(base.Request["kindID"]);
			int userID = TypeUtil.ObjectToInt(base.Request["userID"]);
			GameScoreInfo gameScoreInfoByUserID = new TreasureFacade(kindID).GetGameScoreInfoByUserID(userID);
			if (gameScoreInfoByUserID != null)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "操作成功",
					Data = new
					{
						Score = gameScoreInfoByUserID.Score.ToString("N0"),
						WinCount = gameScoreInfoByUserID.WinCount.ToString(),
						LostCount = gameScoreInfoByUserID.LostCount.ToString(),
						DrawCount = gameScoreInfoByUserID.DrawCount.ToString(),
						FleeCount = gameScoreInfoByUserID.FleeCount.ToString(),
						AllLogonTimes = gameScoreInfoByUserID.AllLogonTimes.ToString() + "次",
						LastLogonDate = gameScoreInfoByUserID.LastLogonDate.ToString("yyyy-MM-dd HH:mm:ss"),
						LogonSpacingTime = Fetch.GetTimeSpan(Convert.ToDateTime(gameScoreInfoByUserID.LastLogonDate), DateTime.Now) + "前",
						LastLogonIP = gameScoreInfoByUserID.LastLogonIP.ToString(),
						LogonIPInfo = IPQuery.GetAddressWithIP(gameScoreInfoByUserID.LastLogonIP.ToString()),
						LastLogonMachine = gameScoreInfoByUserID.LastLogonMachine.ToString(),
						RegisterDate = gameScoreInfoByUserID.RegisterDate.ToString("yyyy-MM-dd HH:mm:ss"),
						RegisterIP = gameScoreInfoByUserID.RegisterIP.ToString(),
						RegIPInfo = IPQuery.GetAddressWithIP(gameScoreInfoByUserID.RegisterIP.ToString()),
						RegisterMachine = gameScoreInfoByUserID.RegisterMachine.ToString(),
						OnLineTimeCount = gameScoreInfoByUserID.OnLineTimeCount.ToString(),
						PlayTimeCount = gameScoreInfoByUserID.PlayTimeCount.ToString()
					}
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "未找到任何数据"
			});
		}

		[CheckCustomer]
		public JsonResult GetConvertPresentList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int num2 = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string text = TypeUtil.ObjectToString(base.Request["SearchType"]);
			int num3 = TypeUtil.ObjectToInt(base.Request["DateType"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			string safeSQL = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["KeyWord"]));
			DateTime result;
			DateTime result2;
			switch (text)
			{
			case "user":
				if (!string.IsNullOrEmpty(safeSQL))
				{
					int num5 = TypeUtil.ObjectToInt(base.Request["Type"]);
					int result4 = 0;
					if (num5 == 1)
					{
						stringBuilder.AppendFormat(" AND UserID={0}", TypeUtil.GetUserIDByAccount(safeSQL));
					}
					else if (int.TryParse(safeSQL, out result4))
					{
						stringBuilder.AppendFormat(" AND UserID={0}", TypeUtil.GetUserIDByGameID(result4));
					}
					else
					{
						stringBuilder.Append(" AND 1=2 ");
					}
				}
				else
				{
					stringBuilder.Append(" AND 1=2 ");
				}
				break;
			case "date":
				result = DateTime.Now;
				result2 = DateTime.Now;
				switch (num3)
				{
				case 1:
				{
					string text4 = Fetch.GetTodayTime().Split('$')[0].ToString();
					string text5 = Fetch.GetTodayTime().Split('$')[1].ToString();
					if (text4 != "" && text5 != "")
					{
						stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", text4, Convert.ToDateTime(text5).AddDays(1.0).ToString("yyyy-MM-dd"));
					}
					break;
				}
				case 2:
				{
					string text4 = Fetch.GetYesterdayTime().Split('$')[0].ToString();
					string text5 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
					if (text4 != "" && text5 != "")
					{
						stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", text4, Convert.ToDateTime(text5).AddDays(1.0).ToString("yyyy-MM-dd"));
					}
					break;
				}
				case 3:
				{
					string text4 = Fetch.GetWeekTime().Split('$')[0].ToString();
					string text5 = Fetch.GetWeekTime().Split('$')[1].ToString();
					if (text4 != "" && text5 != "")
					{
						stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", text4, Convert.ToDateTime(text5).AddDays(1.0).ToString("yyyy-MM-dd"));
					}
					break;
				}
				case 4:
				{
					string text4 = Fetch.GetLastWeekTime().Split('$')[0].ToString();
					string text5 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
					if (text4 != "" && text5 != "")
					{
						stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", text4, Convert.ToDateTime(text5).AddDays(1.0).ToString("yyyy-MM-dd"));
					}
					break;
				}
				case 5:
					if (DateTime.TryParse(base.Request["StartDate"].ToString(), out result))
					{
						stringBuilder.AppendFormat(" AND CollectDate >= '{0}' ", result);
					}
					if (DateTime.TryParse(base.Request["EndDate"].ToString(), out result2))
					{
						stringBuilder.AppendFormat(" AND  CollectDate < '{0}'", result2);
					}
					break;
				}
				break;
			default:
				if (num3 > 0)
				{
					switch (num3)
					{
					case 1:
					{
						string text2 = Fetch.GetTodayTime().Split('$')[0].ToString();
						string text3 = Fetch.GetTodayTime().Split('$')[1].ToString();
						if (text2 != "" && text3 != "")
						{
							stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", text2, Convert.ToDateTime(text3).AddDays(1.0).ToString("yyyy-MM-dd"));
						}
						break;
					}
					case 2:
					{
						string text2 = Fetch.GetYesterdayTime().Split('$')[0].ToString();
						string text3 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
						if (text2 != "" && text3 != "")
						{
							stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", text2, Convert.ToDateTime(text3).AddDays(1.0).ToString("yyyy-MM-dd"));
						}
						break;
					}
					case 3:
					{
						string text2 = Fetch.GetWeekTime().Split('$')[0].ToString();
						string text3 = Fetch.GetWeekTime().Split('$')[1].ToString();
						if (text2 != "" && text3 != "")
						{
							stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", text2, Convert.ToDateTime(text3).AddDays(1.0).ToString("yyyy-MM-dd"));
						}
						break;
					}
					case 4:
					{
						string text2 = Fetch.GetLastWeekTime().Split('$')[0].ToString();
						string text3 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
						if (text2 != "" && text3 != "")
						{
							stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", text2, Convert.ToDateTime(text3).AddDays(1.0).ToString("yyyy-MM-dd"));
						}
						break;
					}
					case 5:
						if (DateTime.TryParse(base.Request["StartDate"].ToString(), out result))
						{
							stringBuilder.AppendFormat(" AND CollectDate >= '{0}' ", result);
						}
						if (DateTime.TryParse(base.Request["EndDate"].ToString(), out result2))
						{
							stringBuilder.AppendFormat(" AND  CollectDate < '{0}'", result2);
						}
						break;
					}
				}
				else if (!string.IsNullOrEmpty(safeSQL))
				{
					int num4 = TypeUtil.ObjectToInt(base.Request["Type"]);
					int result3 = 0;
					if (num4 == 1)
					{
						stringBuilder.AppendFormat(" AND UserID={0}", TypeUtil.GetUserIDByAccount(safeSQL));
					}
					else if (int.TryParse(safeSQL, out result3))
					{
						stringBuilder.AppendFormat(" AND UserID={0}", TypeUtil.GetUserIDByGameID(result3));
					}
					else
					{
						stringBuilder.Append(" AND 1=2 ");
					}
				}
				break;
			}
			PagerSet recordConvertPresentList = FacadeManage.aideRecordFacade.GetRecordConvertPresentList(num, num2, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (recordConvertPresentList != null && recordConvertPresentList.PageSet != null && recordConvertPresentList.PageSet.Tables.Count > 0)
			{
				for (int i = 0; i < recordConvertPresentList.PageSet.Tables[0].Rows.Count; i++)
				{
					DataRow dataRow = recordConvertPresentList.PageSet.Tables[0].Rows[i];
					list.Add(new
					{
						SNO = num2 * (num - 1) + (i + 1),
						CollectDate = TypeUtil.ObjectToDateTime(dataRow["CollectDate"]).ToString("yyyy-MM-dd HH:mm:ss"),
						UserID = TypeUtil.ObjectToInt(dataRow["UserID"]),
						Accounts = TypeUtil.GetAccounts(TypeUtil.ObjectToInt(dataRow["UserID"])),
						GameKindName = TypeUtil.GetGameKindName(TypeUtil.ObjectToInt(dataRow["KindID"])),
						GameRoomName = TypeUtil.GetGameRoomName(TypeUtil.ObjectToInt(dataRow["ServerID"])),
						CurInsureScore = TypeUtil.ObjectToLong(dataRow["CurInsureScore"]).ToString("NO"),
						CurPresent = TypeUtil.ObjectToString(dataRow["CurPresent"]),
						ConvertPresent = TypeUtil.ObjectToString(dataRow["ConvertPresent"]),
						ConvertRate = "1:" + TypeUtil.ObjectToString(dataRow["ConvertRate"]),
						TotalScore = (TypeUtil.ObjectToLong(dataRow["CurInsureScore"]) + TypeUtil.ObjectToLong(dataRow["ConvertPresent"]) * TypeUtil.ObjectToLong(dataRow["ConvertRate"])).ToString("NO"),
						ExchangePlace = TypeUtil.GetExchangePlace(TypeUtil.ObjectToInt(dataRow["IsGamePlaza"])),
						ClientIP = TypeUtil.ObjectToString(dataRow["ClientIP"]),
						Address = TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(dataRow["ClientIP"]))
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = recordConvertPresentList.RecordCount,
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public JsonResult GetConvertMedalList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int num2 = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string text = TypeUtil.ObjectToString(base.Request["SearchType"]);
			int num3 = TypeUtil.ObjectToInt(base.Request["DateType"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			string safeSQL = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["KeyWord"]));
			DateTime result;
			DateTime result2;
			switch (text)
			{
			case "user":
				if (!string.IsNullOrEmpty(safeSQL))
				{
					int num5 = TypeUtil.ObjectToInt(base.Request["Type"]);
					int result4 = 0;
					if (num5 == 1)
					{
						stringBuilder.AppendFormat(" AND UserID={0}", TypeUtil.GetUserIDByAccount(safeSQL));
					}
					else if (int.TryParse(safeSQL, out result4))
					{
						stringBuilder.AppendFormat(" AND UserID={0}", TypeUtil.GetUserIDByGameID(result4));
					}
					else
					{
						stringBuilder.Append(" AND 1=2 ");
					}
				}
				else
				{
					stringBuilder.Append(" AND 1=2 ");
				}
				break;
			case "date":
				result = DateTime.Now;
				result2 = DateTime.Now;
				switch (num3)
				{
				case 1:
				{
					string text4 = Fetch.GetTodayTime().Split('$')[0].ToString();
					string text5 = Fetch.GetTodayTime().Split('$')[1].ToString();
					if (text4 != "" && text5 != "")
					{
						stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", text4, Convert.ToDateTime(text5).AddDays(1.0).ToString("yyyy-MM-dd"));
					}
					break;
				}
				case 2:
				{
					string text4 = Fetch.GetYesterdayTime().Split('$')[0].ToString();
					string text5 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
					if (text4 != "" && text5 != "")
					{
						stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", text4, Convert.ToDateTime(text5).AddDays(1.0).ToString("yyyy-MM-dd"));
					}
					break;
				}
				case 3:
				{
					string text4 = Fetch.GetWeekTime().Split('$')[0].ToString();
					string text5 = Fetch.GetWeekTime().Split('$')[1].ToString();
					if (text4 != "" && text5 != "")
					{
						stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", text4, Convert.ToDateTime(text5).AddDays(1.0).ToString("yyyy-MM-dd"));
					}
					break;
				}
				case 4:
				{
					string text4 = Fetch.GetLastWeekTime().Split('$')[0].ToString();
					string text5 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
					if (text4 != "" && text5 != "")
					{
						stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", text4, Convert.ToDateTime(text5).AddDays(1.0).ToString("yyyy-MM-dd"));
					}
					break;
				}
				case 5:
					if (DateTime.TryParse(base.Request["StartDate"].ToString(), out result))
					{
						stringBuilder.AppendFormat(" AND CollectDate >= '{0}' ", result);
					}
					if (DateTime.TryParse(base.Request["EndDate"].ToString(), out result2))
					{
						stringBuilder.AppendFormat(" AND  CollectDate < '{0}'", result2);
					}
					break;
				}
				break;
			default:
				if (num3 > 0)
				{
					switch (num3)
					{
					case 1:
					{
						string text2 = Fetch.GetTodayTime().Split('$')[0].ToString();
						string text3 = Fetch.GetTodayTime().Split('$')[1].ToString();
						if (text2 != "" && text3 != "")
						{
							stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", text2, Convert.ToDateTime(text3).AddDays(1.0).ToString("yyyy-MM-dd"));
						}
						break;
					}
					case 2:
					{
						string text2 = Fetch.GetYesterdayTime().Split('$')[0].ToString();
						string text3 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
						if (text2 != "" && text3 != "")
						{
							stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", text2, Convert.ToDateTime(text3).AddDays(1.0).ToString("yyyy-MM-dd"));
						}
						break;
					}
					case 3:
					{
						string text2 = Fetch.GetWeekTime().Split('$')[0].ToString();
						string text3 = Fetch.GetWeekTime().Split('$')[1].ToString();
						if (text2 != "" && text3 != "")
						{
							stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", text2, Convert.ToDateTime(text3).AddDays(1.0).ToString("yyyy-MM-dd"));
						}
						break;
					}
					case 4:
					{
						string text2 = Fetch.GetLastWeekTime().Split('$')[0].ToString();
						string text3 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
						if (text2 != "" && text3 != "")
						{
							stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", text2, Convert.ToDateTime(text3).AddDays(1.0).ToString("yyyy-MM-dd"));
						}
						break;
					}
					case 5:
						if (DateTime.TryParse(base.Request["StartDate"].ToString(), out result))
						{
							stringBuilder.AppendFormat(" AND CollectDate >= '{0}' ", result);
						}
						if (DateTime.TryParse(base.Request["EndDate"].ToString(), out result2))
						{
							stringBuilder.AppendFormat(" AND  CollectDate < '{0}'", result2);
						}
						break;
					}
				}
				else if (!string.IsNullOrEmpty(safeSQL))
				{
					int num4 = TypeUtil.ObjectToInt(base.Request["Type"]);
					int result3 = 0;
					if (num4 == 1)
					{
						stringBuilder.AppendFormat(" AND UserID={0}", TypeUtil.GetUserIDByAccount(safeSQL));
					}
					else if (int.TryParse(safeSQL, out result3))
					{
						stringBuilder.AppendFormat(" AND UserID={0}", TypeUtil.GetUserIDByGameID(result3));
					}
					else
					{
						stringBuilder.Append(" AND 1=2 ");
					}
				}
				break;
			}
			PagerSet list = FacadeManage.aideRecordFacade.GetList("RecordConvertUserMedal", num, num2, stringBuilder.ToString(), orderby);
			List<object> list2 = new List<object>();
			if (list != null && list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				for (int i = 0; i < list.PageSet.Tables[0].Rows.Count; i++)
				{
					DataRow dataRow = list.PageSet.Tables[0].Rows[i];
					list2.Add(new
					{
						SNO = num2 * (num - 1) + (i + 1),
						CollectDate = TypeUtil.ObjectToDateTime(dataRow["CollectDate"]).ToString("yyyy-MM-dd HH:mm:ss"),
						UserID = TypeUtil.ObjectToInt(dataRow["UserID"]),
						Accounts = TypeUtil.GetAccounts(TypeUtil.ObjectToInt(dataRow["UserID"])),
						ConvertUserMedal = TypeUtil.ObjectToString(dataRow["ConvertUserMedal"]),
						CurUserMedal = TypeUtil.ObjectToString(dataRow["CurUserMedal"]),
						CurInsureScore = TypeUtil.ObjectToLong(dataRow["CurInsureScore"]).ToString("NO"),
						TotalScore = (TypeUtil.ObjectToLong(dataRow["CurInsureScore"]) + TypeUtil.ObjectToLong(dataRow["ConvertUserMedal"]) * TypeUtil.ObjectToLong(dataRow["ConvertRate"])).ToString("NO"),
						ConvertRate = "1:" + TypeUtil.ObjectToString(dataRow["ConvertRate"]),
						ExchangePlace = TypeUtil.GetExchangePlace(TypeUtil.ObjectToInt(dataRow["IsGamePlaza"])),
						ClientIP = TypeUtil.ObjectToString(dataRow["ClientIP"]),
						Address = TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(dataRow["ClientIP"]))
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
		public JsonResult GetGrantTreasureList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int num2 = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			TypeUtil.ObjectToString(base.Request["SearchType"]);
			int num3 = TypeUtil.ObjectToInt(base.Request["DateType"]);
			string text = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string text2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			string safeSQL = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["KeyWord"]));
			if (safeSQL != "")
			{
				int num4 = TypeUtil.ObjectToInt(base.Request["Type"]);
				int result = 0;
				switch (num4)
				{
				case 1:
					stringBuilder.AppendFormat(" AND Accounts='{0}'", safeSQL);
					break;
				case 2:
					if (!int.TryParse(safeSQL, out result))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "游戏ID格式错误"
						});
					}
					stringBuilder.AppendFormat(" AND GameID={0}", result);
					break;
				default:
					stringBuilder.AppendFormat(" AND Username='{0}'", safeSQL);
					break;
				}
			}
			DateTime now = DateTime.Now;
			DateTime now2 = DateTime.Now;
			switch (num3)
			{
			case 1:
			{
				string text3 = Fetch.GetTodayTime().Split('$')[0].ToString();
				string text4 = Fetch.GetTodayTime().Split('$')[1].ToString();
				if (text3 != "" && text4 != "")
				{
					stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", text3, Convert.ToDateTime(text4).AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			}
			case 2:
			{
				string text3 = Fetch.GetYesterdayTime().Split('$')[0].ToString();
				string text4 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
				if (text3 != "" && text4 != "")
				{
					stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", text3, Convert.ToDateTime(text4).AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			}
			case 3:
			{
				string text3 = Fetch.GetWeekTime().Split('$')[0].ToString();
				string text4 = Fetch.GetWeekTime().Split('$')[1].ToString();
				if (text3 != "" && text4 != "")
				{
					stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", text3, Convert.ToDateTime(text4).AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			}
			case 4:
			{
				string text3 = Fetch.GetLastWeekTime().Split('$')[0].ToString();
				string text4 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
				if (text3 != "" && text4 != "")
				{
					stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", text3, Convert.ToDateTime(text4).AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			}
			case 5:
				if (text != "")
				{
					stringBuilder.AppendFormat(" AND CollectDate > '{0}'", Convert.ToDateTime(text));
				}
				if (text2 != "")
				{
					stringBuilder.AppendFormat(" AND CollectDate <= '{0}'", Convert.ToDateTime(text2));
				}
				break;
			}
			PagerSet list = FacadeManage.aideRecordFacade.GetList("View_RecordGrantTreasure", num, num2, stringBuilder.ToString(), orderby);
			List<object> list2 = new List<object>();
			if (list != null && list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				for (int i = 0; i < list.PageSet.Tables[0].Rows.Count; i++)
				{
					DataRow dataRow = list.PageSet.Tables[0].Rows[i];
					list2.Add(new
					{
						SNO = num2 * (num - 1) + (i + 1),
						CollectDate = TypeUtil.ObjectToDateTime(dataRow["CollectDate"]).ToString("yyyy-MM-dd HH:mm:ss"),
						UserID = TypeUtil.ObjectToInt(dataRow["UserID"]),
						Accounts = TypeUtil.ObjectToString(dataRow["Accounts"]),
						CurGold = TypeUtil.ObjectToDecimal(dataRow["CurGold"]).ToString("N"),
						AddGold = TypeUtil.ObjectToDecimal(dataRow["AddGold"]).ToString("N"),
						TotalScore = ((Convert.ToDecimal(dataRow["CurGold"].ToString()) + Convert.ToDecimal(dataRow["AddGold"].ToString()) < 0m) ? "0" : (Convert.ToDecimal(dataRow["CurGold"].ToString()) + Convert.ToDecimal(dataRow["AddGold"].ToString())).ToString("N")),
						MasterName = TypeUtil.ObjectToString(dataRow["Username"]),
						ClientIP = TypeUtil.ObjectToString(dataRow["ClientIP"]),
						Reason = TypeUtil.ObjectToString(dataRow["Reason"])
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
		public JsonResult GetUsePropertyList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int num2 = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string text = TypeUtil.ObjectToString(base.Request["SearchType"]);
			string orderby = "ORDER BY UseDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			string safeSQL = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["KeyWord"]));
			switch (text)
			{
			case "user":
				if (!string.IsNullOrEmpty(safeSQL))
				{
					stringBuilder.AppendFormat(" AND SourceUserID={0}", TypeUtil.GetUserIDByAccount(safeSQL));
				}
				else
				{
					stringBuilder.Append(" AND 1=2 ");
				}
				break;
			case "date":
			{
				DateTime result3 = DateTime.Now;
				DateTime result4 = DateTime.Now;
				if (DateTime.TryParse(base.Request["StartDate"].ToString(), out result3) && DateTime.TryParse(base.Request["EndDate"].ToString(), out result4) && result3 <= result4)
				{
					stringBuilder.AppendFormat(" AND UseDate >= '{0}' AND UseDate < '{1}'", result3.ToString("yyyy-MM-dd"), result4.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			}
			default:
			{
				if (!string.IsNullOrEmpty(safeSQL))
				{
					stringBuilder.AppendFormat(" AND SourceUserID={0}", TypeUtil.GetUserIDByAccount(safeSQL));
				}
				DateTime result = DateTime.Now;
				DateTime result2 = DateTime.Now;
				if (DateTime.TryParse(TypeUtil.ObjectToString(base.Request["StartDate"]), out result) && DateTime.TryParse(TypeUtil.ObjectToString(base.Request["EndDate"]), out result2) && result2 >= result)
				{
					stringBuilder.AppendFormat(" AND UseDate >= '{0}' AND UseDate < '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			}
			}
			PagerSet list = FacadeManage.aideRecordFacade.GetList("RecordUseProperty", num, num2, stringBuilder.ToString(), orderby);
			List<object> list2 = new List<object>();
			if (list != null && list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				for (int i = 0; i < list.PageSet.Tables[0].Rows.Count; i++)
				{
					DataRow dataRow = list.PageSet.Tables[0].Rows[i];
					list2.Add(new
					{
						SNO = num2 * (num - 1) + (i + 1),
						PropertyName = TypeUtil.ObjectToString(dataRow["PropertyName"]),
						UserID = TypeUtil.ObjectToInt(dataRow["SourceUserID"]),
						Accounts = TypeUtil.GetAccounts(TypeUtil.ObjectToInt(dataRow["SourceUserID"])),
						TargetUser = TypeUtil.GetAccounts(TypeUtil.ObjectToInt(dataRow["TargetUserID"])),
						TargetUserID = TypeUtil.ObjectToInt(dataRow["TargetUserID"]),
						PropertyCount = TypeUtil.ObjectToString(dataRow["PropertyCount"]),
						LovelinessRcv = TypeUtil.ObjectToString(dataRow["LovelinessRcv"]),
						LovelinessSend = TypeUtil.ObjectToString(dataRow["LovelinessSend"]),
						UseResultsGold = TypeUtil.ObjectToString(dataRow["UseResultsGold"]),
						GameKindName = TypeUtil.GetGameKindName(TypeUtil.ObjectToInt(dataRow["KindID"])),
						GameRoomName = TypeUtil.GetGameRoomName(TypeUtil.ObjectToInt(dataRow["ServerID"])),
						UseDate = TypeUtil.ObjectToString(dataRow["UseDate"]),
						ClientIP = TypeUtil.ObjectToString(dataRow["ClientIP"]),
						Address = TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(dataRow["ClientIP"]))
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
		public JsonResult GetBuyPropertyList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int num2 = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string text = TypeUtil.ObjectToString(base.Request["SearchType"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			string safeSQL = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["KeyWord"]));
			switch (text)
			{
			case "user":
				if (!string.IsNullOrEmpty(safeSQL))
				{
					stringBuilder.AppendFormat(" AND UserID={0}", TypeUtil.GetUserIDByAccount(safeSQL));
				}
				else
				{
					stringBuilder.Append(" AND 1=2 ");
				}
				break;
			case "date":
			{
				DateTime result3 = DateTime.Now;
				DateTime result4 = DateTime.Now;
				if (DateTime.TryParse(base.Request["StartDate"].ToString(), out result3) && DateTime.TryParse(base.Request["EndDate"].ToString(), out result4) && result3 <= result4)
				{
					stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", result3.ToString("yyyy-MM-dd"), result4.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			}
			default:
			{
				if (!string.IsNullOrEmpty(safeSQL))
				{
					stringBuilder.AppendFormat(" AND UserID={0}", TypeUtil.GetUserIDByAccount(safeSQL));
				}
				DateTime result = DateTime.Now;
				DateTime result2 = DateTime.Now;
				if (DateTime.TryParse(TypeUtil.ObjectToString(base.Request["StartDate"]), out result) && DateTime.TryParse(TypeUtil.ObjectToString(base.Request["EndDate"]), out result2) && result2 >= result)
				{
					stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			}
			}
			PagerSet list = FacadeManage.aideRecordFacade.GetList("RecordBuyProperty", num, num2, stringBuilder.ToString(), orderby);
			List<object> list2 = new List<object>();
			if (list != null && list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				for (int i = 0; i < list.PageSet.Tables[0].Rows.Count; i++)
				{
					DataRow dataRow = list.PageSet.Tables[0].Rows[i];
					list2.Add(new
					{
						SNO = num2 * (num - 1) + (i + 1),
						PropertyName = TypeUtil.ObjectToString(dataRow["PropertyName"]),
						UserID = TypeUtil.ObjectToInt(dataRow["UserID"]),
						Accounts = TypeUtil.GetAccounts(TypeUtil.ObjectToInt(dataRow["UserID"])),
						Cash = TypeUtil.ObjectToString(dataRow["Cash"]),
						Gold = TypeUtil.ObjectToString(dataRow["Gold"]),
						UserMedal = TypeUtil.ObjectToString(dataRow["UserMedal"]),
						LoveLiness = TypeUtil.ObjectToString(dataRow["LoveLiness"]),
						PropertyCount = TypeUtil.ObjectToString(dataRow["PropertyCount"]),
						MemberDiscount = TypeUtil.ObjectToInt(dataRow["MemberDiscount"]),
						BuyCash = TypeUtil.ObjectToString(dataRow["BuyCash"]),
						BuyGold = TypeUtil.ObjectToString(dataRow["BuyGold"]),
						BuyUserMedal = TypeUtil.ObjectToString(dataRow["BuyUserMedal"]),
						BuyLoveLiness = TypeUtil.ObjectToString(dataRow["BuyUserMedal"]),
						ClientIP = TypeUtil.ObjectToString(dataRow["ClinetIP"]),
						Address = TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(dataRow["ClinetIP"])),
						CollectDate = TypeUtil.ObjectToString(dataRow["CollectDate"])
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

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetInsureList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int num2 = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num3 = TypeUtil.ObjectToInt(base.Request["SearchType"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			string safeSQL = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["KeyWord"]));
			int num4 = TypeUtil.ObjectToInt(base.Request["Type"]);
			int num5 = TypeUtil.ObjectToInt(base.Request["TradeType"]);
			switch (num4)
			{
			case 1:
			{
				if (!string.IsNullOrEmpty(safeSQL))
				{
					int result = 0;
					switch (num3)
					{
					case 1:
						stringBuilder.AppendFormat(" AND SourceUserID={0}", TypeUtil.GetUserIDByAccount(safeSQL));
						break;
					case 2:
						if (int.TryParse(safeSQL, out result))
						{
							stringBuilder.AppendFormat(" AND SourceUserID={0}", TypeUtil.GetUserIDByGameID(result));
						}
						else
						{
							stringBuilder.Append(" AND 1=2 ");
						}
						break;
					case 3:
						stringBuilder.AppendFormat(" AND TargetUserID={0}", TypeUtil.GetUserIDByAccount(safeSQL));
						break;
					case 4:
						if (int.TryParse(safeSQL, out result))
						{
							stringBuilder.AppendFormat(" AND TargetUserID={0}", TypeUtil.GetUserIDByGameID(result));
						}
						else
						{
							stringBuilder.Append(" AND 1=2 ");
						}
						break;
					case 5:
						if (int.TryParse(safeSQL, out result))
						{
							stringBuilder.AppendFormat(" AND KindID={0}", result);
						}
						else
						{
							stringBuilder.Append(" AND 1=2 ");
						}
						break;
					case 6:
						if (int.TryParse(safeSQL, out result))
						{
							stringBuilder.AppendFormat(" AND ServerID={0}", result);
						}
						else
						{
							stringBuilder.Append(" AND 1=2 ");
						}
						break;
					}
				}
				switch (num5)
				{
				case 1:
					stringBuilder.AppendFormat(" AND TradeType=1");
					break;
				case 2:
					stringBuilder.AppendFormat(" AND TradeType=2");
					break;
				case 3:
					stringBuilder.AppendFormat(" AND TradeType=3");
					break;
				}
				DateTime result2 = DateTime.Now;
				DateTime result3 = DateTime.Now;
				if (DateTime.TryParse(TypeUtil.ObjectToString(base.Request["StartDate"]), out result2) && DateTime.TryParse(TypeUtil.ObjectToString(base.Request["EndDate"]), out result3) && result3 >= result2)
				{
					stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", result2.ToString("yyyy-MM-dd"), result3.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			}
			case 2:
			{
				string arg = Fetch.GetTodayTime().Split('$')[0].ToString();
				string arg2 = Fetch.GetTodayTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", arg, arg2);
				break;
			}
			case 3:
			{
				string arg = Fetch.GetYesterdayTime().Split('$')[0].ToString();
				string arg2 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", arg, arg2);
				break;
			}
			case 4:
			{
				string arg = Fetch.GetWeekTime().Split('$')[0].ToString();
				string arg2 = Fetch.GetWeekTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", arg, arg2);
				break;
			}
			case 5:
			{
				string arg = Fetch.GetLastWeekTime().Split('$')[0].ToString();
				string arg2 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND CollectDate >= '{0}' AND CollectDate < '{1}'", arg, arg2);
				break;
			}
			}
			PagerSet recordInsureList = FacadeManage.aideTreasureFacade.GetRecordInsureList(num, num2, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (recordInsureList != null && recordInsureList.PageSet != null && recordInsureList.PageSet.Tables.Count > 0)
			{
				for (int i = 0; i < recordInsureList.PageSet.Tables[0].Rows.Count; i++)
				{
					DataRow dataRow = recordInsureList.PageSet.Tables[0].Rows[i];
					list.Add(new
					{
						SNO = num2 * (num - 1) + (i + 1),
						CollectDate = TypeUtil.ObjectToString(dataRow["CollectDate"]),
						SourceUserID = TypeUtil.ObjectToInt(dataRow["SourceUserID"]),
						Accounts = TypeUtil.GetAccounts(TypeUtil.ObjectToInt(dataRow["SourceUserID"])),
						TargetUserID = TypeUtil.ObjectToInt(dataRow["TargetUserID"]),
						TargetAccounts = ((TypeUtil.ObjectToInt(dataRow["TargetUserID"]) == 3) ? TypeUtil.GetAccounts(TypeUtil.ObjectToInt(dataRow["TargetUserID"])) : ""),
						TradeType = ((TypeUtil.ObjectToInt(dataRow["TradeType"]) == 1) ? "存款" : ((TypeUtil.ObjectToInt(dataRow["TradeType"]) == 2) ? "取款" : "转账")),
						SourceGold = TypeUtil.ObjectToLong(dataRow["SourceGold"]).ToString("NO"),
						SourceBank = TypeUtil.ObjectToLong(dataRow["SourceBank"]).ToString("NO"),
						TargetGold = ((TypeUtil.ObjectToInt(dataRow["TradeType"]) == 3) ? TypeUtil.ObjectToLong(dataRow["TargetGold"]).ToString("NO") : ""),
						TargetBank = ((TypeUtil.ObjectToInt(dataRow["TradeType"]) == 3) ? TypeUtil.ObjectToLong(dataRow["TargetBank"]).ToString("NO") : ""),
						SwapScore = TypeUtil.ObjectToLong(dataRow["SwapScore"]).ToString("NO"),
						Revenue = TypeUtil.ObjectToLong(dataRow["Revenue"]).ToString("NO"),
						IsGamePlaza = ((TypeUtil.ObjectToInt(dataRow["IsGamePlaza"]) == 0) ? "大厅" : "网页"),
						ClientIP = TypeUtil.ObjectToString(dataRow["ClientIP"]),
						Address = TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(dataRow["ClientIP"])),
						GameKindName = TypeUtil.GetGameKindName(TypeUtil.ObjectToInt(dataRow["KindID"])),
						GameRoomName = TypeUtil.GetGameRoomName(TypeUtil.ObjectToInt(dataRow["ServerID"])),
						CollectNote = TypeUtil.ObjectToString(dataRow["CollectNote"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = recordInsureList.RecordCount,
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public JsonResult GetConfineContentList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int num2 = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			TypeUtil.ObjectToInt(base.Request["SearchType"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			string safeSQL = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["KeyWord"]));
			int num3 = TypeUtil.ObjectToInt(base.Request["IsLike"]);
			if (!string.IsNullOrEmpty(safeSQL))
			{
				if (num3 == 1)
				{
					stringBuilder.AppendFormat(" AND String LIKE '%{0}%' ", safeSQL);
				}
				else
				{
					stringBuilder.AppendFormat(" AND String = '{0}' ", safeSQL);
				}
			}
			PagerSet confineContentList = FacadeManage.aideAccountsFacade.GetConfineContentList(num, num2, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (confineContentList != null && confineContentList.PageSet != null && confineContentList.PageSet.Tables.Count > 0)
			{
				for (int i = 0; i < confineContentList.PageSet.Tables[0].Rows.Count; i++)
				{
					DataRow dataRow = confineContentList.PageSet.Tables[0].Rows[i];
					list.Add(new
					{
						ContentID = TypeUtil.ObjectToInt(dataRow["ContentID"]),
						SNO = num2 * (num - 1) + (i + 1),
						String = TypeUtil.ObjectToString(dataRow["String"]),
						EnjoinOverDate = ((TypeUtil.ObjectToString(dataRow["EnjoinOverDate"]) == "") ? "永久限制" : TypeUtil.ObjectToDateTime(dataRow["EnjoinOverDate"]).ToString("yyyy-MM-dd HH:mm:ss")),
						CollectDate = TypeUtil.ObjectToDateTime(dataRow["CollectDate"]).ToString("yyyy-MM-dd HH:mm:ss")
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = confineContentList.RecordCount,
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public JsonResult DelConfineContent()
		{
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			if (!string.IsNullOrEmpty(text))
			{
				if (user != null)
				{
					AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
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
				string[] array = text.Split(',');
				StringBuilder stringBuilder = new StringBuilder();
				string[] array2 = array;
				foreach (string s in array2)
				{
					int result = 0;
					if (int.TryParse(s, out result))
					{
						stringBuilder.AppendFormat("'{0}',", result);
					}
				}
				string sqlQuery = "WHERE ContentID in (" + stringBuilder.ToString().TrimEnd(',') + ")";
				try
				{
					FacadeManage.aideAccountsFacade.DeleteConfineContent(sqlQuery);
					return Json(new
					{
						IsOk = true,
						Msg = "删除成功"
					});
				}
				catch
				{
					return Json(new
					{
						IsOk = false,
						Msg = "删除失败"
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "没有参数"
			});
		}

		[CheckCustomer]
		public JsonResult AddConfineContentInfo()
		{
			string text = TypeUtil.ObjectToString(base.Request["String"]);
			if (string.IsNullOrEmpty(text))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "限制字符不能为空"
				});
			}
			string text2 = TypeUtil.ObjectToString(base.Request["EnjoinOverDate"]);
			ConfineContent confineContent = new ConfineContent();
			DateTime dateTime = Convert.ToDateTime("1900-01-01");
			confineContent.String = text;
			confineContent.EnjoinOverDate = ((text2 == "") ? dateTime : Convert.ToDateTime(text2));
			int num = TypeUtil.ObjectToInt(base.Request["IntParam"]);
			Message message = new Message();
			if (num == 0)
			{
				if (user != null)
				{
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
					message = FacadeManage.aideAccountsFacade.InsertConfineContent(confineContent);
				}
			}
			else
			{
				if (user != null)
				{
					AdminPermission adminPermission2 = new AdminPermission(user, user.MoudleID);
					if (!adminPermission2.GetPermission(4L))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "没有权限",
							Url = "/NoPower/Index"
						});
					}
				}
				ConfineContent confineContentByContentID = FacadeManage.aideAccountsFacade.GetConfineContentByContentID(num);
				if (confineContentByContentID == null)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "不存在ID=" + num + "的限制字符"
					});
				}
				confineContentByContentID.String = text;
				confineContentByContentID.EnjoinOverDate = confineContent.EnjoinOverDate;
				message = FacadeManage.aideAccountsFacade.UpdateConfineContent(confineContentByContentID);
			}
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
		public JsonResult GetConfineAddressList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int num2 = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			TypeUtil.ObjectToInt(base.Request["SearchType"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			string safeSQL = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["KeyWord"]));
			int num3 = TypeUtil.ObjectToInt(base.Request["IsLike"]);
			if (!string.IsNullOrEmpty(safeSQL))
			{
				if (num3 == 1)
				{
					stringBuilder.AppendFormat(" AND AddrString LIKE '%{0}%' ", safeSQL);
				}
				else
				{
					stringBuilder.AppendFormat(" AND AddrString = '{0}' ", safeSQL);
				}
			}
			PagerSet confineAddressList = FacadeManage.aideAccountsFacade.GetConfineAddressList(num, num2, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (confineAddressList != null && confineAddressList.PageSet != null && confineAddressList.PageSet.Tables.Count > 0)
			{
				for (int i = 0; i < confineAddressList.PageSet.Tables[0].Rows.Count; i++)
				{
					DataRow dataRow = confineAddressList.PageSet.Tables[0].Rows[i];
					list.Add(new
					{
						AddrString = TypeUtil.ObjectToString(dataRow["AddrString"]),
						SNO = num2 * (num - 1) + (i + 1),
						EnjoinLogon = TypeUtil.ObjectToBool(dataRow["EnjoinLogon"]),
						EnjoinRegister = TypeUtil.ObjectToBool(dataRow["EnjoinRegister"]),
						EnjoinOverDate = ((TypeUtil.ObjectToString(dataRow["EnjoinOverDate"]) == "") ? "永久限制" : TypeUtil.ObjectToDateTime(dataRow["EnjoinOverDate"]).ToString("yyyy-MM-dd HH:mm:ss")),
						CollectDate = TypeUtil.ObjectToDateTime(dataRow["CollectDate"]).ToString("yyyy-MM-dd HH:mm:ss"),
						CollectNote = TypeUtil.ObjectToString(dataRow["CollectNote"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = confineAddressList.RecordCount,
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public JsonResult DelConfineAddrContent()
		{
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			if (!string.IsNullOrEmpty(text))
			{
				if (user != null)
				{
					AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
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
				string[] array = text.Split(',');
				StringBuilder stringBuilder = new StringBuilder();
				string[] array2 = array;
				foreach (string text2 in array2)
				{
					if (!string.IsNullOrEmpty(text2))
					{
						stringBuilder.AppendFormat("'{0}',", text2);
					}
				}
				string sqlQuery = "WHERE AddrString in (" + stringBuilder.ToString().TrimEnd(',') + ")";
				try
				{
					FacadeManage.aideAccountsFacade.DeleteConfineAddress(sqlQuery);
					return Json(new
					{
						IsOk = true,
						Msg = "删除成功"
					});
				}
				catch
				{
					return Json(new
					{
						IsOk = false,
						Msg = "删除失败"
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "没有参数"
			});
		}

		[CheckCustomer]
		public JsonResult AddConfineAddressInfo()
		{
			string text = TypeUtil.ObjectToString(base.Request["String"]);
			if (string.IsNullOrEmpty(text))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "限制IP地址不能为空"
				});
			}
			string text2 = TypeUtil.ObjectToString(base.Request["EnjoinOverDate"]);
			string content = TypeUtil.ObjectToString(base.Request["CollectNote"]);
			int num = TypeUtil.ObjectToInt(base.Request["EnjoinLogon"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["EnjoinRegister"]);
			Game.Entity.Accounts.ConfineAddress confineAddress = new Game.Entity.Accounts.ConfineAddress();
			DateTime dateTime = Convert.ToDateTime("1900-01-01");
			confineAddress.AddrString = text;
			confineAddress.EnjoinLogon = ((num == 1) ? true : false);
			confineAddress.EnjoinRegister = ((num2 == 1) ? true : false);
			confineAddress.EnjoinOverDate = ((text2 == "") ? dateTime : Convert.ToDateTime(text2));
			confineAddress.CollectNote = TextFilter.FilterAll(content);
			string text3 = TypeUtil.ObjectToString(base.Request["Param"]);
			Message message = new Message();
			if (text3 == "")
			{
				if (user != null)
				{
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
				}
				message = FacadeManage.aideAccountsFacade.InsertConfineAddress(confineAddress);
			}
			else
			{
				if (user != null)
				{
					AdminPermission adminPermission2 = new AdminPermission(user, user.MoudleID);
					if (!adminPermission2.GetPermission(4L))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "没有权限",
							Url = "/NoPower/Index"
						});
					}
				}
				Game.Entity.Accounts.ConfineAddress confineAddressByAddress = FacadeManage.aideAccountsFacade.GetConfineAddressByAddress(text3);
				if (confineAddressByAddress == null)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "不存在ID=" + text3 + "的IP限制字符"
					});
				}
				confineAddressByAddress.AddrString = text;
				confineAddressByAddress.EnjoinOverDate = confineAddress.EnjoinOverDate;
				confineAddressByAddress.CollectDate = confineAddress.CollectDate;
				confineAddressByAddress.CollectNote = confineAddress.CollectNote;
				confineAddressByAddress.EnjoinLogon = confineAddress.EnjoinLogon;
				confineAddressByAddress.EnjoinRegister = confineAddress.EnjoinRegister;
				message = FacadeManage.aideAccountsFacade.UpdateConfineAddress(confineAddressByAddress);
			}
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
		public JsonResult AddConfineAddressTop()
		{
			string value = TypeUtil.ObjectToString(base.Request["cid"]);
			if (string.IsNullOrEmpty(value))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "限制IP地址不能为空"
				});
			}
			if (user != null)
			{
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
			}
			new StringBuilder();
			string formString = GameRequest.GetFormString("cid");
			FacadeManage.aideAccountsFacade.BatchInsertConfineAddress(formString);
			return Json(new
			{
				IsOk = true,
				Msg = "操作成功"
			});
		}

		[CheckCustomer]
		public JsonResult GetConfineMachineList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int num2 = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			TypeUtil.ObjectToInt(base.Request["SearchType"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			string safeSQL = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["KeyWord"]));
			int num3 = TypeUtil.ObjectToInt(base.Request["IsLike"]);
			if (!string.IsNullOrEmpty(safeSQL))
			{
				if (num3 == 1)
				{
					stringBuilder.AppendFormat(" AND MachineSerial LIKE '%{0}%' ", safeSQL);
				}
				else
				{
					stringBuilder.AppendFormat(" AND MachineSerial = '{0}' ", safeSQL);
				}
			}
			PagerSet confineMachineList = FacadeManage.aideAccountsFacade.GetConfineMachineList(num, num2, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (confineMachineList != null && confineMachineList.PageSet != null && confineMachineList.PageSet.Tables.Count > 0)
			{
				for (int i = 0; i < confineMachineList.PageSet.Tables[0].Rows.Count; i++)
				{
					DataRow dataRow = confineMachineList.PageSet.Tables[0].Rows[i];
					list.Add(new
					{
						MachineSerial = TypeUtil.ObjectToString(dataRow["MachineSerial"]),
						SNO = num2 * (num - 1) + (i + 1),
						EnjoinLogon = TypeUtil.ObjectToBool(dataRow["EnjoinLogon"]),
						EnjoinRegister = TypeUtil.ObjectToBool(dataRow["EnjoinRegister"]),
						EnjoinOverDate = ((TypeUtil.ObjectToString(dataRow["EnjoinOverDate"]) == "") ? "永久限制" : TypeUtil.ObjectToDateTime(dataRow["EnjoinOverDate"]).ToString("yyyy-MM-dd HH:mm:ss")),
						CollectDate = TypeUtil.ObjectToDateTime(dataRow["CollectDate"]).ToString("yyyy-MM-dd HH:mm:ss"),
						CollectNote = TypeUtil.ObjectToString(dataRow["CollectNote"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = confineMachineList.RecordCount,
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public JsonResult DelConfineMachine()
		{
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			if (!string.IsNullOrEmpty(text))
			{
				if (user != null)
				{
					AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
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
				string[] array = text.Split(',');
				StringBuilder stringBuilder = new StringBuilder();
				string[] array2 = array;
				foreach (string text2 in array2)
				{
					if (!string.IsNullOrEmpty(text2))
					{
						stringBuilder.AppendFormat("'{0}',", text2);
					}
				}
				string sqlQuery = "WHERE MachineSerial in (" + stringBuilder.ToString().TrimEnd(',') + ")";
				try
				{
					FacadeManage.aideAccountsFacade.DeleteConfineMachine(sqlQuery);
					return Json(new
					{
						IsOk = true,
						Msg = "删除成功"
					});
				}
				catch
				{
					return Json(new
					{
						IsOk = false,
						Msg = "删除失败"
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "没有参数"
			});
		}

		[CheckCustomer]
		public JsonResult AddConfineMachineInfo()
		{
			string text = TypeUtil.ObjectToString(base.Request["String"]);
			if (string.IsNullOrEmpty(text))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "限制机器码不能为空"
				});
			}
			string s = TypeUtil.ObjectToString(base.Request["EnjoinOverDate"]);
			string content = TypeUtil.ObjectToString(base.Request["CollectNote"]);
			int num = TypeUtil.ObjectToInt(base.Request["EnjoinLogon"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["EnjoinRegister"]);
			Game.Entity.Accounts.ConfineMachine confineMachine = new Game.Entity.Accounts.ConfineMachine();
			DateTime result = TypeUtil.StringToDateTime("1900-01-01");
			confineMachine.MachineSerial = text;
			confineMachine.EnjoinLogon = ((num == 1) ? true : false);
			confineMachine.EnjoinRegister = ((num2 == 1) ? true : false);
			confineMachine.EnjoinOverDate = (DateTime.TryParse(s, out result) ? result : result);
			confineMachine.CollectNote = TextFilter.FilterAll(content);
			string text2 = TypeUtil.ObjectToString(base.Request["Param"]);
			Message message = new Message();
			if (text2 == "")
			{
				if (user != null)
				{
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
				}
				message = FacadeManage.aideAccountsFacade.InsertConfineMachine(confineMachine);
			}
			else
			{
				if (user != null)
				{
					AdminPermission adminPermission2 = new AdminPermission(user, user.MoudleID);
					if (!adminPermission2.GetPermission(4L))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "没有权限",
							Url = "/NoPower/Index"
						});
					}
				}
				Game.Entity.Accounts.ConfineMachine confineMachineBySerial = FacadeManage.aideAccountsFacade.GetConfineMachineBySerial(text2);
				if (confineMachineBySerial == null)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "不存在ID=" + text2 + "的IP限制字符"
					});
				}
				confineMachineBySerial.MachineSerial = text;
				confineMachineBySerial.EnjoinOverDate = confineMachine.EnjoinOverDate;
				confineMachineBySerial.CollectDate = confineMachine.CollectDate;
				confineMachineBySerial.CollectNote = confineMachine.CollectNote;
				confineMachineBySerial.EnjoinLogon = confineMachine.EnjoinLogon;
				confineMachineBySerial.EnjoinRegister = confineMachine.EnjoinRegister;
				message = FacadeManage.aideAccountsFacade.UpdateConfineMachine(confineMachineBySerial);
			}
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
		public JsonResult AddConfineMachineTop()
		{
			string value = TypeUtil.ObjectToString(base.Request["cid"]);
			if (string.IsNullOrEmpty(value))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "限制IP地址不能为空"
				});
			}
			if (user != null)
			{
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
			}
			new StringBuilder();
			string formString = GameRequest.GetFormString("cid");
			FacadeManage.aideAccountsFacade.BatchInsertConfineMachine(formString);
			return Json(new
			{
				IsOk = true,
				Msg = "操作成功"
			});
		}

		[CheckCustomer]
		public JsonResult GetAccountsLossReportList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int num2 = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY ReportDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (!string.IsNullOrEmpty(TypeUtil.ObjectToString(base.Request["StartDate"])) && !string.IsNullOrEmpty(TypeUtil.ObjectToString(base.Request["EndDate"])))
			{
				DateTime result = DateTime.Now;
				DateTime result2 = DateTime.Now;
				if (DateTime.TryParse(TypeUtil.ObjectToString(base.Request["StartDate"]), out result) && DateTime.TryParse(TypeUtil.ObjectToString(base.Request["EndDate"]), out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND ReportDate>='{0}' AND ReportDate<'{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				else
				{
					stringBuilder.Append(" AND 1=2 ");
				}
			}
			PagerSet list = FacadeManage.aideNativeWebFacade.GetList("LossReport", num, num2, stringBuilder.ToString(), orderby);
			List<object> list2 = new List<object>();
			if (list != null && list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				for (int i = 0; i < list.PageSet.Tables[0].Rows.Count; i++)
				{
					DataRow dataRow = list.PageSet.Tables[0].Rows[i];
					list2.Add(new
					{
						ProcessStatus = TypeUtil.GetProcess(TypeUtil.ObjectToString(dataRow["ProcessStatus"])),
						SNO = num2 * (num - 1) + (i + 1),
						ReportID = TypeUtil.ObjectToString(dataRow["ReportID"]),
						ReportNo = TypeUtil.ObjectToString(dataRow["ReportNo"]),
						Accounts = TypeUtil.ObjectToString(dataRow["Accounts"]),
						UserID = TypeUtil.ObjectToString(dataRow["UserID"]),
						GameID = TypeUtil.ObjectToString(dataRow["GameID"]),
						ReportEmail = TypeUtil.ObjectToString(dataRow["ReportEmail"]),
						ReportIP = TypeUtil.ObjectToString(dataRow["ReportIP"]),
						ReportDate = TypeUtil.ObjectToString(dataRow["ReportDate"])
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
		public JsonResult SendSuccessEmail()
		{
			if (user != null)
			{
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(131072L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
			}
			int num = TypeUtil.ObjectToInt(base.Request["ReportId"]);
			if (num != 0)
			{
				LossReport lossReport = FacadeManage.aideNativeWebFacade.GetLossReport(num);
				FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(lossReport.UserID);
				string field = FacadeManage.aideNativeWebFacade.GetConfigInfo(EnumerationList.SiteConfigKey.SiteConfig.ToString()).Field2;
				string arg = TextEncrypt.MD5EncryptPassword(lossReport.ReportNo + lossReport.UserID + lossReport.ReportDate.ToString().Trim() + lossReport.Random + ApplicationSettings.Get("ReportForgetPasswordKey"));
				string value = Utility.UrlEncode(field + string.Format("/Member/Complaint-Setp-4.aspx?param={0}&sign={1}&test=test", lossReport.ReportNo, arg));
				ConfigInfo configInfo = FacadeManage.aideNativeWebFacade.GetConfigInfo(EnumerationList.SiteConfigKey.EmailConfig.ToString());
				MailConfigInfo mailConfigInfo = new MailConfigInfo();
				mailConfigInfo.Accounts = configInfo.Field1.Trim();
				mailConfigInfo.Password = configInfo.Field2.Trim();
				mailConfigInfo.Port = Convert.ToInt32(configInfo.Field4);
				mailConfigInfo.SmtpServer = configInfo.Field3.Trim();
				mailConfigInfo.SmtpSenderEmail = configInfo.Field1.Trim();
				mailConfigInfo.LossreportUrl = "";
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				dictionary.Add("reportNO", lossReport.ReportNo);
				dictionary.Add("userName", lossReport.Accounts);
				dictionary.Add("url", value);
				dictionary.Add("mail", configInfo.Field1);
				dictionary.Add("sitename", FacadeManage.aideNativeWebFacade.GetConfigInfo(EnumerationList.SiteConfigKey.SiteConfig.ToString()).Field1);
				dictionary.Add("reason", "");
				string text = DefaultConfigFileManager.ConfigFilePath = TextUtility.GetFullPath("/App_Data/lossReportSuccess.config");
				MailTMLConfigInfo tmlConfig = new MailTMLConfigInfo(TMLForgetConfigManager.LoadConfig().MailContent.Text, TMLForgetConfigManager.LoadConfig().MailTitle);
				EmailForgetPassword emailForgetPassword = new EmailForgetPassword(mailConfigInfo, tmlConfig, dictionary);
				try
				{
					emailForgetPassword.Send(lossReport.ReportEmail);
					lossReport.ProcessStatus = 1;
					FacadeManage.aideNativeWebFacade.UpdateLossReport(lossReport);
					return Json(new
					{
						IsOk = true,
						Msg = "成功发送“申诉成功”邮件"
					});
				}
				catch (Exception)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "邮件发送失败"
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "邮件发送失败"
			});
		}

		[CheckCustomer]
		public JsonResult AddAccountsUpdatePass()
		{
			int userID = TypeUtil.ObjectToInt(base.Request["UserID"]);
			string text = TypeUtil.ObjectToString(base.Request["LoginPass"]);
			string b = TypeUtil.ObjectToString(base.Request["ReLoginPass"]);
			string text2 = TypeUtil.ObjectToString(base.Request["InsurePass"]);
			string b2 = TypeUtil.ObjectToString(base.Request["ReInsurePass"]);
			AccountsInfo accountsInfo = new AccountsInfo();
			accountsInfo.UserID = userID;
			if (!string.IsNullOrEmpty(text))
			{
				if (text != b)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "两次输入的登陆密码不一致"
					});
				}
				accountsInfo.LogonPass = Utility.MD5(text);
			}
			if (!string.IsNullOrEmpty(text2))
			{
				if (text2 != b2)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "两次输入的银行密码不一致"
					});
				}
				accountsInfo.InsurePass = Utility.MD5(text2);
			}
			if (string.IsNullOrEmpty(text) && string.IsNullOrEmpty(text2))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "未修改任何数据"
				});
			}
			if (FacadeManage.aideAccountsFacade.UpdateUserPassword(accountsInfo))
			{
				return Json(new
				{
					IsOk = true,
					Msg = "更新成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "更新失败"
			});
		}

		[CheckCustomer]
		public JsonResult GetRecordAccountsExpendList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int num2 = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num3 = TypeUtil.ObjectToInt(base.Request["UserID"]);
			int num4 = TypeUtil.ObjectToInt(base.Request["Type"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			AccountsInfo accountInfoByUserID = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(num3);
			PagerSet pagerSet = null;
			List<object> list = new List<object>();
			if (accountInfoByUserID != null)
			{
				stringBuilder.AppendFormat(" AND UserID={0} AND Type={1}", num3, num4);
				pagerSet = FacadeManage.aideRecordFacade.GetRecordAccountsExpendList(num, num2, stringBuilder.ToString(), orderby);
				if (pagerSet != null && pagerSet.PageSet != null && pagerSet.PageSet.Tables.Count > 0)
				{
					for (int i = 0; i < pagerSet.PageSet.Tables[0].Rows.Count; i++)
					{
						DataRow dataRow = pagerSet.PageSet.Tables[0].Rows[i];
						list.Add(new
						{
							SNO = num2 * (num - 1) + (i + 1),
							CollectDate = TypeUtil.ObjectToDateTime(dataRow["CollectDate"]).ToString("yyyy-MM-dd hh:mm:ss"),
							ClientIP = TypeUtil.ObjectToString(dataRow["ClientIP"]),
							Address = TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(dataRow["ClientIP"])),
							ReAccounts = TypeUtil.ObjectToString(dataRow["ReAccounts"]),
							MasterName = TypeUtil.GetMasterName(TypeUtil.ObjectToInt(dataRow["OperMasterID"]))
						});
					}
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((pagerSet != null) ? pagerSet.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public JsonResult GetRecordPasswExpendList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int num2 = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num3 = TypeUtil.ObjectToInt(base.Request["UserID"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			AccountsInfo accountInfoByUserID = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(num3);
			PagerSet pagerSet = null;
			List<object> list = new List<object>();
			if (accountInfoByUserID != null)
			{
				stringBuilder.AppendFormat(" AND UserID={0}", num3);
				pagerSet = FacadeManage.aideRecordFacade.GetRecordPasswdExpendList(num, num2, stringBuilder.ToString(), orderby);
				if (pagerSet != null && pagerSet.PageSet != null && pagerSet.PageSet.Tables.Count > 0)
				{
					for (int i = 0; i < pagerSet.PageSet.Tables[0].Rows.Count; i++)
					{
						DataRow dataRow = pagerSet.PageSet.Tables[0].Rows[i];
						list.Add(new
						{
							RecordID = TypeUtil.ObjectToInt(dataRow["RecordID"]),
							ReLogonPasswd = TypeUtil.ObjectToString(dataRow["ReLogonPasswd"]),
							SNO = num2 * (num - 1) + (i + 1),
							CollectDate = TypeUtil.ObjectToDateTime(dataRow["CollectDate"]).ToString("yyyy-MM-dd hh:mm:ss"),
							ClientIP = TypeUtil.ObjectToString(dataRow["ClientIP"]),
							Address = TypeUtil.GetAddressWithIP(TypeUtil.ObjectToString(dataRow["ClientIP"])),
							ReInsurePasswd = TypeUtil.ObjectToString(dataRow["ReInsurePasswd"]),
							MasterName = TypeUtil.GetMasterName(TypeUtil.ObjectToInt(dataRow["OperMasterID"]))
						});
					}
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((pagerSet != null) ? pagerSet.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public JsonResult AddRecordPasswdExpendConfirm()
		{
			int rid = TypeUtil.ObjectToInt(base.Request["RecordID"]);
			int type = TypeUtil.ObjectToInt(base.Request["IntPassType"]);
			string text = TypeUtil.ObjectToString(base.Request["strPass"]);
			if (string.IsNullOrEmpty(text))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "密码不能为空"
				});
			}
			if (FacadeManage.aideRecordFacade.ConfirmPass(rid, Utility.MD5(text), type))
			{
				return Json(new
				{
					IsOk = true,
					Msg = "确认成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "确认失败"
			});
		}

		[ValidateInput(false)]
		[CheckCustomer]
		public JsonResult SubmitSendLossReportInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["ReportId"]);
			if (user != null)
			{
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(131072L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
			}
			if (num > 0)
			{
				LossReport lossReport = FacadeManage.aideNativeWebFacade.GetLossReport(num);
				FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(lossReport.UserID);
				string field = FacadeManage.aideNativeWebFacade.GetConfigInfo(EnumerationList.SiteConfigKey.SiteConfig.ToString()).Field2;
				ConfigInfo configInfo = FacadeManage.aideNativeWebFacade.GetConfigInfo(EnumerationList.SiteConfigKey.EmailConfig.ToString());
				MailConfigInfo mailConfigInfo = new MailConfigInfo();
				mailConfigInfo.Accounts = configInfo.Field1.Trim();
				mailConfigInfo.Password = configInfo.Field2.Trim();
				mailConfigInfo.Port = Convert.ToInt32(configInfo.Field4);
				mailConfigInfo.SmtpServer = configInfo.Field3.Trim();
				mailConfigInfo.SmtpSenderEmail = configInfo.Field1.Trim();
				mailConfigInfo.LossreportUrl = "";
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				dictionary.Add("reportNO", lossReport.ReportNo);
				dictionary.Add("userName", lossReport.Accounts);
				dictionary.Add("mail", configInfo.Field1);
				dictionary.Add("url", "");
				dictionary.Add("sitename", FacadeManage.aideNativeWebFacade.GetConfigInfo(EnumerationList.SiteConfigKey.SiteConfig.ToString()).Field1);
				dictionary.Add("reason", TypeUtil.ObjectToString(base.Request["Reason"]));
				string text = DefaultConfigFileManager.ConfigFilePath = TextUtility.GetFullPath("/App_Data/lossReportFailure.config");
				MailTMLConfigInfo tmlConfig = new MailTMLConfigInfo(TMLForgetConfigManager.LoadConfig().MailContent.Text, TMLForgetConfigManager.LoadConfig().MailTitle);
				EmailForgetPassword emailForgetPassword = new EmailForgetPassword(mailConfigInfo, tmlConfig, dictionary);
				try
				{
					emailForgetPassword.Send(lossReport.ReportEmail);
					lossReport.ProcessStatus = 2;
					FacadeManage.aideNativeWebFacade.UpdateLossReport(lossReport);
					return Json(new
					{
						IsOk = true,
						Msg = "成功发送“申诉失败”邮件"
					});
				}
				catch (Exception)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "邮件发送失败"
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "邮件发送失败"
			});
		}

		[CheckCustomer]
		public string GetGameList()
		{
			DataTable dataTable = new DataTable();
			Cache cache = HttpRuntime.Cache;
			if (cache["GameList"] == null)
			{
				dataTable = FacadeManage.aidePlatformFacade.GameList();
				CacheHelper.AddCache("GameList", dataTable);
			}
			else
			{
				dataTable = (cache["GameList"] as DataTable);
			}
			return JsonHelper.SerializeObject(dataTable);
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetOnlieListNew()
		{
			int @int = GameRequest.GetInt("pageIndex", 1);
			int int2 = GameRequest.GetInt("pageSize", 20);
			try
			{
				RequestMessage requestMessage = new RequestMessage(2);
				string text = requestMessage.Post();
				if (text.Contains("["))
				{
					List<UserOnline> list = JsonHelper.DeserializeJsonToList<UserOnline>(text);
					if (list.Count > 0)
					{
						int total = 0;
						if (list.Count <= 0)
						{
							return Json(new
							{
								IsOk = true,
								Msg = "",
								Total = total,
								Data = ""
							});
						}
						total = list.Count;
						string text2 = "";
						for (int i = 0; i < list.Count; i++)
						{
							text2 = text2 + list[i].UserID + ",";
						}
						text2 = text2.Remove(text2.Length - 1, 1);
						int formInt = GameRequest.GetFormInt("UserType", 0);
						int formInt2 = GameRequest.GetFormInt("TotalPay", 0);
						int formInt3 = GameRequest.GetFormInt("TotalWin", 0);
						int formInt4 = GameRequest.GetFormInt("TotalWinMax", 0);
						StringBuilder stringBuilder = new StringBuilder("WHERE UserID IN(" + text2 + ")");
						if (formInt > 0)
						{
							stringBuilder.AppendFormat(" AND UserType={0}", formInt);
						}
						if (formInt2 > 0)
						{
							stringBuilder.AppendFormat(" AND Amount>={0}", formInt2);
						}
						if (formInt3 != 0 && formInt4 != 0 && formInt4 < formInt3)
						{
							stringBuilder.AppendFormat(" AND (TotalScore>={0} OR TotalScore<={1}) ", formInt3, formInt4);
						}
						else if (formInt3 != 0)
						{
							stringBuilder.AppendFormat(" AND TotalScore>={0}", formInt3);
						}
						else if (formInt4 != 0)
						{
							stringBuilder.AppendFormat(" AND TotalScore<={0}", formInt4);
						}
						int formInt5 = GameRequest.GetFormInt("wType", 0);
						string safeSQL = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["wValue"]));
						int formInt6 = GameRequest.GetFormInt("kindID", -1);
						if (safeSQL != "")
						{
							switch (formInt5)
							{
							case 2:
								if (!TypeUtil.IsInteger(safeSQL))
								{
									return Json(new
									{
										IsOk = false,
										Msg = "玩家ID格式错误"
									});
								}
								stringBuilder.AppendFormat(" AND GameID={0}", safeSQL);
								break;
							case 3:
								stringBuilder.AppendFormat(" AND Accounts='{0}'", safeSQL);
								break;
							default:
								if (TypeUtil.IsInteger(safeSQL))
								{
									stringBuilder.AppendFormat(" AND (GameID={0} OR Accounts='{0}')", safeSQL);
								}
								else
								{
									stringBuilder.AppendFormat(" AND Accounts='{0}'", safeSQL);
								}
								break;
							}
						}
						if (formInt6 >= 0)
						{
							stringBuilder.AppendFormat(" AND KindID={0}", formInt6);
						}
						else if (formInt6 == -2)
						{
							stringBuilder.Append(" AND KindID>0");
						}
						string text3 = TypeUtil.ObjectToString(base.Request["order"]);
						string str = TypeUtil.ObjectToString(base.Request["sort"]);
						string orderby = "ORDER BY WebTypeID DESC";
						if (text3 != "")
						{
							orderby = "ORDER BY " + text3 + " " + str;
						}
						PagerSet list2 = FacadeManage.aideAccountsFacade.GetList("View_UserOnline", @int, int2, stringBuilder.ToString(), orderby);
						return Json(new
						{
							IsOk = true,
							Msg = "",
							Total = list2.RecordCount,
							Data = JsonHelper.SerializeObject(list2.PageSet.Tables[0])
						});
					}
				}
				if (!(text == "") && !text.Contains("null"))
				{
					return Json(new
					{
						IsOk = false,
						Msg = text
					});
				}
				return Json(new
				{
					IsOk = true,
					Msg = "",
					Total = 0,
					Data = ""
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
		public JsonResult GetOnlieList()
		{
			int formInt = GameRequest.GetFormInt("wType", 0);
			string safeSQL = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["wValue"]));
			int formInt2 = GameRequest.GetFormInt("kindID", -1);
			int @int = GameRequest.GetInt("pageIndex", 1);
			int int2 = GameRequest.GetInt("pageSize", 30);
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (!string.IsNullOrEmpty(safeSQL))
			{
				switch (formInt)
				{
				case 2:
					if (!TypeUtil.IsInteger(safeSQL))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "玩家ID格式错误"
						});
					}
					stringBuilder.AppendFormat(" AND GameID={0}", safeSQL);
					break;
				case 3:
					stringBuilder.AppendFormat(" AND Accounts = '{0}'", safeSQL);
					break;
				default:
					if (TypeUtil.IsInteger(safeSQL))
					{
						stringBuilder.AppendFormat(" AND (GameID={0} OR Accounts = '{0}')", safeSQL);
					}
					else
					{
						stringBuilder.AppendFormat(" AND Accounts = '{0}'", safeSQL);
					}
					break;
				}
			}
			if (formInt2 >= 0)
			{
				stringBuilder.AppendFormat(" AND KindID={0}", formInt2);
			}
			else if (formInt2 == -2)
			{
				stringBuilder.Append(" AND KindID>0");
			}
			PagerSet list = FacadeManage.aideAccountsFacade.GetList("View_Online", @int, int2, stringBuilder.ToString(), "ORDER BY TotalScore DESC");
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
		public JsonResult Unlock()
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
			if (user != null && !TypeUtil.IsPower(user.MoudleID, user.RoleID, "冻/解"))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有权限",
					Url = "/NoPower/Index"
				});
			}
			string[] array = text.Trim().Split(',');
			StringBuilder stringBuilder = new StringBuilder();
			string[] array2 = array;
			foreach (string text2 in array2)
			{
				int result = 0;
				if (int.TryParse(text2, out result))
				{
					stringBuilder.Append(text2 + ",");
				}
			}
			if (!string.IsNullOrEmpty(stringBuilder.ToString()))
			{
				string sqlQuery = "WHERE UserID in (" + stringBuilder.ToString().TrimEnd(',') + ")";
				try
				{
					FacadeManage.aideTreasureFacade.DeleteGameScoreLocker(sqlQuery);
					return Json(new
					{
						IsOk = true,
						Msg = "解锁成功"
					});
				}
				catch
				{
					return Json(new
					{
						IsOk = false,
						Msg = "解锁失败"
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "参数不正确"
			});
		}

		[CheckCustomer]
		public ActionResult RecordGlodList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			string text = TypeUtil.ObjectToString(base.Request["Account"]);
			base.ViewBag.Account = text;
			base.ViewBag.UserID = num;
			DateTime now = DateTime.Now;
			base.ViewBag.StartTime = now.AddDays(1.0).ToString("yyyy-MM-dd");
			base.ViewBag.EndTime = now.AddDays(-6.0).ToString("yyyy-MM-dd");
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetRecordGlodList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["UserID"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["TypeID"]);
			string text = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string text2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			string s = TypeUtil.ObjectToString(base.Request["Scorce"]);
			int result = 0;
			string value = TypeUtil.ObjectToString(base.Request["Account"]);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(" Where 1=1 ");
			if (num > 0)
			{
				stringBuilder.AppendFormat(" AND UserID={0} ", num);
			}
			else if (!string.IsNullOrEmpty(value))
			{
				value = FiltUtil.GetSafeSQL(value);
				stringBuilder.AppendFormat(" AND Accounts='{0}' ", value);
			}
			if (num2 > 0)
			{
				stringBuilder.AppendFormat(" AND TypeID={0} ", num2);
			}
			if (int.TryParse(s, out result))
			{
				stringBuilder.AppendFormat(" AND PresentScore>{0} ", result);
			}
			int num3 = TypeUtil.ObjectToInt(base.Request["Type"]);
			DateTime now = DateTime.Now;
			switch (num3)
			{
			case 2:
				text = Fetch.GetTodayTime().Split('$')[0].ToString();
				text2 = Fetch.GetTodayTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND CollectDate BETWEEN '{0}' AND '{1}'", text, text2);
				break;
			case 3:
				text = Fetch.GetYesterdayTime().Split('$')[0].ToString();
				text2 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND CollectDate BETWEEN '{0}' AND '{1}'", text, text2);
				break;
			case 4:
				text = Fetch.GetWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetWeekTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND CollectDate BETWEEN '{0}' AND '{1}'", text, text2);
				break;
			case 5:
				text = Fetch.GetLastWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
				stringBuilder.AppendFormat(" AND CollectDate BETWEEN '{0}' AND '{1}'", text, text2);
				break;
			case 6:
				text = now.ToString("yyyy-MM-01");
				text2 = now.ToString("yyyy-MM-dd 23:59:59");
				stringBuilder.AppendFormat(" AND CollectDate BETWEEN '{0}' AND '{1}'", text, text2);
				break;
			case 7:
				text = now.AddMonths(-1).ToString("yyyy-MM-01");
				text2 = now.ToString("yyyy-MM-01");
				stringBuilder.AppendFormat(" AND CollectDate BETWEEN '{0}' AND '{1}'", text, text2);
				break;
			default:
				if (text != "")
				{
					stringBuilder.AppendFormat(" AND CollectDate > '{0}'", Convert.ToDateTime(text));
				}
				if (text2 != "")
				{
					stringBuilder.AppendFormat(" AND CollectDate <= '{0}'", Convert.ToDateTime(text2));
				}
				break;
			}
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("View_PresentInfo", pageIndex, pageSize, stringBuilder.ToString(), "ORDER BY RecordID DESC");
			DataSet pageSet = list.PageSet;
			decimal sum = 0m;
			List<object> list2 = new List<object>();
			if (pageSet != null && pageSet.Tables.Count > 0 && pageSet.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in pageSet.Tables[0].Rows)
				{
					list2.Add(new
					{
						Account = TypeUtil.ObjectToString(row["Accounts"]),
						PreScore = TypeUtil.ObjectToDecimal(row["PreScore"]).ToString("N"),
						PreInsureScore = TypeUtil.ObjectToDecimal(row["PreInsureScore"]).ToString("N"),
						PresentScore = TypeUtil.ObjectToDecimal(row["PresentScore"]).ToString("N"),
						TypeName = TypeUtil.ObjectToString(row["TypeName"]),
						IPAddress = TypeUtil.ObjectToString(row["IPAddress"]),
						CollectDate = TypeUtil.ObjectToString(row["CollectDate"])
					});
				}
				sum = FacadeManage.aideTreasureFacade.TotalScore(stringBuilder.ToString());
			}
			return Json(new
			{
				IsOk = true,
				Msg = new
				{
					Sum = sum
				},
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list2)
			});
		}

		[CheckCustomer]
		public ActionResult BaseUserUpdate()
		{
			int num = TypeUtil.ObjectToInt(base.Request["id"]);
			if (user != null && num != user.UserID)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有权限",
					Url = "/NoPower/Index"
				});
			}
			Base_Users userByUserID = FacadeManage.aidePlatformManagerFacade.GetUserByUserID(num);
			base.ViewData["data"] = userByUserID;
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DoBaseUser()
		{
			string text = TypeUtil.ObjectToString(base.Request["OOldLogonPass"]);
			string text2 = TypeUtil.ObjectToString(base.Request["OldLogonPass"]);
			string text3 = TypeUtil.ObjectToString(base.Request["LogonPass"]);
			string text4 = TypeUtil.ObjectToString(base.Request["ConfirmLogonPass"]);
			if (string.IsNullOrEmpty(text))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "原密码不能为空"
				}, JsonRequestBehavior.AllowGet);
			}
			if (string.IsNullOrEmpty(text2))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "原密码不能为空"
				}, JsonRequestBehavior.AllowGet);
			}
			if (Utility.MD5(text2) != text)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "原密码输入错误"
				}, JsonRequestBehavior.AllowGet);
			}
			if (string.IsNullOrEmpty(text3))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "新密码不能为空"
				}, JsonRequestBehavior.AllowGet);
			}
			if (string.IsNullOrEmpty(text4))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "确认密码不能为空"
				}, JsonRequestBehavior.AllowGet);
			}
			if (text3 != text4)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "两次输入密码不一致"
				}, JsonRequestBehavior.AllowGet);
			}
			Base_Users base_Users = new Base_Users();
			base_Users.UserID = user.UserID;
			Message message = new Message();
			message = FacadeManage.aidePlatformManagerFacade.ModifyUserLogonPass(base_Users, text, text3);
			if (message.Success)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "密码修改成功！"
				}, JsonRequestBehavior.AllowGet);
			}
			return Json(new
			{
				IsOk = true,
				Msg = message.Content
			}, JsonRequestBehavior.AllowGet);
		}

		[CheckCustomer]
		public ViewResult SuperAccounts()
		{
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult AddSuperAccount(string account)
		{
			if (string.IsNullOrEmpty(account))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "账号不能为空"
				});
			}
			if (FacadeManage.aideAccountsFacade.SetSuperHao(account) > 0)
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
				Msg = "该用户名不存在"
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetSuperAccounts()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			PagerSet pagerSet = null;
			List<object> list = new List<object>();
			string[] fields = new string[4]
			{
				"UserID",
				"GameID",
				"Accounts",
				"NickName"
			};
			pagerSet = FacadeManage.aideAccountsFacade.GetList("AccountsInfo", pageIndex, pageSize, "Where UserRight & 536870912>0", "Order by UserID", fields);
			if (pagerSet != null && pagerSet.PageSet != null && pagerSet.PageSet.Tables.Count > 0)
			{
				for (int i = 0; i < pagerSet.PageSet.Tables[0].Rows.Count; i++)
				{
					DataRow dataRow = pagerSet.PageSet.Tables[0].Rows[i];
					list.Add(new
					{
						UserID = TypeUtil.ObjectToInt(dataRow["UserID"]),
						GameID = TypeUtil.ObjectToInt(dataRow["GameID"]),
						Accounts = TypeUtil.ObjectToString(dataRow["Accounts"]),
						NickName = TypeUtil.ObjectToString(dataRow["NickName"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((pagerSet != null) ? pagerSet.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		public JsonResult CancelSuperAccount(int UserID)
		{
			if (UserID < 1)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数出错"
				});
			}
			if (FacadeManage.aideAccountsFacade.QXSuperHao(UserID) > 0)
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
	}
}
