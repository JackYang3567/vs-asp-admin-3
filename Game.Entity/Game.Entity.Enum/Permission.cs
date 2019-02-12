using Game.Utils;
using System;

namespace Game.Entity.Enum
{
	[Serializable]
	[EnumDescription("管理员操作权限")]
	public enum Permission
	{
		[EnumDescription("查看权限")]
		Read = 1,
		[EnumDescription("添加权限")]
		Add = 2,
		[EnumDescription("编辑权限")]
		Edit = 4,
		[EnumDescription("删除权限")]
		Delete = 8,
		[EnumDescription("赠送会员权限")]
		GrantMember = 0x10,
		[EnumDescription("赠送金币权限")]
		GrantTreasure = 0x20,
		[EnumDescription("赠送经验权限")]
		GrantExperience = 0x40,
		[EnumDescription("赠送积分权限")]
		GrantScore = 0x80,
		[EnumDescription("赠送靓号权限")]
		GrantGameID = 0x100,
		[EnumDescription("清零积分权限")]
		ClearScore = 0x200,
		[EnumDescription("清零逃率权限")]
		ClearFlee = 0x400,
		[EnumDescription("生成实卡权限")]
		CreateCard = 0x800,
		[EnumDescription("导出实卡权限")]
		ExportCard = 0x1000,
		[EnumDescription("冻/解帐号权限 ")]
		Enable = 0x2000,
		[EnumDescription("设置/取消机器人")]
		IsRobot = 0x4000,
		[EnumDescription("启动/禁用")]
		IsNulity = 0x8000,
		[EnumDescription("订单操作")]
		OrderOperating = 0x10000,
		[EnumDescription("发送邮件")]
		SendMail = 0x20000
	}
}
