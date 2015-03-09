SELECT
Bid = BUS.BusinessID,
PayDate = RI.ReceivedTime,
Balance = RI.Amount,
PayId = RI.ReceivedID,
BusType = BUS.ProductType,
BillMonth = BL.BillMonth
FROM Received RI
JOIN BillItem BI ON BI.BillItemID = RI.BillItemID
JOIN Bill BL ON BL.BillID = BI.BillID
JOIN Business BUS ON BUS.BusinessID = BL.BusinessID
WHERE 
RI.ReceivedID > {0} --上次推送最大的实收ID
AND BI.Subject = 37 --科目类型(保费)