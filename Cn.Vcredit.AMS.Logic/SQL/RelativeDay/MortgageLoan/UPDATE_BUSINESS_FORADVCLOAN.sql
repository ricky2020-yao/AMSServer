--更新提前清贷申请操作表，将清贷状态设置为注销状态
UPDATE ca SET CloanApplyStatus = 4
FROM (
	SELECT bl.BusinessID
	FROM Bill bl
	JOIN Business bs ON bl.BusinessID = bs.BusinessID
	WHERE bl.EndTime < DATEADD(DAY, 1, CONVERT(DATE, '{2}'))
	AND bl.BillType = 3
	AND bs.PeriodType = 32 
	AND bs.IsRepayment = 1
	AND bs.ServiceSideID = {0}
	AND bs.LendingSideID = {1}
	AND bs.ProductKind = 'PRODUCTKIND/HUSUCHEDAI'
	GROUP BY bl.BusinessID
) bs
JOIN CloanApply ca ON bs.BusinessID = ca.BusinessID
WHERE ca.CloanApplyStatus = 2

--取消提前清贷订单下所有帐单的搁置状态
UPDATE bl SET IsShelve = 0
FROM (
	SELECT bl.BusinessID
	FROM Bill bl
	JOIN Business bs ON bl.BusinessID = bs.BusinessID
	WHERE bl.EndTime < DATEADD(DAY, 1, CONVERT(DATE, '{2}'))
	AND bl.BillType = 3
	AND bs.PeriodType = 32 
	AND bs.IsRepayment = 1
	AND bs.ServiceSideID = {0}
	AND bs.LendingSideID = {1}
	AND bs.ProductKind = 'PRODUCTKIND/HUSUCHEDAI'
	GROUP BY bl.BusinessID
) bs
JOIN Bill bl ON bs.BusinessID = bl.BusinessID
WHERE bl.IsShelve = 1

--取消提前清贷订单下所有科目的搁置状态
UPDATE bi SET IsShelve = 0
FROM (
	SELECT bl.BusinessID
	FROM Bill bl
	JOIN Business bs ON bl.BusinessID = bs.BusinessID
	WHERE bl.EndTime < DATEADD(DAY, 1, CONVERT(DATE, '{2}'))
	AND bl.BillType = 3
	AND bs.PeriodType = 32 
	AND bs.IsRepayment = 1
	AND bs.ServiceSideID = {0}
	AND bs.LendingSideID = {1}
	AND bs.ProductKind = 'PRODUCTKIND/HUSUCHEDAI'
	GROUP BY bl.BusinessID
) bs
JOIN Bill bl ON bs.BusinessID = bl.BusinessID
JOIN BillItem bi ON bl.BillID = bi.BillID
WHERE bi.IsShelve = 1

--查询出特定子公司下所有提前清贷中的订单
UPDATE bbs SET CLoanStatus = 1
FROM (
	SELECT bl.BusinessID
	FROM Bill bl
	JOIN Business bs ON bl.BusinessID = bs.BusinessID
	WHERE bl.EndTime < DATEADD(DAY, 1, CONVERT(DATE, '{2}'))
	AND bl.BillType = 3
	AND bs.PeriodType = 32 
	AND bs.IsRepayment = 1
	AND bs.ServiceSideID = {0}
	AND bs.LendingSideID = {1}
	AND bs.ProductKind = 'PRODUCTKIND/HUSUCHEDAI'
	GROUP BY bl.BusinessID
) bs 
JOIN Business bbs ON bbs.BusinessID = bs.BusinessID
WHERE bbs.CLoanStatus = 7

--更新提前清贷帐单为注销帐单
UPDATE bl SET BillType = 5
FROM (
	SELECT bl.BusinessID
	FROM Bill bl
	JOIN Business bs ON bl.BusinessID = bs.BusinessID
	WHERE bl.EndTime < DATEADD(DAY, 1, CONVERT(DATE, '{2}'))
	AND bl.BillType = 3
	AND bs.PeriodType = 32 
	AND bs.IsRepayment = 1
	AND bs.ServiceSideID = {0}
	AND bs.LendingSideID = {1}
	AND bs.ProductKind = 'PRODUCTKIND/HUSUCHEDAI'
	GROUP BY bl.BusinessID
) bs
JOIN Bill bl ON bs.BusinessID = bl.BusinessID
WHERE bl.BillType = 3