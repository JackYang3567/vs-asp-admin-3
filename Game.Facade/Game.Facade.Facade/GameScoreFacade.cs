using Game.Data.Factory;
using Game.Entity.GameScore;
using Game.IData;

namespace Game.Facade.Facade
{
	public class GameScoreFacade
	{
		private IGameScoreDataProvider aideGameScoreData;

		public GameScoreFacade(string conn)
		{
			aideGameScoreData = ClassFactory.GetIGameScoreDataProvider(conn);
		}

		public GameScoreInfo GetGameScoreInfoByUserId(int userId)
		{
			return aideGameScoreData.GetSocreInfoByUserId(userId);
		}
	}
}
