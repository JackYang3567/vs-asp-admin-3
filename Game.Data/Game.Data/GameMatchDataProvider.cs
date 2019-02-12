using Game.Entity.GameMatch;
using Game.IData;
using Game.Kernel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Game.Data
{
	public class GameMatchDataProvider : BaseDataProvider, IGameMatchProvider
	{
		private ITableProvider aideMatchInfoProvider;

		private ITableProvider aideMatchPublicProvider;

		public GameMatchDataProvider(string connString)
			: base(connString)
		{
			aideMatchInfoProvider = GetTableProvider("MatchInfo");
			aideMatchPublicProvider = GetTableProvider("MatchPublic");
		}

		public MatchInfo GetMatchInfo(int matchID)
		{
			string where = string.Format("(NOLOCK) WHERE MatchID= {0}", matchID);
			return aideMatchInfoProvider.GetObject<MatchInfo>(where);
		}

		public void UpdateMatchInfo(MatchInfo matchInfo)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE MatchInfo SET ").Append("MatchSummary=@MatchSummary, ").Append("MatchImage=@MatchImage, ")
				.Append("MatchContent=@MatchContent, ")
				.Append("SortID=@SortID ")
				.Append("WHERE MatchID=@MatchID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("MatchSummary", matchInfo.MatchSummary));
			list.Add(base.Database.MakeInParam("MatchImage", matchInfo.MatchImage));
			list.Add(base.Database.MakeInParam("MatchContent", matchInfo.MatchContent));
			list.Add(base.Database.MakeInParam("SortID", matchInfo.SortID));
			list.Add(base.Database.MakeInParam("MatchID", matchInfo.MatchID));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public MatchPublic GetMatchPublicInfo(int matchID)
		{
			string where = string.Format("(NOLOCK) WHERE MatchID= {0}", matchID);
			return aideMatchPublicProvider.GetObject<MatchPublic>(where);
		}

		public DataSet GetAllMatch()
		{
			string commandText = "SELECT * FROM MatchPublic";
			return base.Database.ExecuteDataset(commandText);
		}

		public DataSet GetMatchListByMatchType(byte matchType)
		{
			string commandText = "SELECT * FROM MatchPublic WHERE MatchType=@MatchType";
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("MatchType", matchType));
			return base.Database.ExecuteDataset(CommandType.Text, commandText, list.ToArray());
		}

		public DataSet GetMatchListByMatchID(int matchId)
		{
			string commandText = "SELECT * FROM MatchPublic WHERE MatchID=@MatchID";
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("MatchID", matchId));
			return base.Database.ExecuteDataset(CommandType.Text, commandText, list.ToArray());
		}

		public PagerSet GetTimingMatchHistoryGroup(byte matchType, int pageIndex, int pageSize, string condition, string orderby)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("MatchType", matchType));
			list.Add(base.Database.MakeInParam("PageSize", pageSize));
			list.Add(base.Database.MakeInParam("PageIndex", pageIndex));
			list.Add(base.Database.MakeInParam("Where", condition));
			list.Add(base.Database.MakeInParam("OrderBy", orderby));
			list.Add(base.Database.MakeOutParam("PageCount", typeof(int)));
			list.Add(base.Database.MakeOutParam("RecordCount", typeof(int)));
			DataSet ds;
			base.Database.RunProc("NET_PW_GetMatchHistoryGroup", list, out ds);
			PagerSet pagerSet = new PagerSet(pageIndex, pageSize, Convert.ToInt32(list[list.Count - 3].Value), Convert.ToInt32(list[list.Count - 2].Value), ds);
			pagerSet.PageSet.DataSetName = "PagerSet";
			return pagerSet;
		}

		public DataSet GetMatchRewardList(int matchId)
		{
			string commandText = "SELECT * FROM MatchReward WHERE MatchID=@MatchID";
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("MatchID", matchId));
			return base.Database.ExecuteDataset(CommandType.Text, commandText, list.ToArray());
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
	}
}
