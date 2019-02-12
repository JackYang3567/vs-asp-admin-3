using System;

namespace Game.Entity.GameMatch
{
	[Serializable]
	public class ConfineAddress
	{
		public const string Tablename = "ConfineAddress";

		public const string _AddrString = "AddrString";

		public const string _EnjoinLogon = "EnjoinLogon";

		public const string _EnjoinOverDate = "EnjoinOverDate";

		public const string _CollectDate = "CollectDate";

		public const string _CollectNote = "CollectNote";

		private string m_addrString;

		private bool m_enjoinLogon;

		private DateTime m_enjoinOverDate;

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

		public DateTime EnjoinOverDate
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
			m_enjoinOverDate = DateTime.Now;
			m_collectDate = DateTime.Now;
			m_collectNote = "";
		}
	}
}
