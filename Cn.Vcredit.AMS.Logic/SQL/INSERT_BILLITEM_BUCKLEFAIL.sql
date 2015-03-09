--本息扣失（仅渤海）
INSERT INTO BillItem (BillID, [Subject], Amount, DueAmt, ReceivedAmt, CreateTime, SubjectType, OperatorID, IsCurrent, IsShelve, BusinessID)
SELECT bl.BillID, 21, 50, 50, 0, GETDATE(), 0, {0}, bl.IsCurrent, bl.IsShelve, bl.BusinessID
FROM Bill bl
JOIN Business bs ON bl.BusinessID = bs.BusinessID
WHERE bs.IsRepayment = 1	--未清贷的订单
AND bs.CLoanStatus != 8		--未注销的订单
AND bs.BusinessStatus IN (1, 2)		--必须是正常或担保的订单
AND bs.ServiceSideKey IN ({1})	--代入服务方 
AND bl.BillType IN (1, 2)	--必须是正常或担保的帐单
AND bl.IsCurrent = 1		--必须是当期帐单
AND bl.BillStatus != 3		--未完全付款的帐单
AND EXISTS(
	SELECT * FROM BillItem xbi 
	WHERE xbi.BillID = bl.BillID 
	AND xbi.[Subject] IN (1, 2) 
	AND DueAmt > ReceivedAmt) --本息未还清的
AND bs.LendingSideKey = 'COMPANY/BHXT_LENDING'	--必须是渤海地区
AND bs.DSeqType = 1			--1类型的时间轴

--服务费扣失（仅渤海）
INSERT INTO BillItem (BillID, [Subject], Amount, DueAmt, ReceivedAmt, CreateTime, SubjectType, OperatorID, IsCurrent, IsShelve, BusinessID)
SELECT bl.BillID, 22, 50, 50, 0, GETDATE(), 0, {0}, bl.IsCurrent, bl.IsShelve, bl.BusinessID
FROM Bill bl
JOIN Business bs ON bl.BusinessID = bs.BusinessID
WHERE bs.IsRepayment = 1	--未清贷的订单
AND bs.CLoanStatus != 8		--未注销的订单
AND bs.BusinessStatus IN (1, 2)		--必须是正常或担保的订单
AND bs.ServiceSideKey IN ({1})	--代入服务方 
AND bl.BillType IN (1, 2)	--必须是正常或担保的帐单
AND bl.IsCurrent = 1		--必须是当期帐单
AND bl.BillStatus != 3		--未完全付款的帐单
AND EXISTS(
	SELECT * FROM BillItem xbi 
	WHERE xbi.BillID = bl.BillID 
	AND xbi.[Subject] IN (3, 4) 
	AND DueAmt > ReceivedAmt) --服务费、担保费未还清的
AND bs.LendingSideKey = 'COMPANY/BHXT_LENDING'	--必须是渤海地区
AND bs.DSeqType = 1			--1类型的时间轴


--搁置所有正在提前清贷中的当期扣失科目（仅渤海）
UPDATE BillItem SET IsShelve = 1 
From Business bs
JOIN Bill bl ON bs.BusinessID = bl.BusinessID
JOIN BillItem bi ON bl.BillID = bi.BillID
WHERE bs.CLoanStatus = 7	--必须是提前清贷中的订单
AND bl.BillStatus <> 3		--未完全付款的帐单
AND bl.IsShelve = 0			--帐单非搁置状态
AND bl.IsCurrent = 1		--当期帐单
AND bl.BillType IN (1, 2)	--必须是正常或担保的帐单
AND bs.DSeqType = 1			--1类型的时间轴
AND bs.ServiceSideKey IN ({1})	--代入服务方
AND bs.LendingSideKey = 'COMPANY/BHXT_LENDING'	--必须是渤海地区
AND bs.IsRepayment = 1		--未清贷的订单
AND bi.Subject IN (21, 22)	--必须是本息或服务扣失科目
