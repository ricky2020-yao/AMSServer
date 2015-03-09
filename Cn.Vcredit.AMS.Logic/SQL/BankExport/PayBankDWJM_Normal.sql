--外贸正常客户
SELECT 
VI.BusinessID,VI.BillMonth,VI.DunLevel,
BJ = SUM(VI.Amt_prin),
LX = SUM(VI.Amt_intr),
BXKS = SUM(VI.Amt_intrbf),
FX = SUM(VI.Amt_ptin),
FWF = SUM(VI.Amt_serf),
DBF = SUM(VI.Amt_guarf),
FWFKS = SUM(VI.Amt_serbf),
DunAmount = SUM(VI.Amt_all),
BUS.SavingCard, BUS.SavingUser,
BUS.LoanCapital,BUS.ServiceSideID,BUS.LendingSideID,
BUS.BusinessStatus,BUS.ContractNo,BUS.LoanPeriod,BUS.LoanTime,
BUS.ServiceRate,BUS.ConstructSedNo
,CUS.IdenNo
FROM  ViewBill_Level_DWJM_Excel_Pivot AS VI
JOIN Business AS BUS ON VI.BusinessID = BUS.BusinessID
JOIN Customer AS CUS ON BUS.CustomerID = CUS.CustomerID
WHERE BUS.ServiceSideID = {0} {1} AND BUS.FrozenNo='' 
AND BUS.CLoanStatus NOT IN (5,6,7) 
AND BUS.BusinessStatus =1
GROUP BY VI.BillID,VI.DunLevel,VI.BusinessID,VI.BillMonth,
BUS.SavingCard, BUS.SavingUser,
BUS.LoanCapital,BUS.ServiceSideID,BUS.LendingSideID,
BUS.BusinessStatus,BUS.ContractNo,BUS.LoanPeriod,BUS.LoanTime,
BUS.ServiceRate,BUS.ConstructSedNo,CUS.IdenNo
HAVING SUM(VI.Amt_all)>0
ORDER BY VI.BusinessID,VI.DunLevel,VI.BillMonth