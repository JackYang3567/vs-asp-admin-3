using System;

namespace Game.Entity.GameMatch
{
	[Serializable]
	public class MatchInfo
	{
		public const string Tablename = "MatchInfo";

		public const string _MatchID = "MatchID";

		public const string _MatchName = "MatchName";

		public const string _MatchDate = "MatchDate";

		public const string _MatchSummary = "MatchSummary";

		public const string _MatchImage = "MatchImage";

		public const string _MatchContent = "MatchContent";

		public const string _SortID = "SortID";

		public const string _Nullity = "Nullity";

		public const string _CollectDate = "CollectDate";

		private int m_matchID;

		private string m_matchName;

		private string m_matchDate;

		private string m_matchSummary;

		private string m_matchImage;

		private string m_matchContent;

		private int m_sortID;

		private bool m_nullity;

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

		public string MatchName
		{
			get
			{
				return m_matchName;
			}
			set
			{
				m_matchName = value;
			}
		}

		public string MatchDate
		{
			get
			{
				return m_matchDate;
			}
			set
			{
				m_matchDate = value;
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

		public string MatchImage
		{
			get
			{
				return m_matchImage;
			}
			set
			{
				m_matchImage = value;
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

		public bool Nullity
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

		public MatchInfo()
		{
			m_matchID = 0;
			m_matchName = "";
			m_matchDate = "";
			m_matchSummary = "";
			m_matchImage = "";
			m_matchContent = "";
			m_sortID = 0;
			m_nullity = false;
			m_collectDate = DateTime.Now;
		}
	}
}
