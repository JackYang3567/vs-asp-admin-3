using Game.Utils;
using System;

namespace Game.Entity.Enum
{
	[Serializable]
	[EnumDescription("道具使用范围")]
	public enum IssueArea
	{
		[EnumDescription("大厅")]
		A_MALL = 1,
		[EnumDescription("房间")]
		A_GAME = 2,
		[EnumDescription("游戏")]
		A_GAMEROOM = 4
	}
}
