SELECT
CASE r.ReceivedType
	WHEN 30 THEN '财付通'
	WHEN 22 THEN '富友'
	WHEN 12 THEN '银扣'
	WHEN 11 THEN '转账'
	WHEN 28 THEN '退回'
	WHEN 17 THEN '冲销(负数)'
	WHEN 13 THEN '坏账'
	WHEN 2 THEN '冲销(正数/负数)'
	WHEN 3 THEN '减免(正数/负数)'
	ELSE ''
END ReceivedType,
 bs.ContractNo, 
 p.PersonName AS CustomerName, 
 SUM(CASE bi.Subject WHEN 1 THEN r.Amount ELSE 0 END) CapitalAmt,
 SUM(CASE bi.Subject WHEN 2 THEN r.Amount ELSE 0 END) InterestAmt,
 SUM(CASE bi.Subject WHEN 3 THEN r.Amount ELSE 0 END) ServiceAmt,
 SUM(CASE bi.Subject WHEN 4 THEN r.Amount ELSE 0 END) GuaranteeAmt,
 SUM(CASE bi.Subject WHEN 21 THEN r.Amount ELSE 0 END) CapitalLostAmt,
 SUM(CASE bi.Subject WHEN 22 THEN r.Amount ELSE 0 END) ServiceLostAmt,
 SUM(CASE bi.Subject WHEN 23 THEN r.Amount ELSE 0 END) PenaltyAmt,
 r.ReceivedTime as ReceiveTime, 
 bl.BillMonth,
 e2.Name as Region,
 r.CreateTime
FROM Received r
JOIN BillItem bi ON bi.BillItemID = r.BillItemID
JOIN Bill bl ON bl.BillID = bi.BillID
JOIN Business bs ON bs.BusinessID = bl.BusinessID
--JOIN Customer cs ON bs.CustomerID = cs.CustomerID
INNER JOIN customer.CustomerInfo cs ON bs.BusinessID=cs.Bid
INNER JOIN customer.Person p ON p.PersonId=cs.PersonId

LEFT JOIN ViewEnum e1 ON e1.FullKey = bs.BranchKey
LEFT JOIN ViewEnum e2 ON e2.Id = e1.Super
WHERE bs.ProductKind = 'PRODUCTKIND/WUDIYADAIKUAN'
AND bl.BillType < 3
AND {0}
GROUP BY bs.ContractNo, e2.Name, bl.BillMonth, r.ReceivedType, r.ReceivedTime, r.CreateTime,p.PersonName
order BY r.CreateTime