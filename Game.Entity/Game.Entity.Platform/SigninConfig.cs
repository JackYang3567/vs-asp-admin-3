using System;

namespace Game.Entity.Platform
{
	[Serializable]
	public class SigninConfig
	{
		public const string Tablename = "SigninConfig";

		public const string _DayID = "DayID";

		public const string _RewardGold = "RewardGold";

		private int m_dayID;

		private long m_rewardGold;

		public int DayID
		{
			get
			{
				return m_dayID;
			}
			set
			{
				m_dayID = value;
			}
		}

		public long RewardGold
		{
			get
			{
				return m_rewardGold;
			}
			set
			{
				m_rewardGold = value;
			}
		}

		public SigninConfig()
		{
			m_dayID = 0;
			m_rewardGold = 0L;
		}
	}
}
