--Ϊ�ɶ��޵�Ѻ����������ʵ�һ������������20������ķ�Ϣ
INSERT INTO PenaltyInt (BusinessID, ReasonID, ToBillID, Amount, IsShelve, CreateTime)
SELECT bl.BusinessID, MAX(bl.BillID), 
ISNULL((SELECT MAX(BillID) FROM Bill WHERE BusinessID = bl.BusinessID
 AND IsCurrent = 1 AND BillType IN (1, 2)), 0),
SUM(bi.DueAmt - bi.ReceivedAmt) * 0.001 *
(DATEPART(DAY, GETDATE()- DATEPART(DAY, GETDATE())) + DATEPART(DAY, GETDATE()) - 21),
0, GETDATE()
FROM Bill bl
JOIN BillItem bi ON bi.BillID = bl.BillID
JOIN Business bs ON bl.BusinessID = bs.BusinessID
WHERE bi.Subject in (1, 2, 3) 
AND bl.BillType in (1, 2) 
AND bl.BillStatus <> 3
AND bs.ServiceSideKey = '{1}'
AND bs.IsRepayment = 1 
AND bl.IsShelve = 0
AND bi.IsShelve = 0
AND bs.BusinessStatus in (1, 2) 
AND bl.LimitTime >= '{0}'
GROUP BY bl.BusinessID

--ɾ�����ɴ������
DELETE dbo.PenaltyInt WHERE ToBillID = 0 OR Amount = 0

--Ϊ������������Ϣ��־���ʵ����·�ϢǷ��
UPDATE BillItem SET Amount = bi.Amount + p.Amount, DueAmt = bi.DueAmt + p.Amount
FROM BillItem bi
JOIN Bill bl ON bi.BillID = bl.BillID
JOIN PenaltyInt p ON bl.BillID = p.ToBillID
WHERE p.CreateTime >= CONVERT(DATE, GETDATE())
AND bl.CompanyKey = '{1}'
AND bi.Subject = 23

--Ϊ������������Ϣ��־�ĵ��ʵ���ӷ�Ϣ��Ŀ
INSERT INTO BillItem (BillID, [Subject], Amount, DueAmt, ReceivedAmt, 
CreateTime, SubjectType, OperatorID, IsCurrent, IsShelve, BusinessID)
SELECT p.ToBillID, 23, bl.TotalAmt, bl.TotalAmt, 0, GETDATE(), 1, 0, 0, 0, 0
FROM PenaltyInt p
JOIN (
	SELECT *, 
		ISNULL(
			(
				SELECT SUM(xpi.Amount) 
				FROM PenaltyInt xpi 
				WHERE xpi.ToBillID = BillID
			),0) AS TotalAmt 
	FROM Bill) bl ON p.ToBillID = bl.BillID
WHERE p.CreateTime >= CONVERT(DATE, GETDATE())
AND bl.CompanyKey = '{1}'
AND NOT EXISTS (
	SELECT * FROM BillItem xbi WHERE xbi.Subject = 23 
	AND xbi.BillID = p.ToBillID)
AND bl.TotalAmt > 0
GROUP BY p.ToBillID, bl.TotalAmt
