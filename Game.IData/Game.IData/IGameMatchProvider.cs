using Game.Entity.GameMatch;
using Game.Kernel;
using System.Data;

namespace Game.IData
{
	public interface IGameMatchProvider
	{
		MatchInfo GetMatchInfo(int matchID);

		void UpdateMatchInfo(MatchInfo matchInfo);

		MatchPublic GetMatchPublicInfo(int matchID);

		DataSet GetAllMatch();

		DataSet GetMatchListByMatchType(byte matchType);

		DataSet GetMatchListByMatchID(int matchId);

		PagerSet GetTimingMatchHistoryGroup(byte matchType, int pageIndex, int pageSize, string condition, string orderby);

		DataSet GetMatchRewardList(int matchId);

		PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby);

		int ExecuteSql(string sql);
	}
}
