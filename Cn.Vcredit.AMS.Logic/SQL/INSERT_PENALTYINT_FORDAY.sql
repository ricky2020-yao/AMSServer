--为逾期帐单参与罚息生成的科目分别生成罚息（本金、利息、管理费）
INSERT INTO PenaltyInt (BusinessID, ReasonID, ReasonItemID, ToBillID, Amount, IsShelve, CreateTime)
SELECT bl.BusinessID, bl.BillID, bi.BillItemID, 
	ISNULL(
		(
			SELECT BillID 
			FROM Bill 
			WHERE BusinessID = bl.BusinessID AND IsCurrent = 1
		),0),
	(bi.DueAmt - bi.ReceivedAmt) * 0.001 * 
		DATEDIFF(DAY, (
			CASE WHEN bl.LimitTime >= '{0}' 
			THEN bl.LimitTime ELSE '{0}' END),
			GETDATE()) - bi.PenaltyIntAmt,
0, GETDATE()
FROM Bill bl
JOIN BillItem bi ON bl.BillID = bi.BillID
JOIN Business bs ON bl.BusinessID = bs.BusinessID
WHERE bl.BillType IN (1, 2)
AND bl.BillStatus <> 3
AND bi.Subject IN (1, 2, 3)
AND bl.IsCurrent = 0
AND bl.CompanyKey = '{1}'
AND bl.IsShelve = 0 
AND bi.IsShelve = 0 
AND bs.BusinessStatus IN (1, 2)
AND bs.IsRepayment = 1
AND bi.DueAmt > bi.ReceivedAmt
ORDER BY bl.BusinessID DESC

--为今日增加过罚息的科目进行分别统计
UPDATE BillItem SET	PenaltyIntAmt = pbi.PenaltyIntAmt
FROM BillItem bi
JOIN 
(SELECT p.ReasonItemID, ISNULL(
	(
		SELECT SUM(xp.Amount) FROM PenaltyInt xp WHERE xp.ReasonItemID = p.ReasonItemID
	),0) as PenaltyIntAmt
FROM PenaltyInt p
WHERE p.CreateTime > CONVERT(DATE, GETDATE())
GROUP BY p.ReasonItemID) pbi
ON bi.BillItemID = pbi.ReasonItemID
WHERE pbi.PenaltyIntAmt > 0

--更新当期帐单罚息科目
UPDATE BillItem SET	DueAmt = Amount - DueAmt + pbl.CurPIntAmt, Amount = pbl.CurPIntAmt
FROM BillItem bi
JOIN 
(SELECT p.ToBillID, ISNULL(
	(
		SELECT SUM(xp.Amount) FROM PenaltyInt xp WHERE xp.ToBillID = p.ToBillID
	),0) as CurPIntAmt
FROM PenaltyInt p
WHERE p.CreateTime > CONVERT(DATE, GETDATE())
GROUP BY p.ToBillID) pbl
ON bi.BillID = pbl.ToBillID AND bi.Subject = 23
WHERE pbl.CurPIntAmt > 0
