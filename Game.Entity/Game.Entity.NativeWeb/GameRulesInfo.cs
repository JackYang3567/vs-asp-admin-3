using System;

namespace Game.Entity.NativeWeb
{
	[Serializable]
	public class GameRulesInfo
	{
		public const string Tablename = "GameRulesInfo";

		public const string _KindID = "KindID";

		public const string _KindName = "KindName";

		public const string _ThumbnailUrl = "ThumbnailUrl";

		public const string _ImgRuleUrl = "ImgRuleUrl";

		public const string _MobileImgUrl = "MobileImgUrl";

		public const string _MobileSize = "MobileSize";

		public const string _MobileDate = "MobileDate";

		public const string _MobileVersion = "MobileVersion";

		public const string _MobileGameType = "MobileGameType";

		public const string _AndroidDownloadUrl = "AndroidDownloadUrl";

		public const string _IOSDownloadUrl = "IOSDownloadUrl";

		public const string _HelpIntro = "HelpIntro";

		public const string _HelpRule = "HelpRule";

		public const string _HelpGrade = "HelpGrade";

		public const string _JoinIntro = "JoinIntro";

		public const string _Nullity = "Nullity";

		public const string _CollectDate = "CollectDate";

		public const string _ModifyDate = "ModifyDate";

		private int m_kindID;

		private string m_kindName;

		private string m_thumbnailUrl;

		private string m_imgRuleUrl;

		private string m_mobileImgUrl;

		private string m_mobileSize;

		private string m_mobileDate;

		private string m_mobileVersion;

		private byte m_mobileGameType;

		private string m_androidDownloadUrl;

		private string m_iOSDownloadUrl;

		private string m_helpIntro;

		private string m_helpRule;

		private string m_helpGrade;

		private byte m_joinIntro;

		private byte m_nullity;

		private DateTime m_collectDate;

		private DateTime m_modifyDate;

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

		public string KindName
		{
			get
			{
				return m_kindName;
			}
			set
			{
				m_kindName = value;
			}
		}

		public string ThumbnailUrl
		{
			get
			{
				return m_thumbnailUrl;
			}
			set
			{
				m_thumbnailUrl = value;
			}
		}

		public string ImgRuleUrl
		{
			get
			{
				return m_imgRuleUrl;
			}
			set
			{
				m_imgRuleUrl = value;
			}
		}

		public string MobileImgUrl
		{
			get
			{
				return m_mobileImgUrl;
			}
			set
			{
				m_mobileImgUrl = value;
			}
		}

		public string MobileSize
		{
			get
			{
				return m_mobileSize;
			}
			set
			{
				m_mobileSize = value;
			}
		}

		public string MobileDate
		{
			get
			{
				return m_mobileDate;
			}
			set
			{
				m_mobileDate = value;
			}
		}

		public string MobileVersion
		{
			get
			{
				return m_mobileVersion;
			}
			set
			{
				m_mobileVersion = value;
			}
		}

		public byte MobileGameType
		{
			get
			{
				return m_mobileGameType;
			}
			set
			{
				m_mobileGameType = value;
			}
		}

		public string AndroidDownloadUrl
		{
			get
			{
				return m_androidDownloadUrl;
			}
			set
			{
				m_androidDownloadUrl = value;
			}
		}

		public string IOSDownloadUrl
		{
			get
			{
				return m_iOSDownloadUrl;
			}
			set
			{
				m_iOSDownloadUrl = value;
			}
		}

		public string HelpIntro
		{
			get
			{
				return m_helpIntro;
			}
			set
			{
				m_helpIntro = value;
			}
		}

		public string HelpRule
		{
			get
			{
				return m_helpRule;
			}
			set
			{
				m_helpRule = value;
			}
		}

		public string HelpGrade
		{
			get
			{
				return m_helpGrade;
			}
			set
			{
				m_helpGrade = value;
			}
		}

		public byte JoinIntro
		{
			get
			{
				return m_joinIntro;
			}
			set
			{
				m_joinIntro = value;
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

		public GameRulesInfo()
		{
			m_kindID = 0;
			m_kindName = "";
			m_thumbnailUrl = "";
			m_imgRuleUrl = "";
			m_mobileImgUrl = "";
			m_mobileSize = "";
			m_mobileDate = "";
			m_mobileVersion = "";
			m_mobileGameType = 0;
			m_androidDownloadUrl = "";
			m_iOSDownloadUrl = "";
			m_helpIntro = "";
			m_helpRule = "";
			m_helpGrade = "";
			m_joinIntro = 0;
			m_nullity = 0;
			m_collectDate = DateTime.Now;
			m_modifyDate = DateTime.Now;
		}
	}
}
