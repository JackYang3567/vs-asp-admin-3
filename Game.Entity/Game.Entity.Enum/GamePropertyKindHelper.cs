using Game.Utils;
using System;
using System.Collections.Generic;

namespace Game.Entity.Enum
{
	public class GamePropertyKindHelper
	{
		public static string GetGamePropertyKindDes(GamePropertyKind kind)
		{
			return EnumDescription.GetFieldText(kind);
		}

		public static IList<EnumDescription> GetGamePropertyKindList(Type t)
		{
			return EnumDescription.GetFieldTexts(t);
		}
	}
}
