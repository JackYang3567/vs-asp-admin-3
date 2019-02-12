using Game.Entity;
using Game.Entity.Treasure;
using Game.Kernel;
using System;
using System.Collections.Generic;
using System.Data;

namespace Game.IData
{
	public interface ITreasureDataProvider
	{
		PagerSet GetAgentPayOrder(int pageIndex, int pageSize, string condition, string orderby);

		PagerSet GetGlobalAppInfoList(int pageIndex, int pageSize, string condition, string orderby);

		void DeleteGlobalAppInfo(string sqlQuery);

		DataTable GetGlobalAppInfoes(int AppID);

		int EditGlobalAppInfo(int AppID, decimal Price, decimal PresentCurrency, int SortID);

		PagerSet GetGlobalLivcardList(int pageIndex, int pageSize, string condition, string orderby);

		GlobalLivcard GetGlobalLivcardInfo(int cardTypeID);

		void InsertGlobalLivcard(GlobalLivcard globalLivcard);

		void UpdateGlobalLivcard(GlobalLivcard globalLivcard);

		void DeleteGlobalLivcard(string sqlQuery);

		PagerSet GetLivcardBuildStreamList(int pageIndex, int pageSize, string condition, string orderby);

		LivcardBuildStream GetLivcardBuildStreamInfo(int buildID);

		int InsertLivcardBuildStream(LivcardBuildStream livcardBuildStream);

		void UpdateLivcardBuildStream(LivcardBuildStream livcardBuildStream);

		void UpdateLivcardBuildStream(int buildID);

		PagerSet GetLivcardAssociatorList(int pageIndex, int pageSize, string condition, string orderby);

		LivcardAssociator GetLivcardAssociatorInfo(int cardID);

		LivcardAssociator GetLivcardAssociatorInfo(string serialID);

		ShareDetailInfo GetShareDetailInfo(string serialID);

		string GetSalesperson(int buildID);

		void SetCardDisbale(string sqlQuery);

		void SetCardEnbale(string sqlQuery);

		void InsertLivcardAssociator(LivcardAssociator livcardAssociator, string[,] list);

		DataSet GetLivcardStat();

		DataSet GetLivcardStatByBuildID(int buildID);

		PagerSet GetOnLineOrderList(int pageIndex, int pageSize, string condition, string orderby);

		OnLineOrder GetOnLineOrderInfo(string orderID);

		void DeleteOnlineOrder(string sqlQuery);

		PagerSet GetKQDetailList(int pageIndex, int pageSize, string condition, string orderby);

		ReturnKQDetailInfo GetKQDetailInfo(int detailID);

		void DeleteKQDetail(string sqlQuery);

		PagerSet GetDayDetailList(int pageIndex, int pageSize, string condition, string orderby);

		ReturnDayDetailInfo GetDayDetailInfo(int detailID);

		void DeleteDayDetail(string sqlQuery);

		PagerSet GetYPDetailList(int pageIndex, int pageSize, string condition, string orderby);

		ReturnYPDetailInfo GetYPDetailInfo(int detailID);

		void DeleteYPDetail(string sqlQuery);

		PagerSet GetVBDetailList(int pageIndex, int pageSize, string condition, string orderby);

		ReturnVBDetailInfo GetVBDetailInfo(int detailID);

		void DeleteVBDetail(string sqlQuery);

		PagerSet GetShareDetailList(int pageIndex, int pageSize, string condition, string orderby);

		GlobalShareInfo GetGlobalShareByShareID(int shareID);

		PagerSet GetGlobalShareList(int pageIndex, int pageSize, string condition, string orderby);

		PagerSet GetAppDetailList(int pageIndex, int pageSize, string condition, string orderby);

		ReturnAppDetailInfo GetAppDetailInfo(int detailID);

		GlobalAppInfo GetGlobalAppInfo(int appID);

		void InsertGlobalAppInfo(GlobalAppInfo globalApp);

		void UpdateGlobalAppInfo(GlobalAppInfo globalApp);


        void InsertPlantInfo(T_PayPlatformInfo payPlant);

        void UpdatePlantInfo(T_PayPlatformInfo payPlant);

		PagerSet GetGameScoreInfoList(int pageIndex, int pageSize, string condition, string orderby);

		GameScoreInfo GetGameScoreInfoByUserID(int UserID);

		decimal GetGameScoreByUserID(int UserID);

		void UpdateInsureScore(GameScoreInfo gameScoreInfo);
        Message OffLinePass(string ID, int statu, int intMasterID, string strIP);

		Message GrantTreasure(int type, string strUserIdList, decimal intGold, int intMasterID, string strReason, string strIP);

		Message GrantScore(int userID, int kindID, int score, int masterID, string strReason, string strIP);

		Message GrantClearScore(int userID, int kindID, int masterID, string strReason, string strIP);

		Message GrantFlee(int userID, int kindID, int masterID, string strReason, string strIP);

		UserCurrencyInfo GetUserCurrencyInfo(int userID);

		PagerSet GetRecordDrawInfoList(int pageIndex, int pageSize, string condition, string orderby);

		int DeleteRecordDrawInfoByTime(DateTime dt);

		PagerSet GetRecordDrawScoreList(int pageIndex, int pageSize, string condition, string orderby);

		DataSet GetRecordDrawScoreByDrawID(int drawID);

		int DeleteRecordDrawScoreByTime(DateTime dt);

		PagerSet GetGameScoreLockerList(int pageIndex, int pageSize, string condition, string orderby);

		void DeleteGameScoreLockerByUserID(int userID);

		void DeleteGameScoreLocker(string sqlQuery);

		GameScoreLocker GetGameScoreLockerByUserID(int userID);

		PagerSet GetAndroidList(int pageIndex, int pageSize, string condition, string orderby);

		AndroidManager GetAndroidInfo(int userID);

		void InsertAndroid(AndroidManager android);

		void UpdateAndroid(AndroidManager android);

		void DeleteAndroid(string sqlQuery);

		void NullityAndroid(byte nullity, string sqlQuery);

		PagerSet GetRecordUserInoutList(int pageIndex, int pageSize, string condition, string orderby);

		int DeleteRecordUserInoutByTime(DateTime dt);

		PagerSet GetRecordInsureList(int pageIndex, int pageSize, string condition, string orderby);

		DataSet GetUserTransferTop100();

		int DeleteRecordInsureByTime(DateTime dt);

		GlobalSpreadInfo GetGlobalSpreadInfo(int id);

		void UpdateGlobalSpreadInfo(GlobalSpreadInfo spreadinfo);

		void UpdatePromoterSet(GlobalSpreadInfo spreadinfo);

		PagerSet GetRecordSpreadInfoList(int pageIndex, int pageSize, string condition, string orderby);

		int GetSpreadScore(int userID);

		DataSet GetGoldDistribution();

		DataSet GetPayStat();

		DataSet GetPayStatByDay(string startDate, string endDate);

		DataSet GetActiveUserByDay(int startDateID, int endDateID);

		DataSet GetActivieUserByMonth();

		DataSet StatRecordTable();

		DataSet GetStatInfo();

		Message AppStatFilled(string accounts, string logonPass, string machineID);

		Message AppStatFilledCash(string accounts, string logonPass, string machineID);

		Message AppStatFilledScore(string accounts, string logonPass, string machineID);

		Message AppStatScorePresent(string accounts, string logonPass, string machineID);

		Message AppStatScoreRevenue(string accounts, string logonPass, string machineID);

		Message AppStatScoreWaste(string accounts, string logonPass, string machineID);

		Message AppGetChargeData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate);

		Message AppGetPayScoreData(string accounts, string logonPass, string machineID, int dateType, string startDate, string endDate);

		Message AppGetRevenueData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate);

		Message AppGetPresentData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate);

		Message AppGetWasteData(string accounts, string logonPass, string machineID, int dateType, string startDate, string endDate);

		Message AppGetPlatScoreData(string accounts, string logonPass, string machineID, int dateType, string startDate, string endDate);

		Message AppGetMemberData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate);

		long GetChildRevenueProvide(int userID);

		long GetChildPayProvide(int userID);

		DataSet GetAgentFinance(int userID);

		void StatRevenueHand();

		void StatAgentPayHand();

		LotteryConfig GetLotteryConfig(int id);

		void UpdateLotteryConfig(LotteryConfig model);

		int UpdateLotteryItem(LotteryItem model);

		PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby);

        OffLineQrCode GetOffLineQrCode(int id);

        void AddOffLineQrCode(OffLineQrCode model);

        void UpdateOffLineQrCode(OffLineQrCode model);

		int ExecuteSql(string sql);

		DataSet GetDataSetBySql(string sql);

		string GetScalarBySql(string sql);

		DataSet GetGameRevenue(string stime, string etime);

		DataSet GetRecordPresentInfo(string where);

		DataSet GetRecordType(string TabName);

		int EditRecordType(RecordType entity);

		PagerSet GetPager(Dictionary<string, string> dic, string procName);

		SQLResult GetTable(Dictionary<string, string> dic, string procName);

		DataTable GetAccountScoreById(int id);

		PagerSet GetAgentPlayRecords(int pageIndex, int pageSize, string condition, string orderby);
	}
}
