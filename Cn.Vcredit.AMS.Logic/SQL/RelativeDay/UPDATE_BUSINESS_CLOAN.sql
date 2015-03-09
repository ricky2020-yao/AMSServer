--订单提前清贷3, 坏帐清贷4，满约清贷2
UPDATE Business SET CLoanStatus =
	CASE WHEN EXISTS(
		SELECT bl.BusinessID
		FROM Bill bl 
		WHERE bl.BusinessID = bs.BusinessID
		AND bl.BillType IN (3, 8) 
		AND bl.BillStatus = 3
	) THEN 3 WHEN EXISTS(
		SELECT bl.BusinessID
		FROM Bill bl 
		WHERE bl.BusinessID = bs.BusinessID
		AND bl.BillType = 4 
		AND bl.BillStatus = 3
	) THEN 4
	  ELSE 2 END,
	ClearLoanTime = (
		SELECT MAX(xr.ReceivedTime) 
		FROM Received xr
		JOIN Bill xbl ON xr.BillID = xbl.BillID
		WHERE xbl.BusinessID = bs.BusinessID
	), IsRepayment = 0
FROM Business bs
WHERE bs.ResidualCapital < 1 AND bs.ResidualCapital > -1
AND bs.OverAmount = 0 
AND bs.CurrentOverAmount = 0 AND bs.OtherAmount = 0
AND bs.CLoanStatus <> 8 AND IsRepayment = 1
AND bs.PeriodType = 32

--删除提前清贷成功订单的搁置科目
DELETE BillItem
FROM Business bs
JOIN Bill bl ON bs.BusinessID = bl.BusinessID
JOIN BillItem bi ON bl.BillID = bi.BillID
WHERE bs.ResidualCapital < 1 AND bs.ResidualCapital >= 0 
AND bs.OverAmount = 0 
AND bs.CurrentOverAmount = 0 AND bs.OtherAmount = 0
AND bs.CLoanStatus <> 8 AND IsRepayment = 1
AND bs.PeriodType = 32 
AND bi.IsShelve = 1

--删除提前清贷成功订单的搁置帐单
DELETE Bill
FROM Business bs
JOIN Bill bl ON bs.BusinessID = bl.BusinessID
WHERE bs.ResidualCapital < 1 AND bs.ResidualCapital >= 0 
AND bs.OverAmount = 0 
AND bs.CurrentOverAmount = 0 AND bs.OtherAmount = 0
AND bs.CLoanStatus <> 8 AND IsRepayment = 1
AND bs.PeriodType = 32
AND bl.IsShelve = 1

--删除不存在帐单的应收科目
DELETE BillItem
FROM BillItem bi WHERE NOT EXISTS
(
	SELECT * FROM Bill bl WHERE bl.BillID = bi.BillID
)




