using System;

namespace Game.Entity.GameScore
{
	[Serializable]
	public class GameProperty
	{
		public const string Tablename = "GameProperty";

		public const string _ID = "ID";

		public const string _Name = "Name";

		public const string _Cash = "Cash";

		public const string _Gold = "Gold";

		public const string _Discount = "Discount";

		public const string _IssueArea = "IssueArea";

		public const string _ServiceArea = "ServiceArea";

		public const string _SendLoveLiness = "SendLoveLiness";

		public const string _RecvLoveLiness = "RecvLoveLiness";

		public const string _RegulationsInfo = "RegulationsInfo";

		public const string _Nullity = "Nullity";

		private int m_iD;

		private string m_name;

		private decimal m_cash;

		private long m_gold;

		private short m_discount;

		private short m_issueArea;

		private short m_serviceArea;

		private long m_sendLoveLiness;

		private long m_recvLoveLiness;

		private string m_regulationsInfo;

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

		public short Discount
		{
			get
			{
				return m_discount;
			}
			set
			{
				m_discount = value;
			}
		}

		public short IssueArea
		{
			get
			{
				return m_issueArea;
			}
			set
			{
				m_issueArea = value;
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
			m_cash = 0m;
			m_gold = 0L;
			m_discount = 0;
			m_issueArea = 0;
			m_serviceArea = 0;
			m_sendLoveLiness = 0L;
			m_recvLoveLiness = 0L;
			m_regulationsInfo = "";
			m_nullity = 0;
		}
	}
}
