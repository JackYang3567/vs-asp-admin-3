using Game.Entity.Record;
using Game.Kernel;
using System;
using System.Collections.Generic;
using System.Data;

namespace Game.IData
{
	public interface IRecordDataProvider
	{
		PagerSet GetRecordAccountsExpendList(int pageIndex, int pageSize, string condition, string orderby);

		void InsertRecordAccountsExpend(RecordAccountsExpend actExpend);

		Dictionary<int, string> GetOldNickNameOrAccountsList(int userId, int typeID);

		PagerSet GetRecordPasswdExpendList(int pageIndex, int pageSize, string condition, string orderby);

		void InsertRecordPasswdExpend(RecordPasswdExpend pwExpend);

		RecordPasswdExpend GetRecordPasswdExpendByRid(int rid);

		bool ConfirmPass(int rid, string password, int type);

		Dictionary<int, string> GetOldLogonPassList(int userId);

		PagerSet GetRecordGrantTreasureList(int pageIndex, int pageSize, string condition, string orderby);

		void InsertRecordGrantTreasure(RecordGrantTreasure grantTreasure);

		PagerSet GetRecordGrantMemberList(int pageIndex, int pageSize, string condition, string orderby);

		void InsertRecordGrantMember(RecordGrantMember grantMember);

		Message GrantMember(int userID, int memberOrder, int days, int masterID, string strReason, string strIP);

		PagerSet GetRecordGrantExperienceList(int pageIndex, int pageSize, string condition, string orderby);

		void InsertRecordGrantExperience(RecordGrantExperience grantExperience);

		PagerSet GetRecordGrantGameScoreList(int pageIndex, int pageSize, string condition, string orderby);

		PagerSet GetRecordGrantClearScoreList(int pageIndex, int pageSize, string condition, string orderby);

		PagerSet GetRecordGrantClearFleeList(int pageIndex, int pageSize, string condition, string orderby);

		PagerSet GetRecordConvertPresentList(int pageIndex, int pageSize, string condition, string orderby);

		PagerSet GetRecordGrantGameIDList(int pageIndex, int pageSize, string condition, string orderby);

		Message GrantGameID(int userID, int gameID, int masterID, string strReason, string strIP);

		void DeleteTaskRecord(string sqlQuery);

		DataSet GetLossUserByDay(int startID, int endID);

		DataSet GetLossUserByMonth();

		DataSet GetPayDesireByDay(int startID, int endID);

		DataSet GetBankTaxByDay(int startID, int endID);

		DataSet GetBankTaxByMonth();

		DataSet GetGameTaxByDay(int startID, int endID);

		DataSet GetGameTaxByMonth();

		DataSet GetGameTaxListByDateID(int dateID);

		DataTable GetGameRevenue();

		DataTable GetRoomRevenue();

		DataTable GetGameWaste();

		DataTable GetRoomWaste();

		PagerSet StraightAgentRev(int curId, int aid, string acc, DateTime sTime, DateTime eTime, int pageIndex, int pageSize, int doType);

		double SumCS(string sWhere);

		DataTable AgentLogType();

		double AgentScoreChangeSum(string sWhere);

		PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby);

		DataTable GetAdvertInfo(string Advertiser, int IsWeek);

		DataSet GetPlayer(Dictionary<string, string> dic, string procName);

		DataSet GetRanks(Dictionary<string, string> dic, string procName);

		DataTable GetAdvertisers(string fields, string where);

		DataSet GetRanks(Dictionary<string, string> dic, string ProcName, string account);

		Message EditAdvertiser(TAdvertiser model);

		bool DelAdvertiser(int id);

		DataTable GetAdvertTask(string fields, string where);

		Message EditAdvertTask(AdvertTask entity);

		DataTable GetAdvertTaskKinds(int taskID);

		bool EidterAdvertTaskKinds(List<T_Rec_AdvertTaskKindsEntity> entities);

		void InsertBaiRenGameLog(BarrenGameLog model);

		PagerSet getAgentRecharges(Dictionary<string, object> dic, out decimal total);

		PagerSet getAgentGameRecord(Dictionary<string, object> dic);

		PagerSet getAgentBalance(Dictionary<string, object> dic, out decimal total);

		PagerSet QueryPlayerWinLost(Dictionary<string, object> dic);

		DataTable QueryUserGameWinLost(Dictionary<string, object> dic);

		Message getAgentQueryInfo(Dictionary<string, object> dic);
	}
}
