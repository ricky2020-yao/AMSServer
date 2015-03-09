--²³º£
SELECT 
VI.BusinessID,VI.DunLevel,VI.BillMonth ,
BJ = SUM(VI.Amt_prin),
LX = SUM(VI.Amt_intr),
BXKS = SUM(VI.Amt_intrbf),
FX = SUM(VI.Amt_ptin),
FWF = 0.00,
DBF = 0.00,
FWFKS = 0.00,
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

WHERE BUS.ServiceSideID = {0} {1} AND VI.DunLevel IN (10,22)
AND BUS.CLoanStatus NOT IN (5,6,7) AND BUS.FrozenNo='' 
GROUP BY VI.DunLevel,VI.BusinessID,VI.BillMonth,
BUS.SavingCard, BUS.SavingUser,
BUS.LoanCapital,BUS.ServiceSideID,BUS.LendingSideID,
BUS.BusinessStatus,BUS.ContractNo,BUS.LoanPeriod,BUS.LoanTime,
BUS.InterestRate,BUS.ConstructSedNo,
p.IdentityNo HAVING SUM(VI.Amt_all)>0
UNION ALL
SELECT 
VI.BusinessID ,VI.DunLevel,BillMonth='2012/11',
BJ = SUM(VI.Amt_prin),
LX = SUM(VI.Amt_intr),
BXKS = SUM(VI.Amt_intrbf),
FX = SUM(VI.Amt_ptin),
FWF = 0.00,
DBF = 0.00,
FWFKS = 0.00,
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

WHERE BUS.ServiceSideID = {2} {3} AND VI.DunLevel IN (21,23)
AND BUS.CLoanStatus NOT IN (5,6,7) AND BUS.FrozenNo='' 
GROUP BY VI.DunLevel,VI.BusinessID,
BUS.SavingCard, BUS.SavingUser,
BUS.LoanCapital,BUS.ServiceSideID,BUS.LendingSideID,
BUS.BusinessStatus,BUS.ContractNo,BUS.LoanPeriod,BUS.LoanTime,
BUS.InterestRate,BUS.ConstructSedNo,
p.IdentityNo HAVING SUM(VI.Amt_all)>0
ORDER BY BusinessID,DunLevel,VI.BillMonth