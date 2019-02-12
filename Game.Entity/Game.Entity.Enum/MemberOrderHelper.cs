using Game.Utils;
using System;
using System.Collections.Generic;

namespace Game.Entity.Enum
{
	public class MemberOrderHelper
	{
		public static string GetMemberOrderStatusDes(MemberOrderStatus status)
		{
			return EnumDescription.GetFieldText(status);
		}

		public static IList<EnumDescription> GetMemberOrderStatusList(Type t)
		{
			return EnumDescription.GetFieldTexts(t);
		}
	}
}
