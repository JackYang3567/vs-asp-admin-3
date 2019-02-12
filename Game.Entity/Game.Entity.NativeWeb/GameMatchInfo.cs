using System;

namespace Game.Entity.NativeWeb
{
	[Serializable]
	public class GameMatchInfo
	{
		public const string Tablename = "GameMatchInfo";

		public const string _MatchID = "MatchID";

		public const string _MatchTitle = "MatchTitle";

		public const string _ImageUrl = "ImageUrl";

		public const string _MatchSummary = "MatchSummary";

		public const string _MatchContent = "MatchContent";

		public const string _ApplyBeginDate = "ApplyBeginDate";

		public const string _ApplyEndDate = "ApplyEndDate";

		public const string _MatchStatus = "MatchStatus";

		public const string _Nullity = "Nullity";

		public const string _CollectDate = "CollectDate";

		public const string _ModifyDate = "ModifyDate";

		private int m_matchID;

		private string m_matchTitle;

		private string m_imageUrl;

		private string m_matchSummary;

		private string m_matchContent;

		private DateTime m_applyBeginDate;

		private DateTime m_applyEndDate;

		private int m_matchStatus;

		private byte m_nullity;

		private DateTime m_collectDate;

		private DateTime m_modifyDate;

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

		public string MatchTitle
		{
			get
			{
				return m_matchTitle;
			}
			set
			{
				m_matchTitle = value;
			}
		}

		public string ImageUrl
		{
			get
			{
				return m_imageUrl;
			}
			set
			{
				m_imageUrl = value;
			}
		}

		public string MatchSummary
		{
			get
			{
				return m_matchSummary;
			}
			set
			{
				m_matchSummary = value;
			}
		}

		public string MatchContent
		{
			get
			{
				return m_matchContent;
			}
			set
			{
				m_matchContent = value;
			}
		}

		public DateTime ApplyBeginDate
		{
			get
			{
				return m_applyBeginDate;
			}
			set
			{
				m_applyBeginDate = value;
			}
		}

		public DateTime ApplyEndDate
		{
			get
			{
				return m_applyEndDate;
			}
			set
			{
				m_applyEndDate = value;
			}
		}

		public int MatchStatus
		{
			get
			{
				return m_matchStatus;
			}
			set
			{
				m_matchStatus = value;
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

		public DateTime ModifyDate
		{
			get
			{
				return m_modifyDate;
			}
			set
			{
				m_modifyDate = value;
			}
		}

		public GameMatchInfo()
		{
			m_matchID = 0;
			m_matchTitle = "";
			m_imageUrl = "";
			m_matchSummary = "";
			m_matchContent = "";
			m_applyBeginDate = DateTime.Now;
			m_applyEndDate = DateTime.Now;
			m_matchStatus = 0;
			m_nullity = 0;
			m_collectDate = DateTime.Now;
			m_modifyDate = DateTime.Now;
		}
	}
}
