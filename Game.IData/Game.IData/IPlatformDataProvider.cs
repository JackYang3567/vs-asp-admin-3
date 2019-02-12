using Game.Entity.Platform;
using Game.Kernel;
using System.Collections.Generic;
using System.Data;

namespace Game.IData
{
	public interface IPlatformDataProvider
	{
		PagerSet GetDataBaseList(int pageIndex, int pageSize, string condition, string orderby);

		DataBaseInfo GetDataBaseInfo(int dBInfoID);

		DataBaseInfo GetDataBaseInfo(string dBAddr);

		void InsertDataBase(DataBaseInfo dataBase);

		void UpdateDataBase(DataBaseInfo dataBase);

		void DeleteDataBase(string sqlQuery);

		PagerSet GetGameGameItemList(int pageIndex, int pageSize, string condition, string orderby);

		GameGameItem GetGameGameItemInfo(int gameID);

		int GetMaxGameGameID();

		void InsertGameGameItem(GameGameItem gameGameItem);

		void UpdateGameGameItem(GameGameItem gameGameItem);

		void DeleteGameGameItem(string sqlQuery);

		PagerSet GetGameTypeItemList(int pageIndex, int pageSize, string condition, string orderby);

		GameTypeItem GetGameTypeItemInfo(int typeID);

		int GetMaxGameTypeID();

		void InsertGameTypeItem(GameTypeItem gameTypeItem);

		void UpdateGameTypeItem(GameTypeItem gameTypeItem);

		void DeleteGameTypeItem(string sqlQuery);

		IList<GameKindItem> GetGameList(int typeId);

		PagerSet GetGameNodeItemList(int pageIndex, int pageSize, string condition, string orderby);

		GameNodeItem GetGameNodeItemInfo(int nodeID);

		int GetMaxGameNodeID();

		void InsertGameNodeItem(GameNodeItem gameNodeItem);

		void UpdateGameNodeItem(GameNodeItem gameNodeItem);

		void DeleteGameNodeItem(string sqlQuery);

		PagerSet GetGamePageItemList(int pageIndex, int pageSize, string condition, string orderby);

		GamePageItem GetGamePageItemInfo(int pageID);

		int GetMaxGamePageID();

		void InsertGamePageItem(GamePageItem gamePageItem);

		void UpdateGamePageItem(GamePageItem gamePageItem);

		void DeleteGamePageItem(string sqlQuery);

		DataSet GetGameList();

		PagerSet GetGameKindItemList(int pageIndex, int pageSize, string condition, string orderby);

		GameKindItem GetGameKindItemInfo(int kindID);

		int GetMaxGameKindID();

		void InsertGameKindItem(GameKindItem gameKindItem);

		void UpdateGameKindItem(GameKindItem gameKindItem);

		void DeleteGameKindItem(string sqlQuery);

		void UpdateGameConfig(GameConfig gameConfig);

		GameConfig GetGameConfig(int kindID);

		PagerSet GetMobileKindItemList(int pageIndex, int pageSize, string condition, string orderby);

		MobileKindItem GetMobileKindItemInfo(int kindID);

		int GetMaxMobileKindID();

		void InsertMobileKindItem(MobileKindItem model);

		void UpdateMobileKindItem(MobileKindItem model);

		void DeleteMobileKindItem(string sqlQuery);

		PagerSet GetGameRoomInfoList(int pageIndex, int pageSize, string condition, string orderby);

		GameRoomInfo GetGameRoomInfoInfo(int serverID);

		void InsertGameRoomInfo(GameRoomInfo gameRoomInfo);

		void UpdateGameRoomInfo(GameRoomInfo gameRoomInfo);

		void DeleteGameRoomInfo(string sqlQuery);

		DataSet GetOnLineStreamInfoList(string sqlQuery);

		PagerSet GetOnLineStreamInfoList(int pageIndex, int pageSize, string condition, string orderby);

		PagerSet GetSystemMessageList(int pageIndex, int pageSize, string condition, string orderby);

		SystemMessage GetSystemMessageInfo(int id);

		void InsertSystemMessage(SystemMessage systemMessage);

		void UpdateSystemMessage(SystemMessage systemMessage);

		void DeleteSystemMessage(string sqlQuery);

		PagerSet GetGlobalPlayPresentList(int pageIndex, int pageSize, string condition, string orderby);

		GlobalPlayPresent GetGlobalPlayPresentInfo(int serverID);

		void InsertGlobalPlayPresent(GlobalPlayPresent globalPlayPresent);

		void UpdateGlobalPlayPresent(GlobalPlayPresent globalPlayPresent);

		void DeleteGlobalPlayPresent(string sqlQuery);

		TaskInfo GetTaskInfoByID(int id);

		void InsertTaskInfo(TaskInfo info);

		int UpdateTaskInfo(TaskInfo info);

		void DeleteTaskInfo(string sqlQuery);

		DataSet GetSigninConfig();

		void UpdateSigninConfig(DataSet ds);

		int UpdateGrowLevelConfig(GrowLevelConfig glc);

		PagerSet GetGamePropertyTypeList(int pageIndex, int pageSize, string condition, string orderby);

		GamePropertyType GetGamePropertyTypeInfo(int typeID);

		int GetMaxPropertyTypeID();

		void InsertGamePropertyType(GamePropertyType gamePropertyType);

		void UpdateGamePropertyType(GamePropertyType gamePropertyType);

		void DeleteGamePropertyType(string sqlQuery);

		PagerSet GetGamePropertyList(int pageIndex, int pageSize, string condition, string orderby);

		IList<GameProperty> GetGamePropertyGift(int kind);

		GameProperty GetGamePropertyInfo(int id);

		int GetMaxPropertyID();

		void InsertGameProperty(GameProperty property);

		void UpdateGameProperty(GameProperty property);

		void DeleteGameProperty(string sqlQuery);

		void SetPropertyDisbale(string sqlQuery);

		void SetPropertyEnbale(string sqlQuery);

		void SetPropertyRecommend(int recommend, string sqlQuery);

		GamePropertySub GetGamePropertySubInfo(int id, int ownerID);

		void InsertGamePropertySub(GamePropertySub property);

		void UpdateGamePropertySub(GamePropertySub property);

		void DeleteGamePropertySub(string sqlQuery);

		string GetConn(int kindID);

		Message AppStatOnline(string accounts, string logonPass, string machineID);

		Message AppPlatformGeneral(string accounts, string logonPass, string machineID);

		Message AppStatUserOnline(string accounts, string logonPass, string machineID);

		Message AppGetOnlineData(string accounts, string logonPass, string machineID, int dateType, string startDate, string endDate);

		PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby);

		int ExecuteSql(string sql);

		DataSet GetDataSetBySql(string sql);
	}
}
