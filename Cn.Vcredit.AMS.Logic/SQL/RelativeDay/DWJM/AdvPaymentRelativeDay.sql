--插入提前清贷账单需扣款列表至PayAccount
INSERT INTO
PayAccount(BusinessID,ContractNo,BillID,AccountNo,AccountName,DueAmount,Cellphone,Remark,AccountID,
CompanyKey,IdenNo,SerialNo,BankKey,CreateTime,IsLock,OnlySubject,PeriodType,PayTraceID,TaskKey,BillMonth)
SELECT
BusinessID=b.BusinessID,a.ContractNo,BillID=0,
AccountNo=a.SavingCard,AccountName=a.SavingUser,
DueAmount = SUM(c.DueAmt-c.ReceivedAmt),Cellphone='',
Remark=a.BusinessStatus,AccountID = 
CASE a.BusinessStatus WHEN 2 THEN a.ServiceSideID ELSE a.LendingSideID END,
CompanyKey = b.CompanyKey,
IdenNo = d.IdenNo,SerialNo='',BankKey = a.BankKey,
CreateTime =getdate(),IsLock=0,0,a.PeriodType,0,'{1}','1'
FROM Business AS a
JOIN Customer AS d ON a.CustomerID= d.CustomerID 
JOIN Bill AS b ON a.BusinessID = b.BusinessID
JOIN BillItem AS c ON b.BillID = c.BillID 
WHERE a.IsRepayment = 1 AND b.BillStatus <> 3 
AND a.BusinessStatus <> 3 AND b.BillType <> 5
AND b.IsShelve = 0 AND c.IsShelve = 0
AND a.CLoanStatus = 7 AND a.FrozenNo=''
AND a.PeriodType = 32 AND a.ServiceSideID = {0}
AND a.ProductType = 4 --外贸小额贷款（无抵押贷款）
AND a.LendingSideKey = 'COMPANY/DWJM_LENDING'
GROUP BY b.BusinessID,a.SavingCard,a.SavingUser,d.Mobile,b.CompanyKey,a.LendingSideID,
d.IdenNo,a.BankKey,a.ContractNo,a.PeriodType,a.ServiceSideID,a.BusinessStatus
HAVING SUM(c.DueAmt-c.ReceivedAmt)>0