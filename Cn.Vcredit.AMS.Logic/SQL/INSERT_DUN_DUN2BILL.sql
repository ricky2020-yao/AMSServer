--将当期催收单改为逾期催收单
UPDATE dun SET dun.IsCurrent = 0,dun.CancelTime = GETDATE() FROM Dun dun
JOIN Business bus on bus.BusinessID = dun.BusinessID
WHERE dun.CancelTime IS NULL AND bus.FrozenNo=''
AND dun.CompanyKey IN ({1}) AND dun.PeriodType = 13 
AND bus.LendingSideKey='COMPANY/BHXT_LENDING'

--生成新一期催收单
INSERT INTO Dun ([BusinessID]
           ,[ContractNo]
           ,[DunerID]
           ,[OperationerID]
           ,[CompanyKey]
           ,[DunNumber]
           ,[DunAmount]
           ,[OverMonth]
           ,[CreateTime]
           ,[CustomerID]
           ,[DunMonth]
           ,[IsCurrent]
           ,[BusinessStatus]
           ,[LawsuitStatus]
           ,[PeriodType]
           ,[BranchKey])
SELECT bs.BusinessID, bs.ContractNo, 0, {0}, bs.ServiceSideKey,
0, case bs.BusinessStatus when 3 then 
(SELECT SUM(BI.DueAmt - BI.ReceivedAmt) FROM BillItem BI
JOIN Bill BL ON BI.BillID = BL.BillID
WHERE BL.BusinessID = bs.BusinessID AND BL.IsShelve=0 AND BL.BillType<>5 
AND BI.IsShelve = 0)
else bs.OverAmount end, bs.OverMonth, GETDATE(), bs.CustomerID, 
CONVERT(NCHAR(4),DATEPART(YYYY, GETDATE()))+ '/' + CONVERT(NCHAR(2),
RIGHT('0'+ltrim(DATEPART(MM, GETDATE())),2)), 1,
bs.BusinessStatus,bs.LawsuitStatus,bs.PeriodType,bs.BranchKey
FROM Business bs
WHERE bs.ServiceSideKey IN ({1})
AND bs.LendingSideKey = 'COMPANY/BHXT_LENDING'
AND bs.IsRepayment = 1 AND bs.OverMonth > 0 AND bs.FrozenNo=''

--生成新一期催收单关联的帐单
INSERT INTO Dun2Bill([DunID]
           ,[BillID]
           ,[BillType]
           ,[HistoryBillStatus]
           ,[CreateTime])
SELECT dun.DunID, bl.BillID,bl.BillType, bl.BillStatus, GETDATE()
FROM Bill bl
JOIN Dun dun ON bl.BusinessID = dun.BusinessID
JOIN Business bs ON bl.BusinessID = bs.BusinessID
WHERE dun.CancelTime IS NULL 
AND bl.IsShelve = 0
AND bl.BillStatus <> 3
AND bl.BillType IN (1, 2)
AND dun.CompanyKey IN ({1})
AND dun.PeriodType = 13
AND bs.FrozenNo=''
AND bs.LendingSideKey = 'COMPANY/BHXT_LENDING'

--保存欠费帐单科目快照
INSERT INTO DunItem (BillItemID, BillID, Subject, Amount, DunAmt, ReceivedAmt,
CreateTime, FullPaidTime, SubjectType, OperatorID, Dun2BillID)
SELECT bi.BillItemID, bi.BillID, bi.Subject, bi.Amount, bi.DueAmt, bi.ReceivedAmt,
bi.CreateTime, bi.FullPaidTime, bi.SubjectType, bi.OperatorID, db.Dun2BillID
FROM Dun2Bill db
JOIN Dun ON db.DunID = Dun.DunID
JOIN BillItem bi ON db.BillID = bi.BillID
JOIN Business bs ON bs.BusinessID = Dun.BusinessID
AND dun.CancelTime IS NULL AND CompanyKey IN ({1})
AND bs.LendingSideKey = 'COMPANY/BHXT_LENDING'
AND dun.PeriodType = 13
AND bs.FrozenNo=''