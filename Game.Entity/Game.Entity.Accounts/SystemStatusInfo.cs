using System;

namespace Game.Entity.Accounts
{
	[Serializable]
	public class SystemStatusInfo
	{
		public const string Tablename = "SystemStatusInfo";

		public const string _StatusName = "StatusName";

		public const string _StatusValue = "StatusValue";

		public const string _StatusString = "StatusString";

		public const string _StatusTip = "StatusTip";

		public const string _StatusDescription = "StatusDescription";

		public const string _SortID = "SortID";

		private string m_statusName;

		private decimal m_statusValue;

		private string m_statusString;

		private string m_statusTip;

		private string m_statusDescription;

		private int m_sortID;

		public string StatusName
		{
			get
			{
				return m_statusName;
			}
			set
			{
				m_statusName = value;
			}
		}

		public decimal StatusValue
		{
			get
			{
				return m_statusValue;
			}
			set
			{
				m_statusValue = value;
			}
		}

		public string StatusString
		{
			get
			{
				return m_statusString;
			}
			set
			{
				m_statusString = value;
			}
		}

		public string StatusTip
		{
			get
			{
				return m_statusTip;
			}
			set
			{
				m_statusTip = value;
			}
		}

		public string StatusDescription
		{
			get
			{
				return m_statusDescription;
			}
			set
			{
				m_statusDescription = value;
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

		public SystemStatusInfo()
		{
			m_statusName = "";
			m_statusValue = 0m;
			m_statusString = "";
			m_statusTip = "";
			m_statusDescription = "";
			m_sortID = 0;
		}
	}
}
