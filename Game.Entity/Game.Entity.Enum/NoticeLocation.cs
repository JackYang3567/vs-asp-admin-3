using Game.Utils;
using System;

namespace Game.Entity.Enum
{
	[Serializable]
	[EnumDescription("公告范围")]
	public enum NoticeLocation
	{
		[EnumDescription("首页")]
		A_Index,
		[EnumDescription("新闻公告")]
		A_Notice,
		[EnumDescription("会员中心")]
		A_Member,
		[EnumDescription("充值中心")]
		A_Pay,
		[EnumDescription("比赛中心")]
		A_Match,
		[EnumDescription("游戏商城")]
		A_Mall,
		[EnumDescription("推广系统")]
		A_Spread,
		[EnumDescription("客服中心")]
		A_Server
	}
}
