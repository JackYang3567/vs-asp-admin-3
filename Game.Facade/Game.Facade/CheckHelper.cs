namespace Game.Facade
{
	public class CheckHelper
	{
		public static bool CheckIds(string ids)
		{
			string[] array = ids.Split(',');
			int result = 0;
			string[] array2 = array;
			foreach (string s in array2)
			{
				if (!int.TryParse(s, out result))
				{
					return false;
				}
			}
			return true;
		}
	}
}
