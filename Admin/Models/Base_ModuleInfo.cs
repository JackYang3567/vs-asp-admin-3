using System;
using System.Collections.Generic;

namespace Admin.Models
{
	[Serializable]
	public class Base_ModuleInfo
	{
		public int ParentID
		{
			get;
			set;
		}

		public int ModuleID
		{
			get;
			set;
		}

		public string Title
		{
			get;
			set;
		}

		public List<Base_ModuleSubInfo> Base_ModuleSubInfoes
		{
			get;
			set;
		}
	}
}
