--实收科目沉底
UPDATE Received SET BillID = c.TBillID, BillItemID = c.TBillItemID
FROM Received r
	Join
    (select * from
  		(select bl.BillID  AS SBillID,
			bi.BillItemID AS SBillItemID,
			xbl.BillID AS TBillID,
			xbi.BillItemID AS TBillItemID,
			row_number() over(partition by xbl.BillID,xbi.BillItemID order by bl.BillMonth DESC) AS num

		from Bill bl 
		
		JOIN BillItem bi ON bl.BillID = bi.BillID
			AND bl.BillType IN (1, 2) AND bl.IsShelve = 0
			AND bi.IsShelve = 0 AND bi.ReceivedAmt > 0
			AND bl.IsCurrent = 1 AND bl.BillStatus <> 3
			
		JOIN Bill xbl ON bl.BusinessID=xbl.BusinessID
		
		JOIN BillItem xbi ON xbi.BillID = xbl.BillID
			AND xbi.Subject = bi.Subject
			AND xbl.IsShelve = 0
			AND xbi.IsShelve = 0
			AND xbi.DueAmt = bi.ReceivedAmt
			AND xbi.ReceivedAmt = 0
			AND xbl.IsCurrent = 0
			AND xbl.BillType IN (1, 2) 
			
		JOIN Business bs ON bs.BusinessID=bl.BusinessID 
			AND bs.IsRepayment = 1 ) t
		Where num=1
	)c
ON r.BillID = c.SBillID AND r.BillItemID = c.SBillItemID

--更新科目表ReceivedAmt字段
UPDATE BillItem SET ReceivedAmt = a.RecAmt 
FROM BillItem bi
JOIN (SELECT bi.BillItemID, bi.ReceivedAmt, ISNULL((
	SELECT SUM(r.Amount) 
	FROM Received r 
	WHERE bi.BillItemID = r.BillItemID 
	AND r.ReceivedType > 10), 0) AS RecAmt
FROM BillItem bi) a ON bi.BillItemID = a.BillItemID
WHERE a.ReceivedAmt <> a.RecAmt