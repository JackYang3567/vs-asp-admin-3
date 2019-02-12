using System;

namespace Game.Entity.Platform
{
	[Serializable]
	public class GrowLevelConfig
	{
		public const string Tablename = "GrowLevelConfig";

		public const string _LevelID = "LevelID";

		public const string _Experience = "Experience";

		public const string _RewardGold = "RewardGold";

		public const string _RewardMedal = "RewardMedal";

		public const string _LevelRemark = "LevelRemark";

		private int m_levelID;

		private int m_experience;

		private int m_rewardGold;

		private int m_rewardMedal;

		private string m_levelRemark;

		public int LevelID
		{
			get
			{
				return m_levelID;
			}
			set
			{
				m_levelID = value;
			}
		}

		public int Experience
		{
			get
			{
				return m_experience;
			}
			set
			{
				m_experience = value;
			}
		}

		public int RewardGold
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

		public int RewardMedal
		{
			get
			{
				return m_rewardMedal;
			}
			set
			{
				m_rewardMedal = value;
			}
		}

		public string LevelRemark
		{
			get
			{
				return m_levelRemark;
			}
			set
			{
				m_levelRemark = value;
			}
		}

		public GrowLevelConfig()
		{
			m_levelID = 0;
			m_experience = 0;
			m_rewardGold = 0;
			m_rewardMedal = 0;
			m_levelRemark = "";
		}
	}
}
