using Game.Utils;
using System.Collections.Generic;

namespace Game.Entity.Enum
{
	public class PermissionHelper
	{
		public static string GetPermissionDes(Permission permission)
		{
			return EnumDescription.GetFieldText(permission);
		}

		public static IList<EnumDescription> GetPermissionList()
		{
			return EnumDescription.GetFieldTexts(typeof(Permission));
		}
	}
}
