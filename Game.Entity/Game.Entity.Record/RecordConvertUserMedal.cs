using System;

namespace Game.Entity.Record
{
	[Serializable]
	public class RecordConvertUserMedal
	{
		public const string Tablename = "RecordConvertUserMedal";

		public const string _RecordID = "RecordID";

		public const string _UserID = "UserID";

		public const string _CurInsureScore = "CurInsureScore";

		public const string _CurUserMedal = "CurUserMedal";

		public const string _ConvertUserMedal = "ConvertUserMedal";

		public const string _ConvertRate = "ConvertRate";

		public const string _IsGamePlaza = "IsGamePlaza";

		public const string _ClientIP = "ClientIP";

		public const string _CollectDate = "CollectDate";

		private int m_recordID;

		private int m_userID;

		private long m_curInsureScore;

		private int m_curUserMedal;

		private int m_convertUserMedal;

		private decimal m_convertRate;

		private byte m_isGamePlaza;

		private string m_clientIP;

		private DateTime m_collectDate;

		public int RecordID
		{
			get
			{
				return m_recordID;
			}
			set
			{
				m_recordID = value;
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

		public long CurInsureScore
		{
			get
			{
				return m_curInsureScore;
			}
			set
			{
				m_curInsureScore = value;
			}
		}

		public int CurUserMedal
		{
			get
			{
				return m_curUserMedal;
			}
			set
			{
				m_curUserMedal = value;
			}
		}

		public int ConvertUserMedal
		{
			get
			{
				return m_convertUserMedal;
			}
			set
			{
				m_convertUserMedal = value;
			}
		}

		public decimal ConvertRate
		{
			get
			{
				return m_convertRate;
			}
			set
			{
				m_convertRate = value;
			}
		}

		public byte IsGamePlaza
		{
			get
			{
				return m_isGamePlaza;
			}
			set
			{
				m_isGamePlaza = value;
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

		public RecordConvertUserMedal()
		{
			m_recordID = 0;
			m_userID = 0;
			m_curInsureScore = 0L;
			m_curUserMedal = 0;
			m_convertUserMedal = 0;
			m_convertRate = 0m;
			m_isGamePlaza = 0;
			m_clientIP = "";
			m_collectDate = DateTime.Now;
		}
	}
}
