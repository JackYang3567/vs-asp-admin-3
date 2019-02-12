using System;

namespace Admin.Models
{
	[Serializable]
	public class Base_ModulePermissionInfo
	{
		public int PermissionValue
		{
			get;
			set;
		}

		public string PermissionTitle
		{
			get;
			set;
		}

		public bool IsChecked
		{
			get;
			set;
		}
	}
}
