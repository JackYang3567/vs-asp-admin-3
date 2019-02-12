using Game.Utils;
using System;

namespace Game.Entity.Enum
{
	[Serializable]
	[EnumDescription("道具类型")]
	public enum GamePropertyKind
	{
		[EnumDescription("礼物")]
		KIND1 = 1,
		[EnumDescription("宝石")]
		KIND2,
		[EnumDescription("双卡")]
		KIND3,
		[EnumDescription("防身")]
		KIND4,
		[EnumDescription("防踢")]
		KIND5,
		[EnumDescription("vip")]
		KIND6,
		[EnumDescription("大喇叭")]
		KIND7,
		[EnumDescription("小喇叭")]
		KIND8,
		[EnumDescription("负分清零")]
		KIND9,
		[EnumDescription("逃跑")]
		KIND10,
		[EnumDescription("礼包")]
		KIND11,
		[EnumDescription("金币")]
		KIND12
	}
}
