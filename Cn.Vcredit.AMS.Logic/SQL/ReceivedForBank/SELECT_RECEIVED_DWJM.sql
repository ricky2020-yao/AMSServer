SELECT BI.BillID,BI.BillItemID,Amt = BI.DueAmt-BI.ReceivedAmt
FROM BillItem AS BI
JOIN(
	SELECT TOP 1 B.BillID,D.DunLevel,D.CompanyKey
	FROM BillItem AS A
	JOIN Bill AS B ON A.BillID = B.BillID
	JOIN Business AS C ON B.BusinessID = C.BusinessID
	JOIN BankDunLevel AS D ON A.Subject = D.BillItemSubject
	AND A.IsCurrent = D.IsCurrent AND C.LendingSideKey = D.CompanyKey
	WHERE  C.ContractNo = '{0}'--��ͬ��
	AND D.DunLevel IN ({2}) -- ��ͨ�˵��������з���
	AND B.BillType<>5 AND B.IsShelve =0 AND A.IsShelve=0  
	GROUP BY D.DunLevel,B.BillID,B.BillMonth,D.CompanyKey
	HAVING SUM(A.DueAmt-A.ReceivedAmt)={1} --���
	ORDER BY D.DunLevel,B.BillMonth
) AS RET
ON BI.BillID = RET.BillID
JOIN BankDunLevel AS DL ON BI.Subject = DL.BillItemSubject
AND BI.IsCurrent = DL.IsCurrent AND DL.CompanyKey = RET.CompanyKey
AND DL.DunLevel = RET.DunLevel AND BI.DueAmt-BI.ReceivedAmt>0
AND BI.IsShelve = 0