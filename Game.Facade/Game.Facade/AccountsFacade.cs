using Game.Data.Factory;
using Game.Entity.Accounts;
using Game.Facade.Tools;
using Game.IData;
using Game.Kernel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Game.Facade
{
	public class AccountsFacade
	{
		private IAccountsDataProvider aideAccountsData;

		public AccountsFacade()
		{
			aideAccountsData = ClassFactory.IAccountsDataProvider();
		}

		public PagerSet GetAccountsList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideAccountsData.GetAccountsList(pageIndex, pageSize, condition, orderby);
		}

		public void DongjieAccount(string sqlQuery)
		{
			aideAccountsData.DongjieAccount(sqlQuery);
		}

		public void SetTeshu(int type, string sqlQuery)
		{
			string sql = "UPDATE AccountsInfo SET UserType=" + type + sqlQuery;
			aideAccountsData.ExecuteSql(sql);
		}

		public void JieDongAccount(string sqlQuery)
		{
			aideAccountsData.JieDongAccount(sqlQuery);
		}

		public void SettingAndroid(string sqlQuery)
		{
			aideAccountsData.SettingAndroid(sqlQuery);
		}

		public void CancleAndroid(string sqlQuery)
		{
			aideAccountsData.CancleAndroid(sqlQuery);
		}

		public AccountsInfo GetAccountInfoByUserID(int userID)
		{
			return aideAccountsData.GetAccountInfoByUserID(userID);
		}

		public AccountsInfo GetAccountInfoByAccount(string account)
		{
			return aideAccountsData.GetAccountInfoByAccount(account);
		}

		public AccountsInfo GetAccountInfoByNickname(string nickname)
		{
			return aideAccountsData.GetAccountInfoByNickname(nickname);
		}

		public AccountsInfo GetAccountInfoByGameID(int gameID)
		{
			return aideAccountsData.GetAccountInfoByGameID(gameID);
		}

		public string GetAccountsByGameID(int gameId)
		{
			return aideAccountsData.GetAccountsByGameID(gameId);
		}

		public string GetAccountByUserID(int userID)
		{
			return aideAccountsData.GetAccountByUserID(userID);
		}

		public int GetExperienceByUserID(int userID)
		{
			return aideAccountsData.GetExperienceByUserID(userID);
		}

		public Message UpdateAccount(AccountsInfo account, int masterID, string clientIP)
		{
			return aideAccountsData.UpdateAccount(account, masterID, clientIP);
		}

		public Message AddAccount(AccountsInfo account, IndividualDatum datum)
		{
			return aideAccountsData.AddAccount(account, datum);
		}

		public bool AddUserMedal(int userId, int medal)
		{
			return aideAccountsData.AddUserMedal(userId, medal);
		}

		public bool UpdateUserPassword(AccountsInfo accountsInfo)
		{
			return aideAccountsData.UpdateUserPassword(accountsInfo);
		}

		public DataTable GetUserDetails(int uid)
		{
			string arg = "SELECT u.UserID,GameID,Accounts,,Compellation,Score,InsureScore";
			arg = arg + " FROM AccountsInfo u LEFT JOIN THTreasureDB.dbo.GameScoreInfo s ON u.UserID=s.UserID WHERE u.UserID=" + uid;
			return aideAccountsData.GetDataSetBySql(arg).Tables[0];
		}

		public DataTable GetUserOnline(string ids)
		{
			string sql = "SELECT * FROM View_UserOnline WHERE UserID IN(" + ids + ")";
			return aideAccountsData.GetDataSetBySql(sql).Tables[0];
		}

		public IndividualDatum GetAccountDetailByUserID(int userID)
		{
			return aideAccountsData.GetAccountDetailByUserID(userID);
		}

		public Message InsertAccountDetail(IndividualDatum accountDetail)
		{
			aideAccountsData.InsertAccountDetail(accountDetail);
			return new Message(true);
		}

		public Message UpdateAccountDetail(IndividualDatum accountDetail)
		{
			aideAccountsData.UpdateAccountDetail(accountDetail);
			return new Message(true);
		}

		public string GetUserFaceUrl(int faceID, int customID)
		{
			string empty = string.Empty;
			if (customID != 0)
			{
				AccountsFace accountsFace = GetAccountsFace(customID);
				if (accountsFace != null)
				{
					Random random = new Random();
					double num = random.NextDouble();
					return string.Format("/Tools/UserFace.ashx?customid={0}&x={1}", customID, num);
				}
				return string.Format("/gamepic/Avatar{0}.png", faceID);
			}
			return string.Format("/gamepic/Avatar{0}.png", faceID);
		}

		public DataSet GetAccountsFaceList(int userId)
		{
			return aideAccountsData.GetAccountsFaceList(userId);
		}

		public AccountsFace GetAccountsFace(int customId)
		{
			return aideAccountsData.GetAccountsFace(customId);
		}

		public PagerSet GetConfineContentList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideAccountsData.GetConfineContentList(pageIndex, pageSize, condition, orderby);
		}

		public ConfineContent GetConfineContentByContentID(int contentID)
		{
			return aideAccountsData.GetConfineContentByContentID(contentID);
		}

		public Message InsertConfineContent(ConfineContent content)
		{
			aideAccountsData.InsertConfineContent(content);
			return new Message(true);
		}

		public Message UpdateConfineContent(ConfineContent content)
		{
			aideAccountsData.UpdateConfineContent(content);
			return new Message(true);
		}

		public void DeleteConfineContentByContentID(int contentID)
		{
			aideAccountsData.DeleteConfineContentByContentID(contentID);
		}

		public void DeleteConfineContent(string sqlQuery)
		{
			aideAccountsData.DeleteConfineContent(sqlQuery);
		}

		public bool IsExitStringInConfineContent(string str)
		{
			return aideAccountsData.IsExitStringInConfineContent(str);
		}

		public AccountsProtect GetAccountsProtectByPID(int pid)
		{
			return aideAccountsData.GetAccountsProtectByPID(pid);
		}

		public AccountsProtect GetAccountsProtectByUserID(int uid)
		{
			return aideAccountsData.GetAccountsProtectByUserID(uid);
		}

		public Message UpdateAccountsProtect(AccountsProtect accountProtect)
		{
			aideAccountsData.UpdateAccountsProtect(accountProtect);
			return new Message(true);
		}

		public void DeleteAccountsProtect(int pid)
		{
			aideAccountsData.DeleteAccountsProtect(pid);
		}

		public void DeleteAccountsProtect(string sqlQuery)
		{
			aideAccountsData.DeleteAccountsProtect(sqlQuery);
		}

		public DataSet GetReserveIdentifierList()
		{
			return aideAccountsData.GetReserveIdentifierList();
		}

		public PagerSet GetConfineAddressList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideAccountsData.GetConfineAddressList(pageIndex, pageSize, condition, orderby);
		}

		public ConfineAddress GetConfineAddressByAddress(string strAddress)
		{
			return aideAccountsData.GetConfineAddressByAddress(strAddress);
		}

		public Message InsertConfineAddress(ConfineAddress address)
		{
			aideAccountsData.InsertConfineAddress(address);
			return new Message(true);
		}

		public Message UpdateConfineAddress(ConfineAddress address)
		{
			aideAccountsData.UpdateConfineAddress(address);
			return new Message(true);
		}

		public void DeleteConfineAddressByAddress(string strAddress)
		{
			aideAccountsData.DeleteConfineAddressByAddress(strAddress);
		}

		public void DeleteConfineAddress(string sqlQuery)
		{
			aideAccountsData.DeleteConfineAddress(sqlQuery);
		}

		public DataSet GetIPRegisterTop100()
		{
			return aideAccountsData.GetIPRegisterTop100();
		}

		public void BatchInsertConfineAddress(string ipList)
		{
			aideAccountsData.BatchInsertConfineAddress(ipList);
		}

		public PagerSet GetConfineMachineList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideAccountsData.GetConfineMachineList(pageIndex, pageSize, condition, orderby);
		}

		public ConfineMachine GetConfineMachineBySerial(string strSerial)
		{
			return aideAccountsData.GetConfineMachineBySerial(strSerial);
		}

		public Message InsertConfineMachine(ConfineMachine machine)
		{
			aideAccountsData.InsertConfineMachine(machine);
			return new Message(true);
		}

		public Message UpdateConfineMachine(ConfineMachine machine)
		{
			aideAccountsData.UpdateConfineMachine(machine);
			return new Message(true);
		}

		public void DeleteConfineMachineBySerial(string strSerial)
		{
			aideAccountsData.DeleteConfineMachineBySerial(strSerial);
		}

		public void DeleteConfineMachine(string sqlQuery)
		{
			aideAccountsData.DeleteConfineMachine(sqlQuery);
		}

		public DataSet GetMachineRegisterTop100()
		{
			return aideAccountsData.GetMachineRegisterTop100();
		}

		public void BatchInsertConfineMachine(string machineList)
		{
			aideAccountsData.BatchInsertConfineMachine(machineList);
		}

		public DataTable SystemCount()
		{
			return aideAccountsData.SystemCount();
		}

		public PagerSet GetSystemStatusInfoList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideAccountsData.GetSystemStatusInfoList(pageIndex, pageSize, condition, orderby);
		}

		public SystemStatusInfo GetSystemStatusInfo(string statusName)
		{
			return aideAccountsData.GetSystemStatusInfo(statusName);
		}

		public Message UpdateSystemStatusInfo(SystemStatusInfo systemStatusInfo)
		{
			aideAccountsData.UpdateSystemStatusInfo(systemStatusInfo);
			return new Message(true);
		}

		public int UpdateSystemStatusInfo(string key, decimal value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE SystemStatusInfo SET ").AppendFormat("StatusValue={0} ", value).AppendFormat("WHERE StatusName='{0}'", key);
			return aideAccountsData.ExecuteSql(stringBuilder.ToString());
		}

		public DataSet GetRegUserByDays(string startDate, string endDate)
		{
			return aideAccountsData.GetRegUserByDays(startDate, endDate);
		}

		public DataSet GetRegUserByHours(string startDate, string endDate)
		{
			return aideAccountsData.GetRegUserByHours(startDate, endDate);
		}

		public DataSet GetRegUserByMonth()
		{
			return aideAccountsData.GetRegUserByMonth();
		}

		public DataSet GetUsersStat()
		{
			return aideAccountsData.GetUsersStat();
		}

		public DataSet GetUsersNumber()
		{
			return aideAccountsData.GetUsersNumber();
		}

		public Message AppStatLogon(string accounts, string logonPass, string machineID)
		{
			return aideAccountsData.AppStatLogon(accounts, logonPass, machineID);
		}

		public Message AppStatUserActive(string accounts, string logonPass, string machineID)
		{
			return aideAccountsData.AppStatUserActive(accounts, logonPass, machineID);
		}

		public Message AppStatUserMember(string accounts, string logonPass, string machineID)
		{
			return aideAccountsData.AppStatUserMember(accounts, logonPass, machineID);
		}

		public Message AppGetLogonData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate)
		{
			return aideAccountsData.AppGetLogonData(accounts, logonPass, typeID, machineID, dateType, startDate, endDate);
		}

		public Message AppGetRegData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate)
		{
			return aideAccountsData.AppGetRegData(accounts, logonPass, typeID, machineID, dateType, startDate, endDate);
		}

		public ControlConfig GetControlConfig()
		{
			return aideAccountsData.GetControlConfig();
		}

		public void UpdateControlConfig(ControlConfig model)
		{
			aideAccountsData.UpdateControlConfig(model);
		}

		public void DeleteAccountsControl(string where)
		{
			aideAccountsData.DeleteAccountsControl(where);
		}

		public void AddAccountsControl(AccountsControl model)
		{
			aideAccountsData.AddAccountsControl(model);
		}

		public void UpdateAccountsControl(AccountsControl model)
		{
			aideAccountsData.UpdateAccountsControl(model);
		}

		public AccountsControl GetAccountsControl(int id)
		{
			return aideAccountsData.GetAccountsControl(id);
		}

		public void DongjieAgent(string sqlQuery)
		{
			aideAccountsData.DongjieAgent(sqlQuery);
		}

		public void JieDongAgent(string sqlQuery)
		{
			aideAccountsData.JieDongAgent(sqlQuery);
		}

		public AccountsAgent GetAccountAgentByUserID(int userID)
		{
			return aideAccountsData.GetAccountAgentByUserID(userID);
		}

		public AccountsAgent GetAccountAgentByDomain(string domain)
		{
			return aideAccountsData.GetAccountAgentByDomain(domain);
		}

		public Message AddAgent(AccountsAgent agent)
		{
			return aideAccountsData.AddAgent(agent);
		}

		public bool UpdateAgent(AccountsAgent agent)
		{
			return aideAccountsData.UpdateAgent(agent);
		}

		public int GetAgentChildCount(int userID)
		{
			return aideAccountsData.GetAgentChildCount(userID);
		}

		public AccountsAgentGame GetAccountsAgentGameInfo(int id)
		{
			return aideAccountsData.GetAccountsAgentGameInfo(id);
		}

		public Message InsertAccountsAgentGame(AccountsAgentGame model)
		{
			try
			{
				aideAccountsData.InsertAccountsAgentGame(model);
				return new Message(true);
			}
			catch (Exception ex)
			{
				return new Message(false, ex.Message);
			}
		}

		public Message UpdateAccountsAgentGame(AccountsAgentGame model)
		{
			try
			{
				aideAccountsData.UpdateAccountsAgentGame(model);
				return new Message(true);
			}
			catch (Exception ex)
			{
				return new Message(false, ex.Message);
			}
		}

		public Message DeleteAccountsAgentGame(string sqlQuery)
		{
			try
			{
				aideAccountsData.DeleteAccountsAgentGame(sqlQuery);
				return new Message(true);
			}
			catch (Exception ex)
			{
				return new Message(false, ex.Message);
			}
		}

		public DataTable GetPushConfig()
		{
			string sql = "SELECT AppKey,AppSecret FROM RyAgentDB.dbo.T_AgentIsIOS WHERE AppKey<>'' AND AppSecret<>''";
			return aideAccountsData.GetDataSetBySql(sql).Tables[0];
		}

		public DataSet GetMemberPropertyList()
		{
			return aideAccountsData.GetMemberPropertyList();
		}

		public MemberProperty GetMemberProperty(int memberOrder)
		{
			return aideAccountsData.GetMemberProperty(memberOrder);
		}

		public void UpdateMemberType(MemberProperty memberType)
		{
			aideAccountsData.UpdateMemberType(memberType);
		}

		public PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideAccountsData.GetList(tableName, pageIndex, pageSize, condition, orderby);
		}

		public PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby, string[] fields)
		{
			return aideAccountsData.GetList(tableName, pageIndex, pageSize, condition, orderby, fields);
		}

		public int ExecuteSql(string sql)
		{
			return aideAccountsData.ExecuteSql(sql);
		}

		public DataSet GetDataSetBySql(string sql)
		{
			return aideAccountsData.GetDataSetBySql(sql);
		}

		public string GetScalarBySql(string sql)
		{
			return aideAccountsData.GetScalarBySql(sql);
		}

		public Message ExcuteByProce(string proceName, Dictionary<string, object> dir)
		{
			return aideAccountsData.ExcuteByProce(proceName, dir);
		}

		public decimal GetDecSum(string sql)
		{
			return aideAccountsData.GetDecSum(sql);
		}

		public int GetIntSum(string sql)
		{
			return aideAccountsData.GetIntSum(sql);
		}

		public long GetLongSum(string sql)
		{
			return aideAccountsData.GetLongSum(sql);
		}

		public CashSetting PlayerCashInfo(int id)
		{
			return aideAccountsData.PlayerCashInfo(id);
		}

		public void UpdatePlayerSetting(CashSetting model)
		{
			aideAccountsData.UpdatePlayerSetting(model);
		}

		public int TradeOper(int state, int id)
		{
			return aideAccountsData.TradeOper(state, id);
		}

		public decimal SumPlayerDraw(string sWhere)
		{
			return aideAccountsData.SumPlayerDraw(sWhere);
		}

		public int SuccessCount(string sWhere)
		{
			string sql = "select count(1) FROM View_ApplyOrder " + sWhere + " AND Status=2";
			string scalarBySql = aideAccountsData.GetScalarBySql(sql);
			return Convert.ToInt32(scalarBySql);
		}

		public decimal SumSpreadTrade(string sWhere)
		{
			string sql = "select SUM(RtnFee) from RYAgentDB.dbo.View_SpreadBalance " + sWhere;
			string scalarBySql = GetScalarBySql(sql);
			if (string.IsNullOrEmpty(scalarBySql))
			{
				return 0m;
			}
			return Convert.ToDecimal(scalarBySql);
		}

		public int GetUndoApplyOrderCount()
		{
			return aideAccountsData.GetUndoApplyOrderCount();
		}

		public int GetUndoAgentDrawCount()
		{
			return aideAccountsData.GetUndoAgentDrawCount();
		}

		public string queryXml(string xmlContent, out int status)
		{
			MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlContent));
			XElement root = XDocument.Load(stream).Root;
			if (root.Element("response").Element("is_success").Value.ToUpper() == "TRUE")
			{
				XElement xElement = root.Element("response").Element("remit_status");
				status = Convert.ToInt32(xElement.Value);
				return xElement.Value;
			}
			status = 0;
			return "0";
		}

		public void UpdateAgentBaseURL(AgentStatusInfo model)
		{
			aideAccountsData.UpdateAgentBaseURL(model);
		}

		public Message RegAegnt(AgentInfo model)
		{
			return aideAccountsData.RegAegnt(model);
		}

		public Message AgentDraw(AgentInfo agent)
		{
			return aideAccountsData.AgentDraw(agent);
		}

		public Message AgentRecharge(AgentInfo model, byte opType = 0)
		{
			return aideAccountsData.AgentRecharge(model, opType);
		}

		public double SumAgentPlayerPay(string sWhere)
		{
			return aideAccountsData.SumAgentPlayerPay(sWhere);
		}

		public decimal SumPayOrder(string sWhere)
		{
			string sql = "SELECT SUM(Score) FROM RYAgentDB..T_AgentFillOrder " + sWhere;
			string scalarBySql = aideAccountsData.GetScalarBySql(sql);
			if (scalarBySql == "")
			{
				return 0m;
			}
			return Convert.ToDecimal(scalarBySql);
		}

		public void UpdateAegntBank(AgentInfo model)
		{
			aideAccountsData.UpdateAegntBank(model);
		}

		public string UpdateAegntPwd(AgentInfo model)
		{
			aideAccountsData.UpdateAegntPwd(model);
			return "1";
		}

		public Message AgentUserRecharge(AgentInfo model)
		{
			return aideAccountsData.AgentUserRecharge(model);
		}

		public Message AgentLogonNew(AgentInfo user)
		{
			Message message = aideAccountsData.AgentLogonNew(user);
			if (message.Success)
			{
				AgentInfo agentInfo = message.EntityList[0] as AgentInfo;
				agentInfo.Pwd = user.Pwd;
				List<AgentMenu> list = new List<AgentMenu>();
				if (agentInfo.IsClient == 2)
				{
					AgentMenu agentMenu = new AgentMenu();
					agentMenu.prefixUrl = "Recharge";
					agentMenu.key = "充值记录";
					list.Add(agentMenu);
					AgentMenu agentMenu2 = new AgentMenu();
					agentMenu2.prefixUrl = "Present";
					agentMenu2.key = "赠送记录";
					list.Add(agentMenu2);
				}
				else
				{
					AgentMenu agentMenu3 = new AgentMenu();
					agentMenu3.prefixUrl = "New";
					agentMenu3.key = "代理信息";
					list.Add(agentMenu3);
					if (agentInfo.IsClient == 1)
					{
						AgentMenu agentMenu4 = new AgentMenu();
						agentMenu4.prefixUrl = "Pay";
						agentMenu4.key = "订单充值";
						list.Add(agentMenu4);
						AgentMenu agentMenu5 = new AgentMenu();
						agentMenu5.prefixUrl = "PayOrder";
						agentMenu5.key = "订单充值记录";
						list.Add(agentMenu5);
					}
					if (TypeUtil.AgentRight(agentInfo.QueryRight, 1))
					{
						AgentMenu agentMenu6 = new AgentMenu();
						agentMenu6.prefixUrl = "Recharges";
						agentMenu6.key = "玩家充值记录";
						list.Add(agentMenu6);
						AgentMenu agentMenu7 = new AgentMenu();
						agentMenu7.prefixUrl = "Payunline";
						agentMenu7.key = "玩家线下充值记录";
						list.Add(agentMenu7);
					}
					if (TypeUtil.AgentRight(agentInfo.QueryRight, 2))
					{
						AgentMenu agentMenu8 = new AgentMenu();
						agentMenu8.prefixUrl = "GameChart";
						agentMenu8.key = "玩家游戏报表";
						list.Add(agentMenu8);
					}
					if (TypeUtil.AgentRight(agentInfo.QueryRight, 4))
					{
						AgentMenu agentMenu9 = new AgentMenu();
						agentMenu9.prefixUrl = "Balance";
						agentMenu9.key = "玩家提现记录";
						list.Add(agentMenu9);
					}
					if (TypeUtil.AgentRight(agentInfo.QueryRight, 8))
					{
						AgentMenu agentMenu10 = new AgentMenu();
						agentMenu10.prefixUrl = "AccountScoreList";
						agentMenu10.key = "玩家总输赢";
						list.Add(agentMenu10);
					}
					if (TypeUtil.AgentRight(agentInfo.QueryRight, 16))
					{
						AgentMenu agentMenu11 = new AgentMenu();
						agentMenu11.prefixUrl = "Notice";
						agentMenu11.key = "代理公告";
						list.Add(agentMenu11);
					}
					if (TypeUtil.AgentRight(agentInfo.QueryRight, 32))
					{
						AgentMenu agentMenu12 = new AgentMenu();
						agentMenu12.prefixUrl = "Revise";
						agentMenu12.key = "资料修改";
						list.Add(agentMenu12);
					}
					if (TypeUtil.AgentRight(agentInfo.QueryRight, 64))
					{
						AgentMenu agentMenu13 = new AgentMenu();
						agentMenu13.prefixUrl = "Game";
						agentMenu13.key = "玩家管理";
						list.Add(agentMenu13);
					}
					if (TypeUtil.AgentRight(agentInfo.QueryRight, 128))
					{
						AgentMenu agentMenu14 = new AgentMenu();
						agentMenu14.prefixUrl = "Agent";
						agentMenu14.key = "代理管理";
						list.Add(agentMenu14);
					}
					if (TypeUtil.AgentRight(agentInfo.QueryRight, 256))
					{
						AgentMenu agentMenu15 = new AgentMenu();
						agentMenu15.prefixUrl = "Baobiao";
						agentMenu15.key = "我的报表";
						list.Add(agentMenu15);
					}
					if (TypeUtil.AgentRight(agentInfo.QueryRight, 512))
					{
						AgentMenu agentMenu16 = new AgentMenu();
						agentMenu16.prefixUrl = "Recordcs";
						agentMenu16.key = "我的抽水记录";
						list.Add(agentMenu16);
					}
					if (TypeUtil.AgentRight(agentInfo.QueryRight, 1024))
					{
						AgentMenu agentMenu17 = new AgentMenu();
						agentMenu17.prefixUrl = "Change";
						agentMenu17.key = "金币变化记录";
						list.Add(agentMenu17);
					}
					if (TypeUtil.AgentRight(agentInfo.QueryRight, 2048))
					{
						AgentMenu agentMenu18 = new AgentMenu();
						agentMenu18.prefixUrl = "Agentwin";
						agentMenu18.key = "代理输赢情况";
						list.Add(agentMenu18);
					}
					if (TypeUtil.AgentRight(agentInfo.QueryRight, 4096))
					{
						AgentMenu agentMenu19 = new AgentMenu();
						agentMenu19.prefixUrl = "Gamewin";
						agentMenu19.key = "玩家输赢情况";
						list.Add(agentMenu19);
					}
					if (TypeUtil.AgentRight(agentInfo.QueryRight, 8192))
					{
						AgentMenu agentMenu20 = new AgentMenu();
						agentMenu20.prefixUrl = "Money";
						agentMenu20.key = "提款";
						list.Add(agentMenu20);
					}
					if (TypeUtil.AgentRight(agentInfo.QueryRight, 16384))
					{
						AgentMenu agentMenu21 = new AgentMenu();
						agentMenu21.prefixUrl = "DuiHuanRecord";
						agentMenu21.key = "提款记录";
						list.Add(agentMenu21);
					}
					if (TypeUtil.AgentRight(agentInfo.QueryRight, 32768))
					{
						AgentMenu agentMenu22 = new AgentMenu();
						agentMenu22.prefixUrl = "PayReport";
						agentMenu22.key = "充值提成报表";
						list.Add(agentMenu22);
					}
					if (TypeUtil.AgentRight(agentInfo.QueryRight, 65536))
					{
						AgentMenu agentMenu23 = new AgentMenu();
						agentMenu23.prefixUrl = "OnlinePayer";
						agentMenu23.key = "在线玩家";
						list.Add(agentMenu23);
					}
					agentInfo.menus = list;
					AdminCookie.SetAgentCookieNew(agentInfo);
				}
			}
			return message;
		}

		public DataSet AgentScore(int aid, byte type = 0)
		{
			return aideAccountsData.AgentScore(aid, type);
		}

		public AgentStatusInfo AgentBaseURL()
		{
			return aideAccountsData.AgentBaseURL();
		}

		public double SumAgentDraw(string sWhere)
		{
			string sql = "SELECT SUM(SellScore) FROM RYAgentDB..View_AgentDraw " + sWhere;
			string scalarBySql = aideAccountsData.GetScalarBySql(sql);
			if (string.IsNullOrEmpty(scalarBySql))
			{
				return 0.0;
			}
			return Convert.ToDouble(scalarBySql);
		}

		public DataTable AgentCashInfo()
		{
			string sql = "SELECT TOP 1 IsOpen,WeekOpenDay,SOpenTime,EOpenTime, DailyApplyTimes, BalancePrice,MinBalance,CounterFee,MinCounterFee,DiffAgentRate,MaxAgentRate FROM RYAgentDB..T_Acc_AgentStatusInfo";
			return aideAccountsData.GetDataSetBySql(sql).Tables[0];
		}

		public void UpdateAgentSetting(AgentSetting model)
		{
			aideAccountsData.UpdateAgentSetting(model);
		}

		public AgentInfo GetAgentDetail(int aid)
		{
			return aideAccountsData.GetAgentDetail(aid);
		}

		public Message UpdateAegnt(AgentInfo model)
		{
			return aideAccountsData.UpdateAegnt(model);
		}

		public DataTable AgentLogType()
		{
			string sql = "select * from RYAgentDB..T_Rec_AgentLogType";
			return aideAccountsData.GetDataSetBySql(sql).Tables[0];
		}

		public bool SetRate(int id, int fillRate, int drawFee, int fillRevRate, out string msg)
		{
			DataTable dataTable = aideAccountsData.GetDataSetBySql("SELECT CheckStatus FROM RYAgentDB.dbo.T_FillAgentRpt WHERE ID=" + id).Tables[0];
			if (dataTable.Rows.Count > 0)
			{
				short num = Convert.ToInt16(dataTable.Rows[0][0]);
				if (num == 3)
				{
					msg = "此信息双方都已确认，无法修改";
					return false;
				}
				string sql = "UPDATE RYAgentDB.dbo.T_FillAgentRpt SET FillRate=" + fillRate + ",DrawFee=" + drawFee + ",FillRevRate=" + fillRevRate + " WHERE ID=" + id;
				if (aideAccountsData.ExecuteSql(sql) > 0)
				{
					msg = "设置成功";
					return true;
				}
				msg = "系统错误";
				return false;
			}
			msg = "该信息不存在";
			return false;
		}

		public bool UpdateState(int id, int state, out string msg)
		{
			DataTable dataTable = aideAccountsData.GetDataSetBySql("SELECT CheckStatus,FillRevRate FROM RYAgentDB.dbo.T_FillAgentRpt WHERE ID=" + id).Tables[0];
			if (dataTable.Rows.Count > 0)
			{
				short num = Convert.ToInt16(dataTable.Rows[0][0]);
				if (Convert.ToInt16(dataTable.Rows[0][1]) == 0)
				{
					msg = "提成比例还未设置，确认失败";
					return false;
				}
				if (state == 2 && num != 1)
				{
					msg = "状态不为等待运营商确认，确认失败";
					return false;
				}
				if (state == 3 && num != 2)
				{
					msg = "状态不为等待运营商确认，确认失败";
					return false;
				}
				string sql = "UPDATE RYAgentDB.dbo.T_FillAgentRpt SET CheckStatus=" + state + " WHERE ID=" + id;
				if (aideAccountsData.ExecuteSql(sql) > 0)
				{
					msg = "确认成功";
					return true;
				}
				msg = "系统错误";
				return false;
			}
			msg = "该信息不存在";
			return false;
		}

		public AUFillConfig GetFillConfig(int aid)
		{
			string sql = "SELECT [AgentID],[UserAllAmt],[BeginTime] ,[EndTime],[Amount],[Times] FROM RYAgentDB.dbo.T_AUFillConfig WHERE AgentID=" + aid;
			DataTable dataTable = aideAccountsData.GetDataSetBySql(sql).Tables[0];
			if (dataTable.Rows.Count > 0)
			{
				ModelHandler<AUFillConfig> modelHandler = new ModelHandler<AUFillConfig>();
				return modelHandler.FillModel(dataTable.Rows[0]);
			}
			return null;
		}

		public bool UpdateCheckState(int id, int status, string admin, out string msg)
		{
			string sql = "UPDATE RYAgentDB.dbo.T_UserAccuseYS SET Status=" + status + ",Operator='" + admin + "',OperateDate=GetDate() WHERE Status=0 AND ID=" + id;
			if (aideAccountsData.ExecuteSql(sql) > 0)
			{
				if (status == 1)
				{
					msg = "处理成功";
				}
				else
				{
					msg = "拒绝成功";
				}
				return true;
			}
			msg = "系统错误";
			return false;
		}

		public bool UpdateDailiCheckState(int id, int status, string admin, out string msg)
		{
			string sql = "UPDATE RYAgentDB.dbo.T_UserApplyYs SET YsStatus=" + status + ",Operator='" + admin + "',OperateDate=GetDate() WHERE YsStatus=0 AND ID=" + id;
			if (aideAccountsData.ExecuteSql(sql) > 0)
			{
				if (status == 1)
				{
					msg = "处理成功";
				}
				else
				{
					msg = "拒绝成功";
				}
				return true;
			}
			msg = "系统错误";
			return false;
		}

		public DataTable MySpreadInfo(int uid, int dateType)
		{
			return aideAccountsData.MySpreadInfo(uid, dateType);
		}

		public DataTable MyOwnSpreadCount(int uid)
		{
			return aideAccountsData.MyOwnSpreadCount(uid);
		}

		public Message SpreadBalance(int uid, double balance, string ip)
		{
			return aideAccountsData.SpreadBalance(uid, balance, ip);
		}

		public DataTable MySpreadCntInfo(int uid, int type = 0)
		{
			return aideAccountsData.MySpreadCntInfo(uid, type);
		}

		public PagerSet QuerySpreadScore(int uid, int type, int recordType, DateTime bTime, DateTime eTime, int pageIndex, int pageSize)
		{
			return aideAccountsData.QuerySpreadScore(uid, type, recordType, bTime, eTime, pageIndex, pageSize);
		}

		public PagerSet QuerySpreadDailyRpt(int uid, string account, int lev, DateTime bTime, DateTime eTime, int pageIndex, int pageSize)
		{
			return aideAccountsData.QuerySpreadDailyRpt(uid, account, lev, bTime, eTime, pageIndex, pageSize);
		}

		public DataTable GetUserGoldException(string sTime, string eTime)
		{
			return aideAccountsData.GetUserGoldException(sTime, eTime);
		}

		public void InsertAndroid(AndroidConfigure android)
		{
			aideAccountsData.InsertAndroid(android);
		}

		public void UpdateAndroid(AndroidConfigure android)
		{
			aideAccountsData.UpdateAndroid(android);
		}

		public void DeleteConfigure(string ids)
		{
			string sql = "DELETE AndroidConfigure WHERE BatchID IN(" + ids + ")";
			aideAccountsData.ExecuteSql(sql);
		}

		public int SetSuperHao(string account)
		{
			return aideAccountsData.SetSuperHao(account);
		}

		public int QXSuperHao(int UserID)
		{
			return aideAccountsData.QXSuperHao(UserID);
		}

		public int QXSuperAccount(string ids, string admin)
		{
			string text = "INSERT INTO RYNativeWebDB.dbo.T_SuperUserLog( OpName,UserID,MyFGameID,FGameID,Operator,CollectDate )SELECT '删除',a.UserID,a.GameID,s.FollowGameID,'" + admin + "',GETDATE() FROM dbo.T_SuperUser s WITH(NOLOCK) INNER JOIN RYAccountsDB.dbo.AccountsInfo a  WITH(NOLOCK) ON s.UserID=a.UserID WHERE a.UserID IN(" + ids + ")";
			string text2 = text;
			text = text2 + "Update AccountsInfo set UserRight=UserRight&~536870912 WHERE UserID IN(" + ids + ");DELETE T_SuperUser WHERE UserID IN(" + ids + ");";
			return aideAccountsData.ExecuteSql(text);
		}

		public int QXSuperAccount(string ids)
		{
			string text = "";
			string text2 = text;
			text = text2 + "Update AccountsInfo set UserRight=UserRight&~536870912 WHERE UserID IN(" + ids + ");DELETE T_SuperUser WHERE UserID IN(" + ids + ");";
			return aideAccountsData.ExecuteSql(text);
		}

		public int GetAgentIDByAccounts(string account)
		{
			return aideAccountsData.GetAgentIDByAccounts(account);
		}

		public bool AddIOSConfig(int IsIOSShop, int AgentID, string AgentAcc)
		{
			return aideAccountsData.AddIOSConfig(IsIOSShop, AgentID, AgentAcc);
		}

		public bool DelIOSConfig(string AgentID)
		{
			return aideAccountsData.DelIOSConfig(AgentID);
		}

		public bool UpdateIOSConfig(int AgentID)
		{
			return aideAccountsData.UpdateIOSConfig(AgentID);
		}

		public int AddIOSConfig(int IsIOSShop, int AgentID, string AgentAcc, string AgentName, int VersionNo, int SrcVersionNo, string AppKey, string AppSecret, string AppUrl)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("INSERT INTO RYAgentDB.dbo.T_AgentIsIOS(IsIOSShop,AgentID,AgentAcc,AgentName,VersionNo,SrcVersionNo,AppKey,AppSecret,AppUrl) values({0},{1},'{2}','{3}',{4},{5},'{6}','{7}','{8}')", IsIOSShop, AgentID, AgentAcc, AgentName, VersionNo, SrcVersionNo, AppKey, AppSecret, AppUrl);
			return aideAccountsData.ExecuteSql(stringBuilder.ToString());
		}

		public int UpdateIOSConfig(int IsIOSShop, int AgentID, int VersionNo, int SrcVersionNo, string AgentName, string AppKey, string AppSecret, string AppUrl)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("UPDATE RYAgentDB.dbo.T_AgentIsIOS SET IsIOSShop={0},VersionNo={1},SrcVersionNo={2},AgentName='{4}',AppKey='{5}',AppSecret='{6}',AppUrl='{7}' WHERE AgentID={3}", IsIOSShop, VersionNo, SrcVersionNo, AgentID, AgentName, AppKey, AppSecret, AppUrl);
			return aideAccountsData.ExecuteSql(stringBuilder.ToString());
		}

		public IList<BankCard> GetAllBankCard()
		{
			return aideAccountsData.GetAllBankCard();
		}

		public int EditBankCard(BankCard entity)
		{
			return aideAccountsData.EditBankCard(entity);
		}

		public BankCard GetBankCardByID(int id)
		{
			return aideAccountsData.GetBankCardByID(id);
		}

		public int DelBankCard(int id)
		{
			string sql = "DELETE T_BankCard WHERE ID=" + id;
			return aideAccountsData.ExecuteSql(sql);
		}
	}
}
