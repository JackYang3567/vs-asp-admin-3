using System;

namespace Game.Entity.PlatformManager
{
	[Serializable]
	public class Base_RolePermission
	{
		public const string Tablename = "Base_RolePermission";

		public const string _RoleID = "RoleID";

		public const string _ModuleID = "ModuleID";

		public const string _ManagerPermission = "ManagerPermission";

		public const string _OperationPermission = "OperationPermission";

		public const string _StateFlag = "StateFlag";

		private int m_roleID;

		private int m_moduleID;

		private long m_managerPermission;

		private long m_operationPermission;

		private int m_stateFlag;

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

		public long ManagerPermission
		{
			get
			{
				return m_managerPermission;
			}
			set
			{
				m_managerPermission = value;
			}
		}

		public long OperationPermission
		{
			get
			{
				return m_operationPermission;
			}
			set
			{
				m_operationPermission = value;
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

		public Base_RolePermission()
		{
			m_roleID = 0;
			m_moduleID = 0;
			m_managerPermission = 0L;
			m_operationPermission = 0L;
			m_stateFlag = 0;
		}
	}
}
