SELECT BI.BillID,BI.BillItemID,Amt = BI.DueAmt-BI.ReceivedAmt
FROM BillItem AS BI
JOIN Bill AS BL ON BI.BillID = BL.BillID
JOIN Business AS BUS ON BL.BusinessID = BUS.BusinessID
WHERE BUS.ContractNo='{0}' AND  BI.Subject =21
AND BL.IsShelve = 0 AND BL.BillType<>5 AND BI.IsShelve=0 
AND BI.IsCurrent = 1 AND BI.DueAmt-BI.ReceivedAmt = {1}