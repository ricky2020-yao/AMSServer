SELECT BI.BillID,BI.BillItemID,Amt = BI.DueAmt-BI.ReceivedAmt
FROM BillItem AS BI
JOIN(
	SELECT TOP 1 B.BillID,B.BillMonth
	FROM BillItem AS A
	JOIN Bill AS B ON A.BillID = B.BillID
	WHERE  B.BusinessID = '{0}'-- ¶©µ¥ºÅ
	AND B.IsShelve=0 AND A.IsShelve = 0
	AND B.BillType<>5
	GROUP BY B.BillID,B.BillMonth
	HAVING SUM(A.DueAmt-A.ReceivedAmt)={1} --½ð¶î
	ORDER BY B.BillMonth
) AS RET
ON BI.BillID = RET.BillID
AND BI.DueAmt-BI.ReceivedAmt>0 AND BI.IsShelve=0