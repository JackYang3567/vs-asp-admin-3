using System;

namespace Game.Entity.NativeWeb
{
	[Serializable]
	public class AwardOrder
	{
		public const string Tablename = "AwardOrder";

		public const string _OrderID = "OrderID";

		public const string _UserID = "UserID";

		public const string _AwardID = "AwardID";

		public const string _AwardPrice = "AwardPrice";

		public const string _AwardCount = "AwardCount";

		public const string _TotalAmount = "TotalAmount";

		public const string _Compellation = "Compellation";

		public const string _MobilePhone = "MobilePhone";

		public const string _QQ = "QQ";

		public const string _Province = "Province";

		public const string _City = "City";

		public const string _Area = "Area";

		public const string _DwellingPlace = "DwellingPlace";

		public const string _PostalCode = "PostalCode";

		public const string _OrderStatus = "OrderStatus";

		public const string _BuyIP = "BuyIP";

		public const string _BuyDate = "BuyDate";

		public const string _SolveNote = "SolveNote";

		public const string _SolveDate = "SolveDate";

		private int m_orderID;

		private int m_userID;

		private int m_awardID;

		private int m_awardPrice;

		private int m_awardCount;

		private int m_totalAmount;

		private string m_compellation;

		private string m_mobilePhone;

		private string m_qQ;

		private int m_province;

		private int m_city;

		private int m_area;

		private string m_dwellingPlace;

		private string m_postalCode;

		private int m_orderStatus;

		private string m_buyIP;

		private DateTime m_buyDate;

		private string m_solveNote;

		private DateTime m_solveDate;

		public int OrderID
		{
			get
			{
				return m_orderID;
			}
			set
			{
				m_orderID = value;
			}
		}

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

		public int AwardPrice
		{
			get
			{
				return m_awardPrice;
			}
			set
			{
				m_awardPrice = value;
			}
		}

		public int AwardCount
		{
			get
			{
				return m_awardCount;
			}
			set
			{
				m_awardCount = value;
			}
		}

		public int TotalAmount
		{
			get
			{
				return m_totalAmount;
			}
			set
			{
				m_totalAmount = value;
			}
		}

		public string Compellation
		{
			get
			{
				return m_compellation;
			}
			set
			{
				m_compellation = value;
			}
		}

		public string MobilePhone
		{
			get
			{
				return m_mobilePhone;
			}
			set
			{
				m_mobilePhone = value;
			}
		}

		public string QQ
		{
			get
			{
				return m_qQ;
			}
			set
			{
				m_qQ = value;
			}
		}

		public int Province
		{
			get
			{
				return m_province;
			}
			set
			{
				m_province = value;
			}
		}

		public int City
		{
			get
			{
				return m_city;
			}
			set
			{
				m_city = value;
			}
		}

		public int Area
		{
			get
			{
				return m_area;
			}
			set
			{
				m_area = value;
			}
		}

		public string DwellingPlace
		{
			get
			{
				return m_dwellingPlace;
			}
			set
			{
				m_dwellingPlace = value;
			}
		}

		public string PostalCode
		{
			get
			{
				return m_postalCode;
			}
			set
			{
				m_postalCode = value;
			}
		}

		public int OrderStatus
		{
			get
			{
				return m_orderStatus;
			}
			set
			{
				m_orderStatus = value;
			}
		}

		public string BuyIP
		{
			get
			{
				return m_buyIP;
			}
			set
			{
				m_buyIP = value;
			}
		}

		public DateTime BuyDate
		{
			get
			{
				return m_buyDate;
			}
			set
			{
				m_buyDate = value;
			}
		}

		public string SolveNote
		{
			get
			{
				return m_solveNote;
			}
			set
			{
				m_solveNote = value;
			}
		}

		public DateTime SolveDate
		{
			get
			{
				return m_solveDate;
			}
			set
			{
				m_solveDate = value;
			}
		}

		public AwardOrder()
		{
			m_orderID = 0;
			m_userID = 0;
			m_awardID = 0;
			m_awardPrice = 0;
			m_awardCount = 0;
			m_totalAmount = 0;
			m_compellation = "";
			m_mobilePhone = "";
			m_qQ = "";
			m_province = 0;
			m_city = 0;
			m_area = 0;
			m_dwellingPlace = "";
			m_postalCode = "";
			m_orderStatus = 0;
			m_buyIP = "";
			m_buyDate = DateTime.Now;
			m_solveNote = "";
			m_solveDate = DateTime.Now;
		}
	}
}
