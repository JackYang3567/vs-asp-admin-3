using System;

namespace Game.Entity.GameMatch
{
	[Serializable]
	public class MatchWebShow
	{
		public const string Tablename = "MatchWebShow";

		public const string _MatchID = "MatchID";

		public const string _MatchNo = "MatchNo";

		public const string _ImageUrl = "ImageUrl";

		public const string _BigImageUrl = "BigImageUrl";

		public const string _MatchSummary = "MatchSummary";

		public const string _MatchContent = "MatchContent";

		public const string _IsRecommend = "IsRecommend";

		public const string _MatchStatus = "MatchStatus";

		private int m_matchID;

		private short m_matchNo;

		private string m_imageUrl;

		private string m_bigImageUrl;

		private string m_matchSummary;

		private string m_matchContent;

		private bool m_isRecommend;

		private int m_matchStatus;

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

		public short MatchNo
		{
			get
			{
				return m_matchNo;
			}
			set
			{
				m_matchNo = value;
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

		public string BigImageUrl
		{
			get
			{
				return m_bigImageUrl;
			}
			set
			{
				m_bigImageUrl = value;
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

		public bool IsRecommend
		{
			get
			{
				return m_isRecommend;
			}
			set
			{
				m_isRecommend = value;
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

		public MatchWebShow()
		{
			m_matchID = 0;
			m_matchNo = 0;
			m_imageUrl = "";
			m_bigImageUrl = "";
			m_matchSummary = "";
			m_matchContent = "";
			m_isRecommend = false;
			m_matchStatus = 0;
		}
	}
}
