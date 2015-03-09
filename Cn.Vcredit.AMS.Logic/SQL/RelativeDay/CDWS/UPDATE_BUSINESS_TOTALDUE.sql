UPDATE Business 
SET OverAmount = ISNULL(
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
WHERE IsRepayment = 1 AND PeriodType = 32 
AND ProductType = 5 AND BusinessID IN ({0})