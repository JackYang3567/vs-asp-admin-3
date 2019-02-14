
USE [RYTreasureDB]
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OffLinePayOrders', @level2type=N'COLUMN',@level2name=N'IsAuded'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OffLinePayOrders', @level2type=N'COLUMN',@level2name=N'BankName'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OffLinePayOrders', @level2type=N'COLUMN',@level2name=N'PaymentType'

GO

/****** Object:  Table [dbo].[OffLinePayOrders]    Script Date: 2019/1/23 15:12:33 ******/
DROP TABLE [dbo].[OffLinePayOrders]
GO

/****** Object:  Table [dbo].[OffLinePayOrders]    Script Date: 2019/1/23 15:12:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[OffLinePayOrders](
	[OffLinePayID] [int] IDENTITY(1,1) NOT NULL,
	[Accounts] [nvarchar](31) NOT NULL,
	[OrderID] [nvarchar](50) NOT NULL,
	[PayAmount] [decimal](18, 2) NOT NULL,
	[ApplyDate] [datetime] NOT NULL CONSTRAINT [DF_OffLinePayOrder_ApplyDate]  DEFAULT (getdate()),
	[PaymentType] [varchar](50) NOT NULL CONSTRAINT [DF_OffLinePayOrders_PaymentType]  DEFAULT ((0)),
	[BankName] [nvarchar](50) NULL CONSTRAINT [DF_OffLinePayOrders_BankName]  DEFAULT (N'“”'),
	[IsAuded] [int] NOT NULL CONSTRAINT [DF_OffLinePayOrders_IsAuded]  DEFAULT ((0)),
 CONSTRAINT [PK_OffLinePayOrder] PRIMARY KEY CLUSTERED 
(
	[OffLinePayID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：微信支付
1：支付宝支付
2：银行卡支付' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OffLinePayOrders', @level2type=N'COLUMN',@level2name=N'PaymentType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当PaymentType为2时 ，银行卡支付，记录银行名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OffLinePayOrders', @level2type=N'COLUMN',@level2name=N'BankName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: 审核未通过，1： 审核通过' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OffLinePayOrders', @level2type=N'COLUMN',@level2name=N'IsAuded'
GO


----


USE [RYPlatformManagerDB]
GO

INSERT INTO [dbo].[Base_Module]
           ([ModuleID]
           ,[ParentID]
           ,[Title]
           ,[Link]
           ,[OrderNo]
           ,[Nullity]
           ,[IsMenu]
           ,[Description]
           ,[ManagerPopedom])
     VALUES
           (215
           ,2
           ,'线下充值订单'
           ,'/Filled/OffLinePayList'
           ,56
           ,0
           ,1
           ,''
           ,0)
GO


---
INSERT INTO [dbo].Base_ModulePermission
           ([ModuleID]
           ,[PermissionTitle]
           ,[PermissionValue]
           ,[Nullity]
           ,[StateFlag]
           ,[ParentID])
     VALUES
           (215
           ,'查看'
           ,1
           ,0
           ,0
           ,2)
GO

--==========================

USE [RYTreasureDB]
GO

/****** Object:  Table [dbo].[OffLinePayQrCode]    Script Date: 2019/2/11 14:34:41 ******/
DROP TABLE [dbo].[OffLinePayQrCode]
GO

/****** Object:  Table [dbo].[OffLinePayQrCode]    Script Date: 2019/2/11 14:34:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OffLinePayQrCode](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PaymentName] [nvarchar](50) NOT NULL,
	[IconPath] [nvarchar](255) default '',
	[PaymentTypeID] [int] NOT NULL,
	[OwnerID] [int] NOT NULL
) ON [PRIMARY]

GO


USE [RYPlatformManagerDB]
GO


INSERT INTO [dbo].[Base_Module]
           ([ModuleID]
           ,[ParentID]
           ,[Title]
           ,[Link]
           ,[OrderNo]
           ,[Nullity]
           ,[IsMenu]
           ,[Description]
           ,[ManagerPopedom])
     VALUES
           (216
           ,2
           ,'线下充值二维码'
           ,'/Filled/OffLineQrCodeList'
           ,57
           ,0
           ,1
           ,''
           ,0)
GO


INSERT INTO [dbo].Base_ModulePermission
           ([ModuleID]
           ,[PermissionTitle]
           ,[PermissionValue]
           ,[Nullity]
           ,[StateFlag]
           ,[ParentID])
     VALUES
           (216
           ,'查看'
           ,1
           ,0
           ,0
           ,2)
GO







