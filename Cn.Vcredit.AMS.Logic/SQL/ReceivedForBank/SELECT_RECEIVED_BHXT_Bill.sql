SELECT BI.BillID,BI.BillItemID,Amt = BI.DueAmt-BI.ReceivedAmt
FROM BillItem AS BI
JOIN(
	SELECT TOP 1 B.BillID,B.BillMonth
	FROM BillItem AS A
	JOIN Bill AS B ON A.BillID = B.BillID
	JOIN Business AS C ON B.BusinessID =C.BusinessID
	WHERE  C.ContractNo ='{0}'-- 订单号
	AND A.Subject IN (1,2) -- 普通账单：渤海信托收益本息按账单拆分部分
	AND A.IsShelve = 0 AND B.IsShelve =0
	AND B.BillType<>5
	GROUP BY B.BillID,B.BillMonth
	HAVING SUM(A.DueAmt-A.ReceivedAmt)={1} --金额
	ORDER BY B.BillMonth
) AS RET
ON BI.BillID = RET.BillID AND BI.Subject IN(1,2)
AND BI.DueAmt-BI.ReceivedAmt>0 AND BI.IsShelve = 0