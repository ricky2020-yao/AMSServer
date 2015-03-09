SELECT bs.BusinessID, bs.ContractNo,
	bi.Amount, bl.BillMonth,
		CASE bi.Subject 
		WHEN 1 THEN '本金'
		WHEN 2 THEN '利息'
		WHEN 3 THEN '服务费'
		WHEN 4 THEN '担保管理费'
		WHEN 21 THEN '本息扣失'
		WHEN 22 THEN '服务费扣失'
		WHEN 23 THEN '罚息' END AS Item,
		'应收' AS [Type],
		CONVERT(DATE, bi.CreateTime) AS DeductionDate,
		'' AS Explain,
		CONVERT(INT, bi.BillItemID) AS [Identity]
FROM BillItem bi 
JOIN Bill bl ON bi.BillID = bl.BillID
JOIN Business bs ON bl.BusinessID = bs.BusinessID
WHERE bl.BillType IN (1, 2) AND bi.Subject IN (21, 23) AND bl.BillMonth IN ('2013/02', '2013/03')
AND bs.ServiceSideID = 10
ORDER BY bi.BillItemID ASC
