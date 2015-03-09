--2014年6月13日 曹贝 Modify 将满约清贷的订单中空白账单设置为搁置

--订单提前清贷3, 坏帐清贷4，满约清贷2
UPDATE
    Business
SET 
    CLoanStatus = CASE WHEN EXISTS ( SELECT
                                        bl.BusinessID
                                     FROM
                                        Bill bl
                                     WHERE
                                        bl.BusinessID = bs.BusinessID
                                        AND bl.BillType = 4
                                        AND bl.BillStatus = 3 ) THEN 4
                       WHEN ( SELECT
                                COUNT(*)
                              FROM
                                Bill bl
                              WHERE
                                bl.BusinessID = bs.BusinessID
                                AND bl.BillType IN ( 1, 2 )
                                AND bl.BillStatus = 3
                            ) >= bs.LoanPeriod THEN 2
                       ELSE 3
                  END ,
    ClearLoanTime = ( SELECT
                        MAX(xr.ReceivedTime)
                      FROM
                        Received xr
                        JOIN Bill xbl ON xr.BillID = xbl.BillID
                      WHERE
                        xbl.BusinessID = bs.BusinessID
                    ) ,
    IsRepayment = 0
FROM
    Business bs
WHERE
    bs.ResidualCapital < 1
    AND bs.ResidualCapital > -1
    AND bs.OverAmount = 0
    AND bs.CurrentOverAmount = 0
    AND bs.OtherAmount = 0
    AND bs.CLoanStatus <> 8
    AND IsRepayment = 1
    AND bs.ServiceSideKey IN ( {0} )
    AND bs.DSeqType = 1

--删除提前清贷成功订单的搁置科目
DELETE
    BillItem
FROM
    Business bs
    JOIN Bill bl ON bs.BusinessID = bl.BusinessID
    JOIN BillItem bi ON bl.BillID = bi.BillID
WHERE
    IsRepayment = 0
    AND bs.DSeqType = 1
    AND bs.ServiceSideKey IN ( {0} )
    AND bi.IsShelve = 1
    AND bs.ClearLoanTime >= CONVERT(DATE, GETDATE())

--删除提前清贷成功订单的搁置帐单
DELETE
    Bill
FROM
    Business bs
    JOIN Bill bl ON bs.BusinessID = bl.BusinessID
WHERE
    IsRepayment = 0
    AND bs.DSeqType = 1
    AND bs.ServiceSideKey IN ( {0} )
    AND bl.IsShelve = 1
    AND bs.ClearLoanTime >= CONVERT(DATE, GETDATE())

--满约清贷的订单,将空白账单设置为搁置
UPDATE dbo.Bill
SET IsShelve = 1
WHERE BillID IN (
    SELECT bl.BillID FROM dbo.Business bs
    JOIN dbo.Bill bl
    ON bs.BusinessID = bl.BusinessID
    WHERE 
    bs.CLoanStatus = 2 
    AND IsRepayment = 0
    AND bl.IsShelve = 0
    AND bs.ServiceSideKey IN ( {0} )
    AND NOT EXISTS (SELECT 1 FROM dbo.BillItem WHERE BillID = bl.BillID)
    GROUP BY bl.BillID
)

--设置当前清贷订单的催收单CancelTime为下一账单日
UPDATE
    dun
SET 
    dun.IsCurrent = 0 ,
    dun.CancelTime = CASE bus.PeriodType
                       WHEN 13
                       THEN CONVERT(NVARCHAR(7), DATEADD(MONTH, 1,
                                                         dun.CreateTime), 121)
                            + '-21'
                       WHEN 32 THEN CONVERT(NVARCHAR(10), DATEADD(MONTH, 1,dun.CreateTime), 121)
                     END
FROM
    dbo.Business bus
    JOIN dbo.Dun dun ON dun.BusinessID = bus.BusinessID
WHERE
    bus.IsRepayment = 0
    AND dun.CancelTime IS NULL
    AND bus.ServiceSideKey IN ( {0} )




