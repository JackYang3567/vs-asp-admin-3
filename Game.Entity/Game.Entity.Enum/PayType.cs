using Game.Utils;
using System;

namespace Game.Entity.Enum
{
	[Serializable]
	[EnumDescription("充值渠道")]
	public enum PayType
	{
		[EnumDescription("微信-吉付宝")]
		WXJFB = 1,
		[EnumDescription("QQ-吉付宝")]
		QQJFB = 2,
		[EnumDescription("快捷支付-合易付")]
		KJHYF = 4,
		[EnumDescription("微信-天下付")]
		WXTXF = 8,
		[EnumDescription("支付宝-天下付")]
		ZFBTXF = 0x10,
		[EnumDescription("QQ-天下付")]
		QQTXF = 0x20,
		[EnumDescription("微信-千亿付")]
		WXQYF = 0x40,
		[EnumDescription("微信扫码-千亿付")]
		WXSMQYF = 0x80,
		[EnumDescription("支付宝-千亿付")]
		ZFBQYF = 0x100,
		[EnumDescription("支付宝扫码-千亿付")]
		ZFBSM = 0x200,
		[EnumDescription("QQ-千亿付")]
		QQQYF = 0x400,
		[EnumDescription("QQ扫码-千亿付")]
		QQSMQYF = 0x800,
		[EnumDescription("快捷支付-千亿付")]
		KJQYF = 0x1000,
		[EnumDescription("微信-多得宝")]
		WXDDB = 0x2000,
		[EnumDescription("支付宝-多得宝")]
		ZFBDDB = 0x4000,
		[EnumDescription("QQ-多得宝")]
		QQDDB = 0x8000
	}
}
