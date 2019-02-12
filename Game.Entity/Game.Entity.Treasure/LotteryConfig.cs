using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class LotteryConfig
	{
		public const string Tablename = "LotteryConfig";

		public const string _ID = "ID";

		public const string _FreeCount = "FreeCount";

		public const string _ChargeFee = "ChargeFee";

		public const string _IsCharge = "IsCharge";

		private int m_iD;

		private int m_freeCount;

		private int m_chargeFee;

		private byte m_isCharge;

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

		public int FreeCount
		{
			get
			{
				return m_freeCount;
			}
			set
			{
				m_freeCount = value;
			}
		}

		public int ChargeFee
		{
			get
			{
				return m_chargeFee;
			}
			set
			{
				m_chargeFee = value;
			}
		}

		public byte IsCharge
		{
			get
			{
				return m_isCharge;
			}
			set
			{
				m_isCharge = value;
			}
		}

		public LotteryConfig()
		{
			m_iD = 0;
			m_freeCount = 0;
			m_chargeFee = 0;
			m_isCharge = 0;
		}
	}
}
