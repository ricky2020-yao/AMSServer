UPDATE BUS SET BUS.FrozenNo ='{0}' FROM Business BUS WHERE BUS.BusinessID IN
(SELECT DISTINCT BusinessID FROM dbo.PayAccount WHERE AccountID = '{1}' AND PeriodType = 13)
 AND BUS.FrozenNo='' AND BUS.ServiceSideID ='{1}' AND PeriodType = 13 AND BUS.ProductType = 4