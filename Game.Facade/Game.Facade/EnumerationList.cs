using Game.Utils;
using System;
using System.ComponentModel;

namespace Game.Facade
{
	public class EnumerationList
	{
		[Serializable]
		public enum MallNeedInfo
		{
			[EnumDescription("真实姓名")]
			Compellation = 1,
			[EnumDescription("手机号码")]
			PhoneNumber = 2,
			[EnumDescription("QQ|微信")]
			QQNumber = 4,
			[EnumDescription("收货地址及邮编")]
			AdressAndCode = 8
		}

		public enum AwardOrderStatus
		{
			处理中,
			已发货,
			已收货,
			申请退货,
			同意退货等待客户发货,
			拒绝退货,
			退货成功
		}

		public enum TaskType
		{
			[Description("总赢局")]
			总赢局 = 1,
			[Description("总局数")]
			总局数 = 2,
			[Description("首胜")]
			首胜 = 4,
			[Description("连赢局数")]
			连赢局数 = 8
		}

		public enum SystemStatusKey
		{
			WinExperience
		}

		public enum SiteConfigKey
		{
			[EnumDescription("联系方式配置")]
			ContactConfig,
			[EnumDescription("站点配置")]
			SiteConfig,
			[EnumDescription("大厅整包配置号码")]
			GameFullPackageConfig,
			[EnumDescription("大厅简包配置")]
			GameJanePackageConfig,
			[EnumDescription("邮箱配置")]
			EmailConfig
		}

		public enum SpreadTypes
		{
			注册 = 1,
			游戏时长,
			充值,
			结算
		}

		public enum ClearTableTypes
		{
			玩家进出记录表 = 1,
			游戏记录总局表,
			游戏记录详情表,
			银行操作记录表
		}

		public enum MatchType
		{
			定时赛,
			即时赛
		}

		public enum MatchFeeType
		{
			金币,
			元宝
		}

		public enum UploadFileEnum
		{
			[Description("比赛图片")]
			MatchImg = 1,

			[Description("编辑器图片")]
			EditImg,

			[Description("移动端新闻图片")]
			MobileImg,

			[Description("pc端新闻图片")]
			PcNewsImg,

			[Description("规则图片")]
			RulesImg,

			[Description("活动图片")]
			ActivityImg,

			[Description("网站Logo图片")]
            SiteLogoImg,

			[Description("网站后台LOGO图片")]
			SiteAdminlogoImg,

			[Description("移动版网站LOGO")]
			SiteMobileLogoImg,

			[Description("移动版注册网站LOGO图片")]
			SiteMobileRegLogoImg,

            [Description("代理商收款二维码图片")]
            OffLinePayQrCodeImg
            
		}

		public enum MemberType
		{
			普通会员
		}
	}
}
