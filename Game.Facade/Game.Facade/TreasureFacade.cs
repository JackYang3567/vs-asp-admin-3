using Game.Data.Factory;
using Game.Entity;
using Game.Entity.Treasure;
using Game.IData;
using Game.Kernel;
using System;
using System.Collections.Generic;
using System.Data;

namespace Game.Facade
{
	public class TreasureFacade
	{
		private ITreasureDataProvider aideTreasureData;

		public TreasureFacade()
		{
			aideTreasureData = ClassFactory.GetITreasureDataProvider();
		}

		public TreasureFacade(int kindID)
		{
			aideTreasureData = ClassFactory.GetITreasureDataProvider(kindID);
		}

		public PagerSet GetAgentPayOrder(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetAgentPayOrder(pageIndex, pageSize, condition, orderby);
		}

		public PagerSet GetGlobalAppInfoList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetGlobalAppInfoList(pageIndex, pageSize, condition, orderby);
		}

		public void DeleteGlobalAppInfo(string sqlQuery)
		{
			aideTreasureData.DeleteGlobalAppInfo(sqlQuery);
		}

		public DataTable GetGlobalAppInfo(int AppID)
		{
			return aideTreasureData.GetGlobalAppInfoes(AppID);
		}

		public int EditGlobalAppInfo(int AppID, decimal Price, decimal PresentCurrency, int SortID)
		{
			return aideTreasureData.EditGlobalAppInfo(AppID, Price, PresentCurrency, SortID);
		}

		public PagerSet GetGlobalLivcardList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetGlobalLivcardList(pageIndex, pageSize, condition, orderby);
		}

		public GlobalLivcard GetGlobalLivcardInfo(int cardTypeID)
		{
			return aideTreasureData.GetGlobalLivcardInfo(cardTypeID);
		}

		public Message InsertGlobalLivcard(GlobalLivcard globalLivcard)
		{
			aideTreasureData.InsertGlobalLivcard(globalLivcard);
			return new Message(true);
		}

		public Message UpdateGlobalLivcard(GlobalLivcard globalLivcard)
		{
			aideTreasureData.UpdateGlobalLivcard(globalLivcard);
			return new Message(true);
		}

		public void DeleteGlobalLivcard(string sqlQuery)
		{
			aideTreasureData.DeleteGlobalLivcard(sqlQuery);
		}

		public PagerSet GetLivcardBuildStreamList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetLivcardBuildStreamList(pageIndex, pageSize, condition, orderby);
		}

		public LivcardBuildStream GetLivcardBuildStreamInfo(int buildID)
		{
			return aideTreasureData.GetLivcardBuildStreamInfo(buildID);
		}

		public int InsertLivcardBuildStream(LivcardBuildStream livcardBuildStream)
		{
			return aideTreasureData.InsertLivcardBuildStream(livcardBuildStream);
		}

		public void UpdateLivcardBuildStream(LivcardBuildStream livcardBuildStream)
		{
			aideTreasureData.UpdateLivcardBuildStream(livcardBuildStream);
		}

		public void UpdateLivcardBuildStream(int buildID)
		{
			aideTreasureData.UpdateLivcardBuildStream(buildID);
		}

		public PagerSet GetLivcardAssociatorList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetLivcardAssociatorList(pageIndex, pageSize, condition, orderby);
		}

		public LivcardAssociator GetLivcardAssociatorInfo(int cardID)
		{
			return aideTreasureData.GetLivcardAssociatorInfo(cardID);
		}

		public LivcardAssociator GetLivcardAssociatorInfo(string serialID)
		{
			return aideTreasureData.GetLivcardAssociatorInfo(serialID);
		}

		public ShareDetailInfo GetShareDetailInfo(string serialID)
		{
			return aideTreasureData.GetShareDetailInfo(serialID);
		}

		public string GetSalesperson(int buildID)
		{
			return aideTreasureData.GetSalesperson(buildID);
		}

		public void SetCardDisbale(string sqlQuery)
		{
			aideTreasureData.SetCardDisbale(sqlQuery);
		}

		public void SetCardEnbale(string sqlQuery)
		{
			aideTreasureData.SetCardEnbale(sqlQuery);
		}

		public void InsertLivcardAssociator(LivcardAssociator livcardAssociator, string[,] list)
		{
			aideTreasureData.InsertLivcardAssociator(livcardAssociator, list);
		}

		public DataSet GetLivcardStat()
		{
			return aideTreasureData.GetLivcardStat();
		}

		public DataSet GetLivcardStatByBuildID(int buildID)
		{
			return aideTreasureData.GetLivcardStatByBuildID(buildID);
		}

		public PagerSet GetOnLineOrderList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetOnLineOrderList(pageIndex, pageSize, condition, orderby);
		}

		public OnLineOrder GetOnLineOrderInfo(string orderID)
		{
			return aideTreasureData.GetOnLineOrderInfo(orderID);
		}

		public void DeleteOnlineOrder(string sqlQuery)
		{
			aideTreasureData.DeleteOnlineOrder(sqlQuery);
		}

		public PagerSet GetKQDetailList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetKQDetailList(pageIndex, pageSize, condition, orderby);
		}

		public ReturnKQDetailInfo GetKQDetailInfo(int detailID)
		{
			return aideTreasureData.GetKQDetailInfo(detailID);
		}

		public void DeleteKQDetail(string sqlQuery)
		{
			aideTreasureData.DeleteKQDetail(sqlQuery);
		}

		public PagerSet GetYPDetailList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetYPDetailList(pageIndex, pageSize, condition, orderby);
		}

		public ReturnYPDetailInfo GetYPDetailInfo(int detailID)
		{
			return aideTreasureData.GetYPDetailInfo(detailID);
		}

		public void DeleteYPDetail(string sqlQuery)
		{
			aideTreasureData.DeleteYPDetail(sqlQuery);
		}

		public PagerSet GetVBDetailList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetVBDetailList(pageIndex, pageSize, condition, orderby);
		}

		public ReturnVBDetailInfo GetVBDetailInfo(int detailID)
		{
			return aideTreasureData.GetVBDetailInfo(detailID);
		}

		public void DeleteVBDetail(string sqlQuery)
		{
			aideTreasureData.DeleteVBDetail(sqlQuery);
		}

		public PagerSet GetDayDetailList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetDayDetailList(pageIndex, pageSize, condition, orderby);
		}

		public ReturnDayDetailInfo GetDayDetailInfo(int detailID)
		{
			return aideTreasureData.GetDayDetailInfo(detailID);
		}

		public void DeleteDayDetail(string sqlQuery)
		{
			aideTreasureData.DeleteDayDetail(sqlQuery);
		}

		public PagerSet GetShareDetailList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetShareDetailList(pageIndex, pageSize, condition, orderby);
		}

		public GlobalShareInfo GetGlobalShareByShareID(int shareID)
		{
			return aideTreasureData.GetGlobalShareByShareID(shareID);
		}

		public PagerSet GetGlobalShareList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetGlobalShareList(pageIndex, pageSize, condition, orderby);
		}

		public PagerSet GetAppDetailList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetAppDetailList(pageIndex, pageSize, condition, orderby);
		}

		public ReturnAppDetailInfo GetAppDetailInfo(int detailID)
		{
			return aideTreasureData.GetAppDetailInfo(detailID);
		}

		public DataSet SumPay(string where)
		{
			string str = "SELECT ISNULL(SUM(PayAmount),0) AS PayAmount,Count(1) AS SuccessCount FROM OnLineOrder " + where + " AND OrderStatus=2";
			str = str + ";SELECT ISNULL(SUM(PayAmount),0) FROM OnLineOrder " + where + " AND OrderStatus<>2";
			return aideTreasureData.GetDataSetBySql(str);
		}

		public Message InsertGlobalAppInfo(GlobalAppInfo globalApp)
		{
			aideTreasureData.InsertGlobalAppInfo(globalApp);
			return new Message(true);
		}

		public Message UpdateGlobalAppInfo(GlobalAppInfo globalApp)
		{
			aideTreasureData.UpdateGlobalAppInfo(globalApp);
			return new Message(true);
		}


        public Message InsertPlantInfo(T_PayPlatformInfo globalApp)
        {
            aideTreasureData.InsertPlantInfo(globalApp);
            return new Message(true);
        }

        public Message UpdatePlantInfo(T_PayPlatformInfo globalApp)
        {
            aideTreasureData.UpdatePlantInfo(globalApp);
            return new Message(true);
        }

		public DataTable GetMoney(int userId)
		{
			string sql = "SELECT Score,InsureScore FROM GameScoreInfo WHERE UserID=" + userId;
			return aideTreasureData.GetDataSetBySql(sql).Tables[0];
		}

		public PagerSet GetGameScoreInfoList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetGameScoreInfoList(pageIndex, pageSize, condition, orderby);
		}

		public GameScoreInfo GetGameScoreInfoByUserID(int UserID)
		{
			return aideTreasureData.GetGameScoreInfoByUserID(UserID);
		}

		public decimal GetGameScoreByUserID(int UserID)
		{
			return aideTreasureData.GetGameScoreByUserID(UserID);
		}

		public void UpdateInsureScore(GameScoreInfo gameScoreInfo)
		{
			aideTreasureData.UpdateInsureScore(gameScoreInfo);
		}

        public Message OffLinePass(string ID, int statu, int intMasterID, string strIP)
        {
            return aideTreasureData.OffLinePass(ID, statu,intMasterID, strIP);
        }

		public Message GrantTreasure(int type, string strUserIdList, decimal intGold, int intMasterID, string strReason, string strIP)
		{
			return aideTreasureData.GrantTreasure(type, strUserIdList, intGold, intMasterID, strReason, strIP);
		}

		public Message GrantScore(int userID, int kindID, int score, int masterID, string strReason, string strIP)
		{
			return aideTreasureData.GrantScore(userID, kindID, score, masterID, strReason, strIP);
		}

		public Message GrantClearScore(int userID, int kindID, int masterID, string strReason, string strIP)
		{
			return aideTreasureData.GrantClearScore(userID, kindID, masterID, strReason, strIP);
		}

		public Message GrantFlee(int userID, int kindID, int masterID, string strReason, string strIP)
		{
			return aideTreasureData.GrantFlee(userID, kindID, masterID, strReason, strIP);
		}

		public UserCurrencyInfo GetUserCurrencyInfo(int userID)
		{
			return aideTreasureData.GetUserCurrencyInfo(userID);
		}

		public PagerSet GetRecordDrawInfoList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetRecordDrawInfoList(pageIndex, pageSize, condition, orderby);
		}

		public int DeleteRecordDrawInfoByTime(DateTime dt)
		{
			return aideTreasureData.DeleteRecordDrawInfoByTime(dt);
		}

		public PagerSet GetRecordDrawScoreList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetRecordDrawScoreList(pageIndex, pageSize, condition, orderby);
		}

		public DataSet GetRecordDrawScoreByDrawID(int drawID)
		{
			return aideTreasureData.GetRecordDrawScoreByDrawID(drawID);
		}

		public int DeleteRecordDrawScoreByTime(DateTime dt)
		{
			return aideTreasureData.DeleteRecordDrawScoreByTime(dt);
		}

		public PagerSet GetGameScoreLockerList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetGameScoreLockerList(pageIndex, pageSize, condition, orderby);
		}

		public void DeleteGameScoreLockerByUserID(int userID)
		{
			aideTreasureData.DeleteGameScoreLockerByUserID(userID);
		}

		public void DeleteGameScoreLocker(string sqlQuery)
		{
			aideTreasureData.DeleteGameScoreLocker(sqlQuery);
		}

		public GameScoreLocker GetGameScoreLockerByUserID(int userID)
		{
			return aideTreasureData.GetGameScoreLockerByUserID(userID);
		}

		public PagerSet GetAndroidList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetAndroidList(pageIndex, pageSize, condition, orderby);
		}

		public AndroidManager GetAndroidInfo(int userID)
		{
			return aideTreasureData.GetAndroidInfo(userID);
		}

		public Message InsertAndroid(AndroidManager android)
		{
			aideTreasureData.InsertAndroid(android);
			return new Message(true);
		}

		public Message UpdateAndroid(AndroidManager android)
		{
			aideTreasureData.UpdateAndroid(android);
			return new Message(true);
		}

		public void DeleteAndroid(string sqlQuery)
		{
			aideTreasureData.DeleteAndroid(sqlQuery);
		}

		public void NullityAndroid(byte nullity, string sqlQuery)
		{
			aideTreasureData.NullityAndroid(nullity, sqlQuery);
		}

		public PagerSet GetRecordUserInoutList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetRecordUserInoutList(pageIndex, pageSize, condition, orderby);
		}

		public int DeleteRecordUserInoutByTime(DateTime dt)
		{
			return aideTreasureData.DeleteRecordUserInoutByTime(dt);
		}

		public PagerSet GetRecordInsureList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetRecordInsureList(pageIndex, pageSize, condition, orderby);
		}

		public DataSet GetUserTransferTop100()
		{
			return aideTreasureData.GetUserTransferTop100();
		}

		public int DeleteRecordInsureByTime(DateTime dt)
		{
			return aideTreasureData.DeleteRecordInsureByTime(dt);
		}

		public GlobalSpreadInfo GetGlobalSpreadInfo(int id)
		{
			return aideTreasureData.GetGlobalSpreadInfo(id);
		}

		public void UpdateGlobalSpreadInfo(GlobalSpreadInfo spreadinfo)
		{
			aideTreasureData.UpdateGlobalSpreadInfo(spreadinfo);
		}

		public void UpdatePromoterSet(GlobalSpreadInfo spreadinfo)
		{
			aideTreasureData.UpdatePromoterSet(spreadinfo);
		}

		public PagerSet GetRecordSpreadInfoList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetRecordSpreadInfoList(pageIndex, pageSize, condition, orderby);
		}

		public int GetSpreadScore(int userID)
		{
			return aideTreasureData.GetSpreadScore(userID);
		}

		public DataSet GetGoldDistribution()
		{
			return aideTreasureData.GetGoldDistribution();
		}

		public DataSet GetPayStat()
		{
			return aideTreasureData.GetPayStat();
		}

		public DataSet GetPayStatByDay(string startID, string endID)
		{
			return aideTreasureData.GetPayStatByDay(startID, endID);
		}

		public DataSet GetActiveUserByDay(int startDateID, int endDateID)
		{
			return aideTreasureData.GetActiveUserByDay(startDateID, endDateID);
		}

		public DataSet GetActivieUserByMonth()
		{
			return aideTreasureData.GetActivieUserByMonth();
		}

		public DataSet StatRecordTable()
		{
			return aideTreasureData.StatRecordTable();
		}

		public DataSet GetStatInfo()
		{
			return aideTreasureData.GetStatInfo();
		}

		public Message AppStatFilled(string accounts, string logonPass, string machineID)
		{
			return aideTreasureData.AppStatFilled(accounts, logonPass, machineID);
		}

		public Message AppStatFilledCash(string accounts, string logonPass, string machineID)
		{
			return aideTreasureData.AppStatFilledCash(accounts, logonPass, machineID);
		}

		public Message AppStatFilledScore(string accounts, string logonPass, string machineID)
		{
			return aideTreasureData.AppStatFilledScore(accounts, logonPass, machineID);
		}

		public Message AppStatScorePresent(string accounts, string logonPass, string machineID)
		{
			return aideTreasureData.AppStatScorePresent(accounts, logonPass, machineID);
		}

		public Message AppStatScoreRevenue(string accounts, string logonPass, string machineID)
		{
			return aideTreasureData.AppStatScoreRevenue(accounts, logonPass, machineID);
		}

		public Message AppStatScoreWaste(string accounts, string logonPass, string machineID)
		{
			return aideTreasureData.AppStatScoreWaste(accounts, logonPass, machineID);
		}

		public Message AppGetChargeData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate)
		{
			return aideTreasureData.AppGetChargeData(accounts, logonPass, typeID, machineID, dateType, startDate, endDate);
		}

		public Message AppGetPayScoreData(string accounts, string logonPass, string machineID, int dateType, string startDate, string endDate)
		{
			return aideTreasureData.AppGetPayScoreData(accounts, logonPass, machineID, dateType, startDate, endDate);
		}

		public Message AppGetRevenueData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate)
		{
			return aideTreasureData.AppGetRevenueData(accounts, logonPass, typeID, machineID, dateType, startDate, endDate);
		}

		public Message AppGetPresentData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate)
		{
			return aideTreasureData.AppGetPresentData(accounts, logonPass, typeID, machineID, dateType, startDate, endDate);
		}

		public Message AppGetWasteData(string accounts, string logonPass, string machineID, int dateType, string startDate, string endDate)
		{
			return aideTreasureData.AppGetWasteData(accounts, logonPass, machineID, dateType, startDate, endDate);
		}

		public Message AppGetPlatScoreData(string accounts, string logonPass, string machineID, int dateType, string startDate, string endDate)
		{
			return aideTreasureData.AppGetPlatScoreData(accounts, logonPass, machineID, dateType, startDate, endDate);
		}

		public Message AppGetMemberData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate)
		{
			return aideTreasureData.AppGetMemberData(accounts, logonPass, typeID, machineID, dateType, startDate, endDate);
		}

		public long GetChildRevenueProvide(int userID)
		{
			return aideTreasureData.GetChildRevenueProvide(userID);
		}

		public long GetChildPayProvide(int userID)
		{
			return aideTreasureData.GetChildPayProvide(userID);
		}

		public DataSet GetAgentFinance(int userID)
		{
			return aideTreasureData.GetAgentFinance(userID);
		}

		public void StatRevenueHand()
		{
			aideTreasureData.StatRevenueHand();
		}

		public void StatAgentPayHand()
		{
			aideTreasureData.StatAgentPayHand();
		}

		public LotteryConfig GetLotteryConfig(int id)
		{
			return aideTreasureData.GetLotteryConfig(id);
		}

		public void UpdateLotteryConfig(LotteryConfig model)
		{
			aideTreasureData.UpdateLotteryConfig(model);
		}

		public int UpdateLotteryItem(LotteryItem model)
		{
			return aideTreasureData.UpdateLotteryItem(model);
		}

		public PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetList(tableName, pageIndex, pageSize, condition, orderby);
		}

		public int ExecuteSql(string sql)
		{
			return aideTreasureData.ExecuteSql(sql);
		}

		public DataSet GetDataSetBySql(string sql)
		{
			return aideTreasureData.GetDataSetBySql(sql);
		}

		public string GetScalarBySql(string sql)
		{
			return aideTreasureData.GetScalarBySql(sql);
		}

		public double SumRevenue(string sWhere)
		{
			string sql = "SELECT SUM(Revenue) FROM View_RecordDrawScore " + sWhere;
			string scalarBySql = aideTreasureData.GetScalarBySql(sql);
			if (string.IsNullOrEmpty(scalarBySql))
			{
				return 0.0;
			}
			return Convert.ToDouble(scalarBySql);
		}

		public DataSet SumRevenueAll()
		{
			string sql = "SELECT SUM(Revenue) FROM RecordDrawScore(NOLOCK);SELECT SUM(RealRev) FROM RYAgentDB..view_MyRevRecord(NOLOCK);";
			return aideTreasureData.GetDataSetBySql(sql);
		}

		public DataSet GetGameRevenue(string stime, string etime)
		{
			return aideTreasureData.GetGameRevenue(stime, etime);
		}

		public DataSet GetRecordPresentInfo(string where)
		{
			return aideTreasureData.GetRecordPresentInfo(where);
		}

		public DataSet GetRecordType(string TabName)
		{
			return aideTreasureData.GetRecordType(TabName);
		}

		public decimal TotalScore(string sWhere)
		{
			string sql = "SELECT SUM(PresentScore) FROM View_PresentInfo " + sWhere;
			object scalarBySql = aideTreasureData.GetScalarBySql(sql);
			if (scalarBySql == null)
			{
				return 0m;
			}
			return Convert.ToDecimal(scalarBySql);
		}

		public decimal TotalAgentScore(string sWhere)
		{
			string sql = "SELECT SUM(SwapScore) FROM RYAgentDB..View_AgentScoreLog " + sWhere;
			object scalarBySql = aideTreasureData.GetScalarBySql(sql);
			if (scalarBySql == null)
			{
				return 0m;
			}
			return Convert.ToDecimal(scalarBySql);
		}

		public DataTable GetType(string tabName)
		{
			string sql = "SELECT ID,TypeName,IsShow FROM RecordType WHERE TabName='" + tabName + "'";
			return aideTreasureData.GetDataSetBySql(sql).Tables[0];
		}

		public int DeletePresentByTime(DateTime dt)
		{
			string sql = "DELETE RecordPresentInfo WHERE CollectDate<='" + dt + "'";
			return aideTreasureData.ExecuteSql(sql);
		}

		public int DeleteSpreadByTime(DateTime dt)
		{
			string sql = "DELETE RecordSpreadInfo WHERE CollectDate<='" + dt + "'";
			return aideTreasureData.ExecuteSql(sql);
		}

		public int DeleteAgentScoreByTime(DateTime dt)
		{
			string sql = "DELETE RYAgentDB.dbo.T_Rec_AgentScoreLog WHERE SwapDate<='" + dt + "'";
			return aideTreasureData.ExecuteSql(sql);
		}

		public int DeleteAgentChangeByTime(DateTime dt)
		{
			string sql = "DELETE RYAgentDB.dbo.T_Plm_AgentChangeLog WHERE UpdTime<='" + dt + "'";
			return aideTreasureData.ExecuteSql(sql);
		}

		public int DeleteSystemByTime(DateTime dt)
		{
			string sql = "DELETE RYPlatformManagerDB..SystemSecurity WHERE OperatingTime<='" + dt + "'";
			return aideTreasureData.ExecuteSql(sql);
		}

		public int EditRecordType(RecordType entity)
		{
			return aideTreasureData.EditRecordType(entity);
		}

		public void DelRecordType(string sWhere)
		{
			string sql = "DELETE RecordType " + sWhere;
			aideTreasureData.ExecuteSql(sql);
		}

		public PagerSet GetPager(Dictionary<string, string> dic, string procName)
		{
			return aideTreasureData.GetPager(dic, procName);
		}

		public SQLResult GetTable(Dictionary<string, string> dic, string procName)
		{
			return aideTreasureData.GetTable(dic, procName);
		}

		public DataTable GetAccountScoreById(int id)
		{
			return aideTreasureData.GetAccountScoreById(id);
		}

		public PagerSet GetAgentPlayRecords(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideTreasureData.GetAgentPlayRecords(pageIndex, pageSize, condition, orderby);
		}

		public int Freeze(string ids, int nullity)
		{
            string sql = "UPDATE T_PayPlatformInfo SET Nullity=" + nullity + " WHERE ID IN(" + ids + ")";
			return aideTreasureData.ExecuteSql(sql);
		}

		public int UpdateSort(int id, int sort)
		{
            string sql = "UPDATE T_PayPlatformInfo SET SortID=" + sort + " WHERE ID =" + id;
			return aideTreasureData.ExecuteSql(sql);
		}

		public int FreezeQudao(string ids, int nullity)
		{
			string sql = "UPDATE T_PayQudaoInfo SET IsShow=" + nullity + " WHERE ID IN(" + ids + ")";
			return aideTreasureData.ExecuteSql(sql);
		}

		public int UpdateQudaoSort(int id, int sort)
		{
			string sql = "UPDATE T_PayQudaoInfo SET SortID=" + sort + " WHERE ID =" + id;
			return aideTreasureData.ExecuteSql(sql);
		}

		public DataTable GetPayQudaoList()
		{
			string sql = "SELECT ID,QudaoName FROM T_PayQudaoInfo WHERE (IsShow=1 and ID!=11) OR ID=100";
			return aideTreasureData.GetDataSetBySql(sql).Tables[0];
		}
        public DataTable GetPayQudaoOfOffLinePaymentList()
        {
            string sql = "SELECT ID,QudaoName FROM T_PayQudaoInfo WHERE ID=4  OR ID=5";
            return aideTreasureData.GetDataSetBySql(sql).Tables[0];
        }

        public DataTable GetOffLinPaymentList()
        {
            string sql = "SELECT * FROM OffLinePayOrders WHERE IsAuded=0";
            return aideTreasureData.GetDataSetBySql(sql).Tables[0];
        }

        public int DelOffLinePay(string sqlWhere)
        {
            string sql = "DELETE OffLinePayOrders " + sqlWhere;
            return aideTreasureData.ExecuteSql(sql);
        }

        public int UpdateOffLinePay(int isAuded, int id)
        {
            string sql = "UPDATE OffLinePayOrders SET IsAuded=" + isAuded + " WHERE OffLinePayID =" + id;
            return aideTreasureData.ExecuteSql(sql);
        }

        public int AddOffLineQrCode(int PaymentTypeID, int OwnerID, string PaymentName)        
         {
            string sql = "INSERT OffLinePayQrCode(PaymentTypeID, OwnerID, PaymentName,IconPath) VALUES (" + PaymentTypeID + "," + OwnerID + ",'" + PaymentName + "','')";
			return aideTreasureData.ExecuteSql(sql);
        }

        public int UpdateOffLineQrCode(int PaymentTypeID, int OwnerID, string PaymentName, string IconPath, int ID)
        {
            string sql = "UPDATE OffLinePayQrCode SET PaymentTypeID=" + PaymentTypeID + " WHERE  ID =" + ID;
            return aideTreasureData.ExecuteSql(sql);
        }

        //public OffLineQrCode GetOffLineQrCode(int ID)
        //{
        //    string sql = "SELECT WinScore,WinRate,Accounts,ServerID FROM OffLinePayQrCode WHERE ID=" + ID;
        //    return aideTreasureData.GetDataSetBySql(sql).Tables[0];
        //}

        public OffLineQrCode GetOffLineQrCode(int id)
        {
            return aideTreasureData.GetOffLineQrCode(id);
        }


        public void AddOffLineQrCode(OffLineQrCode model)
		{
            aideTreasureData.AddOffLineQrCode(model);
		}

        public void UpdateOffLineQrCode(OffLineQrCode model)
		{
            aideTreasureData.UpdateOffLineQrCode(model);
		}



        public int DelOffLineQrCode(string sqlWhere)
        {
            string sql = "DELETE OffLinePayQrCode " + sqlWhere;
            return aideTreasureData.ExecuteSql(sql);
        }

		public int AddAmount(int qudaoId, int amount)
		{
			string sql = "INSERT T_QudaoLimit(QudaoID,Limit) VALUES (" + qudaoId + "," + amount + ")";
			return aideTreasureData.ExecuteSql(sql);
		}

		public int UpdateAmount(int amount, int id)
		{
			string sql = "UPDATE T_QudaoLimit SET Limit=" + amount + " WHERE ID =" + id;
			return aideTreasureData.ExecuteSql(sql);
		}

		public int DelAmount(string sqlWhere)
		{
			string sql = "DELETE T_QudaoLimit " + sqlWhere;
			return aideTreasureData.ExecuteSql(sql);
		}

        

		public DataTable GetSpreadLevCfg()
		{
			string sql = "SELECT * FROM RYAgentDB.dbo.view_SpreadLevCfg";
			return GetDataSetBySql(sql).Tables[0];
		}

		public DataTable GetMySpread(int uid)
		{
			string sql = "EXEC RYAgentDB.dbo.P_QueryYjSpread " + uid;
			return GetDataSetBySql(sql).Tables[0];
		}

		public DataTable GetPlayerControl(int userId)
		{
			string sql = "SELECT WinScore,WinRate,Accounts,ServerID FROM View_PlayerControl WHERE UserID=" + userId;
			return aideTreasureData.GetDataSetBySql(sql).Tables[0];
		}

		public int UpdateHighScoreAnnounce(int score, int kid)
		{
			string text = "UPDATE T_HighScoreAnnounce SET Score=" + score + " WHERE KindID =" + kid;
			object obj = text;
			text = obj + ";IF @@ROWCOUNT=0 INSERT T_HighScoreAnnounce(KindID,Score) VALUES(" + kid + "," + score + ")";
			return aideTreasureData.ExecuteSql(text);
		}

		public int DelHighScoreAnnounce(string sqlWhere)
		{
			string sql = "DELETE T_HighScoreAnnounce " + sqlWhere;
			return aideTreasureData.ExecuteSql(sql);
		}
        public string SumWinScore(string sWhere)
        {
            string sql = "select isnull(sum(betscore),0)-isnull(sum(winscore),0) from LotteryBetDraw " + sWhere;
            return aideTreasureData.GetScalarBySql(sql);
        }
        public string Sumbetscore(string sWhere)
        {
            string sql = "select isnull(sum(betscore),0) from LotteryBetDraw " + sWhere;
            return aideTreasureData.GetScalarBySql(sql);
        }
	}
}
