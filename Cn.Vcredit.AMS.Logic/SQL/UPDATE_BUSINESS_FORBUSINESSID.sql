--Step1:更新帐单
UPDATE Bill SET 
BillStatus = 
(CASE WHEN (SELECT SUM(DueAmt-ReceivedAmt) From BillItem WHERE BillID = Bill.BillID AND IsShelve = 0) = 0 THEN 3 
		WHEN (SELECT SUM(ReceivedAmt) From BillItem WHERE BillID = Bill.BillID) = 0 THEN 1 
		ELSE 2 END),
    FullPaidTime = 
    (
	CASE WHEN (SELECT SUM(xbi.DueAmt - xbi.ReceivedAmt) From BillItem xbi WHERE BillID = Bill.BillID) = 0 
		THEN (SELECT TOP 1 ReceivedTime FROM Received xr WHERE xr.BillID = Bill.BillID ORDER BY xr.ReceivedTime DESC)
	ELSE NULL END)
FROM Bill JOIN Business ON Bill.BusinessID = Business.BusinessID
WHERE Business.IsRepayment = 1 AND Business.BusinessID = {0}

--Step2:更新订单的逾期欠费、当期欠费、剩余本金
UPDATE bs SET OverAmount = curBus.OverAmount,
			OtherAmount = curBus.OtherAmount,
			ResidualCapital = curBus.ResidualCapital,
			CurrentOverAmount = curBus.CurrentOverAmount,
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
			AND Bill.LimitTime < getdate()
		), 0
	), 
	CurrentOverAmount = ISNULL(
		(
			SELECT SUM(BillItem.DueAmt - BillItem.ReceivedAmt) 
			FROM  Bill JOIN BillItem ON Bill.BillID = BillItem.BillID 
			WHERE Bill.BusinessID = Business.BusinessID AND Bill.BillStatus != 3 
			AND Bill.BillType in (1, 2) AND BillItem.IsShelve = 0 AND Bill.IsShelve = 0 
			AND Bill.LimitTime >= getdate()
		), 0
	),
	OverMonth = ISNULL(
		(
		    SELECT DATEDIFF(MONTH,  min(DueDate),getdate()) +
            CASE WHEN getdate() >= DATEADD(MONTH,DATEDIFF(MONTH,min(DueDate),getdate()),min(DueDate)) 
            THEN 1 ELSE 0 END 
            FROM Bill
            WHERE Bill.BusinessID = Business.BusinessID AND BillStatus != 3
			AND Bill.BillType in (1, 2) AND Bill.IsShelve = 0
		), 0
	),
	OtherAmount = ISNULL(
		(
			SELECT SUM(BillItem.DueAmt - BillItem.ReceivedAmt) 
			FROM  Bill JOIN BillItem ON Bill.BillID = BillItem.BillID 
			WHERE Bill.BusinessID = Business.BusinessID AND Bill.BillStatus != 3 
			AND Bill.BillType in (3, 4, 6, 7, 8, 9)
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
WHERE bs.BusinessID = {0} AND bs.IsRepayment = 1

--Step4:订单提前清贷3, 坏帐清贷4，满约清贷2
UPDATE Business SET CLoanStatus =
	CASE WHEN EXISTS(
		SELECT bl.BusinessID
		FROM Bill bl 
		WHERE bl.BusinessID = bs.BusinessID
		AND bl.BillType = 4 
		AND bl.BillStatus = 3
	) THEN 4 WHEN ( 
		SELECT COUNT(*)
		FROM Bill bl 
		WHERE bl.BusinessID = bs.BusinessID
		AND bl.BillType IN (1, 2) 
		AND bl.BillStatus = 3
	) >=bs.LoanPeriod THEN 2
	  ELSE 3 END,
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
AND bs.CLoanStatus <> 8 AND bs.IsRepayment = 1
AND bs.BusinessID = {0}

UPDATE Business SET CLoanStatus = 4
FROM Business bs
WHERE bs.BusinessID = {0} AND bs.IsRepayment = 0
AND EXISTS(
	SELECT * 
	FROM Bill xbl 
	WHERE xbl.BillType = 4 
	AND xbl.BusinessID = bs.BusinessID)


--Step5:删除提前清贷成功订单的搁置科目
DELETE BillItem
FROM Business bs
JOIN Bill bl ON bs.BusinessID = bl.BusinessID
JOIN BillItem bi ON bl.BillID = bi.BillID
WHERE IsRepayment = 0 
AND bs.BusinessID = {0} 
AND bi.IsShelve = 1

--Step6:删除提前清贷成功订单的搁置帐单
DELETE Bill
FROM Business bs
JOIN Bill bl ON bs.BusinessID = bl.BusinessID
WHERE IsRepayment = 0
AND bs.BusinessID = {0} 
AND bl.IsShelve = 1

--设置当前清贷订单的催收单CancelTime为下一账单日
UPDATE
    dun
SET 
    dun.IsCurrent = 0 ,
    dun.CancelTime = CASE bus.PeriodType
                       WHEN 13
                       THEN CONVERT(NVARCHAR(7), DATEADD(MONTH, 1,dun.CreateTime), 121) + '-21'
                       WHEN 32 THEN CONVERT(NVARCHAR(10), DATEADD(MONTH, 1,dun.CreateTime), 121)
                     END
FROM
    dbo.Business bus
    JOIN dbo.Dun dun ON dun.BusinessID = bus.BusinessID
WHERE
    bus.IsRepayment = 0
    AND dun.CancelTime IS NULL
    AND bus.BusinessID = {0}
