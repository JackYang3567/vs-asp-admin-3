using System;

namespace Game.Entity.GameMatch
{
	[Serializable]
	public class MatchReward
	{
		public const string Tablename = "MatchReward";

		public const string _MatchID = "MatchID";

		public const string _MatchRank = "MatchRank";

		public const string _RewardGold = "RewardGold";

		public const string _RewardIngot = "RewardIngot";

		public const string _RewardExperience = "RewardExperience";

		public const string _RewardDescibe = "RewardDescibe";

		private int m_matchID;

		private short m_matchRank;

		private long m_rewardGold;

		private long m_rewardIngot;

		private long m_rewardExperience;

		private string m_rewardDescibe;

		public int MatchID
		{
			get
			{
				return m_matchID;
			}
			set
			{
				m_matchID = value;
			}
		}

		public short MatchRank
		{
			get
			{
				return m_matchRank;
			}
			set
			{
				m_matchRank = value;
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

		public long RewardIngot
		{
			get
			{
				return m_rewardIngot;
			}
			set
			{
				m_rewardIngot = value;
			}
		}

		public long RewardExperience
		{
			get
			{
				return m_rewardExperience;
			}
			set
			{
				m_rewardExperience = value;
			}
		}

		public string RewardDescibe
		{
			get
			{
				return m_rewardDescibe;
			}
			set
			{
				m_rewardDescibe = value;
			}
		}

		public MatchReward()
		{
			m_matchID = 0;
			m_matchRank = 0;
			m_rewardGold = 0L;
			m_rewardIngot = 0L;
			m_rewardExperience = 0L;
			m_rewardDescibe = "";
		}
	}
}
