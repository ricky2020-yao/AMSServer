SELECT r.BusinessID, ContractNo, SUM(ReceivedAmt) AS Amount,
CONVERT(nvarchar(7), DATEADD(MONTH, 1, t.BillMonth + '/01'), 111) AS BillMonth,
CASE Subject 
	WHEN 25 THEN '担保违约金'
	WHEN 28 THEN '加收利息'
	WHEN 29 THEN '加收服务费'
	WHEN 36 THEN '加收担保管理费'
	WHEN 35 THEN '提前清贷服务费'
	WHEN 31 THEN '剩余本金' 
END AS Item,
'清贷' AS [Type], CONVERT(DATE, ClearLoanTime) AS DeductionDate, '' Explain, CONVERT(INT, MIN(BillItemID)) AS [Identity]
FROM ViewReceived r
LEFT JOIN (
	SELECT BusinessID, MAX(BillMonth) BillMonth FROM Bill
	WHERE IsShelve = 0 AND BillType IN (1, 2) 
	GROUP BY BusinessID
) t ON t.BusinessID = r.BusinessID
WHERE BillType = 3 AND CLoanStatus = 3 AND BusinessStatus IN (1, 2) AND r.ReceivedType >= 10
GROUP BY r.BusinessID, r.ContractNo, t.BillMonth, Subject, r.ClearLoanTime
HAVING SUM(r.ReceivedAmt) > 0