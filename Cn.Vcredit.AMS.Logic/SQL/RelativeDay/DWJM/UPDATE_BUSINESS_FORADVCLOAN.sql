--更新提前清贷申请操作表，将清贷状态设置为注销状态
UPDATE CloanApply SET CloanApplyStatus = 4, CheckTime = getdate()
FROM (
	SELECT bl.BusinessID
	FROM Bill bl
	JOIN Business bs ON bl.BusinessID = bs.BusinessID
	WHERE bl.EndTime < DATEADD(DAY, 1, CONVERT(DATE, '{2}'))
	AND bl.BillType = 3
	AND bs.PeriodType = 32 
	AND bs.IsRepayment = 1
	AND bs.ProductType = 4
	AND bs.ServiceSideID = {0}
	AND bs.LendingSideID = {1}
	GROUP BY bl.BusinessID
) bs
JOIN CloanApply ca ON bs.BusinessID = ca.BusinessID
WHERE CloanApplyStatus = 2

--取消提前清贷订单下所有帐单的搁置状态
UPDATE Bill SET IsShelve = 0
FROM (
	SELECT bl.BusinessID
	FROM Bill bl
	JOIN Business bs ON bl.BusinessID = bs.BusinessID
	WHERE bl.EndTime < DATEADD(DAY, 1, CONVERT(DATE, '{2}'))
	AND bl.BillType = 3
	AND bs.PeriodType = 32 
	AND bs.IsRepayment = 1
	AND bs.ProductType = 4
	AND bs.ServiceSideID = {0}
	AND bs.LendingSideID = {1}
	GROUP BY bl.BusinessID
) bs
JOIN Bill bl ON bs.BusinessID = bl.BusinessID
WHERE bl.IsShelve = 1

--取消提前清贷订单下所有科目的搁置状态
UPDATE BillItem SET IsShelve = 0
FROM (
	SELECT bl.BusinessID
	FROM Bill bl
	JOIN Business bs ON bl.BusinessID = bs.BusinessID
	WHERE bl.EndTime < DATEADD(DAY, 1, CONVERT(DATE, '{2}'))
	AND bl.BillType = 3
	AND bs.PeriodType = 32 
	AND bs.IsRepayment = 1
	AND bs.ProductType = 4
	AND bs.ServiceSideID = {0}
	AND bs.LendingSideID = {1}
	GROUP BY bl.BusinessID
) bs
JOIN Bill bl ON bs.BusinessID = bl.BusinessID
JOIN BillItem bi ON bl.BillID = bi.BillID
WHERE bi.IsShelve = 1

--查询出特定子公司下所有提前清贷中的订单
UPDATE Business SET CLoanStatus = 1
FROM (
	SELECT bl.BusinessID
	FROM Bill bl
	JOIN Business bs ON bl.BusinessID = bs.BusinessID
	WHERE bl.EndTime < DATEADD(DAY, 1, CONVERT(DATE, '{2}'))
	AND bl.BillType = 3
	AND bs.PeriodType = 32 
	AND bs.IsRepayment = 1
	AND bs.ProductType = 4
	AND bs.ServiceSideID = {0}
	AND bs.LendingSideID = {1}
	GROUP BY bl.BusinessID
) bs 
JOIN Business cb ON cb.BusinessID = bs.BusinessID
WHERE cb.CLoanStatus = 7

--更新提前清贷帐单为注销帐单
UPDATE Bill SET BillType = 5
FROM (
	SELECT bl.BusinessID
	FROM Bill bl
	JOIN Business bs ON bl.BusinessID = bs.BusinessID
	WHERE bl.EndTime < DATEADD(DAY, 1, CONVERT(DATE, '{2}'))
	AND bl.BillType = 3
	AND bs.PeriodType = 32 
	AND bs.IsRepayment = 1
	AND bs.ProductType = 4
	AND bs.ServiceSideID = {0}
	AND bs.LendingSideID = {1}
	GROUP BY bl.BusinessID
) bs
JOIN Bill bl ON bs.BusinessID = bl.BusinessID
WHERE bl.BillType = 3
