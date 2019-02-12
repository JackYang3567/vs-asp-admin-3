using Game.Data.Factory;
using Game.Entity.GameMatch;
using Game.IData;
using Game.Kernel;
using System.Data;

namespace Game.Facade
{
	public class GameMatchFacade
	{
		private IGameMatchProvider gameMatchData;

		public GameMatchFacade()
		{
			gameMatchData = ClassFactory.GetIGameMatchProvider();
		}

		public PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby)
		{
			return gameMatchData.GetList(tableName, pageIndex, pageSize, condition, orderby);
		}

		public int ExecuteSql(string sql)
		{
			return gameMatchData.ExecuteSql(sql);
		}

		public MatchInfo GetMatchInfo(int matchID)
		{
			return gameMatchData.GetMatchInfo(matchID);
		}

		public Message UpdateMatchInfo(MatchInfo matchInfo)
		{
			try
			{
				gameMatchData.UpdateMatchInfo(matchInfo);
				return new Message(true);
			}
			catch
			{
				return new Message(false);
			}
		}

		public MatchPublic GetMatchPublicInfo(int matchID)
		{
			return gameMatchData.GetMatchPublicInfo(matchID);
		}

		public DataSet GetAllMatch()
		{
			return gameMatchData.GetAllMatch();
		}

		public DataSet GetMatchListByMatchType(byte matchType)
		{
			return gameMatchData.GetMatchListByMatchType(matchType);
		}

		public DataSet GetMatchListByMatchID(int matchId)
		{
			return gameMatchData.GetMatchListByMatchID(matchId);
		}

		public PagerSet GetTimingMatchHistoryGroup(byte matchType, int pageIndex, int pageSize, string condition, string orderby)
		{
			return gameMatchData.GetTimingMatchHistoryGroup(matchType, pageIndex, pageSize, condition, orderby);
		}

		public DataSet GetMatchRewardList(int matchId)
		{
			return gameMatchData.GetMatchRewardList(matchId);
		}
	}
}
