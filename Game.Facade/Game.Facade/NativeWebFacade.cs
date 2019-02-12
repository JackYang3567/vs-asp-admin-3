using Game.Data.Factory;
using Game.Entity.NativeWeb;
using Game.IData;
using Game.Kernel;
using System.Data;

namespace Game.Facade
{
	public class NativeWebFacade
	{
		private INativeWebDataProvider aideNativeWebData;

		public NativeWebFacade()
		{
			aideNativeWebData = ClassFactory.GetINativeWebDataProvider();
		}

		public News GetAgentNotice()
		{
			return aideNativeWebData.GetAgentNotice();
		}

		public PagerSet GetNewsList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideNativeWebData.GetNewsList(pageIndex, pageSize, condition, orderby);
		}

		public News GetNewsInfo(int newsID)
		{
			return aideNativeWebData.GetNewsInfo(newsID);
		}

		public Message InsertNews(News news)
		{
			aideNativeWebData.InsertNews(news);
			return new Message(true);
		}

		public Message UpdateNews(News news)
		{
			aideNativeWebData.UpdateNews(news);
			return new Message(true);
		}

		public void DeleteNews(string sqlQuery)
		{
			aideNativeWebData.DeleteNews(sqlQuery);
		}

		public void JinyongNews(int id, int value)
		{
			string sql = "Update News Set IsDelete=" + value + " Where NewsID=" + id;
			aideNativeWebData.ExecuteSql(sql);
		}

		public PagerSet GetGameRulesList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideNativeWebData.GetGameRulesList(pageIndex, pageSize, condition, orderby);
		}

		public GameRulesInfo GetGameRulesInfo(int kindID)
		{
			return aideNativeWebData.GetGameRulesInfo(kindID);
		}

		public Message InsertGameRules(GameRulesInfo gameRules)
		{
			aideNativeWebData.InsertGameRules(gameRules);
			return new Message(true);
		}

		public Message UpdateGameRules(GameRulesInfo gameRules, int kindID)
		{
			aideNativeWebData.UpdateGameRules(gameRules, kindID);
			return new Message(true);
		}

		public void DeleteGameRules(string sqlQuery)
		{
			aideNativeWebData.DeleteGameRules(sqlQuery);
		}

		public bool JudgeRulesIsExistence(int kindID)
		{
			return aideNativeWebData.JudgeRulesIsExistence(kindID);
		}

		public PagerSet GetGameIssueList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideNativeWebData.GetGameIssueList(pageIndex, pageSize, condition, orderby);
		}

		public GameIssueInfo GetGameIssueInfo(int issueID)
		{
			return aideNativeWebData.GetGameIssueInfo(issueID);
		}

		public Message InsertGameIssue(GameIssueInfo gameIssue)
		{
			aideNativeWebData.InsertGameIssue(gameIssue);
			return new Message(true);
		}

		public Message UpdateGameIssue(GameIssueInfo gameIssue)
		{
			aideNativeWebData.UpdateGameIssue(gameIssue);
			return new Message(true);
		}

		public void DeleteGameIssue(string sqlQuery)
		{
			aideNativeWebData.DeleteGameIssue(sqlQuery);
		}

		public PagerSet GetGameFeedbackList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideNativeWebData.GetGameFeedbackList(pageIndex, pageSize, condition, orderby);
		}

		public GameFeedbackInfo GetGameFeedbackInfo(int feedbackID)
		{
			return aideNativeWebData.GetGameFeedbackInfo(feedbackID);
		}

		public Message RevertGameFeedback(GameFeedbackInfo gameFeedback)
		{
			aideNativeWebData.RevertGameFeedback(gameFeedback);
			return new Message(true);
		}

		public void DeleteGameFeedback(string sqlQuery)
		{
			aideNativeWebData.DeleteGameFeedback(sqlQuery);
		}

		public void SetFeedbackDisbale(string sqlQuery)
		{
			aideNativeWebData.SetFeedbackDisbale(sqlQuery);
		}

		public void SetFeedbackEnbale(string sqlQuery)
		{
			aideNativeWebData.SetFeedbackEnbale(sqlQuery);
		}

		public int GetNewMessageCounts()
		{
			return aideNativeWebData.GetNewMessageCounts();
		}

		public PagerSet GetNoticeList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideNativeWebData.GetNoticeList(pageIndex, pageSize, condition, orderby);
		}

		public Notice GetNoticeInfo(int noticeID)
		{
			return aideNativeWebData.GetNoticeInfo(noticeID);
		}

		public Message InsertNotice(Notice notice)
		{
			aideNativeWebData.InsertNotice(notice);
			return new Message(true);
		}

		public Message UpdateNotice(Notice notice)
		{
			aideNativeWebData.UpdateNotice(notice);
			return new Message(true);
		}

		public void DeleteNotice(string sqlQuery)
		{
			aideNativeWebData.DeleteNotice(sqlQuery);
		}

		public void SetNoticeDisbale(string sqlQuery)
		{
			aideNativeWebData.SetNoticeDisbale(sqlQuery);
		}

		public void SetNoticeEnbale(string sqlQuery)
		{
			aideNativeWebData.SetNoticeEnbale(sqlQuery);
		}

		public PagerSet GetGameMatchInfoList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideNativeWebData.GetGameMatchInfoList(pageIndex, pageSize, condition, orderby);
		}

		public GameMatchInfo GetGameMatchInfo(int matchID)
		{
			return aideNativeWebData.GetGameMatchInfo(matchID);
		}

		public void InsertGameMatchInfo(GameMatchInfo gameMatch)
		{
			aideNativeWebData.InsertGameMatchInfo(gameMatch);
		}

		public void UpdateGameMatchInfo(GameMatchInfo gameMatch)
		{
			aideNativeWebData.UpdateGameMatchInfo(gameMatch);
		}

		public void DeleteGameMatchInfo(string sqlQuery)
		{
			aideNativeWebData.DeleteGameMatchInfo(sqlQuery);
		}

		public void SetMatchDisbale(string sqlQuery)
		{
			aideNativeWebData.SetMatchDisbale(sqlQuery);
		}

		public void SetMatchEnbale(string sqlQuery)
		{
			aideNativeWebData.SetMatchEnbale(sqlQuery);
		}

		public PagerSet GetGameMatchUserInfoList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideNativeWebData.GetGameMatchUserInfoList(pageIndex, pageSize, condition, orderby);
		}

		public void SetUserMatchDisbale(string sqlQuery)
		{
			aideNativeWebData.SetUserMatchDisbale(sqlQuery);
		}

		public void SetUserMatchEnbale(string sqlQuery)
		{
			aideNativeWebData.SetUserMatchEnbale(sqlQuery);
		}

		public AwardType GetAwardTypeByPID(int typeID)
		{
			return aideNativeWebData.GetAwardTypeByPID(typeID);
		}

		public DataSet GetAllAwardType()
		{
			return aideNativeWebData.GetAllAwardType();
		}

		public DataSet GetAllChildType()
		{
			return aideNativeWebData.GetAllChildType();
		}

		public AwardType GetAwardTypeByID(int typeID)
		{
			return aideNativeWebData.GetAwardTypeByID(typeID);
		}

		public bool InsertAwardTypeInfo(AwardType awardType)
		{
			aideNativeWebData.InsertAwardTypeInfo(awardType);
			return true;
		}

		public bool UpdateAwardTypeInfo(AwardType awardType)
		{
			int num = aideNativeWebData.UpdateAwardTypeInfo(awardType);
			if (num <= 0)
			{
				return false;
			}
			return true;
		}

		public int UpdateNulity(string typeId, int state)
		{
			return aideNativeWebData.UpdateNulity(typeId, state);
		}

		public string GetChildAwardTypeByPID(int Pid)
		{
			return aideNativeWebData.GetChildAwardTypeByPID(Pid);
		}

		public Message DeleteAwardType(int typeId)
		{
			return aideNativeWebData.DeleteAwardType(typeId);
		}

		public AwardInfo GetAwardInfoByID(int id)
		{
			return aideNativeWebData.GetAwardInfoByID(id);
		}

		public int UpdateAwardInfoNulity(string idList, int state)
		{
			return aideNativeWebData.UpdateAwardInfoNulity(idList, state);
		}

		public bool InsertAwardInfo(AwardInfo info)
		{
			aideNativeWebData.InsertAwardInfo(info);
			return true;
		}

		public bool UpdateAwardInfo(AwardInfo info)
		{
			int num = aideNativeWebData.UpdateAwardInfo(info);
			if (num <= 0)
			{
				return false;
			}
			return true;
		}

		public int GetMaxAwardInfoID()
		{
			return aideNativeWebData.GetMaxAwardInfoID();
		}

		public bool IsHaveGoods(int typeID)
		{
			return aideNativeWebData.IsHaveGoods(typeID);
		}

		public AwardOrder GetAwardOrderByID(int orderID)
		{
			return aideNativeWebData.GetAwardOrderByID(orderID);
		}

		public int UpdateOrderState(int state, int orderID, string note)
		{
			return aideNativeWebData.UpdateOrderState(state, orderID, note);
		}

		public int GetNewOrderCounts()
		{
			return aideNativeWebData.GetNewOrderCounts();
		}

		public ConfigInfo GetConfigInfo(int configID)
		{
			return aideNativeWebData.GetConfigInfo(configID);
		}

		public ConfigInfo GetConfigInfo(string configKey)
		{
			return aideNativeWebData.GetConfigInfo(configKey);
		}

		public void UpdateConfigInfo(ConfigInfo ci)
		{
			aideNativeWebData.UpdateConfigInfo(ci);
		}

		public int GetConfigInfoMinID()
		{
			return aideNativeWebData.GetConfigInfoMinID();
		}

		public SinglePage GetSinglePage(int pageID)
		{
			return aideNativeWebData.GetSinglePage(pageID);
		}

		public SinglePage GetSinglePage(string keyValue)
		{
			return aideNativeWebData.GetSinglePage(keyValue);
		}

		public int UpdateSinglePage(SinglePage singlePage)
		{
			return aideNativeWebData.UpdateSinglePage(singlePage);
		}

		public int GetSinglePageMinID()
		{
			return aideNativeWebData.GetSinglePageMinID();
		}

		public Ads GetAds(int ID)
		{
			return aideNativeWebData.GetAds(ID);
		}

		public void DeleteAds(string sqlQuery)
		{
			aideNativeWebData.DeleteAds(sqlQuery);
		}

		public void InsertAds(Ads ads)
		{
			aideNativeWebData.InsertAds(ads);
		}

		public void UpdateAds(Ads ads)
		{
			aideNativeWebData.UpdateAds(ads);
		}

		public LossReport GetLossReport(int reportID)
		{
			return aideNativeWebData.GetLossReport(reportID);
		}

		public bool UpdateLossReport(LossReport lossReport)
		{
			return aideNativeWebData.UpdateLossReport(lossReport);
		}

		public void DeleteActivity(string idList)
		{
			aideNativeWebData.DeleteActivity(idList);
		}

		public Activity GetActivity(int id)
		{
			return aideNativeWebData.GetActivity(id);
		}

		public void AddActivity(Activity model)
		{
			aideNativeWebData.AddActivity(model);
		}

		public void UpdateActivity(Activity model)
		{
			aideNativeWebData.UpdateActivity(model);
		}

		public PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby)
		{
			return aideNativeWebData.GetList(tableName, pageIndex, pageSize, condition, orderby);
		}

		public int ExecuteSql(string sql)
		{
			return aideNativeWebData.ExecuteSql(sql);
		}

		public DataSet GetDataSetBySql(string sql)
		{
			return aideNativeWebData.GetDataSetBySql(sql);
		}

		public string GetScalarBySql(string sql)
		{
			return aideNativeWebData.GetScalarBySql(sql);
		}

		public void DelReport(string sWhere)
		{
			string sql = "DELETE GameAccuseInfo " + sWhere;
			aideNativeWebData.ExecuteSql(sql);
		}

		public GameAccuseInfo GetGameAccuseInfo(int accuseID)
		{
			return aideNativeWebData.GetGameAccuseInfo(accuseID);
		}

		public int UpdateReport(int id, string body, string admin)
		{
			return aideNativeWebData.UpdateReport(id, body, admin);
		}

		public void InsertPushMessage(string accounts, string device, string msg, string strOperator, string ip)
		{
			string sql = string.Format("INSERT T_PushedNews(Accounts,Device,Msg,Operator,ClinetIP)VALUES('{0}','{1}','{2}','{3}','{4}')", accounts, device, msg, strOperator, ip);
			aideNativeWebData.ExecuteSql(sql);
		}

		public int GetUnDoGameFeedbackInfoCount()
		{
			return aideNativeWebData.GetUnDoGameFeedbackInfoCount();
		}

		public int AddPlatformDraw(PlatformDraw model)
		{
			return aideNativeWebData.AddPlatformDraw(model);
		}

		public string SumPlayerDraw(string sWhere)
		{
			string sql = "select isnull(sum(DrawAmt),0) from T_PlatformDraw " + sWhere;
			return aideNativeWebData.GetScalarBySql(sql);
		}
        
	}
}
