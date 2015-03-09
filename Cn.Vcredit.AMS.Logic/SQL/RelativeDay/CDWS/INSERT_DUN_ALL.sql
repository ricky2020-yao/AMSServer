--将当天相对日扣款逾期催收单改为逾期催收单
UPDATE Dun SET Dun.IsCurrent = 0,Dun.CancelTime = GETDATE()
FROM Dun JOIN Business bus ON bus.BusinessID = Dun.BusinessID
WHERE bus.FrozenNo = '' AND DUN.IsCurrent = 1 AND DUN.BusinessID IN ({0})

--更新在催收周期内已完成清贷的相对日订单
--UPDATE Dun SET IsCurrent = 0
--FROM Dun d
--JOIN Business bs ON d.BusinessID = bs.BusinessID
--WHERE bs.IsRepayment = 0 
--	AND d.IsCurrent = 1
--	AND bs.PeriodType = 32
--	AND bs.RelativeDate < CONVERT(DATE, GETDATE())
--	AND bs.FrozenNo=''
--	AND CONVERT(DATE, GETDATE()) =
--	CONVERT(DATE,
--		DATEADD(
--			MONTH,
--			DATEDIFF(
--				MONTH, 
--				bs.RelativeDate, 
--				GETDATE()
--			),
--			bs.RelativeDate
--		))

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
SELECT bs.BusinessID, bs.ContractNo, 0, 0, bs.ServiceSideKey,
0, case bs.BusinessStatus when 3 then 
(SELECT SUM(BI.DueAmt - BI.ReceivedAmt) FROM BillItem BI
JOIN Bill BL ON BI.BillID = BL.BillID
WHERE BL.BusinessID = bs.BusinessID AND BL.IsShelve=0 AND BL.BillType<>5 
AND BI.IsShelve = 0)
else bs.OverAmount+bs.CurrentOverAmount end, bs.OverMonth, GETDATE(), bs.CustomerID, 
CONVERT(NCHAR(4),DATEPART(YYYY, GETDATE()))+ '/' + CONVERT(NCHAR(2),
RIGHT('0'+ltrim(DATEPART(MM, GETDATE())),2)), 1,
bs.BusinessStatus,bs.LawsuitStatus,bs.PeriodType,bs.BranchKey
FROM Business bs
WHERE bs.BusinessID in ({0})
AND bs.IsRepayment = 1 AND bs.OverAmount+bs.CurrentOverAmount > 0
AND bs.OverMonth>0 AND bs.FrozenNo=''


--生成新一期催收单关联的帐单
INSERT INTO Dun2Bill([DunID]
           ,[BillID]
           ,[BillType]
           ,[HistoryBillStatus]
           ,[CreateTime])
SELECT dun.DunID, bl.BillID,bl.BillType, bl.BillStatus, GETDATE()
FROM Bill bl
JOIN Dun dun ON bl.BusinessID = dun.BusinessID
JOIN Business bus ON bus.BusinessID = dun.BusinessID
WHERE dun.IsCurrent = 1 
AND bl.IsShelve = 0
AND bl.BillStatus <> 3
AND bl.BillType IN (1, 2)
AND bus.FrozenNo=''
AND dun.BusinessID in ({0})


--保存欠费帐单科目快照
INSERT INTO DunItem (BillItemID, BillID, Subject, Amount, DunAmt, ReceivedAmt,
CreateTime, FullPaidTime, SubjectType, OperatorID, Dun2BillID)
SELECT bi.BillItemID, bi.BillID, bi.Subject, bi.Amount, bi.DueAmt, bi.ReceivedAmt,
GETDATE(), bi.FullPaidTime, bi.SubjectType, bi.OperatorID, db.Dun2BillID
FROM Dun2Bill db
JOIN Dun ON db.DunID = Dun.DunID
JOIN Business bus ON bus.BusinessID = Dun.BusinessID
JOIN BillItem bi ON db.BillID = bi.BillID
AND dun.BusinessID in ({0})
AND dun.IsCurrent = 1
