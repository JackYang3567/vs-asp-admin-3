using System;

namespace Game.Entity.PlatformManager
{
	[Serializable]
	public class Base_UserRole
	{
		public const string Tablename = "Base_UserRole";

		public const string _UserID = "UserID";

		public const string _RoleID = "RoleID";

		private int m_userID;

		private int m_roleID;

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

		public Base_UserRole()
		{
			m_userID = 0;
			m_roleID = 0;
		}
	}
}
