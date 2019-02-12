using Game.Utils;
using System;
using System.Collections.Generic;

namespace Game.Entity.Enum
{
	public class IssueAreaHelper
	{
		public static string GetIssueAreaDes(IssueArea status)
		{
			return EnumDescription.GetFieldText(status);
		}

		public static IList<EnumDescription> GetIssueAreaList(Type t)
		{
			return EnumDescription.GetFieldTexts(t);
		}
	}
}
