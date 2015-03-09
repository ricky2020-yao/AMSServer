--当期催收单张数计算
SELECT COUNT(*) FROM Dun
WHERE Dun.IsCurrent = 1 AND Dun.BusinessID IN ({0})
