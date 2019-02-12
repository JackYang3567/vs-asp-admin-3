using System.Data;

namespace Game.Entity
{
	public class SQLResult
	{
		public string Msg
		{
			get;
			set;
		}

		public bool Success
		{
			get;
			set;
		}

		public DataTable Data
		{
			get;
			set;
		}
	}
}
