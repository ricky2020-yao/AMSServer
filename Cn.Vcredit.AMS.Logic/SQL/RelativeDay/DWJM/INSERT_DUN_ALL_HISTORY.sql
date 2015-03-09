--保存欠费帐单科目快照
INSERT INTO DunItem (BillItemID, BillID, Subject, Amount, DunAmt, ReceivedAmt,
CreateTime, FullPaidTime, SubjectType, OperatorID, Dun2BillID)
SELECT bi.BillItemID, bi.BillID, bi.Subject, bi.Amount, bi.DueAmt, bi.ReceivedAmt,
GETDATE(), bi.FullPaidTime, bi.SubjectType, bi.OperatorID, db.Dun2BillID
FROM Dun2Bill db
JOIN Dun ON db.DunID = Dun.DunID
JOIN BillItem bi ON db.BillID = bi.BillID
AND dun.BusinessID in ({0})
AND dun.IsCurrent = 1
