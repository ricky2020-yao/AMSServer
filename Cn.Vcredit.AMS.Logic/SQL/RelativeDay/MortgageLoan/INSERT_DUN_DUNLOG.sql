--当期催收单汇总统计信息
INSERT INTO DunLog (DunCount, DunerCount, DunMonth, DunAmount, CompanyKey, 
OperatorID, CreateTime)
SELECT COUNT(DunID), {0}, MAX(DunMonth), SUM(DunAmount), '{2}', 0, GETDATE()
FROM Dun dun JOIN Business bus on dun.BusinessID =bus.BusinessID
WHERE dun.IsCurrent = 1 AND dun.PeriodType = 32 AND  bus.ServiceSideID= '{1}' 
