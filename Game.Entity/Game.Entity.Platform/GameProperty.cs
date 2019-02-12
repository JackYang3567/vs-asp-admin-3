using System;

namespace Game.Entity.Platform
{
	[Serializable]
	public class GameProperty
	{
		public const string Tablename = "GameProperty";

		public const string _ID = "ID";

		public const string _Name = "Name";

		public const string _Kind = "Kind";

		public const string _PTypeID = "PTypeID";

		public const string _MTypeID = "MTypeID";

		public const string _Cash = "Cash";

		public const string _Gold = "Gold";

		public const string _UserMedal = "UserMedal";

		public const string _LoveLiness = "LoveLiness";

		public const string _UseArea = "UseArea";

		public const string _ServiceArea = "ServiceArea";

		public const string _SuportMobile = "SuportMobile";

		public const string _RegulationsInfo = "RegulationsInfo";

		public const string _SendLoveLiness = "SendLoveLiness";

		public const string _RecvLoveLiness = "RecvLoveLiness";

		public const string _UseResultsGold = "UseResultsGold";

		public const string _UseResultsValidTime = "UseResultsValidTime";

		public const string _UseResultsValidTimeScoreMultiple = "UseResultsValidTimeScoreMultiple";

		public const string _UseResultsGiftPackage = "UseResultsGiftPackage";

		public const string _Recommend = "Recommend";

		public const string _Nullity = "Nullity";

		private int m_iD;

		private string m_name;

		private int m_kind;

		private int m_pTypeID;

		private int m_mTypeID;

		private decimal m_cash;

		private long m_gold;

		private int m_userMedal;

		private int m_loveLiness;

		private short m_useArea;

		private short m_serviceArea;

		private byte m_suportMobile;

		private string m_regulationsInfo;

		private long m_sendLoveLiness;

		private long m_recvLoveLiness;

		private long m_useResultsGold;

		private long m_useResultsValidTime;

		private int m_useResultsValidTimeScoreMultiple;

		private int m_useResultsGiftPackage;

		private int m_recommend;

		private byte m_nullity;

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

		public string Name
		{
			get
			{
				return m_name;
			}
			set
			{
				m_name = value;
			}
		}

		public int Kind
		{
			get
			{
				return m_kind;
			}
			set
			{
				m_kind = value;
			}
		}

		public int PTypeID
		{
			get
			{
				return m_pTypeID;
			}
			set
			{
				m_pTypeID = value;
			}
		}

		public int MTypeID
		{
			get
			{
				return m_mTypeID;
			}
			set
			{
				m_mTypeID = value;
			}
		}

		public decimal Cash
		{
			get
			{
				return m_cash;
			}
			set
			{
				m_cash = value;
			}
		}

		public long Gold
		{
			get
			{
				return m_gold;
			}
			set
			{
				m_gold = value;
			}
		}

		public int UserMedal
		{
			get
			{
				return m_userMedal;
			}
			set
			{
				m_userMedal = value;
			}
		}

		public int LoveLiness
		{
			get
			{
				return m_loveLiness;
			}
			set
			{
				m_loveLiness = value;
			}
		}

		public short UseArea
		{
			get
			{
				return m_useArea;
			}
			set
			{
				m_useArea = value;
			}
		}

		public short ServiceArea
		{
			get
			{
				return m_serviceArea;
			}
			set
			{
				m_serviceArea = value;
			}
		}

		public byte SuportMobile
		{
			get
			{
				return m_suportMobile;
			}
			set
			{
				m_suportMobile = value;
			}
		}

		public string RegulationsInfo
		{
			get
			{
				return m_regulationsInfo;
			}
			set
			{
				m_regulationsInfo = value;
			}
		}

		public long SendLoveLiness
		{
			get
			{
				return m_sendLoveLiness;
			}
			set
			{
				m_sendLoveLiness = value;
			}
		}

		public long RecvLoveLiness
		{
			get
			{
				return m_recvLoveLiness;
			}
			set
			{
				m_recvLoveLiness = value;
			}
		}

		public long UseResultsGold
		{
			get
			{
				return m_useResultsGold;
			}
			set
			{
				m_useResultsGold = value;
			}
		}

		public long UseResultsValidTime
		{
			get
			{
				return m_useResultsValidTime;
			}
			set
			{
				m_useResultsValidTime = value;
			}
		}

		public int UseResultsValidTimeScoreMultiple
		{
			get
			{
				return m_useResultsValidTimeScoreMultiple;
			}
			set
			{
				m_useResultsValidTimeScoreMultiple = value;
			}
		}

		public int UseResultsGiftPackage
		{
			get
			{
				return m_useResultsGiftPackage;
			}
			set
			{
				m_useResultsGiftPackage = value;
			}
		}

		public int Recommend
		{
			get
			{
				return m_recommend;
			}
			set
			{
				m_recommend = value;
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

		public GameProperty()
		{
			m_iD = 0;
			m_name = "";
			m_kind = 0;
			m_pTypeID = 0;
			m_mTypeID = 0;
			m_cash = 0m;
			m_gold = 0L;
			m_userMedal = 0;
			m_loveLiness = 0;
			m_useArea = 0;
			m_serviceArea = 0;
			m_suportMobile = 0;
			m_regulationsInfo = "";
			m_sendLoveLiness = 0L;
			m_recvLoveLiness = 0L;
			m_useResultsGold = 0L;
			m_useResultsValidTime = 0L;
			m_useResultsValidTimeScoreMultiple = 0;
			m_useResultsGiftPackage = 0;
			m_recommend = 0;
			m_nullity = 0;
		}
	}
}
