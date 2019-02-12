using Game.Entity.Accounts;
using Game.Kernel;
using System;
using System.Collections.Generic;
using System.Data;

namespace Game.IData
{
	public interface IAccountsDataProvider
	{
		PagerSet GetAccountsList(int pageIndex, int pageSize, string condition, string orderby);

		string GetAccountByUserID(int userID);

		int GetExperienceByUserID(int userID);

		AccountsInfo GetAccountInfoByUserID(int userID);

		AccountsInfo GetAccountInfoByAccount(string account);

		AccountsInfo GetAccountInfoByNickname(string nickname);

		AccountsInfo GetAccountInfoByGameID(int gameID);

		string GetAccountsByGameID(int gameId);

		void DongjieAccount(string sqlQuery);

		void JieDongAccount(string sqlQuery);

		void SettingAndroid(string sqlQuery);

		void CancleAndroid(string sqlQuery);

		Message UpdateAccount(AccountsInfo account, int masterID, string clientIP);

		Message AddAccount(AccountsInfo account, IndividualDatum datum);

		bool AddUserMedal(int userId, int medal);

		bool UpdateUserPassword(AccountsInfo accountsInfo);

		IndividualDatum GetAccountDetailByUserID(int userID);

		void InsertAccountDetail(IndividualDatum accountDetail);

		void UpdateAccountDetail(IndividualDatum accountDetail);

		DataSet GetAccountsFaceList(int userId);

		AccountsFace GetAccountsFace(int customId);

		AccountsProtect GetAccountsProtectByPID(int pid);

		AccountsProtect GetAccountsProtectByUserID(int uid);

		void UpdateAccountsProtect(AccountsProtect accountProtect);

		void DeleteAccountsProtect(int pid);

		void DeleteAccountsProtect(string sqlQuery);

		DataSet GetReserveIdentifierList();

		PagerSet GetConfineContentList(int pageIndex, int pageSize, string condition, string orderby);

		ConfineContent GetConfineContentByContentID(int contentID);

		void InsertConfineContent(ConfineContent content);

		void UpdateConfineContent(ConfineContent content);

		void DeleteConfineContentByContentID(int contentID);

		void DeleteConfineContent(string sqlQuery);

		bool IsExitStringInConfineContent(string str);

		PagerSet GetConfineAddressList(int pageIndex, int pageSize, string condition, string orderby);

		ConfineAddress GetConfineAddressByAddress(string strAddress);

		void InsertConfineAddress(ConfineAddress address);

		void UpdateConfineAddress(ConfineAddress address);

		void DeleteConfineAddressByAddress(string strAddress);

		void DeleteConfineAddress(string sqlQuery);

		DataSet GetIPRegisterTop100();

		void BatchInsertConfineAddress(string ipList);

		PagerSet GetConfineMachineList(int pageIndex, int pageSize, string condition, string orderby);

		ConfineMachine GetConfineMachineBySerial(string strSerial);

		void InsertConfineMachine(ConfineMachine machine);

		void UpdateConfineMachine(ConfineMachine machine);

		void DeleteConfineMachineBySerial(string strSerial);

		void DeleteConfineMachine(string sqlQuery);

		DataSet GetMachineRegisterTop100();

		void BatchInsertConfineMachine(string machineList);

		DataTable SystemCount();

		PagerSet GetSystemStatusInfoList(int pageIndex, int pageSize, string condition, string orderby);

		SystemStatusInfo GetSystemStatusInfo(string statusName);

		void UpdateSystemStatusInfo(SystemStatusInfo systemStatusInfo);

		DataSet GetRegUserByDays(string startDate, string endDate);

		DataSet GetRegUserByHours(string startDate, string endDate);

		DataSet GetRegUserByMonth();

		DataSet GetUsersStat();

		DataSet GetUsersNumber();

		Message AppStatLogon(string accounts, string logonPass, string machineID);

		Message AppStatUserActive(string accounts, string logonPass, string machineID);

		Message AppStatUserMember(string accounts, string logonPass, string machineID);

		Message AppGetLogonData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate);

		Message AppGetRegData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate);

		ControlConfig GetControlConfig();

		void UpdateControlConfig(ControlConfig model);

		void DeleteAccountsControl(string where);

		void AddAccountsControl(AccountsControl model);

		void UpdateAccountsControl(AccountsControl model);

		AccountsControl GetAccountsControl(int id);

		void DongjieAgent(string sqlQuery);

		void JieDongAgent(string sqlQuery);

		AccountsAgent GetAccountAgentByUserID(int userID);

		AccountsAgent GetAccountAgentByDomain(string domain);

		Message AddAgent(AccountsAgent agent);

		bool UpdateAgent(AccountsAgent agent);

		int GetAgentChildCount(int userID);

		AccountsAgentGame GetAccountsAgentGameInfo(int id);

		void InsertAccountsAgentGame(AccountsAgentGame model);

		void UpdateAccountsAgentGame(AccountsAgentGame model);

		void DeleteAccountsAgentGame(string sqlQuery);

		DataSet GetMemberPropertyList();

		MemberProperty GetMemberProperty(int memberOrder);

		void UpdateMemberType(MemberProperty memberType);

		PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby);

		PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby, string[] fields);

		int ExecuteSql(string sql);

		DataSet GetDataSetBySql(string sql);

		string GetScalarBySql(string sql);

		Message ExcuteByProce(string proceName, Dictionary<string, object> dir);

		decimal GetDecSum(string sql);

		int GetIntSum(string sql);

		long GetLongSum(string sql);

		CashSetting PlayerCashInfo(int id);

		void UpdatePlayerSetting(CashSetting model);

		int TradeOper(int state, int id);

		int TradePay_DaiFu(string operateUser, int tradeId, string orderId, string bankAccount, string bankAccountCode, string bankAmount, string selectBankCode, string bankName);

		int Trade_DaiFu_ApiStatus(string operateUser, int tradeId, string orderId, string apiErrorMessage, int status);

		decimal SumPlayerDraw(string sWhere);

		int GetUndoApplyOrderCount();

		int GetUndoAgentDrawCount();

		void UpdateAgentBaseURL(AgentStatusInfo model);

		Message RegAegnt(AgentInfo model);

		Message AgentDraw(AgentInfo agent);

		Message AgentRecharge(AgentInfo model, byte opType = 0);

		double SumAgentPlayerPay(string sWhere);

		void UpdateAegntBank(AgentInfo model);

		void UpdateAegntPwd(AgentInfo model);

		Message AgentUserRecharge(AgentInfo model);

		Message AgentLogonNew(AgentInfo user);

		DataSet AgentScore(int aid, byte type = 0);

		AgentStatusInfo AgentBaseURL();

		void UpdateAgentSetting(AgentSetting model);

		Message UpdateAegnt(AgentInfo model);

		AgentInfo GetAgentDetail(int aid);

		DataTable MySpreadInfo(int uid, int dateType);

		DataTable MyOwnSpreadCount(int uid);

		Message SpreadBalance(int uid, double balance, string ip);

		DataTable MySpreadCntInfo(int uid, int type);

		PagerSet QuerySpreadScore(int uid, int type, int recordType, DateTime bTime, DateTime eTime, int pageIndex, int pageSize);

		PagerSet QuerySpreadDailyRpt(int uid, string account, int lev, DateTime bTime, DateTime eTime, int pageIndex, int pageSize);

		DataTable GetUserGoldException(string sTime, string eTime);

		void InsertAndroid(AndroidConfigure android);

		void UpdateAndroid(AndroidConfigure android);

		int SetSuperHao(string account);

		int QXSuperHao(int UserID);

		int GetAgentIDByAccounts(string account);

		bool AddIOSConfig(int IsIOSShop, int AgentID, string AgentAcc);

		bool DelIOSConfig(string AgentID);

		bool UpdateIOSConfig(int AgentID);

		IList<BankCard> GetAllBankCard();

		int EditBankCard(BankCard entity);

		BankCard GetBankCardByID(int id);
	}
}
