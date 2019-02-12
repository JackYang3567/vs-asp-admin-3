using System;

namespace Game.Entity.NativeWeb
{
	[Serializable]
	public class AwardInfo
	{
		public const string Tablename = "AwardInfo";

		public const string _AwardID = "AwardID";

		public const string _AwardName = "AwardName";

		public const string _TypeID = "TypeID";

		public const string _Price = "Price";

		public const string _Inventory = "Inventory";

		public const string _BuyCount = "BuyCount";

		public const string _SmallImage = "SmallImage";

		public const string _BigImage = "BigImage";

		public const string _NeedInfo = "NeedInfo";

		public const string _IsMember = "IsMember";

		public const string _IsReturn = "IsReturn";

		public const string _SortID = "SortID";

		public const string _Nullity = "Nullity";

		public const string _Description = "Description";

		public const string _CollectDate = "CollectDate";

		private int m_awardID;

		private string m_awardName;

		private int m_typeID;

		private int m_price;

		private int m_inventory;

		private int m_buyCount;

		private string m_smallImage;

		private string m_bigImage;

		private int m_needInfo;

		private bool m_isMember;

		private bool m_isReturn;

		private int m_sortID;

		private byte m_nullity;

		private string m_description;

		private DateTime m_collectDate;

		public int AwardID
		{
			get
			{
				return m_awardID;
			}
			set
			{
				m_awardID = value;
			}
		}

		public string AwardName
		{
			get
			{
				return m_awardName;
			}
			set
			{
				m_awardName = value;
			}
		}

		public int TypeID
		{
			get
			{
				return m_typeID;
			}
			set
			{
				m_typeID = value;
			}
		}

		public int Price
		{
			get
			{
				return m_price;
			}
			set
			{
				m_price = value;
			}
		}

		public int Inventory
		{
			get
			{
				return m_inventory;
			}
			set
			{
				m_inventory = value;
			}
		}

		public int BuyCount
		{
			get
			{
				return m_buyCount;
			}
			set
			{
				m_buyCount = value;
			}
		}

		public string SmallImage
		{
			get
			{
				return m_smallImage;
			}
			set
			{
				m_smallImage = value;
			}
		}

		public string BigImage
		{
			get
			{
				return m_bigImage;
			}
			set
			{
				m_bigImage = value;
			}
		}

		public int NeedInfo
		{
			get
			{
				return m_needInfo;
			}
			set
			{
				m_needInfo = value;
			}
		}

		public bool IsMember
		{
			get
			{
				return m_isMember;
			}
			set
			{
				m_isMember = value;
			}
		}

		public bool IsReturn
		{
			get
			{
				return m_isReturn;
			}
			set
			{
				m_isReturn = value;
			}
		}

		public int SortID
		{
			get
			{
				return m_sortID;
			}
			set
			{
				m_sortID = value;
			}
		}

		public byte Nullity
		{
			get
			{
				return m_nullity;
			}
			set
			{
				m_nullity = value;
			}
		}

		public string Description
		{
			get
			{
				return m_description;
			}
			set
			{
				m_description = value;
			}
		}

		public DateTime CollectDate
		{
			get
			{
				return m_collectDate;
			}
			set
			{
				m_collectDate = value;
			}
		}

		public AwardInfo()
		{
			m_awardID = 0;
			m_awardName = "";
			m_typeID = 0;
			m_price = 0;
			m_inventory = 0;
			m_buyCount = 0;
			m_smallImage = "";
			m_bigImage = "";
			m_needInfo = 0;
			m_isMember = false;
			m_isReturn = false;
			m_sortID = 0;
			m_nullity = 0;
			m_description = "";
			m_collectDate = DateTime.Now;
		}
	}
}
