using System;

namespace Game.Utils
{
	[Flags]
	public enum FilterType
	{
		Script = 0x1,
		Html = 0x2,
		Object = 0x3,
		AHrefScript = 0x4,
		Iframe = 0x5,
		Frameset = 0x6,
		Src = 0x7,
		BadWords = 0x8,
		Sql = 0x9,
		All = 0x10
	}
}
