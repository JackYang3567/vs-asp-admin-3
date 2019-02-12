using System;

namespace Game.Entity.PlatformManager
{
	[Serializable]
	public class ModulePage
	{
		private int m_moduleID;

		private string m_moduleName;

		private string m_pageName;

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

		public string ModuleName
		{
			get
			{
				return m_moduleName;
			}
			set
			{
				m_moduleName = value;
			}
		}

		public string PageName
		{
			get
			{
				return m_pageName;
			}
			set
			{
				m_pageName = value;
			}
		}

		public ModulePage()
		{
		}

		public ModulePage(int moduleID, string moduleName, string pageName)
		{
			m_moduleID = moduleID;
			m_moduleName = moduleName;
			m_pageName = pageName;
		}
	}
}
