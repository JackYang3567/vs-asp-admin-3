using System;

namespace Game.Entity.Accounts
{
	[Serializable]
	public class IndividualDatum
	{
		public const string Tablename = "IndividualDatum";

		public const string _UserID = "UserID";

		public const string _QQ = "QQ";

		public const string _EMail = "EMail";

		public const string _SeatPhone = "SeatPhone";

		public const string _MobilePhone = "MobilePhone";

		public const string _DwellingPlace = "DwellingPlace";

		public const string _PostalCode = "PostalCode";

		public const string _CollectDate = "CollectDate";

		public const string _UserNote = "UserNote";

		public const string _Compellation = "Compellation";

		public const string _BankNO = "BankNO";

		public const string _BankName = "BankName";

		public const string _BankAddress = "BankAddress";

		private int m_userID;

		private string m_qQ;

		private string m_eMail;

		private string m_seatPhone;

		private string m_mobilePhone;

		private string m_dwellingPlace;

		private string m_postalCode;

		private DateTime m_collectDate;

		private string m_userNote;

		private string m_bankNO;

		public const string _AlipayID = "AlipayID";

		private string m_alipayID;

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

		public string SeatPhone
		{
			get
			{
				return m_seatPhone;
			}
			set
			{
				m_seatPhone = value;
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

		public string UserNote
		{
			get
			{
				return m_userNote;
			}
			set
			{
				m_userNote = value;
			}
		}

		public string BankNO
		{
			get;
			set;
		}

		public string BankName
		{
			get;
			set;
		}

		public string BankAddress
		{
			get;
			set;
		}

		public string Compellation
		{
			get;
			set;
		}

		public string AlipayID
		{
			get
			{
				return m_alipayID;
			}
			set
			{
				m_alipayID = value;
			}
		}

		public IndividualDatum()
		{
			m_userID = 0;
			m_qQ = "";
			m_eMail = "";
			m_seatPhone = "";
			m_mobilePhone = "";
			m_dwellingPlace = "";
			m_postalCode = "";
			m_collectDate = DateTime.Now;
			m_userNote = "";
			m_alipayID = "";
		}
	}
}
