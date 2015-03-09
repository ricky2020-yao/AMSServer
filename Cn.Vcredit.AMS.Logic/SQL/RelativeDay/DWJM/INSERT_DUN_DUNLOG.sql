--当期催收单汇总统计信息
INSERT INTO DunLog (DunCount, DunerCount, DunMonth, DunAmount, CompanyKey, 
OperatorID, CreateTime)
SELECT COUNT(DunID), {0}, MAX(DunMonth), SUM(DunAmount), '{1}', 0, GETDATE()
FROM Dun 
WHERE IsCurrent = 1 AND PeriodType = 32 AND CompanyKey = '{1}'
