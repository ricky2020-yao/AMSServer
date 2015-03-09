--更新当期科目为逾期
UPDATE BillItem SET IsCurrent = 0 
FROM BillItem bi
JOIN Bill bl ON bi.BillID = bl.BillID
JOIN Business bs ON bl.BusinessID = bs.BusinessID
WHERE bl.IsCurrent = 1
AND bs.ServiceSideKey IN ({0})
AND bs.IsRepayment = 1
AND bs.LendingSideKey = 'COMPANY/BHXT_LENDING'

--更新当期帐单为逾期
UPDATE Bill SET IsCurrent = 0 
FROM Bill bl
JOIN Business bs ON bl.BusinessID = bs.BusinessID
WHERE bl.IsCurrent = 1 
AND bs.ServiceSideKey IN ({0})
AND bs.IsRepayment = 1
AND bs.LendingSideKey = 'COMPANY/BHXT_LENDING'

