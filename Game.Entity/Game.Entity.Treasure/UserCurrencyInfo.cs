using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class UserCurrencyInfo
	{
		public const string Tablename = "UserCurrencyInfo";

		public const string _UserID = "UserID";

		public const string _Currency = "Currency";

		private int m_userID;

		private decimal m_currency;

		public int UserID
		{
			get
			{
				return m_userID;
			}
			set
			{
				m_userID = value;
			}
		}

		public decimal Currency
		{
			get
			{
				return m_currency;
			}
			set
			{
				m_currency = value;
			}
		}

		public UserCurrencyInfo()
		{
			m_userID = 0;
			m_currency = 0m;
		}
	}
}
