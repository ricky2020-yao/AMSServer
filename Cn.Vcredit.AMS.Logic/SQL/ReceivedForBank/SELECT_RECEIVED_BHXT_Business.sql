SELECT BI.BillID,BI.BillItemID,Amt = BI.DueAmt-BI.ReceivedAmt
FROM BillItem AS BI
JOIN Bill AS BL ON BI.BillID = BL.BillID
AND BL.BusinessID =(
		SELECT TOP 1 BUS.BusinessID
		FROM BillItem AS BI
		JOIN Bill AS BL ON BI.BillID = BL.BillID
		JOIN Business AS BUS ON BL.BusinessID = BUS.BusinessID
		WHERE BUS.ContractNo='{0}' AND  BI.Subject ={1}
		AND BL.IsShelve = 0 AND BL.BillType<>5 AND BI.IsShelve=0 
		AND BI.IsCurrent = 0
		group by BUS.BusinessID having SUM(BI.DueAmt-BI.ReceivedAmt)={2}
)AND BI.DueAmt-BI.ReceivedAmt>0 AND Subject ={3}
AND BI.IsCurrent = 0 AND BL.IsShelve = 0 AND BI.IsShelve = 0 AND BL.BillType<>5