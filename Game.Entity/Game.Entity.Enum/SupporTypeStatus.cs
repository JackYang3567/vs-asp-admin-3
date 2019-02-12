using Game.Utils;
using System;

namespace Game.Entity.Enum
{
	[Serializable]
	[EnumDescription("支持类型")]
	public enum SupporTypeStatus
	{
		[EnumDescription("金币类型")]
		SP_TREASURE = 1,
		[EnumDescription("点值类型")]
		SP_SCORE = 2,
		[EnumDescription("定时比赛")]
		SP_Timing_Match = 4,
		[EnumDescription("训练类型")]
		SP_EXERCISE = 8,
		[EnumDescription("即时比赛")]
		SP_Immediately_Match = 0x10
	}
}
