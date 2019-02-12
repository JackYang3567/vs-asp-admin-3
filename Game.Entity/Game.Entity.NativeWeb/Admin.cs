using System;

namespace Game.Entity.NativeWeb
{
	[Serializable]
	public class Admin
	{
		public const string Tablename = "admin";

		public const string _UserID = "UserID";

		public const string _UserName = "UserName";

		public const string _Password = "Password";

		public const string _Classcode = "classcode";

		public const string _Classname = "classname";

		private int m_userID;

		private string m_userName;

		private string m_password;

		private string m_classcode;

		private string m_classname;

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

		public string UserName
		{
			get
			{
				return m_userName;
			}
			set
			{
				m_userName = value;
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

		public string Classcode
		{
			get
			{
				return m_classcode;
			}
			set
			{
				m_classcode = value;
			}
		}

		public string Classname
		{
			get
			{
				return m_classname;
			}
			set
			{
				m_classname = value;
			}
		}

		public Admin()
		{
			m_userID = 0;
			m_userName = "";
			m_password = "";
			m_classcode = "";
			m_classname = "";
		}
	}
}
