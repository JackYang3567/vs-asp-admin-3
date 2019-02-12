using System;

namespace Game.Entity.NativeWeb
{
	[Serializable]
	public class GameMatchUserInfo
	{
		public const string Tablename = "GameMatchUserInfo";

		public const string _MatchID = "MatchID";

		public const string _UserID = "UserID";

		public const string _Accounts = "Accounts";

		public const string _GameID = "GameID";

		public const string _Compellation = "Compellation";

		public const string _Gender = "Gender";

		public const string _PassportID = "PassportID";

		public const string _MobilePhone = "MobilePhone";

		public const string _EMail = "EMail";

		public const string _QQ = "QQ";

		public const string _DwellingPlace = "DwellingPlace";

		public const string _PostalCode = "PostalCode";

		public const string _Nullity = "Nullity";

		public const string _ClientIP = "ClientIP";

		public const string _CollectDate = "CollectDate";

		private int m_matchID;

		private int m_userID;

		private string m_accounts;

		private int m_gameID;

		private string m_compellation;

		private byte m_gender;

		private string m_passportID;

		private string m_mobilePhone;

		private string m_eMail;

		private string m_qQ;

		private string m_dwellingPlace;

		private string m_postalCode;

		private byte m_nullity;

		private string m_clientIP;

		private DateTime m_collectDate;

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

		public string Accounts
		{
			get
			{
				return m_accounts;
			}
			set
			{
				m_accounts = value;
			}
		}

		public int GameID
		{
			get
			{
				return m_gameID;
			}
			set
			{
				m_gameID = value;
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

		public byte Gender
		{
			get
			{
				return m_gender;
			}
			set
			{
				m_gender = value;
			}
		}

		public string PassportID
		{
			get
			{
				return m_passportID;
			}
			set
			{
				m_passportID = value;
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

		public string EMail
		{
			get
			{
				return m_eMail;
			}
			set
			{
				m_eMail = value;
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

		public string ClientIP
		{
			get
			{
				return m_clientIP;
			}
			set
			{
				m_clientIP = value;
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

		public GameMatchUserInfo()
		{
			m_matchID = 0;
			m_userID = 0;
			m_accounts = "";
			m_gameID = 0;
			m_compellation = "";
			m_gender = 0;
			m_passportID = "";
			m_mobilePhone = "";
			m_eMail = "";
			m_qQ = "";
			m_dwellingPlace = "";
			m_postalCode = "";
			m_nullity = 0;
			m_clientIP = "";
			m_collectDate = DateTime.Now;
		}
	}
}
