--一次性生成逾期所欠本息总额3%的罚息，计入当期帐单中，插入日志表（仅渤海）
INSERT INTO PenaltyInt (BusinessID, ReasonID, ReasonItemID, ToBillID, Amount, IsShelve, CreateTime)
SELECT bl.BusinessID, bl.BillID, 0, cbl.BillID, bi.Amount * 0.03, cbl.IsShelve, GETDATE()
FROM Bill bl
JOIN BillItem bi ON bl.BillID = bi.BillID
JOIN Business bs ON bl.BusinessID = bs.BusinessID
JOIN Bill cbl ON bl.BusinessID = cbl.BusinessID AND cbl.IsCurrent = 1 AND cbl.BillType IN (1, 2)
WHERE bi.[Subject] in (1, 2) 
AND bl.IsCurrent = 0 
AND bl.BillType in (1, 2) 
AND bl.BillStatus <> 3
AND bs.IsRepayment = 1 
AND bs.ServiceSideKey IN ({1})
AND bs.LendingSideKey = 'COMPANY/BHXT_LENDING'
AND bs.DSeqType = 1
AND bi.ReceivedAmt = 0

--删除生成错误的行
DELETE dbo.PenaltyInt WHERE ToBillID = 0 OR Amount = 0

--根据日志表，在当前账单中插入新罚息
INSERT INTO BillItem (BillID, [Subject], Amount, DueAmt, ReceivedAmt, CreateTime, 
							SubjectType, OperatorID, IsCurrent, IsShelve, BusinessID)
SELECT p.ToBillID, 23, SUM(p.Amount), SUM(p.Amount), 0, GETDATE(), 0, {0}, 0, 0, 0
FROM PenaltyInt p
JOIN Bill b ON p.ToBillID = b.BillID
JOIN Business bs ON b.BusinessID = bs.BusinessID
WHERE bs.DSeqType = 1
AND bs.ServiceSideKey IN ({1})
AND bs.LendingSideKey = 'COMPANY/BHXT_LENDING'
AND bs.PeriodType = 13
AND bs.IsRepayment = 1
AND b.IsCurrent = 1
GROUP BY p.ToBillID HAVING SUM(Amount) > 0

--如无任何科目生成，删除该期帐单
DELETE Bill
FROM Bill bl 
WHERE NOT EXISTS (SELECT * FROM BillItem bi WHERE bi.BillID = bl.BillID)

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
AND bi.Subject = 23	--必须是罚息科目

