UPDATE BillItem SET BillItem.BillID = bl.BillID 
FROM BillItem 
JOIN Bill bl ON BillItem.BusinessID = bl.BusinessID 
JOIN Business bs ON bl.BusinessID = bs.BusinessID
WHERE BillItem.IsCurrent = 1 
AND bl.IsCurrent = 1 
AND BillItem.BusinessID > 0
AND bs.DSeqType = {1}
AND bs.PeriodType = {2} 
{0}