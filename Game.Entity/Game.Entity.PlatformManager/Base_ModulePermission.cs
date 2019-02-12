using System;

namespace Game.Entity.PlatformManager
{
	[Serializable]
	public class Base_ModulePermission
	{
		public const string Tablename = "Base_ModulePermission";

		public const string _ModuleID = "ModuleID";

		public const string _PermissionTitle = "PermissionTitle";

		public const string _PermissionValue = "PermissionValue";

		public const string _Nullity = "Nullity";

		public const string _StateFlag = "StateFlag";

		public const string _ParentID = "ParentID";

		private int m_moduleID;

		private string m_permissionTitle;

		private long m_permissionValue;

		private byte m_nullity;

		private int m_stateFlag;

		private int m_parentID;

		public int ModuleID
		{
			get
			{
				return m_moduleID;
			}
			set
			{
				m_moduleID = value;
			}
		}

		public string PermissionTitle
		{
			get
			{
				return m_permissionTitle;
			}
			set
			{
				m_permissionTitle = value;
			}
		}

		public long PermissionValue
		{
			get
			{
				return m_permissionValue;
			}
			set
			{
				m_permissionValue = value;
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

		public int StateFlag
		{
			get
			{
				return m_stateFlag;
			}
			set
			{
				m_stateFlag = value;
			}
		}

		public int ParentID
		{
			get
			{
				return m_parentID;
			}
			set
			{
				m_parentID = value;
			}
		}

		public Base_ModulePermission()
		{
			m_moduleID = 0;
			m_permissionTitle = "";
			m_permissionValue = 0L;
			m_nullity = 0;
			m_stateFlag = 0;
			m_parentID = 0;
		}
	}
}
