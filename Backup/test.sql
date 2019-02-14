USE [RYTreasureDB]
GO

INSERT INTO [dbo].[ShareDetailInfo]
           ([OperUserID]
           ,[ShareID]
           ,[UserID]
           ,[GameID]
           ,[Accounts]
           ,[CardTypeID]
           ,[SerialID]
           ,[OrderID]
           ,[OrderAmount]
           ,[DiscountScale]
           ,[PayAmount]
           ,[Currency]
           ,[BeforeCurrency]
           ,[IPAddress]
           ,[ApplyDate]
           ,[Gold]
           ,[BeforeGold]
           ,[RoomCard]
           ,[BeforeRoomCard]
           ,[AUShow])
     VALUES
           (14
           ,99999  -- 99999 :线下充值，4：支付宝扫码 5：微信扫码
           ,119673
           ,335013
           ,'test12345'
           ,0
           ,''
           ,''
           ,0.00
           ,1.00
           ,500
           ,0.00
           ,0.00
           ,''
           ,'2019-02-13 09:22:10.830'
           ,500
           ,0.00
           ,0
           ,0
           ,'True')
GO


-- =====
USE [RYTreasureDB]
GO
select * from GameScoreInfo 
--UPDATE [dbo].[GameScoreInfo]
--   SET [Score] = 20098.90
      
WHERE UserID = 119673
--GO


--=====
USE [RYTreasureDB]
GO

/****** Object:  View [dbo].[View_OffLinePayOrders]   Script Date: 2019/2/13 16:49:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[View_OffLinePayOrders]  
AS  
SELECT o.OffLinePayID, o.Accounts, o.OrderID,o.PayAmount,   
    o.ApplyDate,o.PaymentType, o.BankName, o.IsAuded,	 
    a.UserID,a.GameID
FROM dbo.OffLinePayOrders AS o WITH(NOLOCK)  
LEFT JOIN RYAccountsDB.dbo.AccountsInfo AS a WITH(NOLOCK) ON o.Accounts = a.Accounts   

GO



