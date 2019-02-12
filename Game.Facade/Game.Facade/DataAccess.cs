using Game.Entity.Platform;
using System.Text;

namespace Game.Facade
{
	public class DataAccess
	{
		protected internal PlatformFacade aidePlatformFacade = new PlatformFacade();

		public string GetConn(int kindID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			GameKindItem gameKindItemInfo = aidePlatformFacade.GetGameKindItemInfo(kindID);
			GameGameItem gameGameItemInfo = aidePlatformFacade.GetGameGameItemInfo(gameKindItemInfo.GameID);
			stringBuilder.AppendFormat("Data Source={0}; Initial Catalog={1}; User ID={2}; Password={3}; Pooling=true", gameGameItemInfo.DataBaseAddr, gameGameItemInfo.DataBaseName, "sa", "3112546");
			return stringBuilder.ToString();
		}
	}
}
