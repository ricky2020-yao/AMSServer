SELECT BI.BillID,BI.BillItemID,Amt = BI.DueAmt-BI.ReceivedAmt
FROM BillItem AS BI
JOIN(
	SELECT TOP 1 B.BillID,B.BillMonth
	FROM BillItem AS A
	JOIN Bill AS B ON A.BillID = B.BillID
	WHERE  B.BusinessID = '{0}'-- 订单号
	AND B.IsShelve=0 AND A.IsShelve = 0
	AND B.BillType<>5 AND A.Subject IN ({1}) -- 普通账单：服务方收益分为两类(所有基本款项、仅服务方收益款项)  
	GROUP BY B.BillID,B.BillMonth
	HAVING SUM(A.DueAmt-A.ReceivedAmt)={2} --金额
	ORDER BY B.BillMonth
) AS RET
ON BI.BillID = RET.BillID AND BI.Subject IN({3})
AND BI.DueAmt-BI.ReceivedAmt>0 AND BI.IsShelve=0