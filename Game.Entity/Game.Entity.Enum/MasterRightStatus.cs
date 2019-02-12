using Game.Utils;
using System;

namespace Game.Entity.Enum
{
	[Serializable]
	[EnumDescription("管理权限")]
	public enum MasterRightStatus
	{
		[EnumDescription("允许禁止游戏")]
		UR_CAN_LIMIT_PLAY = 1,
		[EnumDescription("允许禁止旁观")]
		UR_CAN_LIMIT_LOOKON = 2,
		[EnumDescription("允许禁止私聊")]
		UR_CAN_LIMIT_WISPER = 4,
		[EnumDescription("允许禁止房间聊天")]
		UR_CAN_LIMIT_ROOM_CHAT = 8,
		[EnumDescription("允许禁止游戏聊天")]
		UR_CAN_LIMIT_GAME_CHAT = 0x10,
		[EnumDescription("允许踢出用户")]
		UR_CAN_KILL_USER = 0x100,
		[EnumDescription("允许解散游戏")]
		UR_CAN_DISMISS_GAME = 0x400,
		[EnumDescription("允许发布消息")]
		UR_CAN_ISSUE_MESSAGE = 0x1000000,
		[EnumDescription("允许管理房间")]
		UR_CAN_MANAGER_SERVER = 0x2000000,
		[EnumDescription("允许管理机器人")]
		UR_CAN_MANAGER_ANDROID = 0x8000000
	}
}
