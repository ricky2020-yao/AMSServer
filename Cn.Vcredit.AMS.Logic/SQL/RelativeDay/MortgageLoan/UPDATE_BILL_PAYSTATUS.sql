UPDATE Bill SET 
	BillStatus = 
	(
		CASE 
		WHEN (SELECT SUM(xbi.DueAmt - xbi.ReceivedAmt) From BillItem xbi WHERE xbi.BillID = bl.BillID) = 0 THEN 3 
		WHEN (SELECT SUM(xbi.ReceivedAmt) From BillItem xbi WHERE xbi.BillID = bl.BillID) = 0 THEN 1 
		ELSE 2 END
	), 
	FullPaidTime = 
	(
		CASE WHEN (SELECT SUM(xbi.DueAmt - xbi.ReceivedAmt) From BillItem xbi WHERE BillID = bl.BillID) = 0 
			THEN (SELECT TOP 1 ReceivedTime FROM Received xr WHERE xr.BillID = bl.BillID ORDER BY xr.ReceivedTime DESC)
		ELSE NULL END)
FROM Bill bl
JOIN Business bs ON bl.BusinessID = bs.BusinessID
WHERE bs.IsRepayment = 1 AND bs.PeriodType = 32
AND bs.BusinessID IN ({0})