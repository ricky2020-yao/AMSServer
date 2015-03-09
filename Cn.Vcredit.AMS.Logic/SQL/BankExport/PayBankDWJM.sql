--ÍâÃ³
SELECT 
VI.BusinessID,VI.BillMonth,VI.DunLevel,
BJ = SUM(VI.Amt_prin),
LX = SUM(VI.Amt_intr),
BXKS = SUM(VI.Amt_intrbf),
FX = SUM(VI.Amt_ptin),
DunAmount = SUM(VI.Amt_all),
BUS.SavingCard, BUS.SavingUser,
BUS.LoanCapital,BUS.ServiceSideID,BUS.LendingSideID,
BUS.BusinessStatus,BUS.ContractNo,BUS.LoanPeriod,BUS.LoanTime,
BUS.InterestRate,BUS.ConstructSedNo
,p.IdentityNo AS IdenNo
FROM dbo.ViewBill_Level_Lend_Pivot AS VI
JOIN Business AS BUS ON VI.BusinessID = BUS.BusinessID
--JOIN Customer AS CUS ON BUS.CustomerID = CUS.CustomerID
JOIN customer.CustomerInfo CUS ON BUS.BusinessID=CUS.Bid
JOIN customer.Person p ON p.PersonId=CUS.PersonId
WHERE BUS.ServiceSideID = {0} {1} AND BUS.FrozenNo='' 
AND BUS.CLoanStatus NOT IN (5,6,7)
GROUP BY VI.DunLevel,VI.BusinessID,VI.BillMonth,
BUS.SavingCard, BUS.SavingUser,
BUS.LoanCapital,BUS.ServiceSideID,BUS.LendingSideID,
BUS.BusinessStatus,BUS.ContractNo,BUS.LoanPeriod,BUS.LoanTime,
BUS.InterestRate,BUS.ConstructSedNo,
p.IdentityNo
HAVING SUM(VI.Amt_all)>0
ORDER BY VI.BusinessID,VI.DunLevel,VI.BillMonth
