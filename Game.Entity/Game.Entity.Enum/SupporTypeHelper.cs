using Game.Utils;
using System;
using System.Collections.Generic;

namespace Game.Entity.Enum
{
	public class SupporTypeHelper
	{
		public static string GetSupporTypeDes(SupporTypeStatus status)
		{
			return EnumDescription.GetFieldText(status);
		}

		public static IList<EnumDescription> GetSupporTypeList(Type t)
		{
			return EnumDescription.GetFieldTexts(t);
		}

		public static bool IsExists(int supporType, SupporTypeStatus status)
		{
			return (supporType & (int)status) == 0;
		}
	}
}
