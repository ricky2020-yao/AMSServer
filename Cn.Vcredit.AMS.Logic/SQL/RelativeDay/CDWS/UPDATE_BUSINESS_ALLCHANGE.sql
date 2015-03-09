--STEP1.更新帐单支付状态
UPDATE Bill SET 
BillStatus = 
(CASE WHEN (SELECT SUM(DueAmt-ReceivedAmt) From BillItem WHERE BillID = Bill.BillID) = 0 THEN 3 
		WHEN (SELECT SUM(ReceivedAmt) From BillItem WHERE BillID = Bill.BillID) = 0 THEN 1 
		ELSE 2 END), 
FullPaidTime = 
(CASE WHEN (SELECT SUM(DueAmt-ReceivedAmt) From BillItem WHERE BillID = Bill.BillID) = 0 THEN GETDATE() 
	ELSE NULL END)
FROM Bill JOIN Business ON Bill.BusinessID = Business.BusinessID
WHERE Business.IsRepayment = 1 AND Business.PeriodType = 32
AND Business.ServiceSideID = {0}

--STEP2.更新订单各统计科目
UPDATE bs SET OverAmount = curBus.OverAmount,
			OtherAmount = curBus.OtherAmount,
			ResidualCapital = curBus.ResidualCapital,
			OverMonth = curBus.OverMonth
FROM Business bs
JOIN (
	SELECT BusinessID,
	OverAmount = ISNULL(
		(
			SELECT SUM(BillItem.DueAmt - BillItem.ReceivedAmt) 
			FROM  Bill JOIN BillItem ON Bill.BillID = BillItem.BillID 
			WHERE Bill.BusinessID = Business.BusinessID AND Bill.BillStatus != 3 
			AND Bill.BillType in (1, 2) AND BillItem.IsShelve = 0 AND Bill.IsShelve = 0 
		), 0
	), 
	CurrentOverAmount = 0,
	OverMonth = ISNULL(
		(
			SELECT COUNT(*) FROM Bill 
			WHERE Bill.BusinessID = Business.BusinessID AND BillStatus != 3
			AND Bill.BillType in (1, 2) AND Bill.IsShelve = 0
		), 0
	),
	OtherAmount = ISNULL(
		(
			SELECT SUM(BillItem.DueAmt - BillItem.ReceivedAmt) 
			FROM  Bill JOIN BillItem ON Bill.BillID = BillItem.BillID 
			WHERE Bill.BusinessID = Business.BusinessID AND Bill.BillStatus != 3 
			AND BillItem.IsCurrent = 1 AND Bill.BillType in (3, 4, 6, 7, 8, 9)
			AND BillItem.IsShelve = 0 AND Bill.IsShelve = 0
		), 0
	), 
	ResidualCapital = LoanCapital - ISNULL(
		(
			SELECT SUM(CASE WHEN bi.ReceivedAmt=bi.DueAmt THEN bi.Amount
			else ReceivedAmt END) 
			FROM BillItem bi
			JOIN Bill bl ON bi.BillID = bl.BillID
			JOIN Business bs ON bl.BusinessID = bs.BusinessID
			WHERE bs.BusinessID = Business.BusinessID AND bi.Subject IN (1, 31) 
			AND bi.IsShelve = 0 AND bl.IsShelve = 0 AND bl.BillType <> 5
		), 0
	)
	FROM Business
) curBus ON bs.BusinessID = curBus.BusinessID
WHERE IsRepayment = 1 AND PeriodType = 32 AND ServiceSideID = {0}

--STEP3更新订单清贷状态
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
WHERE bs.ResidualCapital < 1 AND bs.ResidualCapital >= -1 
AND bs.OverAmount = 0 
AND bs.CurrentOverAmount = 0 AND bs.OtherAmount = 0
AND bs.CLoanStatus <> 8 AND IsRepayment = 1
AND bs.ServiceSideID = {0}

--删除提前清贷成功订单的搁置科目
DELETE BillItem
FROM Business bs
JOIN Bill bl ON bs.BusinessID = bl.BusinessID
JOIN BillItem bi ON bl.BillID = bi.BillID
WHERE bs.ResidualCapital < 1 AND bs.ResidualCapital >= -1 
AND bs.OverAmount = 0 
AND bs.CurrentOverAmount = 0 AND bs.OtherAmount = 0
AND bs.CLoanStatus <> 8 AND IsRepayment = 1
AND bs.PeriodType = 32 
AND bi.IsShelve = 1
AND bs.ServiceSideID = {0}

--删除提前清贷成功订单的搁置帐单
DELETE Bill
FROM Business bs
JOIN Bill bl ON bs.BusinessID = bl.BusinessID
WHERE bs.ResidualCapital < 1 AND bs.ResidualCapital >= -1
AND bs.OverAmount = 0 
AND bs.CurrentOverAmount = 0 AND bs.OtherAmount = 0
AND bs.CLoanStatus <> 8 AND IsRepayment = 1
AND bs.PeriodType = 32
AND bl.IsShelve = 1
AND bs.ServiceSideID = {0}

--删除不存在帐单的应收科目
DELETE BillItem
FROM BillItem bi WHERE NOT EXISTS
(
	SELECT * FROM Bill bl WHERE bl.BillID = bi.BillID
) AND bi.BillID <> 0