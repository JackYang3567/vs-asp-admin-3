using Game.Entity.Record;
using Game.IData;
using Game.Kernel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Game.Data
{
	public class RecordDataProvider : BaseDataProvider, IRecordDataProvider
	{
		private ITableProvider aideRecordAccountsExpendProvider;

		private ITableProvider aideRecordPasswdExpendProvider;

		private ITableProvider aideRecordGrantTreasureProvider;

		private ITableProvider aideRecordGrantMemberProvider;

		private ITableProvider aideRecordGrantExperienceProvider;

		private ITableProvider aideRecordGrantGameScoreProvider;

		private ITableProvider aideRecordGrantClearScoreProvider;

		private ITableProvider aideRecordGrantClearFleeProvider;

		private ITableProvider aideRecordConvertPresentProvider;

		private ITableProvider aideTaskRecordProvider;

		public RecordDataProvider(string connString)
			: base(connString)
		{
			aideRecordAccountsExpendProvider = GetTableProvider("RecordAccountsExpend");
			aideRecordPasswdExpendProvider = GetTableProvider("RecordPasswdExpend");
			aideRecordGrantTreasureProvider = GetTableProvider("RecordGrantTreasure");
			aideRecordGrantMemberProvider = GetTableProvider("RecordGrantMember");
			aideRecordGrantExperienceProvider = GetTableProvider("RecordGrantExperience");
			aideRecordGrantGameScoreProvider = GetTableProvider("RecordGrantGameScore");
			aideRecordGrantClearScoreProvider = GetTableProvider("RecordGrantClearScore");
			aideRecordGrantClearFleeProvider = GetTableProvider("RecordGrantClearFlee");
			aideRecordConvertPresentProvider = GetTableProvider("RecordConvertPresent");
			aideTaskRecordProvider = GetTableProvider("RecordTask");
		}

		public PagerSet GetRecordAccountsExpendList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("RecordAccountsExpend", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public void InsertRecordAccountsExpend(RecordAccountsExpend actExpend)
		{
			DataRow dataRow = aideRecordAccountsExpendProvider.NewRow();
			dataRow["ReAccounts"] = actExpend.ReAccounts;
			dataRow["UserID"] = actExpend.UserID;
			dataRow["ClientIP"] = actExpend.ClientIP;
			dataRow["OperMasterID"] = actExpend.OperMasterID;
			dataRow["CollectDate"] = DateTime.Now;
			aideRecordAccountsExpendProvider.Insert(dataRow);
		}

		public Dictionary<int, string> GetOldNickNameOrAccountsList(int userId, int typeID)
		{
			string commandText = string.Format("SELECT ReAccounts FROM dbo.RecordAccountsExpend WHERE UserID={0} AND Type={1}", userId, typeID);
			DataSet dataSet = base.Database.ExecuteDataset(commandText);
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			int num = 1;
			foreach (DataRow row in dataSet.Tables[0].Rows)
			{
				string value = row["ReAccounts"].ToString();
				dictionary.Add(num, value);
				num++;
			}
			return dictionary;
		}

		public PagerSet GetRecordPasswdExpendList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("RecordPasswdExpend", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public void InsertRecordPasswdExpend(RecordPasswdExpend pwExpend)
		{
			DataRow dataRow = aideRecordPasswdExpendProvider.NewRow();
			dataRow["ReLogonPasswd"] = pwExpend.ReLogonPasswd;
			dataRow["ReInsurePasswd"] = pwExpend.ReInsurePasswd;
			dataRow["UserID"] = pwExpend.UserID;
			dataRow["ClientIP"] = pwExpend.ClientIP;
			dataRow["OperMasterID"] = pwExpend.OperMasterID;
			dataRow["CollectDate"] = DateTime.Now;
			aideRecordPasswdExpendProvider.Insert(dataRow);
		}

		public RecordPasswdExpend GetRecordPasswdExpendByRid(int rid)
		{
			string where = string.Format("(NOLOCK) WHERE RecordID= N'{0}'", rid);
			return aideRecordPasswdExpendProvider.GetObject<RecordPasswdExpend>(where);
		}

		public bool ConfirmPass(int rid, string password, int type)
		{
			string empty = string.Empty;
			empty = ((type != 0) ? string.Format("WHERE RecordID={0} AND ReInsurePasswd='{1}'", rid, password) : string.Format("WHERE RecordID={0} AND ReLogonPasswd='{1}'", rid, password));
			int recordsCount = aideRecordPasswdExpendProvider.GetRecordsCount(empty);
			if (recordsCount > 0)
			{
				return true;
			}
			return false;
		}

		public Dictionary<int, string> GetOldLogonPassList(int userId)
		{
			string commandText = string.Format("SELECT ReLogonPasswd,ReInsurePasswd FROM dbo.RecordPasswdExpend WHERE UserID={0}", userId);
			DataSet dataSet = base.Database.ExecuteDataset(commandText);
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			int num = 1;
			foreach (DataRow row in dataSet.Tables[0].Rows)
			{
				string value = row["ReLogonPasswd"].ToString();
				dictionary.Add(num, value);
				num++;
			}
			return dictionary;
		}

		public PagerSet GetRecordGrantTreasureList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("RecordGrantTreasure", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public void InsertRecordGrantTreasure(RecordGrantTreasure grantTreasure)
		{
			DataRow dataRow = aideRecordGrantTreasureProvider.NewRow();
			dataRow["MasterID"] = grantTreasure.MasterID;
			dataRow["CurGold"] = grantTreasure.CurGold;
			dataRow["UserID"] = grantTreasure.UserID;
			dataRow["ClientIP"] = grantTreasure.ClientIP;
			dataRow["AddGold"] = grantTreasure.AddGold;
			dataRow["Reason"] = grantTreasure.Reason;
			dataRow["CollectDate"] = DateTime.Now;
			aideRecordGrantTreasureProvider.Insert(dataRow);
		}

		public PagerSet GetRecordGrantMemberList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("RecordGrantMember", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public void InsertRecordGrantMember(RecordGrantMember grantMember)
		{
			DataRow dataRow = aideRecordGrantMemberProvider.NewRow();
			dataRow["MasterID"] = grantMember.MasterID;
			dataRow["GrantCardType"] = grantMember.GrantCardType;
			dataRow["UserID"] = grantMember.UserID;
			dataRow["ClientIP"] = grantMember.ClientIP;
			dataRow["MemberDays"] = grantMember.MemberDays;
			dataRow["Reason"] = grantMember.Reason;
			dataRow["CollectDate"] = DateTime.Now;
			aideRecordGrantMemberProvider.Insert(dataRow);
		}

		public Message GrantMember(int userID, int memberOrder, int days, int masterID, string strReason, string strIP)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("UserID", userID));
			list.Add(base.Database.MakeInParam("MemberOrder", memberOrder));
			list.Add(base.Database.MakeInParam("MemberDays", days));
			list.Add(base.Database.MakeInParam("MasterID", masterID));
			list.Add(base.Database.MakeInParam("Reason", strReason));
			list.Add(base.Database.MakeInParam("ClientIP", strIP));
			return MessageHelper.GetMessage(base.Database, "WSP_PM_GrantMember", list);
		}

		public PagerSet GetRecordGrantExperienceList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("RecordGrantExperience", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public void InsertRecordGrantExperience(RecordGrantExperience grantExperience)
		{
			DataRow dataRow = aideRecordGrantExperienceProvider.NewRow();
			dataRow["MasterID"] = grantExperience.MasterID;
			dataRow["CurExperience"] = grantExperience.CurExperience;
			dataRow["UserID"] = grantExperience.UserID;
			dataRow["ClientIP"] = grantExperience.ClientIP;
			dataRow["AddExperience"] = grantExperience.AddExperience;
			dataRow["Reason"] = grantExperience.Reason;
			dataRow["CollectDate"] = DateTime.Now;
			aideRecordGrantExperienceProvider.Insert(dataRow);
		}

		public PagerSet GetRecordGrantGameScoreList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("RecordGrantGameScore", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public PagerSet GetRecordGrantClearScoreList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("RecordGrantClearScore", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public PagerSet GetRecordGrantClearFleeList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("RecordGrantClearFlee", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public PagerSet GetRecordConvertPresentList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("RecordConvertPresent", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public PagerSet GetRecordGrantGameIDList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("RecordGrantGameID", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public Message GrantGameID(int userID, int gameID, int masterID, string strReason, string strIP)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("UserID", userID));
			list.Add(base.Database.MakeInParam("ReGameID", gameID));
			list.Add(base.Database.MakeInParam("MasterID", masterID));
			list.Add(base.Database.MakeInParam("Reason", strReason));
			list.Add(base.Database.MakeInParam("ClientIP", strIP));
			return MessageHelper.GetMessage(base.Database, "WSP_PM_GrantGameID", list);
		}

		public void DeleteTaskRecord(string sqlQuery)
		{
			aideTaskRecordProvider.Delete(sqlQuery);
		}

		public DataSet GetLossUserByDay(int startID, int endID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT");
			stringBuilder.Append(" DateID,LossUser,LossPayUser");
			stringBuilder.Append(" FROM RecordEveryDayData WHERE DateID>=@StartID AND DateID<=@EndID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("StartID", startID));
			list.Add(base.Database.MakeInParam("EndID", endID));
			return base.Database.ExecuteDataset(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public DataSet GetLossUserByMonth()
		{
			string str = "SELECT CONVERT(char(7), CollectDate,120 ) AS CollectDate,SUM(LossUser) AS LossUserTotal,SUM(LossPayUser) AS LossPayUserTotal FROM RecordEveryDayData";
			str += " GROUP BY CONVERT(char(7), CollectDate, 120)";
			return base.Database.ExecuteDataset(str);
		}

		public DataSet GetPayDesireByDay(int startID, int endID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT");
			stringBuilder.Append(" DateID,UserTotal,PayUserTotal,ActiveUserTotal,LossUserTotal,");
			stringBuilder.Append(" LossPayUserTotal,PayTotalAmount,PayAmountForCurrency,");
			stringBuilder.Append(" GoldTotal,CurrencyTotal,GameTaxTotal,UserAVGOnlineTime");
			stringBuilder.Append(" FROM RecordEveryDayData WHERE DateID>=@StartID AND DateID<=@EndID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("StartID", startID));
			list.Add(base.Database.MakeInParam("EndID", endID));
			return base.Database.ExecuteDataset(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public DataSet GetBankTaxByDay(int startID, int endID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT DateID,BankTax FROM RecordEveryDayData WHERE DateID>=@StartID AND DateID<=@EndID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("StartID", startID));
			list.Add(base.Database.MakeInParam("EndID", endID));
			return base.Database.ExecuteDataset(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public DataSet GetBankTaxByMonth()
		{
			string str = "SELECT SUM(BankTax) AS BankTax,CONVERT(char(7),CollectDate,120) AS StatDate FROM RecordEveryDayData";
			str += " GROUP BY CONVERT(char(7),CollectDate,120) ORDER BY StatDate ASC";
			return base.Database.ExecuteDataset(str);
		}

		public DataSet GetGameTaxByDay(int startID, int endID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT DateID,GameTax FROM RecordEveryDayData WHERE DateID>=@StartID AND DateID<=@EndID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("StartID", startID));
			list.Add(base.Database.MakeInParam("EndID", endID));
			return base.Database.ExecuteDataset(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public DataSet GetGameTaxByMonth()
		{
			string str = "SELECT SUM(GameTax) AS GameTax,CONVERT(char(7),CollectDate,120) AS StatDate FROM RecordEveryDayData";
			str += " GROUP BY CONVERT(char(7),CollectDate,120) ORDER BY StatDate ASC";
			return base.Database.ExecuteDataset(str);
		}

		public DataSet GetGameTaxListByDateID(int dateID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT KindID,SUM(Revenue) AS Revenue FROM RecordEveryDayRoomData WHERE DateID=@DateID GROUP BY KindID ORDER BY KindID ASC");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("DateID", dateID));
			return base.Database.ExecuteDataset(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public DataTable GetGameRevenue()
		{
			string commandText = "SELECT MAX(k.KindName) KindName,SUM(Revenue) AS Revenue FROM RecordEveryDayRoomData d INNER JOIN RYPlatformDB..GameKindItem k ON k.KindID = d.KindID GROUP BY d.KindID HAVING SUM(Revenue)>0";
			DataSet dataSet = base.Database.ExecuteDataset(CommandType.Text, commandText);
			return dataSet.Tables[0];
		}

		public DataTable GetRoomRevenue()
		{
			string commandText = "SELECT MAX(k.ServerName) ServerName,SUM(Revenue) AS Revenue FROM RecordEveryDayRoomData d INNER JOIN RYPlatformDB..GameRoomInfo k ON k.ServerID = d.ServerID GROUP BY d.ServerID HAVING SUM(Revenue)>0";
			DataSet dataSet = base.Database.ExecuteDataset(CommandType.Text, commandText);
			return dataSet.Tables[0];
		}

		public DataTable GetGameWaste()
		{
			string commandText = "SELECT MAX(k.KindName) KindName,SUM(Waste) AS Waste FROM RecordEveryDayRoomData d INNER JOIN RYPlatformDB..GameKindItem k ON k.KindID = d.KindID GROUP BY d.KindID HAVING SUM(Waste)>0";
			DataSet dataSet = base.Database.ExecuteDataset(CommandType.Text, commandText);
			return dataSet.Tables[0];
		}

		public DataTable GetRoomWaste()
		{
			string commandText = "SELECT MAX(k.ServerName) ServerName,SUM(Waste) AS Waste FROM RecordEveryDayRoomData  d INNER JOIN RYPlatformDB..GameRoomInfo k ON k.ServerID = d.ServerID GROUP BY d.ServerID HAVING SUM(Waste)>0";
			DataSet dataSet = base.Database.ExecuteDataset(CommandType.Text, commandText);
			return dataSet.Tables[0];
		}

		public PagerSet StraightAgentRev(int curId, int aid, string acc, DateTime sTime, DateTime eTime, int pageIndex, int pageSize, int doType)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("CurAgent", curId));
			list.Add(base.Database.MakeInParam("ParentAgent", aid));
			if (doType == 1)
			{
				list.Add(base.Database.MakeInParam("AgentAcc", acc));
			}
			else
			{
				list.Add(base.Database.MakeInParam("UserAcc", acc));
			}
			list.Add(base.Database.MakeInParam("StartDate", sTime));
			list.Add(base.Database.MakeInParam("EndDate", eTime));
			list.Add(base.Database.MakeInParam("PageSize", pageSize));
			list.Add(base.Database.MakeInParam("PageIndex", pageIndex));
			list.Add(base.Database.MakeOutParam("PageCount", typeof(int)));
			list.Add(base.Database.MakeOutParam("RecordCount", typeof(int)));
			list.Add(base.Database.MakeOutParam("SumAgentRevTotal", typeof(double)));
			list.Add(base.Database.MakeOutParam("SumWinlost", typeof(double)));
			list.Add(base.Database.MakeOutParam("StrErr", typeof(string), 127));
			string commandText = "";
			if (doType == 1)
			{
				commandText = "RYAgentDB..P_Rec_StraightAgentRev";
			}
			if (doType == 2)
			{
				commandText = "RYAgentDB..P_Rec_AgentPlayerRev";
			}
			if (doType == 3)
			{
				commandText = "RYAgentDB..P_Rec_PlayerGXRevDetail";
			}
			DataSet dataSet = base.Database.ExecuteDataset(CommandType.StoredProcedure, commandText, list.ToArray());
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("SumWinlost");
			dataTable.Columns.Add("SumAgentRevTotal");
			DataRow dataRow = dataTable.NewRow();
			dataRow["SumWinlost"] = Convert.ToDecimal(list[list.Count - 2].Value);
			dataRow["SumAgentRevTotal"] = Convert.ToDecimal(list[list.Count - 3].Value);
			dataTable.Rows.Add(dataRow);
			dataSet.Tables.Add(dataTable);
			return new PagerSet(1, 1, 1, Convert.ToInt32(list[list.Count - 4].Value), dataSet);
		}

		public double SumCS(string sWhere)
		{
			string commandText = "SELECT SUM(RealRev) FROM RYAgentDB..view_MyRevRecord " + sWhere;
			object obj = base.Database.ExecuteScalar(CommandType.Text, commandText);
			if (obj is DBNull)
			{
				return 0.0;
			}
			return Convert.ToDouble(obj);
		}

		public DataTable AgentLogType()
		{
			string commandText = "select * from RYAgentDB..T_Rec_AgentLogType(NOLOCK)";
			return base.Database.ExecuteDataset(commandText).Tables[0];
		}

		public double AgentScoreChangeSum(string sWhere)
		{
			string commandText = "SELECT SUM(SwapScore) FROM RYAgentDB..T_Rec_AgentScoreLog (NOLOCK) " + sWhere;
			object obj = base.Database.ExecuteScalar(CommandType.Text, commandText);
			if (obj is DBNull)
			{
				return 0.0;
			}
			return Convert.ToDouble(obj);
		}

		public PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters(tableName, orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public DataTable GetAdvertInfo(string Advertiser, int IsWeek)
		{
			DataTable dataTable = new DataTable();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("select adv.TaskID,adv.BeginTime,adv.EndTime,adv.AdKey,adv.AdID,adv.PcUrl,k.KindID,k.IsWeek,k.CountType from (select top 1 t.BeginTime,t.EndTime,t.ID as TaskID,a.AdKey,a.PcUrl,t.AdID from dbo.T_Rec_Advertiser a join dbo.T_Rec_AdvertTask t on a.ID=t.AdvertiserID and a.StatusT=1 and t.StatusT=1 where a.Advertiser='{0}' order by t.AddTime desc) as adv inner join T_Rec_AdvertTaskKinds k on adv.TaskID=k.TaskID", Advertiser);
			if (IsWeek == 1)
			{
				stringBuilder.Append("  and k.IsWeek=1 ");
			}
			return base.Database.ExecuteDataset(stringBuilder.ToString()).Tables[0];
		}

		public DataSet GetPlayer(Dictionary<string, string> dic, string procName)
		{
			List<DbParameter> list = new List<DbParameter>();
			if (dic != null)
			{
				foreach (string key in dic.Keys)
				{
					list.Add(base.Database.MakeInParam(key, dic[key]));
				}
			}
			return base.Database.ExecuteDataset(CommandType.StoredProcedure, procName, list.ToArray());
		}

		public DataSet GetRanks(Dictionary<string, string> dic, string procName)
		{
			List<DbParameter> list = new List<DbParameter>();
			if (dic != null)
			{
				foreach (string key in dic.Keys)
				{
					list.Add(base.Database.MakeInParam(key, dic[key]));
				}
			}
			return base.Database.ExecuteDataset(CommandType.StoredProcedure, procName, list.ToArray());
		}

		public DataTable GetAdvertisers(string fields, string where)
		{
			DataTable dataTable = new DataTable();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("select {0} from dbo.T_Rec_Advertiser {1}", fields, where);
			return base.Database.ExecuteDataset(stringBuilder.ToString()).Tables[0];
		}

		public DataSet GetRanks(Dictionary<string, string> dic, string ProcName, string account)
		{
			List<DbParameter> list = new List<DbParameter>();
			if (dic != null)
			{
				foreach (string key in dic.Keys)
				{
					if (key.Contains("|"))
					{
						string[] array = key.Split('|');
						if (array.Length > 1)
						{
							list.Add(base.Database.MakeOutParam(array[1], typeof(int)));
						}
					}
					else
					{
						list.Add(base.Database.MakeInParam(key, dic[key]));
					}
				}
			}
			DataSet dataSet = base.Database.ExecuteDataset(CommandType.StoredProcedure, ProcName, list.ToArray());
			if (!string.IsNullOrEmpty(account))
			{
				int num = 0;
				int num2 = 0;
				DataSet dataSet2 = new DataSet();
				if (dataSet != null && dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
				{
					DataRow[] array2 = dataSet.Tables[0].Select(" Accounts like '%" + account + "%' or NickName like '%" + account + "%'");
					DataTable dataTable = dataSet.Tables[0].Clone();
					if (array2 != null)
					{
						DataRow[] array3 = array2;
						foreach (DataRow dataRow in array3)
						{
							DataRow dataRow2 = dataTable.NewRow();
							dataRow2["rnk"] = dataRow["rnk"];
							dataRow2["UserID"] = dataRow["UserID"];
							dataRow2["Accounts"] = dataRow["Accounts"];
							dataRow2["NickName"] = dataRow["NickName"];
							dataRow2["ADID"] = dataRow["ADID"];
							dataRow2["Score"] = dataRow["Score"];
							dataRow2["PlayTimeCount"] = dataRow["PlayTimeCount"];
							dataRow2["MyScore"] = dataRow["MyScore"];
							dataTable.Rows.Add(dataRow2);
						}
					}
					num = 1;
					num2 = array2.Length;
					dataSet2.Tables.Add(dataTable);
				}
				DataTable dataTable2 = new DataTable();
				dataTable2.Columns.Add("PageCount");
				dataTable2.Columns.Add("RecordCount");
				DataRow dataRow3 = dataTable2.NewRow();
				dataRow3["PageCount"] = num;
				dataRow3["RecordCount"] = num2;
				dataTable2.Rows.Add(dataRow3);
				dataSet2.Tables.Add(dataTable2);
				dataSet = dataSet2;
			}
			else
			{
				DataTable dataTable3 = new DataTable();
				dataTable3.Columns.Add("PageCount");
				dataTable3.Columns.Add("RecordCount");
				DataRow dataRow4 = dataTable3.NewRow();
				dataRow4["PageCount"] = Convert.ToInt32(list[list.Count - 3].Value);
				dataRow4["RecordCount"] = Convert.ToInt32(list[list.Count - 2].Value);
				dataTable3.Rows.Add(dataRow4);
				dataSet.Tables.Add(dataTable3);
			}
			return dataSet;
		}

		public Message EditAdvertiser(TAdvertiser model)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("ID", model.ID));
			list.Add(base.Database.MakeInParam("AdKey", model.AdKey));
			list.Add(base.Database.MakeInParam("Advertiser", model.Advertiser));
			list.Add(base.Database.MakeInParam("Url", model.Url));
			list.Add(base.Database.MakeInParam("PcUrl", model.PcUrl));
			list.Add(base.Database.MakeInParam("Descr", model.Descr));
			list.Add(base.Database.MakeInParam("StatusT", model.StatusT));
			list.Add(base.Database.MakeInParam("Operator", model.Operator));
			list.Add(base.Database.MakeInParam("strClientIP", model.ClientIP));
			list.Add(base.Database.MakeOutParam("strErr", typeof(string), 127));
			return MessageHelper.GetMessage(base.Database, "NET_PW_EditAdvertiser", list);
		}

		public bool DelAdvertiser(int id)
		{
			string commandText = "DELETE  FROM T_Rec_Advertiser WHERE ID=" + id;
			return base.Database.ExecuteNonQuery(commandText) > 0;
		}

		public DataTable GetAdvertTask(string fields, string where)
		{
			DataTable dataTable = new DataTable();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("select {0} from dbo.T_Rec_AdvertTask {1}", fields, where);
			return base.Database.ExecuteDataset(stringBuilder.ToString()).Tables[0];
		}

		public Message EditAdvertTask(AdvertTask model)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("ID", model.ID));
			list.Add(base.Database.MakeInParam("AdvertiserID", model.AdvertiserID));
			list.Add(base.Database.MakeInParam("TaskName", model.TaskName));
			list.Add(base.Database.MakeInParam("StatusT", model.StatusT));
			list.Add(base.Database.MakeInParam("AdID", model.AdID));
			list.Add(base.Database.MakeInParam("BeginTime", model.BeginTime));
			list.Add(base.Database.MakeInParam("EndTime", model.EndTime));
			list.Add(base.Database.MakeOutParam("strErr", typeof(string), 127));
			return MessageHelper.GetMessage(base.Database, "NET_PW_EditAdvertTask", list);
		}

		public DataTable GetAdvertTaskKinds(int taskID)
		{
			string commandText = "select t1.KindID,t1.KindName,isnull(t2.KindID,0) as itemKingId,isnull(t2.CountType,1) CountTypes,t2.IsWeek from RYPlatformDB..GameKindItem t1 left join T_Rec_AdvertTaskKinds t2  on t1.KindID = t2.KindID and TaskID= " + taskID;
			return base.Database.ExecuteDataset(commandText).Tables[0];
		}

		public bool EidterAdvertTaskKinds(List<T_Rec_AdvertTaskKindsEntity> entities)
		{
			bool flag = true;
			if (entities != null && entities.Count > 0)
			{
				int taskID = entities[0].TaskID;
				if (taskID > 0)
				{
					base.Database.ExecuteNonQuery("DELETE T_Rec_AdvertTaskKinds where TaskID=" + taskID);
				}
				else
				{
					flag = false;
				}
				if (flag)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("insert into dbo.T_Rec_AdvertTaskKinds(TaskID,KindID,CountType,IsWeek) values");
					foreach (T_Rec_AdvertTaskKindsEntity entity in entities)
					{
						stringBuilder.AppendFormat("({0},{1},{2},{3}),", entity.TaskID, entity.KindID, entity.CountType, entity.IsWeek);
					}
					string text = stringBuilder.ToString().TrimEnd(',');
					flag = (!string.IsNullOrEmpty(text) && ((base.Database.ExecuteNonQuery(text) > 0) ? true : false));
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public void InsertBaiRenGameLog(BarrenGameLog model)
		{
			string commandText = "INSERT INTO BarrenGameLog(ServerID,ServerName,StorageStart,AttenuationScore,StorageDeduct,[Operator],ClientIP)VALUES(@ServerID,@ServerName,@StorageStart,@AttenuationScore,@StorageDeduct,@Operator,@ClientIP)";
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("ServerID", model.ServerID));
			list.Add(base.Database.MakeInParam("ServerName", model.ServerName));
			list.Add(base.Database.MakeInParam("StorageStart", model.StorageStart));
			list.Add(base.Database.MakeInParam("AttenuationScore", model.AttenuationScore));
			list.Add(base.Database.MakeInParam("StorageDeduct", model.StorageDeduct));
			list.Add(base.Database.MakeInParam("Operator", model.Operator));
			list.Add(base.Database.MakeInParam("ClientIP", model.ClientIP));
			base.Database.ExecuteNonQuery(CommandType.Text, commandText, list.ToArray());
		}

		public PagerSet getAgentRecharges(Dictionary<string, object> dic, out decimal total)
		{
			DataSet ds = null;
			PagerSet pagerSet = new PagerSet();
			List<DbParameter> list = new List<DbParameter>();
			foreach (string key in dic.Keys)
			{
				list.Add(base.Database.MakeInParam(key, dic[key]));
			}
			list.Add(base.Database.MakeOutParam("PageCount", typeof(int)));
			list.Add(base.Database.MakeOutParam("RecordCount", typeof(int)));
			list.Add(base.Database.MakeOutParam("SumGold", typeof(decimal)));
			base.Database.RunProc("RYAgentDB..P_QueryAgentFill", list, out ds);
			pagerSet.PageSet = ds;
			pagerSet.RecordCount = Convert.ToInt32(list[8].Value);
			total = Convert.ToDecimal(list[9].Value);
			return pagerSet;
		}

		public Message getAgentQueryInfo(Dictionary<string, object> dic)
		{
			List<DbParameter> list = new List<DbParameter>();
			foreach (string key in dic.Keys)
			{
				list.Add(base.Database.MakeInParam(key, dic[key]));
			}
			list.Add(base.Database.MakeOutParam("strErr", typeof(string), 127));
			return MessageHelper.GetMessageForDataSet(base.Database, "RYAgentDB..P_AgentQueryInfo", list);
		}

		public PagerSet getAgentGameRecord(Dictionary<string, object> dic)
		{
			DataSet ds = null;
			PagerSet pagerSet = new PagerSet();
			List<DbParameter> list = new List<DbParameter>();
			foreach (string key in dic.Keys)
			{
				list.Add(base.Database.MakeInParam(key, dic[key]));
			}
			list.Add(base.Database.MakeOutParam("PageCount", typeof(int)));
			list.Add(base.Database.MakeOutParam("RecordCount", typeof(int)));
			base.Database.RunProc("RYAgentDB..P_QueryPlayerGame", list, out ds);
			pagerSet.PageSet = ds;
			pagerSet.RecordCount = Convert.ToInt32(list[8].Value);
			return pagerSet;
		}

		public PagerSet getAgentBalance(Dictionary<string, object> dic, out decimal total)
		{
			DataSet ds = null;
			PagerSet pagerSet = new PagerSet();
			List<DbParameter> list = new List<DbParameter>();
			foreach (string key in dic.Keys)
			{
				list.Add(base.Database.MakeInParam(key, dic[key]));
			}
			list.Add(base.Database.MakeOutParam("PageCount", typeof(int)));
			list.Add(base.Database.MakeOutParam("RecordCount", typeof(int)));
			list.Add(base.Database.MakeOutParam("SumGold", typeof(decimal)));
			base.Database.RunProc("RYAgentDB..P_QueryPlayerDraw", list, out ds);
			pagerSet.PageSet = ds;
			pagerSet.RecordCount = Convert.ToInt32(list[8].Value);
			total = Convert.ToDecimal(list[9].Value);
			return pagerSet;
		}

		public PagerSet QueryPlayerWinLost(Dictionary<string, object> dic)
		{
			DataSet ds = null;
			PagerSet pagerSet = new PagerSet();
			List<DbParameter> list = new List<DbParameter>();
			foreach (string key in dic.Keys)
			{
				list.Add(base.Database.MakeInParam(key, dic[key]));
			}
			list.Add(base.Database.MakeOutParam("PageCount", typeof(int)));
			list.Add(base.Database.MakeOutParam("RecordCount", typeof(int)));
			list.Add(base.Database.MakeOutParam("SumWinlost", typeof(string), 100));
			base.Database.RunProc("RYAgentDB..P_QueryPlayerWinLost", list, out ds);
			pagerSet.PageSet = ds;
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("SumWinlost");
			DataRow dataRow = dataTable.NewRow();
			decimal result = 0m;
			dataRow["SumWinlost"] = (decimal.TryParse(list[10].Value.ToString(), out result) ? result : 0m);
			dataTable.Rows.Add(dataRow);
			pagerSet.PageSet.Tables.Add(dataTable);
			pagerSet.RecordCount = Convert.ToInt32(list[9].Value);
			return pagerSet;
		}

		public DataTable QueryUserGameWinLost(Dictionary<string, object> dic)
		{
			DataSet ds = null;
			List<DbParameter> list = new List<DbParameter>();
			foreach (string key in dic.Keys)
			{
				list.Add(base.Database.MakeInParam(key, dic[key]));
			}
			base.Database.RunProc("RYAgentDB..P_QueryUserGameWinLost", list, out ds);
			return ds.Tables[0];
		}
	}
}
