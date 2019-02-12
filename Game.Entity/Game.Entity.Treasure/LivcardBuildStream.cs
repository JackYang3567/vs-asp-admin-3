using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class LivcardBuildStream
	{
		public const string Tablename = "LivcardBuildStream";

		public const string _BuildID = "BuildID";

		public const string _AdminName = "AdminName";

		public const string _CardTypeID = "CardTypeID";

		public const string _CardPrice = "CardPrice";

		public const string _Currency = "Currency";

		public const string _BuildCount = "BuildCount";

		public const string _BuildAddr = "BuildAddr";

		public const string _BuildDate = "BuildDate";

		public const string _DownLoadCount = "DownLoadCount";

		public const string _NoteInfo = "NoteInfo";

		public const string _BuildCardPacket = "BuildCardPacket";

		public const string _Gold = "Gold";

		private int m_buildID;

		private string m_adminName;

		private int m_cardTypeID;

		private decimal m_cardPrice;

		private decimal m_currency;

		private int m_buildCount;

		private string m_buildAddr;

		private DateTime m_buildDate;

		private int m_downLoadCount;

		private string m_noteInfo;

		private byte[] m_buildCardPacket;

		private int m_gold;

		public int Gold
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

		public int BuildID
		{
			get
			{
				return m_buildID;
			}
			set
			{
				m_buildID = value;
			}
		}

		public string AdminName
		{
			get
			{
				return m_adminName;
			}
			set
			{
				m_adminName = value;
			}
		}

		public int CardTypeID
		{
			get
			{
				return m_cardTypeID;
			}
			set
			{
				m_cardTypeID = value;
			}
		}

		public decimal CardPrice
		{
			get
			{
				return m_cardPrice;
			}
			set
			{
				m_cardPrice = value;
			}
		}

		public decimal Currency
		{
			get
			{
				return m_currency;
			}
			set
			{
				m_currency = value;
			}
		}

		public int BuildCount
		{
			get
			{
				return m_buildCount;
			}
			set
			{
				m_buildCount = value;
			}
		}

		public string BuildAddr
		{
			get
			{
				return m_buildAddr;
			}
			set
			{
				m_buildAddr = value;
			}
		}

		public DateTime BuildDate
		{
			get
			{
				return m_buildDate;
			}
			set
			{
				m_buildDate = value;
			}
		}

		public int DownLoadCount
		{
			get
			{
				return m_downLoadCount;
			}
			set
			{
				m_downLoadCount = value;
			}
		}

		public string NoteInfo
		{
			get
			{
				return m_noteInfo;
			}
			set
			{
				m_noteInfo = value;
			}
		}

		public byte[] BuildCardPacket
		{
			get
			{
				return m_buildCardPacket;
			}
			set
			{
				m_buildCardPacket = value;
			}
		}

		public LivcardBuildStream()
		{
			m_gold = 0;
			m_buildID = 0;
			m_adminName = "";
			m_cardTypeID = 0;
			m_cardPrice = 0m;
			m_currency = 0m;
			m_buildCount = 0;
			m_buildAddr = "";
			m_buildDate = DateTime.Now;
			m_downLoadCount = 0;
			m_noteInfo = "";
			m_buildCardPacket = null;
		}
	}
}
