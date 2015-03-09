SELECT pl.PhoneListLogID, pll.IsCurrent, pl.PhoneListID, pll.VisitType, pl.CustomerID, b.BillID,
ISNULL((
	SELECT TOP 1 TrackSituation FROM CustomerTrack ct WHERE BusinessID = b.BusinessID 
	AND ct.CreateTime > pll.CreateTime ORDER BY CreateTime DESC
),0) AS TrackSituation,
(
	SELECT SUM(DueAmt) FROM BillItem WHERE BillID = b.BillID
) AS DueAmt,
(
	SELECT SUM(ReceivedAmt) FROM BillItem WHERE BillID = b.BillID
) AS ReceivedAmt
FROM PhoneListLog pll
JOIN PhoneList pl ON pll.PhoneListLogID = pl.PhoneListLogID
JOIN Bill b ON pl.BillID = b.BillID
WHERE pll.IsCurrent = 1
