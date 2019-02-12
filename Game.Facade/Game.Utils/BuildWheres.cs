namespace Game.Utils
{
	public class BuildWheres
	{
		private object obj;

		public BuildWheres()
		{
			obj = " where ";
		}

		public void Append(string where)
		{
			if (obj.ToString().Trim() != "where")
			{
				obj = obj + " and " + where;
			}
			else
			{
				obj += where;
			}
		}

		public override string ToString()
		{
			if (obj == null || obj.ToString().Trim() == "where")
			{
				return "";
			}
			return obj.ToString();
		}
	}
}
