using Game.Utils;
using System;
using System.Collections.Generic;

namespace Game.Entity.Enum
{
	public class NoticeLocationHelper
	{
		public static string GetNoticeLocationDes(NoticeLocation status)
		{
			return EnumDescription.GetFieldText(status);
		}

		public static IList<EnumDescription> GetNoticeLocationList(Type t)
		{
			return EnumDescription.GetFieldTexts(t);
		}
	}
}
