using System;

namespace Game.Entity.GameMatch
{
	[Serializable]
	public class MatchPublic
	{
		public const string Tablename = "MatchPublic";

		public const string _MatchID = "MatchID";

		public const string _MatchStatus = "MatchStatus";

		public const string _KindID = "KindID";

		public const string _MatchName = "MatchName";

		public const string _MatchType = "MatchType";

		public const string _SignupMode = "SignupMode";

		public const string _FeeType = "FeeType";

		public const string _SignupFee = "SignupFee";

		public const string _DeductArea = "DeductArea";

		public const string _JoinCondition = "JoinCondition";

		public const string _MemberOrder = "MemberOrder";

		public const string _Experience = "Experience";

		public const string _FromMatchID = "FromMatchID";

		public const string _FilterType = "FilterType";

		public const string _MaxRankID = "MaxRankID";

		public const string _MatchEndDate = "MatchEndDate";

		public const string _MatchStartDate = "MatchStartDate";

		public const string _RankingMode = "RankingMode";

		public const string _CountInnings = "CountInnings";

		public const string _FilterGradesMode = "FilterGradesMode";

		public const string _DistributeRule = "DistributeRule";

		public const string _MinDistributeUser = "MinDistributeUser";

		public const string _DistributeTimeSpace = "DistributeTimeSpace";

		public const string _MinPartakeGameUser = "MinPartakeGameUser";

		public const string _MaxPartakeGameUser = "MaxPartakeGameUser";

		public const string _MatchRule = "MatchRule";

		public const string _ServiceMachine = "ServiceMachine";

		public const string _Nullity = "Nullity";

		public const string _CollectDate = "CollectDate";

		private int m_matchID;

		private byte m_matchStatus;

		private int m_kindID;

		private string m_matchName;

		private byte m_matchType;

		private byte m_signupMode;

		private byte m_feeType;

		private long m_signupFee;

		private byte m_deductArea;

		private byte m_joinCondition;

		private byte m_memberOrder;

		private int m_experience;

		private int m_fromMatchID;

		private byte m_filterType;

		private short m_maxRankID;

		private DateTime m_matchEndDate;

		private DateTime m_matchStartDate;

		private byte m_rankingMode;

		private short m_countInnings;

		private byte m_filterGradesMode;

		private byte m_distributeRule;

		private short m_minDistributeUser;

		private short m_distributeTimeSpace;

		private short m_minPartakeGameUser;

		private short m_maxPartakeGameUser;

		private string m_matchRule;

		private string m_serviceMachine;

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

		public byte MatchStatus
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

		public int KindID
		{
			get
			{
				return m_kindID;
			}
			set
			{
				m_kindID = value;
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

		public byte MatchType
		{
			get
			{
				return m_matchType;
			}
			set
			{
				m_matchType = value;
			}
		}

		public byte SignupMode
		{
			get
			{
				return m_signupMode;
			}
			set
			{
				m_signupMode = value;
			}
		}

		public byte FeeType
		{
			get
			{
				return m_feeType;
			}
			set
			{
				m_feeType = value;
			}
		}

		public long SignupFee
		{
			get
			{
				return m_signupFee;
			}
			set
			{
				m_signupFee = value;
			}
		}

		public byte DeductArea
		{
			get
			{
				return m_deductArea;
			}
			set
			{
				m_deductArea = value;
			}
		}

		public byte JoinCondition
		{
			get
			{
				return m_joinCondition;
			}
			set
			{
				m_joinCondition = value;
			}
		}

		public byte MemberOrder
		{
			get
			{
				return m_memberOrder;
			}
			set
			{
				m_memberOrder = value;
			}
		}

		public int Experience
		{
			get
			{
				return m_experience;
			}
			set
			{
				m_experience = value;
			}
		}

		public int FromMatchID
		{
			get
			{
				return m_fromMatchID;
			}
			set
			{
				m_fromMatchID = value;
			}
		}

		public byte FilterType
		{
			get
			{
				return m_filterType;
			}
			set
			{
				m_filterType = value;
			}
		}

		public short MaxRankID
		{
			get
			{
				return m_maxRankID;
			}
			set
			{
				m_maxRankID = value;
			}
		}

		public DateTime MatchEndDate
		{
			get
			{
				return m_matchEndDate;
			}
			set
			{
				m_matchEndDate = value;
			}
		}

		public DateTime MatchStartDate
		{
			get
			{
				return m_matchStartDate;
			}
			set
			{
				m_matchStartDate = value;
			}
		}

		public byte RankingMode
		{
			get
			{
				return m_rankingMode;
			}
			set
			{
				m_rankingMode = value;
			}
		}

		public short CountInnings
		{
			get
			{
				return m_countInnings;
			}
			set
			{
				m_countInnings = value;
			}
		}

		public byte FilterGradesMode
		{
			get
			{
				return m_filterGradesMode;
			}
			set
			{
				m_filterGradesMode = value;
			}
		}

		public byte DistributeRule
		{
			get
			{
				return m_distributeRule;
			}
			set
			{
				m_distributeRule = value;
			}
		}

		public short MinDistributeUser
		{
			get
			{
				return m_minDistributeUser;
			}
			set
			{
				m_minDistributeUser = value;
			}
		}

		public short DistributeTimeSpace
		{
			get
			{
				return m_distributeTimeSpace;
			}
			set
			{
				m_distributeTimeSpace = value;
			}
		}

		public short MinPartakeGameUser
		{
			get
			{
				return m_minPartakeGameUser;
			}
			set
			{
				m_minPartakeGameUser = value;
			}
		}

		public short MaxPartakeGameUser
		{
			get
			{
				return m_maxPartakeGameUser;
			}
			set
			{
				m_maxPartakeGameUser = value;
			}
		}

		public string MatchRule
		{
			get
			{
				return m_matchRule;
			}
			set
			{
				m_matchRule = value;
			}
		}

		public string ServiceMachine
		{
			get
			{
				return m_serviceMachine;
			}
			set
			{
				m_serviceMachine = value;
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

		public long MatchFee
		{
			get;
			set;
		}

		public MatchPublic()
		{
			m_matchID = 0;
			m_matchStatus = 0;
			m_kindID = 0;
			m_matchName = "";
			m_matchType = 0;
			m_signupMode = 0;
			m_feeType = 0;
			m_signupFee = 0L;
			m_deductArea = 0;
			m_joinCondition = 0;
			m_memberOrder = 0;
			m_experience = 0;
			m_fromMatchID = 0;
			m_filterType = 0;
			m_maxRankID = 0;
			m_matchEndDate = DateTime.Now;
			m_matchStartDate = DateTime.Now;
			m_rankingMode = 0;
			m_countInnings = 0;
			m_filterGradesMode = 0;
			m_distributeRule = 0;
			m_minDistributeUser = 0;
			m_distributeTimeSpace = 0;
			m_minPartakeGameUser = 0;
			m_maxPartakeGameUser = 0;
			m_matchRule = "";
			m_serviceMachine = "";
			m_nullity = false;
			m_collectDate = DateTime.Now;
		}
	}
}
