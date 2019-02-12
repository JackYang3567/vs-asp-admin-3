using System;
using System.Collections.Generic;

namespace Admin.Models
{
	[Serializable]
	public class Base_ModuleSubInfo
	{
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

		public List<Base_ModulePermissionInfo> Base_ModulePermissionInfoes
		{
			get;
			set;
		}
	}
}
