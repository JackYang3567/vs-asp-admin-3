using Game.Utils;
using System;

namespace Game.Entity.Enum
{
	[Serializable]
	[EnumDescription("道具作用范围")]
	public enum ServiceArea
	{
		[EnumDescription("自己")]
		A_MYSELF = 1,
		[EnumDescription("玩家")]
		A_PLAYER = 2,
		[EnumDescription("旁观")]
		A_LOOKER = 4
	}
}
