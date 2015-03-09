UPDATE BUS SET BUS.FrozenNo ='{0}' FROM Business BUS WHERE BUS.BusinessID IN
(SELECT DISTINCT BusinessID FROM dbo.PayAccount WHERE AccountID = '{1}')
 AND BUS.FrozenNo='' AND BUS.LendingSideID ='{1}' AND PeriodType = 13