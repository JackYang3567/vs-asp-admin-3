using Game.Entity.GameScore;
using Game.IData;
using Game.Kernel;

namespace Game.Data
{
	public class GameScoreDataProvider : BaseDataProvider, IGameScoreDataProvider
	{
		private ITableProvider aideGameScoreInfo;

		public GameScoreDataProvider(string connString)
			: base(connString)
		{
			aideGameScoreInfo = GetTableProvider("GameScoreInfo");
		}

		public GameScoreInfo GetSocreInfoByUserId(int userId)
		{
			string where = string.Format("(NOLOCK) WHERE UserID= N'{0}'", userId);
			return aideGameScoreInfo.GetObject<GameScoreInfo>(where);
		}
	}
}
