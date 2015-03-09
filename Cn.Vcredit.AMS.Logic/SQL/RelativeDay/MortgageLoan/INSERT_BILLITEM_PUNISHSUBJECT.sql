--为沪、苏车贷的订单创建50元本息扣失(2013年1月1日之前)========================================================
INSERT INTO BillItem (BillID, [Subject], Amount, DueAmt, ReceivedAmt, CreateTime, SubjectType, OperatorID, IsCurrent, IsShelve, BusinessID)
SELECT bl.BillID, 21, bs.PrincipalPunish, bs.PrincipalPunish, 0, getdate(), 0, 0, bl.IsCurrent, bl.IsShelve, bl.BusinessID
FROM Bill bl
JOIN Business bs ON bl.BusinessID = bs.BusinessID
WHERE bl.BillType IN (1, 2)
AND bl.IsCurrent = 1 
AND bl.BillStatus <> 3
AND bs.FrozenNo = ''
AND bs.BusinessID IN ({0})
AND bs.ProductKind = 'PRODUCTKIND/HUSUCHEDAI'
AND bs.PeriodType = 32
AND bs.IsRepayment = 1
AND EXISTS(
	SELECT * 
	FROM BillItem bi
	WHERE bi.BillID = bl.BillID 
	AND bi.Subject IN (1, 2)	--如本金或利息未还清，则生成本息扣失科目
	AND bi.DueAmt > bi.ReceivedAmt)
AND NOT EXISTS (
	SELECT *
	FROM BillItem bi
	WHERE bi.BillID = bl.BillID
	AND bi.Subject = 21
)


