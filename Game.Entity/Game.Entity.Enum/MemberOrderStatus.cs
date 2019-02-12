using Game.Utils;
using System;

namespace Game.Entity.Enum
{
	[Serializable]
	[EnumDescription("会员等级")]
	public enum MemberOrderStatus
	{
		[EnumDescription("普通玩家")]
		普通玩家,
		[EnumDescription("VIP1")]
		VIP1,
		[EnumDescription("VIP2")]
		VIP2,
		[EnumDescription("VIP3")]
		VIP3,
		[EnumDescription("VIP4")]
		VIP4,
		[EnumDescription("VIP5")]
		VIP5
	}
}
