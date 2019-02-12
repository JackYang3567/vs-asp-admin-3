using Game.Entity.NativeWeb;
using Game.Kernel;
using System.Data;

namespace Game.IData
{
	public interface INativeWebDataProvider
	{
		News GetAgentNotice();

		PagerSet GetNewsList(int pageIndex, int pageSize, string condition, string orderby);

		News GetNewsInfo(int newsID);

		void InsertNews(News news);

		void UpdateNews(News news);

		void DeleteNews(string sqlQuery);

		PagerSet GetGameRulesList(int pageIndex, int pageSize, string condition, string orderby);

		GameRulesInfo GetGameRulesInfo(int kindID);

		void InsertGameRules(GameRulesInfo gameRules);

		void UpdateGameRules(GameRulesInfo gameRules, int kindID);

		void DeleteGameRules(string sqlQuery);

		bool JudgeRulesIsExistence(int kindID);

		PagerSet GetGameIssueList(int pageIndex, int pageSize, string condition, string orderby);

		GameIssueInfo GetGameIssueInfo(int issueID);

		void InsertGameIssue(GameIssueInfo gameIssue);

		void UpdateGameIssue(GameIssueInfo gameIssue);

		void DeleteGameIssue(string sqlQuery);

		PagerSet GetGameFeedbackList(int pageIndex, int pageSize, string condition, string orderby);

		GameFeedbackInfo GetGameFeedbackInfo(int feedbackID);

		void RevertGameFeedback(GameFeedbackInfo gameFeedback);

		void DeleteGameFeedback(string sqlQuery);

		void SetFeedbackDisbale(string sqlQuery);

		void SetFeedbackEnbale(string sqlQuery);

		int GetNewMessageCounts();

		PagerSet GetNoticeList(int pageIndex, int pageSize, string condition, string orderby);

		Notice GetNoticeInfo(int noticeID);

		void InsertNotice(Notice notice);

		void UpdateNotice(Notice notice);

		void DeleteNotice(string sqlQuery);

		void SetNoticeDisbale(string sqlQuery);

		void SetNoticeEnbale(string sqlQuery);

		PagerSet GetGameMatchInfoList(int pageIndex, int pageSize, string condition, string orderby);

		GameMatchInfo GetGameMatchInfo(int matchID);

		void InsertGameMatchInfo(GameMatchInfo gameMatch);

		void UpdateGameMatchInfo(GameMatchInfo gameMatch);

		void DeleteGameMatchInfo(string sqlQuery);

		void SetMatchDisbale(string sqlQuery);

		void SetMatchEnbale(string sqlQuery);

		PagerSet GetGameMatchUserInfoList(int pageIndex, int pageSize, string condition, string orderby);

		void SetUserMatchDisbale(string sqlQuery);

		void SetUserMatchEnbale(string sqlQuery);

		AwardType GetAwardTypeByPID(int typeID);

		DataSet GetAllAwardType();

		DataSet GetAllChildType();

		AwardType GetAwardTypeByID(int typeID);

		void InsertAwardTypeInfo(AwardType awardType);

		int UpdateAwardTypeInfo(AwardType awardType);

		int UpdateNulity(string typeId, int state);

		string GetChildAwardTypeByPID(int Pid);

		Message DeleteAwardType(int typeId);

		AwardInfo GetAwardInfoByID(int id);

		int UpdateAwardInfoNulity(string idList, int state);

		void InsertAwardInfo(AwardInfo info);

		int UpdateAwardInfo(AwardInfo info);

		int GetMaxAwardInfoID();

		bool IsHaveGoods(int typeID);

		AwardOrder GetAwardOrderByID(int orderID);

		int UpdateOrderState(int state, int orderID, string note);

		int GetNewOrderCounts();

		ConfigInfo GetConfigInfo(int configID);

		ConfigInfo GetConfigInfo(string configKey);

		void UpdateConfigInfo(ConfigInfo ci);

		int GetConfigInfoMinID();

		SinglePage GetSinglePage(int pageID);

		SinglePage GetSinglePage(string keyValue);

		int UpdateSinglePage(SinglePage singlePage);

		int GetSinglePageMinID();

		Ads GetAds(int ID);

		void DeleteAds(string sqlQuery);

		void InsertAds(Ads ads);

		void UpdateAds(Ads ads);

		LossReport GetLossReport(int reportID);

		bool UpdateLossReport(LossReport lossReport);

		void DeleteActivity(string idList);

		Activity GetActivity(int id);

		void AddActivity(Activity model);

		void UpdateActivity(Activity model);

		PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby);

		int ExecuteSql(string sql);

		DataSet GetDataSetBySql(string sql);

		string GetScalarBySql(string sql);

		GameAccuseInfo GetGameAccuseInfo(int accuseID);

		int UpdateReport(int id, string body, string admin);

		int GetUnDoGameFeedbackInfoCount();

		int AddPlatformDraw(PlatformDraw model);
	}
}
