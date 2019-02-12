using Game.Data.Factory;
using Game.Entity.Platform;
using Game.IData;
using Game.Kernel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Game.Facade
{
	public class PlatformFacade
	{
		private IPlatformDataProvider aidePlatformData;

		public PlatformFacade()
		{
			aidePlatformData = ClassFactory.GetIPlatformDataProvider();
		}

		public PagerSet GetDataBaseList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aidePlatformData.GetDataBaseList(pageIndex, pageSize, condition, orderby);
		}

		public DataBaseInfo GetDataBaseInfo(int dBInfoID)
		{
			return aidePlatformData.GetDataBaseInfo(dBInfoID);
		}

		public DataBaseInfo GetDataBaseInfo(string dBAddr)
		{
			return aidePlatformData.GetDataBaseInfo(dBAddr);
		}

		public Message InsertDataBase(DataBaseInfo dataBase)
		{
			aidePlatformData.InsertDataBase(dataBase);
			return new Message(true);
		}

		public Message UpdateDataBase(DataBaseInfo dataBase)
		{
			aidePlatformData.UpdateDataBase(dataBase);
			return new Message(true);
		}

		public void DeleteDataBase(string sqlQuery)
		{
			aidePlatformData.DeleteDataBase(sqlQuery);
		}

		public bool IsExistsDBAddr(string address)
		{
			DataBaseInfo dataBaseInfo = GetDataBaseInfo(address);
			if (dataBaseInfo == null)
			{
				return false;
			}
			return true;
		}

		public PagerSet GetGameGameItemList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aidePlatformData.GetGameGameItemList(pageIndex, pageSize, condition, orderby);
		}

		public GameGameItem GetGameGameItemInfo(int gameID)
		{
			return aidePlatformData.GetGameGameItemInfo(gameID);
		}

		public int GetMaxGameGameID()
		{
			return aidePlatformData.GetMaxGameGameID();
		}

		public Message InsertGameGameItem(GameGameItem gameGameItem)
		{
			aidePlatformData.InsertGameGameItem(gameGameItem);
			return new Message(true);
		}

		public Message UpdateGameGameItem(GameGameItem gameGameItem)
		{
			aidePlatformData.UpdateGameGameItem(gameGameItem);
			return new Message(true);
		}

		public void DeleteGameGameItem(string sqlQuery)
		{
			aidePlatformData.DeleteGameGameItem(sqlQuery);
		}

		public bool IsExistsGameID(int gameID)
		{
			GameGameItem gameGameItemInfo = GetGameGameItemInfo(gameID);
			if (gameGameItemInfo == null)
			{
				return false;
			}
			return true;
		}

		public PagerSet GetGameTypeItemList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aidePlatformData.GetGameTypeItemList(pageIndex, pageSize, condition, orderby);
		}

		public GameTypeItem GetGameTypeItemInfo(int typeID)
		{
			return aidePlatformData.GetGameTypeItemInfo(typeID);
		}

		public int GetMaxGameTypeID()
		{
			return aidePlatformData.GetMaxGameTypeID();
		}

		public Message InsertGameTypeItem(GameTypeItem gameTypeItem)
		{
			aidePlatformData.InsertGameTypeItem(gameTypeItem);
			return new Message(true);
		}

		public Message UpdateGameTypeItem(GameTypeItem gameTypeItem)
		{
			aidePlatformData.UpdateGameTypeItem(gameTypeItem);
			return new Message(true);
		}

		public void DeleteGameTypeItem(string sqlQuery)
		{
			aidePlatformData.DeleteGameTypeItem(sqlQuery);
		}

		public bool IsExistsTypeID(int typeID)
		{
			GameTypeItem gameTypeItemInfo = GetGameTypeItemInfo(typeID);
			if (gameTypeItemInfo == null)
			{
				return false;
			}
			return true;
		}

		public IList<GameKindItem> GetGameList(int typeId)
		{
			return aidePlatformData.GetGameList(typeId);
		}

		public PagerSet GetGameNodeItemList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aidePlatformData.GetGameNodeItemList(pageIndex, pageSize, condition, orderby);
		}

		public GameNodeItem GetGameNodeItemInfo(int nodeID)
		{
			return aidePlatformData.GetGameNodeItemInfo(nodeID);
		}

		public int GetMaxGameNodeID()
		{
			return aidePlatformData.GetMaxGameNodeID();
		}

		public Message InsertGameNodeItem(GameNodeItem gameNodeItem)
		{
			aidePlatformData.InsertGameNodeItem(gameNodeItem);
			return new Message(true);
		}

		public Message UpdateGameNodeItem(GameNodeItem gameNodeItem)
		{
			aidePlatformData.UpdateGameNodeItem(gameNodeItem);
			return new Message(true);
		}

		public void DeleteGameNodeItem(string sqlQuery)
		{
			aidePlatformData.DeleteGameNodeItem(sqlQuery);
		}

		public bool IsExistsNodeID(int nodeID)
		{
			GameNodeItem gameNodeItemInfo = GetGameNodeItemInfo(nodeID);
			if (gameNodeItemInfo == null)
			{
				return false;
			}
			return true;
		}

		public PagerSet GetGamePageItemList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aidePlatformData.GetGamePageItemList(pageIndex, pageSize, condition, orderby);
		}

		public GamePageItem GetGamePageItemInfo(int pageID)
		{
			return aidePlatformData.GetGamePageItemInfo(pageID);
		}

		public int GetMaxGamePageID()
		{
			return aidePlatformData.GetMaxGamePageID();
		}

		public Message InsertGamePageItem(GamePageItem gamePageItem)
		{
			aidePlatformData.InsertGamePageItem(gamePageItem);
			return new Message(true);
		}

		public Message UpdateGamePageItem(GamePageItem gamePageItem)
		{
			aidePlatformData.UpdateGamePageItem(gamePageItem);
			return new Message(true);
		}

		public void DeleteGamePageItem(string sqlQuery)
		{
			aidePlatformData.DeleteGamePageItem(sqlQuery);
		}

		public bool IsExistsPageID(int pageID)
		{
			GamePageItem gamePageItemInfo = GetGamePageItemInfo(pageID);
			if (gamePageItemInfo == null)
			{
				return false;
			}
			return true;
		}

		public DataSet GetGameList()
		{
			return aidePlatformData.GetGameList();
		}

		public PagerSet GetGameKindItemList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aidePlatformData.GetGameKindItemList(pageIndex, pageSize, condition, orderby);
		}

		public GameKindItem GetGameKindItemInfo(int kindID)
		{
			return aidePlatformData.GetGameKindItemInfo(kindID);
		}

		public int GetMaxGameKindID()
		{
			return aidePlatformData.GetMaxGameKindID();
		}

		public Message InsertGameKindItem(GameKindItem gameKindItem)
		{
			aidePlatformData.InsertGameKindItem(gameKindItem);
			return new Message(true);
		}

		public Message UpdateGameKindItem(GameKindItem gameKindItem)
		{
			aidePlatformData.UpdateGameKindItem(gameKindItem);
			return new Message(true);
		}

		public void DeleteGameKindItem(string sqlQuery)
		{
			aidePlatformData.DeleteGameKindItem(sqlQuery);
		}

		public bool IsExistsKindID(int kindID)
		{
			GameKindItem gameKindItemInfo = GetGameKindItemInfo(kindID);
			if (gameKindItemInfo == null)
			{
				return false;
			}
			return true;
		}

		public void UpdateGameConfig(GameConfig gameConfig)
		{
			aidePlatformData.UpdateGameConfig(gameConfig);
		}

		public GameConfig GetGameConfig(int kindID)
		{
			return aidePlatformData.GetGameConfig(kindID);
		}

		public DataTable GameList()
		{
			string sql = "SELECT KindID,KindName,WebTypeID FROM GameKindItem where Nullity=0";
			return aidePlatformData.GetDataSetBySql(sql).Tables[0];
		}

		public PagerSet GetMobileKindItemList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aidePlatformData.GetMobileKindItemList(pageIndex, pageSize, condition, orderby);
		}

		public MobileKindItem GetMobileKindItemInfo(int kindID)
		{
			return aidePlatformData.GetMobileKindItemInfo(kindID);
		}

		public int GetMaxMobileKindID()
		{
			return aidePlatformData.GetMaxMobileKindID();
		}

		public Message InsertMobileKindItem(MobileKindItem model)
		{
			try
			{
				aidePlatformData.InsertMobileKindItem(model);
				return new Message(true);
			}
			catch
			{
				return new Message(false);
			}
		}

		public Message UpdateMobileKindItem(MobileKindItem model)
		{
			try
			{
				aidePlatformData.UpdateMobileKindItem(model);
				return new Message(true);
			}
			catch
			{
				return new Message(false);
			}
		}

		public Message DeleteMobileKindItem(string sqlQuery)
		{
			try
			{
				aidePlatformData.DeleteMobileKindItem(sqlQuery);
				return new Message(true);
			}
			catch
			{
				return new Message(false);
			}
		}

		public PagerSet GetGameRoomInfoList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aidePlatformData.GetGameRoomInfoList(pageIndex, pageSize, condition, orderby);
		}

		public GameRoomInfo GetGameRoomInfoInfo(int serverID)
		{
			return aidePlatformData.GetGameRoomInfoInfo(serverID);
		}

		public Message InsertGameRoomInfo(GameRoomInfo gameRoomInfo)
		{
			aidePlatformData.InsertGameRoomInfo(gameRoomInfo);
			return new Message(true);
		}

		public Message UpdateGameRoomInfo(GameRoomInfo gameRoomInfo)
		{
			aidePlatformData.UpdateGameRoomInfo(gameRoomInfo);
			return new Message(true);
		}

		public void DeleteGameRoomInfo(string sqlQuery)
		{
			aidePlatformData.DeleteGameRoomInfo(sqlQuery);
		}

		public DataTable GetRoomList(int kid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT ServerID,ServerName,KindID FROM GameRoomInfo WHERE Nullity=0 ");
			if (kid > 0)
			{
				stringBuilder.AppendFormat(" AND KindID={0}", kid);
			}
			return aidePlatformData.GetDataSetBySql(stringBuilder.ToString()).Tables[0];
		}

		public DataSet GetOnLineStreamInfoList(string year, string month, string day)
		{
			string text = "";
			if (month == "-1")
			{
				text = "select CONVERT(varchar(7),InsertDateTime, 120) as InsertDateTime,max(OnLineCountSum) as MaxCount,min(OnLineCountSum) as MinCount,";
				text += "convert(int,round(avg(OnLineCountSum*1.0),0)) as AvgCount from OnLineStreamInfo ";
				text = text + "where year(InsertDateTime)='" + year + "' group by CONVERT(varchar(7),InsertDateTime, 120) order by InsertDateTime asc";
			}
			else if (day == "-1")
			{
				DateTime dateTime = Convert.ToDateTime(year + "-" + month + "-01");
				DateTime dateTime2 = dateTime.AddMonths(1);
				text = "select CONVERT(varchar(10),InsertDateTime, 120) as InsertDateTime,max(OnLineCountSum) as MaxCount,min(OnLineCountSum) as MinCount,";
				text += "convert(int,round(avg(OnLineCountSum*1.0),0)) as AvgCount from OnLineStreamInfo ";
				string text2 = text;
				text = text2 + "where InsertDateTime>'" + dateTime.ToString() + "' and InsertDateTime<'" + dateTime2.ToString() + "' group by CONVERT(varchar(10),InsertDateTime, 120) order by InsertDateTime asc";
			}
			else
			{
				string str = year + "-" + month + "-" + day;
				text = "select datepart(hh,InsertDateTime) as InsertDateTime,max(OnLineCountSum) as MaxCount,min(OnLineCountSum) as MinCount,";
				text += "convert(int,round(avg(OnLineCountSum*1.0),0)) as AvgCount from OnLineStreamInfo ";
				text = text + "where convert(varchar(10),InsertDateTime,120)='" + str + "' group by datepart(hh,InsertDateTime) order by InsertDateTime asc";
			}
			return aidePlatformData.GetOnLineStreamInfoList(text);
		}

		public DataSet GetOnlineStreamGameInfoList(string year, string month, string day, string hour)
		{
			string text = "";
			if (day == "-1")
			{
				DateTime dateTime = Convert.ToDateTime(year + "-" + month + "-01");
				DateTime dateTime2 = dateTime.AddMonths(1);
				text = "select * from OnLineStreamInfo where InsertDateTime>'" + dateTime.ToString() + "' and InsertDateTime<'" + dateTime2.ToString() + "' order by InsertDateTime asc";
			}
			else if (hour == "-1")
			{
				string str = year + "-" + month + "-" + day;
				text = "select * from OnLineStreamInfo where convert(varchar(10),InsertDateTime,120)='" + str + "' order by InsertDateTime asc";
			}
			else
			{
				string text2 = year + "-" + month + "-" + day;
				text = "select * from OnLineStreamInfo where convert(varchar(10),InsertDateTime,120)='" + text2 + "' and datepart(hh,InsertDateTime)='" + hour + "' order by InsertDateTime asc";
			}
			return aidePlatformData.GetOnLineStreamInfoList(text);
		}

		public PagerSet GetOnLineStreamInfoList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aidePlatformData.GetOnLineStreamInfoList(pageIndex, pageSize, condition, orderby);
		}

		public PagerSet GetSystemMessageList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aidePlatformData.GetSystemMessageList(pageIndex, pageSize, condition, orderby);
		}

		public SystemMessage GetSystemMessageInfo(int id)
		{
			return aidePlatformData.GetSystemMessageInfo(id);
		}

		public Message InsertSystemMessage(SystemMessage systemMessage)
		{
			aidePlatformData.InsertSystemMessage(systemMessage);
			return new Message(true);
		}

		public Message UpdateSystemMessage(SystemMessage systemMessage)
		{
			aidePlatformData.UpdateSystemMessage(systemMessage);
			return new Message(true);
		}

		public void DeleteSystemMessage(string sqlQuery)
		{
			aidePlatformData.DeleteSystemMessage(sqlQuery);
		}

		public PagerSet GetGlobalPlayPresentList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aidePlatformData.GetGlobalPlayPresentList(pageIndex, pageSize, condition, orderby);
		}

		public GlobalPlayPresent GetGlobalPlayPresentInfo(int serverID)
		{
			return aidePlatformData.GetGlobalPlayPresentInfo(serverID);
		}

		public Message InsertGlobalPlayPresent(GlobalPlayPresent globalPlayPresent)
		{
			aidePlatformData.InsertGlobalPlayPresent(globalPlayPresent);
			return new Message(true);
		}

		public Message UpdateGlobalPlayPresent(GlobalPlayPresent globalPlayPresent)
		{
			aidePlatformData.UpdateGlobalPlayPresent(globalPlayPresent);
			return new Message(true);
		}

		public void DeleteGlobalPlayPresent(string sqlQuery)
		{
			aidePlatformData.DeleteGlobalPlayPresent(sqlQuery);
		}

		public TaskInfo GetTaskInfoByID(int id)
		{
			return aidePlatformData.GetTaskInfoByID(id);
		}

		public bool InsertTaskInfo(TaskInfo info)
		{
			aidePlatformData.InsertTaskInfo(info);
			return true;
		}

		public bool UpdateTaskInfo(TaskInfo info)
		{
			int num = aidePlatformData.UpdateTaskInfo(info);
			if (num > 0)
			{
				return true;
			}
			return false;
		}

		public void DeleteTaskInfo(string sqlQuery)
		{
			aidePlatformData.DeleteTaskInfo(sqlQuery);
		}

		public void JinyongTask(int id, int value)
		{
			string sql = "Update TaskInfo Set Nullity=" + value + " Where TaskID=" + id;
			aidePlatformData.ExecuteSql(sql);
		}

		public DataSet GetSigninConfig()
		{
			return aidePlatformData.GetSigninConfig();
		}

		public void UpdateSigninConfig(DataSet ds)
		{
			aidePlatformData.UpdateSigninConfig(ds);
		}

		public int UpdateGrowLevelConfig(GrowLevelConfig glc)
		{
			return aidePlatformData.UpdateGrowLevelConfig(glc);
		}

		public PagerSet GetGamePropertyTypeList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aidePlatformData.GetGamePropertyTypeList(pageIndex, pageSize, condition, orderby);
		}

		public GamePropertyType GetGamePropertyTypeInfo(int typeID)
		{
			return aidePlatformData.GetGamePropertyTypeInfo(typeID);
		}

		public int GetMaxPropertyTypeID()
		{
			return aidePlatformData.GetMaxPropertyTypeID();
		}

		public Message InsertGamePropertyType(GamePropertyType gamePropertyType)
		{
			aidePlatformData.InsertGamePropertyType(gamePropertyType);
			return new Message(true);
		}

		public Message UpdateGamePropertyType(GamePropertyType gamePropertyType)
		{
			aidePlatformData.UpdateGamePropertyType(gamePropertyType);
			return new Message(true);
		}

		public void DeleteGamePropertyType(string sqlQuery)
		{
			aidePlatformData.DeleteGamePropertyType(sqlQuery);
		}

		public PagerSet GetGamePropertyList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aidePlatformData.GetGamePropertyList(pageIndex, pageSize, condition, orderby);
		}

		public IList<GameProperty> GetGamePropertyGift(int kind)
		{
			return aidePlatformData.GetGamePropertyGift(kind);
		}

		public GameProperty GetGamePropertyInfo(int id)
		{
			return aidePlatformData.GetGamePropertyInfo(id);
		}

		public int GetMaxPropertyID()
		{
			return aidePlatformData.GetMaxPropertyID();
		}

		public void InsertGameProperty(GameProperty property)
		{
			aidePlatformData.InsertGameProperty(property);
		}

		public void UpdateGameProperty(GameProperty property)
		{
			aidePlatformData.UpdateGameProperty(property);
		}

		public void DeleteGameProperty(string sqlQuery)
		{
			aidePlatformData.DeleteGameProperty(sqlQuery);
		}

		public void SetPropertyDisbale(string sqlQuery)
		{
			aidePlatformData.SetPropertyDisbale(sqlQuery);
		}

		public void SetPropertyEnbale(string sqlQuery)
		{
			aidePlatformData.SetPropertyEnbale(sqlQuery);
		}

		public void SetPropertyRecommend(int recommend, string sqlQuery)
		{
			aidePlatformData.SetPropertyRecommend(recommend, sqlQuery);
		}

		public GamePropertySub GetGamePropertySubInfo(int id, int ownerID)
		{
			return aidePlatformData.GetGamePropertySubInfo(id, ownerID);
		}

		public void InsertGamePropertySub(GamePropertySub property)
		{
			aidePlatformData.InsertGamePropertySub(property);
		}

		public void UpdateGamePropertySub(GamePropertySub property)
		{
			aidePlatformData.UpdateGamePropertySub(property);
		}

		public void DeleteGamePropertySub(string sqlQuery)
		{
			aidePlatformData.DeleteGamePropertySub(sqlQuery);
		}

		public string GetConn(int kindID)
		{
			return aidePlatformData.GetConn(kindID);
		}

		public Message AppStatOnline(string accounts, string logonPass, string machineID)
		{
			return aidePlatformData.AppStatOnline(accounts, logonPass, machineID);
		}

		public Message AppPlatformGeneral(string accounts, string logonPass, string machineID)
		{
			return aidePlatformData.AppPlatformGeneral(accounts, logonPass, machineID);
		}

		public Message AppStatUserOnline(string accounts, string logonPass, string machineID)
		{
			return aidePlatformData.AppStatUserOnline(accounts, logonPass, machineID);
		}

		public Message AppGetOnlineData(string accounts, string logonPass, string machineID, int dateType, string startDate, string endDate)
		{
			return aidePlatformData.AppGetOnlineData(accounts, logonPass, machineID, dateType, startDate, endDate);
		}

		public PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby)
		{
			return aidePlatformData.GetList(tableName, pageIndex, pageSize, condition, orderby);
		}

		public int ExecuteSql(string sql)
		{
			return aidePlatformData.ExecuteSql(sql);
		}

		public void UpdateCount(int kid, int sid, int count)
		{
			string text = "UPDATE OnlineCount SET AddCounts=" + count + " where ServerID=" + sid;
			object obj = text;
			text = obj + ";if @@ROWCOUNT=0 INSERT OnlineCount(KindID,ServerID,AddCounts) VALUES(" + kid + "," + sid + "," + count + ") ";
			aidePlatformData.ExecuteSql(text);
		}

		public DataTable GetOnlineCount(int sid)
		{
			string sql = "SELECT * FROM OnlineCount WHERE ServerID=" + sid;
			return aidePlatformData.GetDataSetBySql(sql).Tables[0];
		}

		public void DeleteOnlineCount(string sWhere)
		{
			string sql = "DELETE OnlineCount " + sWhere;
			aidePlatformData.ExecuteSql(sql);
		}
	}
}
