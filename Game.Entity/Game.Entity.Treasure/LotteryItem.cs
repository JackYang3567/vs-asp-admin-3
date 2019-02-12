using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class LotteryItem
	{
		public const string Tablename = "LotteryItem";

		public const string _ItemIndex = "ItemIndex";

		public const string _ItemType = "ItemType";

		public const string _ItemQuota = "ItemQuota";

		public const string _ItemRate = "ItemRate";

		private int m_itemIndex;

		private int m_itemType;

		private int m_itemQuota;

		private int m_itemRate;

		public int ItemIndex
		{
			get
			{
				return m_itemIndex;
			}
			set
			{
				m_itemIndex = value;
			}
		}

		public int ItemType
		{
			get
			{
				return m_itemType;
			}
			set
			{
				m_itemType = value;
			}
		}

		public int ItemQuota
		{
			get
			{
				return m_itemQuota;
			}
			set
			{
				m_itemQuota = value;
			}
		}

		public int ItemRate
		{
			get
			{
				return m_itemRate;
			}
			set
			{
				m_itemRate = value;
			}
		}

		public LotteryItem()
		{
			m_itemIndex = 0;
			m_itemType = 0;
			m_itemQuota = 0;
			m_itemRate = 0;
		}
	}
}
