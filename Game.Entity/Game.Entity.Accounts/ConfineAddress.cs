using System;

namespace Game.Entity.Accounts
{
	[Serializable]
	public class ConfineAddress
	{
		public const string Tablename = "ConfineAddress";

		public const string _AddrString = "AddrString";

		public const string _EnjoinLogon = "EnjoinLogon";

		public const string _EnjoinRegister = "EnjoinRegister";

		public const string _EnjoinOverDate = "EnjoinOverDate";

		public const string _CollectDate = "CollectDate";

		public const string _CollectNote = "CollectNote";

		private string m_addrString;

		private bool m_enjoinLogon;

		private bool m_enjoinRegister;

		private DateTime? m_enjoinOverDate;

		private DateTime m_collectDate;

		private string m_collectNote;

		public string AddrString
		{
			get
			{
				return m_addrString;
			}
			set
			{
				m_addrString = value;
			}
		}

		public bool EnjoinLogon
		{
			get
			{
				return m_enjoinLogon;
			}
			set
			{
				m_enjoinLogon = value;
			}
		}

		public bool EnjoinRegister
		{
			get
			{
				return m_enjoinRegister;
			}
			set
			{
				m_enjoinRegister = value;
			}
		}

		public DateTime? EnjoinOverDate
		{
			get
			{
				return m_enjoinOverDate;
			}
			set
			{
				m_enjoinOverDate = value;
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

		public string CollectNote
		{
			get
			{
				return m_collectNote;
			}
			set
			{
				m_collectNote = value;
			}
		}

		public ConfineAddress()
		{
			m_addrString = "";
			m_enjoinLogon = false;
			m_enjoinRegister = false;
			m_enjoinOverDate = null;
			m_collectDate = DateTime.Now;
			m_collectNote = "";
		}
	}
}
