--插入正常账单需扣款列表至PayAccount
INSERT INTO
PayAccount(BusinessID,ContractNo,BillID,AccountNo,AccountName,DueAmount,Cellphone,Remark,AccountID,
CompanyKey,IdenNo,SerialNo,BankKey,CreateTime,IsLock,OnlySubject,PeriodType,PayTraceID,TaskKey,BillMonth)
SELECT
BusinessID=b.BusinessID,ContractNo = bus.ContractNo,
BillID=b.BillID,AccountNo=bus.SavingCard,
AccountName=bus.SavingUser,DueAmount = SUM(a.DueAmt-a.ReceivedAmt),
Cellphone='',Remark=bus.BusinessStatus,AccountID =
CASE bus.BusinessStatus WHEN 2 THEN bus.ServiceSideID ELSE bus.LendingSideID END
,CompanyKey = b.CompanyKey,IdenNo = c.IdenNo,SerialNo='',
BankKey = bus.BankKey,
CreateTime =GETDATE(),IsLock=0,0,bus.PeriodType,0,'{1}',b.BillMonth
FROM BillItem AS a 
JOIN Bill AS b ON a.BillID =b.BillID
JOIN Business AS bus ON b.BusinessID = bus.BusinessID
JOIN Customer AS c ON bus.CustomerID = c.CustomerID
WHERE bus.IsRepayment=1 AND  b.BillStatus<>3 AND a.IsShelve = 0
AND bus.CLoanStatus NOT IN (5,6,7) AND bus.BusinessStatus<>3 
AND b.IsShelve=0 AND PeriodType = 32 AND bus.ServiceSideID = {0}
AND bus.LendingSideKey = 'COMPANY/DWJM_LENDING'
AND bus.ProductType = 4 --外贸小额贷款（无抵押贷款）
AND bus.FrozenNo='' AND b.BillType<>5 AND bus.ServiceSideKey <>''
GROUP BY bus.SavingUser,bus.SavingCard,b.CompanyKey,b.BillID,bus.PeriodType,
b.BusinessID,c.Mobile,bus.BankKey,c.IdenNo,b.BillMonth,bus.ContractNo,
bus.ServiceSideID,bus.BusinessStatus,bus.LendingSideID,b.BillMonth
HAVING SUM(a.DueAmt-a.ReceivedAmt)>0 ORDER BY b.BusinessID,b.BillMonth 