using System;

namespace Game.Entity.PlatformManager
{
	[Serializable]
	public class Base_Users
	{
		public const string Tablename = "Base_Users";

		public const string _UserID = "UserID";

		public const string _Username = "Username";

		public const string _Password = "Password";

		public const string _RoleID = "RoleID";

		public const string _Nullity = "Nullity";

		public const string _PreLogintime = "PreLogintime";

		public const string _PreLoginIP = "PreLoginIP";

		public const string _LastLogintime = "LastLogintime";

		public const string _LastLoginIP = "LastLoginIP";

		public const string _LoginTimes = "LoginTimes";

		public const string _IsBand = "IsBand";

		public const string _BandIP = "BandIP";

		public const string _IsAssist = "IsAssist";

		private int m_userID;

		private string m_username;

		private string m_password;

		private int m_roleID;

		private byte m_nullity;

		private DateTime m_preLogintime;

		private string m_preLoginIP;

		private DateTime m_lastLogintime;

		private string m_lastLoginIP;

		private int m_loginTimes;

		private int m_isBand;

		private string m_bandIP;

		private byte m_isAssist;

		private int moudleId;

		public int MoudleID
		{
			get
			{
				return moudleId;
			}
			set
			{
				moudleId = value;
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

		public string Username
		{
			get
			{
				return m_username;
			}
			set
			{
				m_username = value;
			}
		}

		public string Password
		{
			get
			{
				return m_password;
			}
			set
			{
				m_password = value;
			}
		}

		public int RoleID
		{
			get
			{
				return m_roleID;
			}
			set
			{
				m_roleID = value;
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

		public DateTime PreLogintime
		{
			get
			{
				return m_preLogintime;
			}
			set
			{
				m_preLogintime = value;
			}
		}

		public string PreLoginIP
		{
			get
			{
				return m_preLoginIP;
			}
			set
			{
				m_preLoginIP = value;
			}
		}

		public DateTime LastLogintime
		{
			get
			{
				return m_lastLogintime;
			}
			set
			{
				m_lastLogintime = value;
			}
		}

		public string LastLoginIP
		{
			get
			{
				return m_lastLoginIP;
			}
			set
			{
				m_lastLoginIP = value;
			}
		}

		public int LoginTimes
		{
			get
			{
				return m_loginTimes;
			}
			set
			{
				m_loginTimes = value;
			}
		}

		public int IsBand
		{
			get
			{
				return m_isBand;
			}
			set
			{
				m_isBand = value;
			}
		}

		public string BandIP
		{
			get
			{
				return m_bandIP;
			}
			set
			{
				m_bandIP = value;
			}
		}

		public byte IsAssist
		{
			get
			{
				return m_isAssist;
			}
			set
			{
				m_isAssist = value;
			}
		}

		public string MobilePhone
		{
			get;
			set;
		}

		public bool IsMobileNeed
		{
			get;
			set;
		}

		public Base_Users()
		{
			m_userID = 0;
			m_username = "";
			m_password = "";
			m_roleID = 0;
			m_nullity = 0;
			m_preLogintime = DateTime.Now;
			m_preLoginIP = "";
			m_lastLogintime = DateTime.Now;
			m_lastLoginIP = "";
			m_loginTimes = 0;
			m_isBand = 0;
			m_bandIP = "";
			m_isAssist = 0;
			moudleId = 0;
		}
	}
}
