using System;

namespace Game.Entity.PlatformManager
{
	[Serializable]
	public class Base_Roles
	{
		public const string Tablename = "Base_Roles";

		public const string _RoleID = "RoleID";

		public const string _RoleName = "RoleName";

		public const string _Description = "Description";

		private int m_roleID;

		private string m_roleName;

		private string m_description;

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

		public string RoleName
		{
			get
			{
				return m_roleName;
			}
			set
			{
				m_roleName = value;
			}
		}

		public string Description
		{
			get
			{
				return m_description;
			}
			set
			{
				m_description = value;
			}
		}

		public Base_Roles()
		{
			m_roleID = 0;
			m_roleName = "";
			m_description = "";
		}
	}
}
