using Game.Entity;
using Game.Entity.Treasure;
using Game.IData;
using Game.Kernel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Game.Data
{
	public class TreasureDataProvider : BaseDataProvider, ITreasureDataProvider
	{
		private ITableProvider aideShareDetialProvider;

		private ITableProvider aideGlobalShareProvider;

		private ITableProvider aideOnLineOrderProvider;

		private ITableProvider aideDayDetailProvider;

		private ITableProvider aideKQDetailProvider;

		private ITableProvider aideYPDetailProvider;

		private ITableProvider aideGameScoreInfoProvider;

		private ITableProvider aideRecordDrawInfoProvider;

		private ITableProvider aideRecordDrawScoreProvider;

		private ITableProvider aideGameScoreLockerProvider;

		private ITableProvider aideAndroidProvider;

		private ITableProvider aideGlobalLivcardProvider;

		private ITableProvider aideLivcardAssociatorProvider;

		private ITableProvider aideLivcardBuildStreamProvider;

		private ITableProvider aideGlobalSpreadInfoProvider;

		private ITableProvider aideGameScoreLocker;

		private ITableProvider aideVBDetailProvider;

		private ITableProvider aideAppDetailProvider;

		private ITableProvider aideGlobalAppProvider;

		private ITableProvider aideRecordExchCurrency;

        private ITableProvider aideLotteryConfigProvider;
        private ITableProvider payPlantProvider;

		public TreasureDataProvider(string connString)
			: base(connString)
		{
			aideShareDetialProvider = GetTableProvider("ShareDetailInfo");
			aideGlobalShareProvider = GetTableProvider("GlobalShareInfo");
			aideOnLineOrderProvider = GetTableProvider("OnLineOrder");
			aideDayDetailProvider = GetTableProvider("ReturnDayDetailInfo");
			aideKQDetailProvider = GetTableProvider("ReturnKQDetailInfo");
			aideYPDetailProvider = GetTableProvider("ReturnYPDetailInfo");
			aideVBDetailProvider = GetTableProvider("ReturnVBDetailInfo");
			aideAppDetailProvider = GetTableProvider("ReturnAppDetailInfo");
			aideGlobalAppProvider = GetTableProvider("GlobalAppInfo");
			aideGameScoreInfoProvider = GetTableProvider("GameScoreInfo");
			aideRecordDrawInfoProvider = GetTableProvider("RecordDrawInfo");
			aideRecordDrawScoreProvider = GetTableProvider("RecordDrawScore");
			aideGameScoreLockerProvider = GetTableProvider("GameScoreLocker");
			aideAndroidProvider = GetTableProvider("AndroidManager");
			aideGlobalLivcardProvider = GetTableProvider("GlobalLivcard");
			aideLivcardAssociatorProvider = GetTableProvider("LivcardAssociator");
			aideLivcardBuildStreamProvider = GetTableProvider("LivcardBuildStream");
			aideGlobalSpreadInfoProvider = GetTableProvider("GlobalSpreadInfo");
			aideGameScoreLocker = GetTableProvider("GameScoreLocker");
			aideRecordExchCurrency = GetTableProvider("RecordExchCurrency");
			aideLotteryConfigProvider = GetTableProvider("LotteryConfig");
            payPlantProvider = GetTableProvider("T_PayPlatformInfo");
            
		}

		public PagerSet GetAgentPayOrder(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("RYAgentDB..T_AgentFillOrder", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public PagerSet GetGlobalAppInfoList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("GlobalAppInfo", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public void DeleteGlobalAppInfo(string sqlQuery)
		{
			aideGlobalAppProvider.Delete(sqlQuery);
		}

		public DataTable GetGlobalAppInfoes(int AppID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select AppId,Price,SortID,PresentCurrency from GlobalAppInfo where ").Append("AppId=@AppId ");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("AppId", AppID));
			DataSet dataSet = base.Database.ExecuteDataset(CommandType.Text, stringBuilder.ToString(), list.ToArray());
			DataTable result = new DataTable();
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				result = dataSet.Tables[0];
			}
			return result;
		}

		public int EditGlobalAppInfo(int AppID, decimal Price, decimal PresentCurrency, int SortID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<DbParameter> list = new List<DbParameter>();
			if (AppID > 0)
			{
				stringBuilder.Append("UPDATE GlobalAppInfo SET ").Append("Price=@Price, ").Append("PresentCurrency=@PresentCurrency, ")
					.Append("SortID=@SortID ")
					.Append("WHERE AppId=@AppId");
				list.Add(base.Database.MakeInParam("Price", Price));
				list.Add(base.Database.MakeInParam("PresentCurrency", PresentCurrency));
				list.Add(base.Database.MakeInParam("SortID", SortID));
				list.Add(base.Database.MakeInParam("AppId", AppID));
			}
			else
			{
				stringBuilder.Append("INSERT INTO GlobalAppInfo(Price,SortID,PresentCurrency)values(@Price,@SortID,@PresentCurrency)");
				list.Add(base.Database.MakeInParam("Price", Price));
				list.Add(base.Database.MakeInParam("SortID", SortID));
				list.Add(base.Database.MakeInParam("PresentCurrency", PresentCurrency));
			}
			return base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public PagerSet GetGlobalLivcardList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("GlobalLivcard", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public GlobalLivcard GetGlobalLivcardInfo(int cardTypeID)
		{
			string where = string.Format("(NOLOCK) WHERE CardTypeID= '{0}'", cardTypeID);
			return aideGlobalLivcardProvider.GetObject<GlobalLivcard>(where);
		}

		public void InsertGlobalLivcard(GlobalLivcard globalLivcard)
		{
			DataRow dataRow = aideGlobalLivcardProvider.NewRow();
			dataRow["CardName"] = globalLivcard.CardName;
			dataRow["CardPrice"] = globalLivcard.CardPrice;
			dataRow["Currency"] = globalLivcard.Currency;
			dataRow["InputDate"] = DateTime.Now;
			dataRow["Gold"] = globalLivcard.Gold;
			aideGlobalLivcardProvider.Insert(dataRow);
		}

		public void UpdateGlobalLivcard(GlobalLivcard globalLivcard)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE GlobalLivcard SET ").Append("CardName=@CardName, ").Append("CardPrice=@CardPrice, ")
				.Append("Currency=@Currency ")
				.Append("WHERE CardTypeID=@CardTypeID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("CardName", globalLivcard.CardName));
			list.Add(base.Database.MakeInParam("CardPrice", globalLivcard.CardPrice));
			list.Add(base.Database.MakeInParam("Currency", globalLivcard.Currency));
			list.Add(base.Database.MakeInParam("CardTypeID", globalLivcard.CardTypeID));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public void DeleteGlobalLivcard(string sqlQuery)
		{
			aideGlobalLivcardProvider.Delete(sqlQuery);
		}

		public PagerSet GetLivcardBuildStreamList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("LivcardBuildStream", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public LivcardBuildStream GetLivcardBuildStreamInfo(int buildID)
		{
			string where = string.Format("(NOLOCK) WHERE BuildID= '{0}'", buildID);
			return aideLivcardBuildStreamProvider.GetObject<LivcardBuildStream>(where);
		}

		public int InsertLivcardBuildStream(LivcardBuildStream livcardBuildStream)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("AdminName", livcardBuildStream.AdminName));
			list.Add(base.Database.MakeInParam("CardTypeID", livcardBuildStream.CardTypeID));
			list.Add(base.Database.MakeInParam("CardPrice", livcardBuildStream.CardPrice));
			list.Add(base.Database.MakeInParam("Currency", livcardBuildStream.Currency));
			list.Add(base.Database.MakeInParam("BuildCount", livcardBuildStream.BuildCount));
			list.Add(base.Database.MakeInParam("BuildAddr", livcardBuildStream.BuildAddr));
			list.Add(base.Database.MakeInParam("NoteInfo", livcardBuildStream.NoteInfo));
			list.Add(base.Database.MakeInParam("BuildCardPacket", livcardBuildStream.BuildCardPacket));
			list.Add(base.Database.MakeInParam("Gold", livcardBuildStream.Gold));
			object obj;
			base.Database.RunProc("WSP_PM_BuildStreamAdd", list, out obj);
			if (obj == null || obj.ToString().Length <= 0)
			{
				return 0;
			}
			return int.Parse(obj.ToString());
		}

		public void UpdateLivcardBuildStream(LivcardBuildStream livcardBuildStream)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE LivcardBuildStream SET ").Append("BuildCardPacket=@BuildCardPacket ").Append("WHERE BuildID=@BuildID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("BuildCardPacket", livcardBuildStream.BuildCardPacket));
			list.Add(base.Database.MakeInParam("BuildID", livcardBuildStream.BuildID));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public void UpdateLivcardBuildStream(int buildID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE LivcardBuildStream SET ").Append("DownLoadCount=DownLoadCount+1 ").Append("WHERE BuildID=@BuildID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("BuildID", buildID));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public PagerSet GetLivcardAssociatorList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("LivcardAssociator", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public LivcardAssociator GetLivcardAssociatorInfo(int cardID)
		{
			string where = string.Format("(NOLOCK) WHERE CardID= '{0}'", cardID);
			return aideLivcardAssociatorProvider.GetObject<LivcardAssociator>(where);
		}

		public LivcardAssociator GetLivcardAssociatorInfo(string serialID)
		{
			string where = string.Format("(NOLOCK) WHERE SerialID= '{0}'", serialID);
			return aideLivcardAssociatorProvider.GetObject<LivcardAssociator>(where);
		}

		public ShareDetailInfo GetShareDetailInfo(string serialID)
		{
			string where = string.Format("(NOLOCK) WHERE SerialID= '{0}'", serialID);
			return aideShareDetialProvider.GetObject<ShareDetailInfo>(where);
		}

		public string GetSalesperson(int buildID)
		{
			string commandText = string.Format("SELECT TOP 1 Salesperson FROM LivcardAssociator(NOLOCK) WHERE BuildID= '{0}'", buildID);
			return base.Database.ExecuteScalarToStr(CommandType.Text, commandText);
		}

		public void SetCardDisbale(string sqlQuery)
		{
			string commandText = string.Format("UPDATE LivcardAssociator SET Nullity=1 {0}", sqlQuery);
			base.Database.ExecuteNonQuery(commandText);
		}

		public void SetCardEnbale(string sqlQuery)
		{
			string commandText = string.Format("UPDATE LivcardAssociator SET Nullity=0 {0}", sqlQuery);
			base.Database.ExecuteNonQuery(commandText);
		}

		public void InsertLivcardAssociator(LivcardAssociator livcardAssociator, string[,] list)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			int length = list.GetLength(0);
			for (int i = 0; i < length; i += 100)
			{
				List<DbParameter> list2 = new List<DbParameter>();
				empty = string.Empty;
				empty2 = string.Empty;
				for (int j = i; j < i + 100 && j < length; j++)
				{
					empty += string.Format("{0},", list[j, 0]);
					empty2 += string.Format("{0},", list[j, 1]);
				}
				if (!string.IsNullOrEmpty(empty) && !string.IsNullOrEmpty(empty2))
				{
					empty = empty.TrimEnd(',');
					empty2 = empty2.TrimEnd(',');
					list2.Add(base.Database.MakeInParam("SerialID", empty));
					list2.Add(base.Database.MakeInParam("Password", empty2));
					list2.Add(base.Database.MakeInParam("BuildID", livcardAssociator.BuildID));
					list2.Add(base.Database.MakeInParam("CardTypeID", livcardAssociator.CardTypeID));
					list2.Add(base.Database.MakeInParam("CardPrice", livcardAssociator.CardPrice));
					list2.Add(base.Database.MakeInParam("Currency", livcardAssociator.Currency));
					list2.Add(base.Database.MakeInParam("UseRange", livcardAssociator.UseRange));
					list2.Add(base.Database.MakeInParam("SalesPerson", livcardAssociator.SalesPerson));
					list2.Add(base.Database.MakeInParam("ValidDate", livcardAssociator.ValidDate));
					list2.Add(base.Database.MakeInParam("Gold", livcardAssociator.Gold));
					base.Database.RunProc("WSP_PM_LivcardAdd", list2);
				}
			}
		}

		public DataSet GetLivcardStat()
		{
			DataSet ds;
			base.Database.RunProc("WSP_PM_LivcardStat", out ds);
			return ds;
		}

		public DataSet GetLivcardStatByBuildID(int buildID)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("BuildID", buildID));
			DataSet ds;
			base.Database.RunProc("NET_PM_LivcardStatByBuildID", list, out ds);
			return ds;
		}

		public PagerSet GetOnLineOrderList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("OnLineOrder", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public OnLineOrder GetOnLineOrderInfo(string orderID)
		{
			string where = string.Format("(NOLOCK) WHERE OrderID= '{0}'", orderID);
			return aideOnLineOrderProvider.GetObject<OnLineOrder>(where);
		}

		public void DeleteOnlineOrder(string sqlQuery)
		{
			aideOnLineOrderProvider.Delete(sqlQuery);
		}

		public PagerSet GetKQDetailList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("ReturnKQDetailInfo", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public ReturnKQDetailInfo GetKQDetailInfo(int detailID)
		{
			string where = string.Format("(NOLOCK) WHERE DetailID= {0}", detailID);
			return aideKQDetailProvider.GetObject<ReturnKQDetailInfo>(where);
		}

		public void DeleteKQDetail(string sqlQuery)
		{
			aideKQDetailProvider.Delete(sqlQuery);
		}

		public PagerSet GetYPDetailList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("ReturnYPDetailInfo", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public ReturnYPDetailInfo GetYPDetailInfo(int detailID)
		{
			string where = string.Format("(NOLOCK) WHERE DetailID= {0}", detailID);
			return aideYPDetailProvider.GetObject<ReturnYPDetailInfo>(where);
		}

		public void DeleteYPDetail(string sqlQuery)
		{
			aideYPDetailProvider.Delete(sqlQuery);
		}

		public PagerSet GetVBDetailList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("ReturnVBDetailInfo", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public ReturnVBDetailInfo GetVBDetailInfo(int detailID)
		{
			string where = string.Format("(NOLOCK) WHERE DetailID= {0}", detailID);
			return aideVBDetailProvider.GetObject<ReturnVBDetailInfo>(where);
		}

		public void DeleteVBDetail(string sqlQuery)
		{
			aideVBDetailProvider.Delete(sqlQuery);
		}

		public PagerSet GetDayDetailList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("ReturnDayDetailInfo", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public ReturnDayDetailInfo GetDayDetailInfo(int detailID)
		{
			return new ReturnDayDetailInfo();
		}

		public void DeleteDayDetail(string sqlQuery)
		{
			aideDayDetailProvider.Delete(sqlQuery);
		}

		public PagerSet GetShareDetailList(int pageIndex, int pageSize, string condition, string orderby)
		{
			string arg = "RYTreasureDB.dbo.ShareDetailInfo(nolock) as sd inner join RYAccountsDB..AccountsInfo(nolock) as acc on sd.UserID=acc.UserID left join RYAgentDB.dbo.T_Acc_Agent(nolock) as agt on acc.ParentId = agt.AgentID left join RYAccountsDB.dbo.AccountsInfo(nolock) as acc1 on acc.SpreaderID>0 and acc.SpreaderID=acc1.UserID ";
			string arg2 = "ApplyDate,ShareID,sd.UserID,acc.Accounts,acc.GameID,SerialID,OrderID,OrderAmount,PayAmount,CardTypeID,Currency,Gold,BeforeGold,IPAddress,BeforeCurrency,OperUserID,isnull(agt.AgentAcc,'') as AgentAcc,isnull(acc1.accounts,'') as Spreader";
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(" SELECT {0}", "ApplyDate,ShareID,UserID,Accounts,GameID,SerialID,OrderID,OrderAmount,PayAmount,CardTypeID,Currency,Gold,BeforeGold,IPAddress,BeforeCurrency,OperUserID,AgentAcc,Spreader");
			stringBuilder.Append(" FROM(");
			stringBuilder.AppendFormat(" SELECT {0},ROW_NUMBER() over(order by {1}) as RowNumber", arg2, orderby);
			stringBuilder.AppendFormat(" FROM {0} {1}) as t", arg, condition);
			stringBuilder.AppendFormat(" WHERE t.RowNumber>{0} and t.RowNumber<={1}", (pageIndex - 1) * pageSize, pageIndex * pageSize);
			stringBuilder.AppendFormat(" SELECT COUNT(*) FROM {0} {1}", arg, condition);
			DataSet dataSet = base.Database.ExecuteDataset(stringBuilder.ToString());
			PagerSet pagerSet = new PagerSet();
			pagerSet.PageSet = dataSet;
			int result = 0;
			if (dataSet != null && dataSet.Tables.Count > 1)
			{
				int.TryParse(dataSet.Tables[1].Rows[0][0].ToString(), out result);
			}
			pagerSet.RecordCount = result;
			return pagerSet;
		}

		public GlobalShareInfo GetGlobalShareByShareID(int shareID)
		{
			string where = string.Format("(NOLOCK) WHERE ShareID= N'{0}'", shareID);
			return aideGlobalShareProvider.GetObject<GlobalShareInfo>(where);
		}

		public PagerSet GetGlobalShareList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("GlobalShareInfo", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public PagerSet GetAppDetailList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("ReturnAppDetailInfo", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public ReturnAppDetailInfo GetAppDetailInfo(int detailID)
		{
			string where = string.Format("(NOLOCK) WHERE DetailID= {0}", detailID);
			return aideAppDetailProvider.GetObject<ReturnAppDetailInfo>(where);
		}

		public GlobalAppInfo GetGlobalAppInfo(int appID)
		{
			string where = string.Format("(NOLOCK) WHERE AppID= '{0}'", appID);
			return aideGlobalAppProvider.GetObject<GlobalAppInfo>(where);
		}

		public void InsertGlobalAppInfo(GlobalAppInfo globalApp)
		{
			DataRow dataRow = aideGlobalAppProvider.NewRow();
			dataRow["ProductID"] = globalApp.ProductID;
			dataRow["ProductName"] = globalApp.ProductName;
			dataRow["Description"] = globalApp.Description;
			dataRow["Price"] = globalApp.Price;
			dataRow["AttachCurrency"] = globalApp.AttachCurrency;
			dataRow["TagID"] = globalApp.TagID;
			dataRow["CollectDate"] = globalApp.CollectDate;
			aideGlobalAppProvider.Insert(dataRow);
		}

		public void UpdateGlobalAppInfo(GlobalAppInfo globalApp)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE GlobalAppInfo SET ").Append("ProductID=@ProductID, ").Append("ProductName=@ProductName, ")
				.Append("Description=@Description, ")
				.Append("Price=@Price, ")
				.Append("AttachCurrency=@AttachCurrency, ")
				.Append("TagID=@TagID ")
				.Append("WHERE AppID=@AppID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("ProductID", globalApp.ProductID));
			list.Add(base.Database.MakeInParam("ProductName", globalApp.ProductName));
			list.Add(base.Database.MakeInParam("Description", globalApp.Description));
			list.Add(base.Database.MakeInParam("Price", globalApp.Price));
			list.Add(base.Database.MakeInParam("AttachCurrency", globalApp.AttachCurrency));
			list.Add(base.Database.MakeInParam("TagID", globalApp.TagID));
			list.Add(base.Database.MakeInParam("AppID", globalApp.AppID));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

        public void InsertPlantInfo(T_PayPlatformInfo globalApp)
        {
            DataRow dataRow = payPlantProvider.NewRow();
            dataRow["PlatformName"] = globalApp.PlatformName;
            dataRow["PlatformCode"] = globalApp.PlatformCode;
            dataRow["Nullity"] = globalApp.Nullity;
            dataRow["SortID"] = globalApp.SortID;
            dataRow["QudaoName"] = globalApp.QudaoName;
            //dataRow["QudaoCode"] = globalApp.QudaoCode;
            dataRow["priKey"] = globalApp.PriKey;
            dataRow["pubKey"] = globalApp.PubKey;
            dataRow["url"] = globalApp.Url;
            dataRow["findUrl"] = globalApp.FindUrl;
            dataRow["backName"] = globalApp.BackName;
            dataRow["backAcc"] = globalApp.BackAcc;
            dataRow["backAdd"] = globalApp.BackAdd;
            dataRow["PryType"] = globalApp.PryType;
            payPlantProvider.Insert(dataRow);
        }

        public void UpdatePlantInfo(T_PayPlatformInfo globalApp)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("UPDATE T_PayPlatformInfo SET ").Append("PlatformName=@PlatformName, ").Append("PlatformCode=@PlatformCode, ")
                //.Append("Nullity=@Nullity, ")
                //.Append("SortID=@SortID, ")
                .Append("QudaoName=@QudaoName, ")
                .Append("QudaoCode=@QudaoCode, ")
                .Append("priKey=@priKey, ")
                .Append("pubKey=@pubKey, ")
                .Append("url=@url, ")
                .Append("findUrl=@findUrl, ")
                .Append("backName=@backName, ")
                .Append("backAcc=@backAcc, ")
                .Append("backAdd=@backAdd, ")
                .Append("PryType=@PryType ")
                .Append("WHERE ID=@ID");
            List<DbParameter> list = new List<DbParameter>();
            list.Add(base.Database.MakeInParam("PlatformName", globalApp.PlatformName));
            list.Add(base.Database.MakeInParam("PlatformCode", globalApp.PlatformCode));
            //list.Add(base.Database.MakeInParam("PlatformID", globalApp.PlatformID));
            //list.Add(base.Database.MakeInParam("QudaoID", globalApp.QudaoID));
            //list.Add(base.Database.MakeInParam("Nullity", globalApp.Nullity));
            //list.Add(base.Database.MakeInParam("SortID", globalApp.SortID));
            list.Add(base.Database.MakeInParam("QudaoName", globalApp.QudaoName));
            list.Add(base.Database.MakeInParam("QudaoCode", globalApp.PlatformCode));
            list.Add(base.Database.MakeInParam("priKey", globalApp.PriKey));
            list.Add(base.Database.MakeInParam("pubKey", globalApp.PubKey));
            list.Add(base.Database.MakeInParam("url", globalApp.Url));
            list.Add(base.Database.MakeInParam("findUrl", globalApp.FindUrl));
            list.Add(base.Database.MakeInParam("backName", globalApp.BackName));
            list.Add(base.Database.MakeInParam("backAcc", globalApp.BackAcc));
            list.Add(base.Database.MakeInParam("backAdd", globalApp.BackAdd));
            list.Add(base.Database.MakeInParam("PryType", globalApp.PryType));
            list.Add(base.Database.MakeInParam("ID", globalApp.ID));
            base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
        }

		public PagerSet GetGameScoreInfoList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("GameScoreInfo", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public GameScoreInfo GetGameScoreInfoByUserID(int UserID)
		{
			string where = string.Format("(NOLOCK) WHERE UserID= N'{0}'", UserID);
			return aideGameScoreInfoProvider.GetObject<GameScoreInfo>(where);
		}

		public decimal GetGameScoreByUserID(int UserID)
		{
			GameScoreInfo gameScoreInfoByUserID = GetGameScoreInfoByUserID(UserID);
			if (gameScoreInfoByUserID == null)
			{
				return 0m;
			}
			return gameScoreInfoByUserID.InsureScore;
		}

		public void UpdateInsureScore(GameScoreInfo gameScoreInfo)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE GameScoreInfo SET ").Append("InsureScore=@InsureScore ").Append("WHERE UserID=@UserID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("InsureScore", gameScoreInfo.InsureScore));
			list.Add(base.Database.MakeInParam("UserID", gameScoreInfo.UserID));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

        public Message OffLinePass(string ID, int statu, int intMasterID, string strIP)
        {
            List<DbParameter> list = new List<DbParameter>();
            list.Add(base.Database.MakeInParam("dwMasterID", intMasterID));
            list.Add(base.Database.MakeInParam("dwDetailID", ID));
            list.Add(base.Database.MakeInParam("OrderStatus", statu));
            list.Add(base.Database.MakeInParam("strClientIP", strIP));
            return MessageHelper.GetMessage(base.Database, "NET_PM_OffLineRecharge", list);
        }

		public Message GrantTreasure(int type, string strUserIdList, decimal intGold, int intMasterID, string strReason, string strIP)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("dwTypeId", type));
			list.Add(base.Database.MakeInParam("strUserIDList", strUserIdList));
			list.Add(base.Database.MakeInParam("dwAddGold", intGold));
			list.Add(base.Database.MakeInParam("dwMasterID", intMasterID));
			list.Add(base.Database.MakeInParam("strReason", strReason));
			list.Add(base.Database.MakeInParam("strClientIP", strIP));
			return MessageHelper.GetMessage(base.Database, "NET_PM_GrantTreasure", list);
		}

		public Message GrantScore(int userID, int kindID, int score, int masterID, string strReason, string strIP)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("UserID", userID));
			list.Add(base.Database.MakeInParam("KindID", kindID));
			list.Add(base.Database.MakeInParam("AddScore", score));
			list.Add(base.Database.MakeInParam("MasterID", masterID));
			list.Add(base.Database.MakeInParam("Reason", strReason));
			list.Add(base.Database.MakeInParam("ClientIP", strIP));
			return MessageHelper.GetMessage(base.Database, "WSP_PM_GrantGameScore", list);
		}

		public Message GrantClearScore(int userID, int kindID, int masterID, string strReason, string strIP)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("UserID", userID));
			list.Add(base.Database.MakeInParam("KindID", kindID));
			list.Add(base.Database.MakeInParam("MasterID", masterID));
			list.Add(base.Database.MakeInParam("Reason", strReason));
			list.Add(base.Database.MakeInParam("ClientIP", strIP));
			return MessageHelper.GetMessage(base.Database, "WSP_PM_GrantClearScore", list);
		}

		public Message GrantFlee(int userID, int kindID, int masterID, string strReason, string strIP)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("UserID", userID));
			list.Add(base.Database.MakeInParam("KindID", kindID));
			list.Add(base.Database.MakeInParam("MasterID", masterID));
			list.Add(base.Database.MakeInParam("Reason", strReason));
			list.Add(base.Database.MakeInParam("ClientIP", strIP));
			return MessageHelper.GetMessage(base.Database, "WSP_PM_GrantClearFlee", list);
		}

		public UserCurrencyInfo GetUserCurrencyInfo(int userID)
		{
			string commandText = "SELECT * FROM UserCurrencyInfo WHERE UserID=@UserID";
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("UserID", userID));
			return base.Database.ExecuteObject<UserCurrencyInfo>(commandText, list);
		}
       
        public OffLineQrCode GetOffLineQrCode(int id)
        {
            string commandText = "SELECT * FROM OffLinePayQrCode WHERE ID=@ID";
            List<DbParameter> list = new List<DbParameter>();
            list.Add(base.Database.MakeInParam("ID", id));
            return base.Database.ExecuteObject<OffLineQrCode>(commandText, list);
        }

        public void AddOffLineQrCode(OffLineQrCode model)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("INSERT INTO OffLinePayQrCode(PaymentName,IconPath,PaymentTypeID,OwnerID) ");
            stringBuilder.Append("VALUES(@PaymentName,@IconPath,@PaymentTypeID,@OwnerID)");
            List<DbParameter> list = new List<DbParameter>();
            list.Add(base.Database.MakeInParam("PaymentName", model.PaymentName));
            list.Add(base.Database.MakeInParam("IconPath", model.IconPath));
            list.Add(base.Database.MakeInParam("PaymentTypeID", model.PaymentTypeID));
            list.Add(base.Database.MakeInParam("OwnerID", model.OwnerID));
            base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
        }

        public void UpdateOffLineQrCode(OffLineQrCode model)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("UPDATE OffLinePayQrCode SET PaymentName=@PaymentName,IconPath=@IconPath,PaymentTypeID=@PaymentTypeID,OwnerID=@OwnerID ");
            stringBuilder.Append("WHERE ID=@ID");
            List<DbParameter> list = new List<DbParameter>();
            list.Add(base.Database.MakeInParam("PaymentName", model.PaymentName));
            list.Add(base.Database.MakeInParam("IconPath", model.IconPath));
            list.Add(base.Database.MakeInParam("PaymentTypeID", model.PaymentTypeID));
            list.Add(base.Database.MakeInParam("OwnerID", model.OwnerID));
            list.Add(base.Database.MakeInParam("ID", model.ID));
            base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
        }

		public PagerSet GetRecordDrawInfoList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("RecordDrawInfo", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public int DeleteRecordDrawInfoByTime(DateTime dt)
		{
			string commandText = "DELETE RecordDrawInfo WHERE InsertTime<@Date";
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("Date", dt));
			return base.Database.ExecuteNonQuery(CommandType.Text, commandText, list.ToArray());
		}

		public PagerSet GetRecordDrawScoreList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("RecordDrawScore", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public DataSet GetRecordDrawScoreByDrawID(int drawID)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("dwDrawID", drawID));
			return base.Database.ExecuteDataset(CommandType.StoredProcedure, "WSP_PM_GetRecordDrawScoreByDrawID", list.ToArray());
		}

		public int DeleteRecordDrawScoreByTime(DateTime dt)
		{
			string commandText = "DELETE RecordDrawScore WHERE InsertTime<@Date";
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("Date", dt));
			return base.Database.ExecuteNonQuery(CommandType.Text, commandText, list.ToArray());
		}

		public PagerSet GetGameScoreLockerList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("GameScoreLocker", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public void DeleteGameScoreLockerByUserID(int userID)
		{
			string where = string.Format("WHERE UserID='{0}'", userID);
			aideGameScoreLockerProvider.Delete(where);
		}

		public void DeleteGameScoreLocker(string sqlQuery)
		{
			aideGameScoreLockerProvider.Delete(sqlQuery);
		}

		public GameScoreLocker GetGameScoreLockerByUserID(int userID)
		{
			string where = string.Format("(NOLOCK) WHERE UserID= N'{0}'", userID);
			return aideGameScoreLocker.GetObject<GameScoreLocker>(where);
		}

		public PagerSet GetAndroidList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("AndroidManager", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public AndroidManager GetAndroidInfo(int userID)
		{
			string where = string.Format("(NOLOCK) WHERE UserID= {0}", userID);
			return aideAndroidProvider.GetObject<AndroidManager>(where);
		}

		public void InsertAndroid(AndroidManager android)
		{
			DataRow dataRow = aideAndroidProvider.NewRow();
			dataRow["UserID"] = android.UserID;
			dataRow["ServerID"] = android.ServerID;
			dataRow["MinPlayDraw"] = android.MinPlayDraw;
			dataRow["MaxPlayDraw"] = android.MaxPlayDraw;
			dataRow["MinTakeScore"] = android.MinTakeScore;
			dataRow["MaxTakeScore"] = android.MaxTakeScore;
			dataRow["MinReposeTime"] = android.MinReposeTime;
			dataRow["MaxReposeTime"] = android.MaxReposeTime;
			dataRow["ServiceGender"] = android.ServiceGender;
			dataRow["ServiceTime"] = android.ServiceTime;
			dataRow["AndroidNote"] = android.AndroidNote;
			dataRow["Nullity"] = android.Nullity;
			dataRow["CreateDate"] = android.CreateDate;
			aideAndroidProvider.Insert(dataRow);
		}

		public void UpdateAndroid(AndroidManager android)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE AndroidManager SET ").Append("ServerID=@ServerID ,").Append("MinPlayDraw=@MinPlayDraw,")
				.Append("MaxPlayDraw=@MaxPlayDraw,")
				.Append("MinTakeScore=@MinTakeScore,")
				.Append("MaxTakeScore=@MaxTakeScore,")
				.Append("MinReposeTime=@MinReposeTime,")
				.Append("MaxReposeTime=@MaxReposeTime,")
				.Append("ServiceGender=@ServiceGender,")
				.Append("ServiceTime=@ServiceTime,")
				.Append("AndroidNote=@AndroidNote,")
				.Append("Nullity=@Nullity ")
				.Append("WHERE UserID= @UserID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("ServerID", android.ServerID));
			list.Add(base.Database.MakeInParam("MinPlayDraw", android.MinPlayDraw));
			list.Add(base.Database.MakeInParam("MaxPlayDraw", android.MaxPlayDraw));
			list.Add(base.Database.MakeInParam("MinTakeScore", android.MinTakeScore));
			list.Add(base.Database.MakeInParam("MaxTakeScore", android.MaxTakeScore));
			list.Add(base.Database.MakeInParam("MinReposeTime", android.MinReposeTime));
			list.Add(base.Database.MakeInParam("MaxReposeTime", android.MaxReposeTime));
			list.Add(base.Database.MakeInParam("ServiceGender", android.ServiceGender));
			list.Add(base.Database.MakeInParam("ServiceTime", android.ServiceTime));
			list.Add(base.Database.MakeInParam("AndroidNote", android.AndroidNote));
			list.Add(base.Database.MakeInParam("Nullity", android.Nullity));
			list.Add(base.Database.MakeInParam("UserID", android.UserID));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public void DeleteAndroid(string sqlQuery)
		{
			aideAndroidProvider.Delete(sqlQuery);
		}

		public void NullityAndroid(byte nullity, string sqlQuery)
		{
			string commandText = string.Format("UPDATE AndroidManager SET Nullity={0} {1}", nullity, sqlQuery);
			base.Database.ExecuteNonQuery(commandText);
		}

		public PagerSet GetRecordUserInoutList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("RecordUserInout", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public int DeleteRecordUserInoutByTime(DateTime dt)
		{
			string commandText = "DELETE RecordUserInout WHERE LeaveTime<@Date";
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("Date", dt));
			return base.Database.ExecuteNonQuery(CommandType.Text, commandText, list.ToArray());
		}

		public PagerSet GetRecordInsureList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("RecordInsure", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public DataSet GetUserTransferTop100()
		{
			return base.Database.ExecuteDataset(CommandType.StoredProcedure, "WSP_PM_GetUserTransferTop100");
		}

		public int DeleteRecordInsureByTime(DateTime dt)
		{
			string commandText = "DELETE RecordInsure WHERE CollectDate<@Date";
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("Date", dt));
			return base.Database.ExecuteNonQuery(CommandType.Text, commandText, list.ToArray());
		}

		public GlobalSpreadInfo GetGlobalSpreadInfo(int id)
		{
			string where = string.Format("(NOLOCK) WHERE ID= {0}", id);
			return aideGlobalSpreadInfoProvider.GetObject<GlobalSpreadInfo>(where);
		}

		public void UpdateGlobalSpreadInfo(GlobalSpreadInfo spreadinfo)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE GlobalSpreadInfo SET ").Append("RegisterGrantScore=@RegisterGrantScore ,").Append("PlayTimeCount=@PlayTimeCount,")
				.Append("PlayTimeGrantScore=@PlayTimeGrantScore,")
				.Append("FillGrantRate=@FillGrantRate,")
				.Append("BalanceRate=@BalanceRate,")
				.Append("MinBalanceScore=@MinBalanceScore ")
				.Append("WHERE ID= @ID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("ID", spreadinfo.ID));
			list.Add(base.Database.MakeInParam("RegisterGrantScore", spreadinfo.RegisterGrantScore));
			list.Add(base.Database.MakeInParam("PlayTimeCount", spreadinfo.PlayTimeCount));
			list.Add(base.Database.MakeInParam("PlayTimeGrantScore", spreadinfo.PlayTimeGrantScore));
			list.Add(base.Database.MakeInParam("FillGrantRate", spreadinfo.FillGrantRate));
			list.Add(base.Database.MakeInParam("BalanceRate", spreadinfo.BalanceRate));
			list.Add(base.Database.MakeInParam("MinBalanceScore", spreadinfo.MinBalanceScore));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public void UpdatePromoterSet(GlobalSpreadInfo spreadinfo)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE GlobalSpreadInfo SET ").Append("To1UperRate=@To1UperRate ,").Append("To2UperRate=@To2UperRate,")
				.Append("To3UperRate=@To3UperRate,")
				.Append("To4UperRate=@To4UperRate,")
				.Append("To5UperRate=@To5UperRate");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("To1UperRate", spreadinfo.To1UperRate));
			list.Add(base.Database.MakeInParam("To2UperRate", spreadinfo.To2UperRate));
			list.Add(base.Database.MakeInParam("To3UperRate", spreadinfo.To3UperRate));
			list.Add(base.Database.MakeInParam("To4UperRate", spreadinfo.To4UperRate));
			list.Add(base.Database.MakeInParam("To5UperRate", spreadinfo.To5UperRate));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public PagerSet GetRecordSpreadInfoList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("RecordSpreadInfo", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public int GetSpreadScore(int userID)
		{
			string commandText = string.Format("SELECT ISNULL(SUM(Score),0) FROM RecordSpreadInfo WHERE Score>0 AND UserID= {0}", userID);
			object obj = base.Database.ExecuteScalar(CommandType.Text, commandText);
			if (obj == null || obj.ToString().Length <= 0)
			{
				return 0;
			}
			int result = 0;
			int.TryParse(obj.ToString(), out result);
			return result;
		}

		public DataSet GetGoldDistribution()
		{
			return base.Database.ExecuteDataset(CommandType.StoredProcedure, "NET_PM_AnalGoldDistribution");
		}

		public DataSet GetPayStat()
		{
			return base.Database.ExecuteDataset("NET_PM_AnalPayStat");
		}

		public DataSet GetPayStatByDay(string startDate, string endDate)
		{
			string str = "SELECT CONVERT(VARCHAR(10),ApplyDate,120) AS ApplyDate,SUM(PayAmount) AS PayAmount FROM ShareDetailInfo WHERE ApplyDate>=@StartDate AND ApplyDate<=@EndDate";
			str += " GROUP BY CONVERT(VARCHAR(10),ApplyDate,120) ORDER BY ApplyDate DESC";
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("StartDate", startDate));
			list.Add(base.Database.MakeInParam("EndDate", endDate));
			return base.Database.ExecuteDataset(CommandType.Text, str, list.ToArray());
		}

		public DataSet GetActiveUserByDay(int startDateID, int endDateID)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("StartDateID", startDateID));
			list.Add(base.Database.MakeInParam("EndDateID", endDateID));
			return base.Database.ExecuteDataset(CommandType.StoredProcedure, "WSP_PM_StatActiveUserTotalByDay", list.ToArray());
		}

		public DataSet GetActivieUserByMonth()
		{
			return base.Database.ExecuteDataset(CommandType.StoredProcedure, "WSP_PM_StatActiveUserTotalByMonth", (DbParameter[])null);
		}

		public DataSet StatRecordTable()
		{
			return base.Database.ExecuteDataset(CommandType.StoredProcedure, "WSP_PM_StatRecordTable", (DbParameter[])null);
		}

		public DataSet GetStatInfo()
		{
			DataSet ds;
			base.Database.RunProc("NET_PM_StatInfo", out ds);
			return ds;
		}

		public Message AppStatFilled(string accounts, string logonPass, string machineID)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strAccounts", accounts));
			list.Add(base.Database.MakeInParam("strPassword", logonPass));
			list.Add(base.Database.MakeInParam("strMachineSerial", machineID));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessageForDataSet(base.Database, "APP_PM_StatFilled", list);
		}

		public Message AppStatFilledCash(string accounts, string logonPass, string machineID)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strAccounts", accounts));
			list.Add(base.Database.MakeInParam("strPassword", logonPass));
			list.Add(base.Database.MakeInParam("strMachineSerial", machineID));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessageForDataSet(base.Database, "APP_PM_StatFilledCash", list);
		}

		public Message AppStatFilledScore(string accounts, string logonPass, string machineID)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strAccounts", accounts));
			list.Add(base.Database.MakeInParam("strPassword", logonPass));
			list.Add(base.Database.MakeInParam("strMachineSerial", machineID));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessageForDataSet(base.Database, "APP_PM_StatFilledScore", list);
		}

		public Message AppStatScorePresent(string accounts, string logonPass, string machineID)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strAccounts", accounts));
			list.Add(base.Database.MakeInParam("strPassword", logonPass));
			list.Add(base.Database.MakeInParam("strMachineSerial", machineID));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessageForDataSet(base.Database, "APP_PM_StatScorePresent", list);
		}

		public Message AppStatScoreRevenue(string accounts, string logonPass, string machineID)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strAccounts", accounts));
			list.Add(base.Database.MakeInParam("strPassword", logonPass));
			list.Add(base.Database.MakeInParam("strMachineSerial", machineID));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessageForDataSet(base.Database, "APP_PM_StatScoreRevenue", list);
		}

		public Message AppStatScoreWaste(string accounts, string logonPass, string machineID)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strAccounts", accounts));
			list.Add(base.Database.MakeInParam("strPassword", logonPass));
			list.Add(base.Database.MakeInParam("strMachineSerial", machineID));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessageForDataSet(base.Database, "APP_PM_StatScoreWaste", list);
		}

		public Message AppGetChargeData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strAccounts", accounts));
			list.Add(base.Database.MakeInParam("strPassword", logonPass));
			list.Add(base.Database.MakeInParam("strMachineSerial", machineID));
			list.Add(base.Database.MakeInParam("dwTypeID", typeID));
			list.Add(base.Database.MakeInParam("dwDateType", dateType));
			list.Add(base.Database.MakeInParam("strStartDate", startDate));
			list.Add(base.Database.MakeInParam("strEndDate", endDate));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessageForDataSet(base.Database, "APP_PM_GetChargeData", list);
		}

		public Message AppGetPayScoreData(string accounts, string logonPass, string machineID, int dateType, string startDate, string endDate)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strAccounts", accounts));
			list.Add(base.Database.MakeInParam("strPassword", logonPass));
			list.Add(base.Database.MakeInParam("strMachineSerial", machineID));
			list.Add(base.Database.MakeInParam("dwDateType", dateType));
			list.Add(base.Database.MakeInParam("strStartDate", startDate));
			list.Add(base.Database.MakeInParam("strEndDate", endDate));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessageForDataSet(base.Database, "APP_PM_GetPayScoreData", list);
		}

		public Message AppGetRevenueData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strAccounts", accounts));
			list.Add(base.Database.MakeInParam("strPassword", logonPass));
			list.Add(base.Database.MakeInParam("strMachineSerial", machineID));
			list.Add(base.Database.MakeInParam("dwTypeID", typeID));
			list.Add(base.Database.MakeInParam("dwDateType", dateType));
			list.Add(base.Database.MakeInParam("strStartDate", startDate));
			list.Add(base.Database.MakeInParam("strEndDate", endDate));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessageForDataSet(base.Database, "APP_PM_GetRevenueData", list);
		}

		public Message AppGetPresentData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strAccounts", accounts));
			list.Add(base.Database.MakeInParam("strPassword", logonPass));
			list.Add(base.Database.MakeInParam("strMachineSerial", machineID));
			list.Add(base.Database.MakeInParam("dwTypeID", typeID));
			list.Add(base.Database.MakeInParam("dwDateType", dateType));
			list.Add(base.Database.MakeInParam("strStartDate", startDate));
			list.Add(base.Database.MakeInParam("strEndDate", endDate));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessageForDataSet(base.Database, "APP_PM_GetPresentData", list);
		}

		public Message AppGetWasteData(string accounts, string logonPass, string machineID, int dateType, string startDate, string endDate)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strAccounts", accounts));
			list.Add(base.Database.MakeInParam("strPassword", logonPass));
			list.Add(base.Database.MakeInParam("strMachineSerial", machineID));
			list.Add(base.Database.MakeInParam("dwDateType", dateType));
			list.Add(base.Database.MakeInParam("strStartDate", startDate));
			list.Add(base.Database.MakeInParam("strEndDate", endDate));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessageForDataSet(base.Database, "APP_PM_GetWasteData", list);
		}

		public Message AppGetPlatScoreData(string accounts, string logonPass, string machineID, int dateType, string startDate, string endDate)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strAccounts", accounts));
			list.Add(base.Database.MakeInParam("strPassword", logonPass));
			list.Add(base.Database.MakeInParam("strMachineSerial", machineID));
			list.Add(base.Database.MakeInParam("dwDateType", dateType));
			list.Add(base.Database.MakeInParam("strStartDate", startDate));
			list.Add(base.Database.MakeInParam("strEndDate", endDate));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessageForDataSet(base.Database, "APP_PM_GetPlatScoreData", list);
		}

		public Message AppGetMemberData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strAccounts", accounts));
			list.Add(base.Database.MakeInParam("strPassword", logonPass));
			list.Add(base.Database.MakeInParam("strMachineSerial", machineID));
			list.Add(base.Database.MakeInParam("dwTypeID", typeID));
			list.Add(base.Database.MakeInParam("dwDateType", dateType));
			list.Add(base.Database.MakeInParam("strStartDate", startDate));
			list.Add(base.Database.MakeInParam("strEndDate", endDate));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessageForDataSet(base.Database, "APP_PM_GetMemberData", list);
		}

		public long GetChildRevenueProvide(int userID)
		{
			string commandText = string.Format("SELECT ISNULL(SUM(AgentRevenue),0) FROM RecordUserRevenue WHERE UserID= {0}", userID);
			object obj = base.Database.ExecuteScalar(CommandType.Text, commandText);
			if (obj == null || obj.ToString().Length <= 0)
			{
				return 0L;
			}
			return Convert.ToInt64(obj);
		}

		public long GetChildPayProvide(int userID)
		{
			string commandText = string.Format("SELECT ISNULL(SUM(Score),0) FROM RecordAgentInfo WHERE ChildrenID= {0} AND TypeID=1", userID);
			object obj = base.Database.ExecuteScalar(CommandType.Text, commandText);
			if (obj == null || obj.ToString().Length <= 0)
			{
				return 0L;
			}
			return Convert.ToInt64(obj);
		}

		public DataSet GetAgentFinance(int userID)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("dwUserID", userID));
			return base.Database.ExecuteDataset(CommandType.StoredProcedure, "WSP_PM_GetAgentFinance", list.ToArray());
		}

		public void StatRevenueHand()
		{
			base.Database.ExecuteScalar(CommandType.StoredProcedure, "WSP_PM_StatAccountRevenueHand");
		}

		public void StatAgentPayHand()
		{
			base.Database.ExecuteScalar(CommandType.StoredProcedure, "WSP_PM_StatAgentPayHand");
		}

		public LotteryConfig GetLotteryConfig(int id)
		{
			string where = string.Format("(NOLOCK) WHERE ID= {0}", id);
			return aideLotteryConfigProvider.GetObject<LotteryConfig>(where);
		}

		public void UpdateLotteryConfig(LotteryConfig model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE LotteryConfig SET ").Append("FreeCount=@FreeCount ,").Append("ChargeFee=@ChargeFee,")
				.Append("IsCharge=@IsCharge ")
				.Append("WHERE ID= @ID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("ID", model.ID));
			list.Add(base.Database.MakeInParam("FreeCount", model.FreeCount));
			list.Add(base.Database.MakeInParam("ChargeFee", model.ChargeFee));
			list.Add(base.Database.MakeInParam("IsCharge", model.IsCharge));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public int UpdateLotteryItem(LotteryItem model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE LotteryItem SET ").Append("ItemType=@ItemType,").Append("ItemQuota=@ItemQuota,")
				.Append("ItemRate=@ItemRate ")
				.Append("WHERE ItemIndex=@ItemIndex");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("ItemType", model.ItemType));
			list.Add(base.Database.MakeInParam("ItemQuota", model.ItemQuota));
			list.Add(base.Database.MakeInParam("ItemRate", model.ItemRate));
			list.Add(base.Database.MakeInParam("ItemIndex", model.ItemIndex));
			return base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters(tableName, orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public int ExecuteSql(string sql)
		{
			return base.Database.ExecuteNonQuery(sql);
		}

		public DataSet GetDataSetBySql(string sql)
		{
			return base.Database.ExecuteDataset(sql);
		}

		public string GetScalarBySql(string sql)
		{
			return base.Database.ExecuteScalarToStr(CommandType.Text, sql);
		}

		public DataSet GetGameRevenue(string stime, string etime)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT r.KindID,r.ServerID,ServerName,sum(UserCount) UserCount,sum(r.Revenue) Revenue from RecordDrawInfo r inner join RYPlatformDB.dbo.GameRoomInfo room on r.ServerID=room.ServerID WHERE 1=1");
			if (!string.IsNullOrEmpty(stime))
			{
				stringBuilder.Append(" AND InsertTime>=@stime");
			}
			if (!string.IsNullOrEmpty(etime))
			{
				stringBuilder.Append(" AND InsertTime<=@etime");
			}
			stringBuilder.Append(" GROUP BY r.KindID,ServerName,r.ServerID having sum(r.Revenue)>0");
			stringBuilder.Append(";SELECT SUM(Revenue) AS Revenue FROM RecordDrawInfo(NOLOCK) WHERE 1=1");
			if (!string.IsNullOrEmpty(stime))
			{
				stringBuilder.Append(" AND InsertTime>=@stime");
			}
			if (!string.IsNullOrEmpty(etime))
			{
				stringBuilder.Append(" AND InsertTime<=@etime");
			}
			List<DbParameter> list = new List<DbParameter>();
			if (!string.IsNullOrEmpty(stime))
			{
				list.Add(base.Database.MakeInParam("stime", Convert.ToDateTime(stime)));
			}
			if (!string.IsNullOrEmpty(etime))
			{
				list.Add(base.Database.MakeInParam("etime", Convert.ToDateTime(etime).AddDays(1.0)));
			}
			return base.Database.ExecuteDataset(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public DataSet GetRecordPresentInfo(string where)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT * from View_PresentInfo ");
			if (!string.IsNullOrEmpty(where))
			{
				stringBuilder.AppendFormat("{0}", where);
			}
			return base.Database.ExecuteDataset(CommandType.Text, stringBuilder.ToString());
		}

		public DataSet GetRecordType(string TabName)
		{
			return base.Database.ExecuteDataset(CommandType.Text, "SELECT ID,TypeName FROM  RecordType WHERE IsShow=0 " + (string.IsNullOrEmpty(TabName) ? "" : (" AND TabName='" + TabName + "'")));
		}

		public int EditRecordType(RecordType entity)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<DbParameter> list = new List<DbParameter>();
			if (entity.Mode == "edit")
			{
				stringBuilder.Append("UPDATE RecordType SET ").Append("TypeName=@TypeName, ").Append("IsShow=@IsShow ")
					.Append("WHERE ID=@ID AND TabName=@TabName");
				list.Add(base.Database.MakeInParam("IsShow", entity.IsShow));
			}
			else
			{
				stringBuilder.Append("INSERT INTO RecordType(TabName,ID,TypeName,Memo,IsShow)VALUES(@TabName,@ID,@TypeName,@Memo,1)");
				list.Add(base.Database.MakeInParam("Memo", entity.Memo));
			}
			list.Add(base.Database.MakeInParam("TypeName", entity.TypeName));
			list.Add(base.Database.MakeInParam("ID", entity.ID));
			list.Add(base.Database.MakeInParam("TabName", entity.TabName));
			return base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public PagerSet GetPager(Dictionary<string, string> dic, string procName)
		{
			PagerSet pagerSet = new PagerSet();
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
							string[] array2 = array;
							foreach (string text in array2)
							{
								if (!string.IsNullOrEmpty(text))
								{
									list.Add(base.Database.MakeOutParam(text, typeof(int)));
								}
							}
						}
					}
					else
					{
						list.Add(base.Database.MakeInParam(key, dic[key]));
					}
				}
			}
			DataSet dataSet2 = pagerSet.PageSet = base.Database.ExecuteDataset(CommandType.StoredProcedure, procName, list.ToArray());
			pagerSet.PageSize = Convert.ToInt32(list[list.Count - 2].Value);
			pagerSet.RecordCount = Convert.ToInt32(list[list.Count - 1].Value);
			return pagerSet;
		}

		public SQLResult GetTable(Dictionary<string, string> dic, string procName)
		{
			SQLResult sQLResult = new SQLResult();
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
							string[] array2 = array;
							foreach (string text in array2)
							{
								if (!string.IsNullOrEmpty(text))
								{
									list.Add(base.Database.MakeOutParam(text, typeof(int)));
								}
							}
						}
					}
					else
					{
						list.Add(base.Database.MakeInParam(key, dic[key]));
					}
				}
			}
			DataSet dataSet = base.Database.ExecuteDataset(CommandType.StoredProcedure, procName, list.ToArray());
			sQLResult.Data = dataSet.Tables[0];
			if (string.IsNullOrEmpty(list[list.Count - 1].Value.ToString()))
			{
				sQLResult.Success = false;
				sQLResult.Msg = list[list.Count - 1].Value.ToString();
			}
			else
			{
				sQLResult.Success = true;
			}
			return sQLResult;
		}

		public DataTable GetAccountScoreById(int id)
		{
            string commandText = "SELECT KindID,KindName,SUM(Score)AS Score FROM  View_UserGameRDScore (NOLOCK) WHERE UserID=" + id + " GROUP BY KindID,KindName UNION SELECT 0,'统计',SUM(Score)AS Score FROM  View_UserGameRDScore (NOLOCK) WHERE UserID=" + id + " UNION SELECT 0,ctype,SUM(isnull(winscore,0)-isnull(betscore,0)) AS Score FROM  LotteryBetDraw WHERE iscomplete =1 and  UserID=" + id + " group by ctype ";
			DataSet dataSet = base.Database.ExecuteDataset(CommandType.Text, commandText);
			return dataSet.Tables[0];
		}

		public PagerSet GetAgentPlayRecords(int pageIndex, int pageSize, string condition, string orderby)
		{
			string arg = "RYTreasureDB..ShareDetailInfo as sdi inner join (select u.UserID,u.Accounts,isnull(a.ORechargeRate,0) as RechargeRate,a.AgentID from RYAccountsDB..AccountsInfo as u inner join RYAgentDB..T_Plm_AgentChangeLog as a on u.AgentID = a.AgentID) as t on sdi.UserID = t.UserID";
			string arg2 = "sdi.DetailID,t.UserID,t.Accounts,t.RechargeRate,sdi.Currency,sdi.ApplyDate,sdi.ShareID,t.AgentID";
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(" SELECT {0}", "DetailID,UserID,Accounts,RechargeRate,Currency,ApplyDate,ShareID ");
			stringBuilder.Append(" FROM(");
			stringBuilder.AppendFormat(" SELECT {0},ROW_NUMBER() over(order by {1}) as RowNumber", arg2, orderby);
			stringBuilder.AppendFormat(" FROM {0}", arg);
			stringBuilder.AppendFormat(" {0}) as t", condition);
			stringBuilder.AppendFormat(" WHERE t.RowNumber>{0} and t.RowNumber<={1}", (pageIndex - 1) * pageSize, pageIndex * pageSize);
			stringBuilder.AppendFormat(" SELECT COUNT(*) FROM {0}", arg);
			stringBuilder.AppendFormat(" {0}", condition);
			DataSet dataSet = base.Database.ExecuteDataset(stringBuilder.ToString());
			PagerSet pagerSet = new PagerSet();
			pagerSet.PageSet = dataSet;
			int result = 0;
			if (dataSet != null && dataSet.Tables.Count > 1)
			{
				int.TryParse(dataSet.Tables[1].Rows[0][0].ToString(), out result);
			}
			pagerSet.RecordCount = result;
			return pagerSet;
		}
	}
}
