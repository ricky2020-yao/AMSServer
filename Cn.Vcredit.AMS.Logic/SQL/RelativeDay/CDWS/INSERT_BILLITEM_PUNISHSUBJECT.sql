--为成都无抵押贷款类型的订单生成50元的本息扣失==============================================================
INSERT INTO BillItem (BillID, [Subject], Amount, DueAmt, ReceivedAmt, CreateTime, SubjectType, OperatorID, IsCurrent, IsShelve, BusinessID)
SELECT bl.BillID, 21, bs.PrincipalPunish, bs.PrincipalPunish, 0, getdate(), 0, 0, bl.IsCurrent, bl.IsShelve, bl.BusinessID
FROM Bill bl
JOIN Business bs ON bl.BusinessID = bs.BusinessID
WHERE bl.BillType IN (1, 2)
AND bl.IsCurrent = 1 
AND bs.BusinessID IN ({0})
AND bs.ServiceSideKey = 'COMPANY/WX_CDWS_LENDING'
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
AND bs.PrincipalPunish > 0


--为成都无抵押贷款类型的订单生成50元的本息扣失==============================================================
INSERT INTO BillItem (BillID, [Subject], Amount, DueAmt, ReceivedAmt, CreateTime, SubjectType, OperatorID, IsCurrent, IsShelve, BusinessID)
SELECT bl.BillID, 22, bs.ServicePunish, bs.ServicePunish, 0, getdate(), 0, 0, bl.IsCurrent, bl.IsShelve, bl.BusinessID
FROM Bill bl
JOIN Business bs ON bl.BusinessID = bs.BusinessID
WHERE bl.BillType IN (1, 2)
AND bl.IsCurrent = 1 
AND bs.BusinessID IN ({0})
AND bs.ServiceSideKey = 'COMPANY/WX_CDWS_LENDING'
AND bs.PeriodType = 32
AND bs.IsRepayment = 1
AND bs.FrozenNo = ''
AND EXISTS(
	SELECT * 
	FROM BillItem bi
	WHERE bi.BillID = bl.BillID 
	AND bi.Subject = 3	--如管理费未还清，则生成管理费扣失科目
	AND bi.DueAmt > bi.ReceivedAmt)
AND NOT EXISTS (
	SELECT *
	FROM BillItem xbi
	WHERE xbi.Subject = 22
	AND xbi.BillID = bl.BillID
)
AND bs.ServicePunish > 0

