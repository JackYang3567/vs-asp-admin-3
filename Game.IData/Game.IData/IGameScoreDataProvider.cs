using Game.Entity.GameScore;

namespace Game.IData
{
	public interface IGameScoreDataProvider
	{
		GameScoreInfo GetSocreInfoByUserId(int userId);
	}
}
