--当期催收单汇总统计信息
INSERT INTO DunLog (DunCount, DunerCount, DunMonth, DunAmount, CompanyKey, 
OperatorID, CreateTime)
SELECT COUNT(DunID), {2}, MAX(DunMonth), SUM(DunAmount), '{3}', {0}, GETDATE()
FROM Dun
JOIN Business bs ON Dun.BusinessID = bs.BusinessID
WHERE dun.CancelTime IS NULL
AND Dun.CompanyKey IN ({1})
AND bs.LendingSideKey = 'COMPANY/BHXT_LENDING'
AND bs.FrozenNo=''