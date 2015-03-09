--更新当期科目为逾期
UPDATE BillItem SET IsCurrent = 0 
FROM BillItem bi
JOIN Bill bl ON bi.BillID = bl.BillID
WHERE bl.BusinessID IN ({0}) AND bl.IsCurrent = 1
AND bl.BillMonth != '{1}'

--更新当期帐单为逾期
UPDATE Bill SET IsCurrent = 0 
FROM Bill bl
WHERE bl.BusinessID IN ({0}) AND bl.IsCurrent = 1
AND bl.BillMonth != '{1}'

