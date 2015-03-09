SELECT bs.BusinessID, bs.ContractNo,
		CASE 
		WHEN r.ReceivedType IN (2, 3) THEN -r.Amount
		ELSE r.Amount END AS Amount, 
		bl.BillMonth,
		CASE bi.Subject 
		WHEN 1 THEN '本金'
		WHEN 2 THEN '利息'
		WHEN 3 THEN '服务费'
		WHEN 4 THEN '担保管理费'
		WHEN 21 THEN '本息扣失'
		WHEN 22 THEN '服务费扣失'
		WHEN 23 THEN '罚息' END AS Item,
		CASE r.ReceivedType 
		WHEN 2 THEN '冲销' 
		WHEN 3 THEN '减免'
		WHEN 11 THEN '转账'
		WHEN 12 THEN '银扣'
		WHEN 22 THEN '富友'
		WHEN 13 THEN '坏账'
		WHEN 17 THEN '冲销'
		WHEN 28 THEN '退回' END AS [Type],
		CONVERT(DATE, r.ReceivedTime) AS DeductionDate,
		r.Explain,
		r.ReceivedID AS [Identity]
FROM Received r
JOIN BillItem bi ON r.BillItemID = bi.BillItemID
JOIN Bill bl ON bi.BillID = bl.BillID
JOIN Business bs ON bl.BusinessID = bs.BusinessID
WHERE bl.BillType IN (1, 2) AND r.ReceivedTime >= '2013/3/15' 
AND bs.ServiceSideKey IN ('COMPANY/WX_SHWS_SERVICE', 'COMPANY/WX_SHWX_SERVICE')
ORDER BY r.ReceivedID ASC