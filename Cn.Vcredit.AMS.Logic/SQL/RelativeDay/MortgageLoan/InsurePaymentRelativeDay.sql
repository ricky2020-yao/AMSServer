Insert into
dbo.PayAccount
SELECT
BusinessID=b.BusinessID,ContractNo = bus.ContractNo,BillID= -1,
AccountNo=bus.SavingCard,AccountName=bus.SavingUser,
DueAmount = SUM(a.DueAmt-a.ReceivedAmt),Cellphone='',Remark='',
AccountID= CASE bus.BusinessStatus WHEN 2 THEN bus.ServiceSideID ELSE bus.LendingSideID END,
CompanyKey = b.CompanyKey,
IdenNo = c.IdenNo,SerialNo='',BankKey = bus.BankKey,
CreateTime =getdate(),IsLock=0,OnlySubject = 40,PeriodType = 32,
0,'{1}'
FROM BillItem as a 
INNER JOIN Bill as b on a.BillID =b.BillID
INNER JOIN Business as bus on b.BusinessID = bus.BusinessID
INNER JOIN Customer as c on bus.CustomerID = c.CustomerID
where bus.IsRepayment=1 and  b.BillStatus!=3 and a.IsShelve = 0
and bus.CLoanStatus not in (5,6,7) and bus.BusinessStatus=1
and b.IsShelve=0 and bus.ServiceSideID = {0}
and bus.ProductType = 2 --µÖÑº´û¿î£¨³µ´û£©
and bus.FrozenNo='' and b.BillType<>5 and bus.ServiceSideKey <>''
and a.Subject = 37
Group by bus.SavingUser,bus.SavingCard,b.CompanyKey,
b.BusinessID,c.Mobile,bus.BankKey,c.IdenNo,b.BillMonth,bus.ContractNo
,bus.BusinessStatus,bus.ServiceSideID,bus.LendingSideID
having SUM(a.DueAmt-a.ReceivedAmt)>0 order by b.BusinessID,b.BillMonth 