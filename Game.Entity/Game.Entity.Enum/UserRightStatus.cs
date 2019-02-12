using Game.Utils;
using System;

namespace Game.Entity.Enum
{
	[Serializable]
	[EnumDescription("用户权限")]
	public enum UserRightStatus
	{
		[EnumDescription("不能进行游戏")]
		UR_CANNOT_PLAY = 1,
		[EnumDescription("不能旁观游戏")]
		UR_CANNOT_LOOKON = 2,
		[EnumDescription("不能发送私聊")]
		UR_CANNOT_WISPER = 4,
		[EnumDescription("不能大厅聊天")]
		UR_CANNOT_ROOM_CHAT = 8,
		[EnumDescription("不能游戏聊天")]
		UR_CANNOT_GAME_CHAT = 0x10,
		[EnumDescription("不能进行转账")]
		UR_CANNOT_TRANSFER_ACCOT = 0x40,
		[EnumDescription("游戏踢出用户")]
		UR_GAME_KICK_OUT_USER = 0x200,
		[EnumDescription("游戏比赛用户")]
		UR_GAME_MATCH_USER = 0x10000000,
		[EnumDescription("游戏作弊用户")]
		UR_GAME_ZUOBI_USER = 0x20000000
	}
}
