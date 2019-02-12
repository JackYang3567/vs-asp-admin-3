using System;

namespace Game.Entity.PlatformManager
{
	[Serializable]
	public class Base_Module
	{
		public const string Tablename = "Base_Module";

		public const string _ModuleID = "ModuleID";

		public const string _ParentID = "ParentID";

		public const string _Title = "Title";

		public const string _Link = "Link";

		public const string _OrderNo = "OrderNo";

		public const string _Nullity = "Nullity";

		public const string _IsMenu = "IsMenu";

		public const string _Description = "Description";

		public const string _ManagerPopedom = "ManagerPopedom";

		private int m_moduleID;

		private int m_parentID;

		private string m_title;

		private string m_link;

		private int m_orderNo;

		private bool m_nullity;

		private bool m_isMenu;

		private string m_description;

		private int m_managerPopedom;

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

		public string Title
		{
			get
			{
				return m_title;
			}
			set
			{
				m_title = value;
			}
		}

		public string Link
		{
			get
			{
				return m_link;
			}
			set
			{
				m_link = value;
			}
		}

		public int OrderNo
		{
			get
			{
				return m_orderNo;
			}
			set
			{
				m_orderNo = value;
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

		public bool IsMenu
		{
			get
			{
				return m_isMenu;
			}
			set
			{
				m_isMenu = value;
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

		public int ManagerPopedom
		{
			get
			{
				return m_managerPopedom;
			}
			set
			{
				m_managerPopedom = value;
			}
		}

		public Base_Module()
		{
			m_moduleID = 0;
			m_parentID = 0;
			m_title = "";
			m_link = "";
			m_orderNo = 0;
			m_nullity = false;
			m_isMenu = false;
			m_description = "";
			m_managerPopedom = 0;
		}
	}
}
