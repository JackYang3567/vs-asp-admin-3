using Game.Utils;
using System;
using System.Collections.Generic;

namespace Game.Entity.Enum
{
	public class ServiceAreaHelper
	{
		public static string GetServiceAreaDes(ServiceArea status)
		{
			return EnumDescription.GetFieldText(status);
		}

		public static IList<EnumDescription> GetServiceAreaList(Type t)
		{
			return EnumDescription.GetFieldTexts(t);
		}
	}
}
