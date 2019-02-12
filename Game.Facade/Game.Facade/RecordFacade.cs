using Game.Data.Factory;
using Game.Entity.Record;
using Game.IData;
using Game.Kernel;
using System;
using System.Collections.Generic;
using System.Data;

namespace Game.Facade
{
	public class RecordFacade
	{
		private IRecordDataProvider aideRecordData;

		public RecordFacade()
		{
			aideRecordData = ClassFactory.GetIRecordDataProvider();
		}

		public PagerSet GetRecordAccountsExpendList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideRecordData.GetRecordAccountsExpendList(pageIndex, pageSize, condition, orderby);
		}

		public void InsertRecordAccountsExpend(RecordAccountsExpend actExpend)
		{
			aideRecordData.InsertRecordAccountsExpend(actExpend);
		}

		public Dictionary<int, string> GetOldNickNameOrAccountsList(int userId, int typeID)
		{
			return aideRecordData.GetOldNickNameOrAccountsList(userId, typeID);
		}

		public PagerSet GetRecordPasswdExpendList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideRecordData.GetRecordPasswdExpendList(pageIndex, pageSize, condition, orderby);
		}

		public void InsertRecordPasswdExpend(RecordPasswdExpend pwExpend)
		{
			aideRecordData.InsertRecordPasswdExpend(pwExpend);
		}

		public RecordPasswdExpend GetRecordPasswdExpendByRid(int rid)
		{
			return aideRecordData.GetRecordPasswdExpendByRid(rid);
		}

		public bool ConfirmPass(int rid, string password, int type)
		{
			return aideRecordData.ConfirmPass(rid, password, type);
		}

		public Dictionary<int, string> GetOldLogonPassList(int userId)
		{
			return aideRecordData.GetOldLogonPassList(userId);
		}

		public PagerSet GetRecordGrantTreasureList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideRecordData.GetRecordGrantTreasureList(pageIndex, pageSize, condition, orderby);
		}

		public void InsertRecordGrantTreasure(RecordGrantTreasure grantTreasure)
		{
			aideRecordData.InsertRecordGrantTreasure(grantTreasure);
		}

		public PagerSet GetRecordGrantMemberList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideRecordData.GetRecordGrantMemberList(pageIndex, pageSize, condition, orderby);
		}

		public void InsertRecordGrantMember(RecordGrantMember grantMember)
		{
			aideRecordData.InsertRecordGrantMember(grantMember);
		}

		public void GrantMember(int userID, int memberOrder, int days, int masterID, string strReason, string strIP)
		{
			aideRecordData.GrantMember(userID, memberOrder, days, masterID, strReason, strIP);
		}

		public PagerSet GetRecordGrantExperienceList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideRecordData.GetRecordGrantExperienceList(pageIndex, pageSize, condition, orderby);
		}

		public void InsertRecordGrantExperience(RecordGrantExperience grantExperience)
		{
			aideRecordData.InsertRecordGrantExperience(grantExperience);
		}

		public PagerSet GetRecordGrantGameScoreList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideRecordData.GetRecordGrantGameScoreList(pageIndex, pageSize, condition, orderby);
		}

		public PagerSet GetRecordGrantClearScoreList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideRecordData.GetRecordGrantClearScoreList(pageIndex, pageSize, condition, orderby);
		}

		public PagerSet GetRecordGrantClearFleeList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideRecordData.GetRecordGrantClearFleeList(pageIndex, pageSize, condition, orderby);
		}

		public PagerSet GetRecordConvertPresentList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideRecordData.GetRecordConvertPresentList(pageIndex, pageSize, condition, orderby);
		}

		public PagerSet GetRecordGrantGameIDList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideRecordData.GetRecordGrantGameIDList(pageIndex, pageSize, condition, orderby);
		}

		public Message GrantGameID(int userID, int gameID, int masterID, string strReason, string strIP)
		{
			return aideRecordData.GrantGameID(userID, gameID, masterID, strReason, strIP);
		}

		public void DeleteTaskRecord(string sqlQuery)
		{
			aideRecordData.DeleteTaskRecord(sqlQuery);
		}

		public DataSet GetLossUserByDay(int startID, int endID)
		{
			return aideRecordData.GetLossUserByDay(startID, endID);
		}

		public DataSet GetLossUserByMonth()
		{
			return aideRecordData.GetLossUserByMonth();
		}

		public DataSet GetPayDesireByDay(int startID, int endID)
		{
			return aideRecordData.GetPayDesireByDay(startID, endID);
		}

		public DataSet GetBankTaxByDay(int startID, int endID)
		{
			return aideRecordData.GetBankTaxByDay(startID, endID);
		}

		public DataSet GetBankTaxByMonth()
		{
			return aideRecordData.GetBankTaxByMonth();
		}

		public DataSet GetGameTaxByDay(int startID, int endID)
		{
			return aideRecordData.GetGameTaxByDay(startID, endID);
		}

		public DataSet GetGameTaxByMonth()
		{
			return aideRecordData.GetGameTaxByMonth();
		}

		public DataSet GetGameTaxListByDateID(int dateID)
		{
			return aideRecordData.GetGameTaxListByDateID(dateID);
		}

		public DataTable GetGameRevenue()
		{
			return aideRecordData.GetGameRevenue();
		}

		public DataTable GetRoomRevenue()
		{
			return aideRecordData.GetRoomRevenue();
		}

		public DataTable GetGameWaste()
		{
			return aideRecordData.GetGameWaste();
		}

		public DataTable GetRoomWaste()
		{
			return aideRecordData.GetRoomWaste();
		}

		public PagerSet StraightAgentRev(int curId, int aid, string acc, DateTime sTime, DateTime eTime, int pageIndex, int pageSize, int doType = 1)
		{
			return aideRecordData.StraightAgentRev(curId, aid, acc, sTime, eTime, pageIndex, pageSize, doType);
		}

		public double SumCS(string sWhere)
		{
			return aideRecordData.SumCS(sWhere);
		}

		public DataTable AgentLogType()
		{
			return aideRecordData.AgentLogType();
		}

		public double AgentScoreChangeSum(string sWhere)
		{
			return aideRecordData.AgentScoreChangeSum(sWhere);
		}

		public PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideRecordData.GetList(tableName, pageIndex, pageSize, condition, orderby);
		}

		public DataTable GetAdvertInfo(string Advertiser, int IsWeek = 1)
		{
			return aideRecordData.GetAdvertInfo(Advertiser, IsWeek);
		}

		public DataSet GetPlayer(Dictionary<string, string> dic, string procName)
		{
			return aideRecordData.GetPlayer(dic, procName);
		}

		public DataSet GetRanks(Dictionary<string, string> dic, string procName)
		{
			return aideRecordData.GetRanks(dic, procName);
		}

		public DataTable GetAdvertisers(string fields, string where)
		{
			return aideRecordData.GetAdvertisers(fields, where);
		}

		public DataTable GetAdvertTask(string fields, string where)
		{
			return aideRecordData.GetAdvertTask(fields, where);
		}

		public DataSet GetRanks(Dictionary<string, string> dic, string ProcName, string account)
		{
			return aideRecordData.GetRanks(dic, ProcName, account);
		}

		public Message EditAdvertiser(TAdvertiser model)
		{
			return aideRecordData.EditAdvertiser(model);
		}

		public bool DelAdvertiser(int id)
		{
			return aideRecordData.DelAdvertiser(id);
		}

		public Message EditAdvertTask(AdvertTask entity)
		{
			return aideRecordData.EditAdvertTask(entity);
		}

		public DataTable GetAdvertTaskKinds(int taskID)
		{
			return aideRecordData.GetAdvertTaskKinds(taskID);
		}

		public bool EidterAdvertTaskKinds(List<T_Rec_AdvertTaskKindsEntity> entities)
		{
			return aideRecordData.EidterAdvertTaskKinds(entities);
		}

		public void InsertBaiRenGameLog(BarrenGameLog model)
		{
			aideRecordData.InsertBaiRenGameLog(model);
		}

		public PagerSet getAgentRecharges(Dictionary<string, object> dic, out decimal total)
		{
			return aideRecordData.getAgentRecharges(dic, out total);
		}

		public PagerSet getAgentGameRecord(Dictionary<string, object> dic)
		{
			return aideRecordData.getAgentGameRecord(dic);
		}

		public PagerSet getAgentBalance(Dictionary<string, object> dic, out decimal total)
		{
			return aideRecordData.getAgentBalance(dic, out total);
		}

		public PagerSet QueryPlayerWinLost(Dictionary<string, object> dic)
		{
			return aideRecordData.QueryPlayerWinLost(dic);
		}

		public DataTable QueryUserGameWinLost(Dictionary<string, object> dic)
		{
			return aideRecordData.QueryUserGameWinLost(dic);
		}

		public Message getAgentQueryInfo(Dictionary<string, object> dic)
		{
			return aideRecordData.getAgentQueryInfo(dic);
		}
	}
}
