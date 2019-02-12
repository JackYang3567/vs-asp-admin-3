using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class GlobalSpreadInfo
	{
		public const string Tablename = "GlobalSpreadInfo";

		public const string _ID = "ID";

		public const string _RegisterGrantScore = "RegisterGrantScore";

		public const string _PlayTimeCount = "PlayTimeCount";

		public const string _PlayTimeGrantScore = "PlayTimeGrantScore";

		public const string _FillGrantRate = "FillGrantRate";

		public const string _BalanceRate = "BalanceRate";

		public const string _MinBalanceScore = "MinBalanceScore";

		private int m_iD;

		private int m_registerGrantScore;

		private int m_playTimeCount;

		private int m_playTimeGrantScore;

		private decimal m_fillGrantRate;

		private decimal m_balanceRate;

		private int m_minBalanceScore;

		public int ID
		{
			get
			{
				return m_iD;
			}
			set
			{
				m_iD = value;
			}
		}

		public int RegisterGrantScore
		{
			get
			{
				return m_registerGrantScore;
			}
			set
			{
				m_registerGrantScore = value;
			}
		}

		public int PlayTimeCount
		{
			get
			{
				return m_playTimeCount;
			}
			set
			{
				m_playTimeCount = value;
			}
		}

		public int PlayTimeGrantScore
		{
			get
			{
				return m_playTimeGrantScore;
			}
			set
			{
				m_playTimeGrantScore = value;
			}
		}

		public decimal FillGrantRate
		{
			get
			{
				return m_fillGrantRate;
			}
			set
			{
				m_fillGrantRate = value;
			}
		}

		public decimal BalanceRate
		{
			get
			{
				return m_balanceRate;
			}
			set
			{
				m_balanceRate = value;
			}
		}

		public int MinBalanceScore
		{
			get
			{
				return m_minBalanceScore;
			}
			set
			{
				m_minBalanceScore = value;
			}
		}

		public int To1UperRate
		{
			get;
			set;
		}

		public int To2UperRate
		{
			get;
			set;
		}

		public int To3UperRate
		{
			get;
			set;
		}

		public int To4UperRate
		{
			get;
			set;
		}

		public int To5UperRate
		{
			get;
			set;
		}

		public GlobalSpreadInfo()
		{
			m_iD = 0;
			m_registerGrantScore = 0;
			m_playTimeCount = 0;
			m_playTimeGrantScore = 0;
			m_fillGrantRate = 0m;
			m_balanceRate = 0m;
			m_minBalanceScore = 0;
		}
	}
}
