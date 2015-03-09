--为当前相对日订单生成本息扣失科目
INSERT INTO BillItem (BillID, [Subject], Amount, DueAmt, ReceivedAmt, CreateTime, SubjectType, OperatorID, IsCurrent, IsShelve, BusinessID)
SELECT bl.BillID, 21, bs.PrincipalPunish, bs.PrincipalPunish, 0, getdate(), 0, 0, bl.IsCurrent, bl.IsShelve, bl.BusinessID
FROM Bill bl
JOIN Business bs ON bl.BusinessID = bs.BusinessID
WHERE bl.BillType IN (1, 2)
AND bl.IsCurrent = 1 
AND bl.BillStatus <> 3
AND bs.BusinessID IN ({0})
AND bs.LendingSideKey = 'COMPANY/DWJM_LENDING'
AND bs.PeriodType = 32
AND bs.IsRepayment = 1
AND bs.FrozenNo = ''
AND EXISTS(
	SELECT * 
	FROM BillItem bi
	WHERE bi.BillID = bl.BillID 
	AND bi.Subject IN (1, 2)	--如本金或利息未还清，则生成本息扣失科目
	AND bi.DueAmt > bi.ReceivedAmt)
AND NOT EXISTS (
	SELECT *
	FROM BillItem xbi
	WHERE xbi.Subject = 21 
	AND xbi.BillID = bl.BillID
)

--为当前相对日订单生成服务费扣失科目
INSERT INTO BillItem (BillID, [Subject], Amount, DueAmt, ReceivedAmt, CreateTime, SubjectType, OperatorID, IsCurrent, IsShelve, BusinessID)
SELECT bl.BillID, 22, bs.ServicePunish, bs.ServicePunish, 0, getdate(), 0, 0, bl.IsCurrent, bl.IsShelve, bl.BusinessID
FROM Bill bl
JOIN Business bs ON bl.BusinessID = bs.BusinessID
WHERE bl.BillType IN (1, 2)
AND bl.IsCurrent = 1 
AND bl.BillStatus <> 3
AND bs.BusinessID IN ({0})
AND bs.LendingSideKey = 'COMPANY/DWJM_LENDING'
AND bs.PeriodType = 32
AND bs.IsRepayment = 1
AND bs.FrozenNo = ''
AND EXISTS(
	SELECT * 
	FROM BillItem bi
	WHERE bi.BillID = bl.BillID 
	AND bi.Subject IN (3, 4)	--如服务费、担保费未还清，则生成服务费扣失科目
	AND bi.DueAmt > bi.ReceivedAmt)
AND NOT EXISTS (
	SELECT *
	FROM BillItem xbi
	WHERE xbi.Subject = 22 
	AND xbi.BillID = bl.BillID
)
	
