Insert into
PayAccount(BusinessID,ContractNo,BillID,AccountNo,AccountName,DueAmount,Cellphone,Remark,AccountID,
CompanyKey,IdenNo,SerialNo,BankKey,CreateTime,IsLock,OnlySubject,PeriodType,PayTraceID,TaskKey)
SELECT
BusinessID=b.BusinessID,ContractNo = bus.ContractNo,BillID=b.BillID,
AccountNo=bus.SavingCard,AccountName=bus.SavingUser,
DueAmount = SUM(a.DueAmt-a.ReceivedAmt),Cellphone='',
Remark='',AccountID={0},CompanyKey = b.CompanyKey,
IdenNo = c.IdenNo,SerialNo='',BankKey = bus.BankKey,
CreateTime =getdate(),IsLock=0,OnlySubject = 0,PeriodType = 13
FROM BillItem as a 
INNER JOIN Bill as b on a.BillID =b.BillID
INNER JOIN Business as bus on b.BusinessID = bus.BusinessID
INNER JOIN Customer as c on bus.CustomerID = c.CustomerID
where bus.IsRepayment=1 and  b.BillStatus!=3 and a.IsShelve = 0
and bus.CLoanStatus not in (5,6,7) and bus.BusinessStatus=1
and b.IsShelve=0 and bus.LendingSideID={0} {1}
and bus.FrozenNo='' and b.BillType!=5 and bus.ServiceSideKey !=''
Group by bus.SavingUser,bus.SavingCard,b.CompanyKey,b.BillID,
b.BusinessID,c.Mobile,bus.BankKey,c.IdenNo,b.BillMonth,bus.ContractNo
having SUM(a.DueAmt-a.ReceivedAmt)>0 order by b.BusinessID,b.BillMonth 