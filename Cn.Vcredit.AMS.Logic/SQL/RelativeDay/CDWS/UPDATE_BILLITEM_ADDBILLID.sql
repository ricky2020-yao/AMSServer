--将当期的科目绑定给当期的帐单
UPDATE BillItem SET BillID = bl.BillID
FROM BillItem bi
JOIN Bill bl ON bi.BusinessID = bl.BusinessID 
AND bl.IsCurrent = 1 AND bl.BillType IN (1, 2)
WHERE bl.BusinessID IN ({0}) AND bi.IsCurrent = 1