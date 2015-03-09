Insert into
PayAccount(BusinessID,ContractNo,BillID,AccountNo,AccountName,DueAmount,Cellphone,Remark,AccountID,
CompanyKey,IdenNo,SerialNo,BankKey,CreateTime,IsLock,OnlySubject,PeriodType,PayTraceID,TaskKey)
SELECT
BusinessID=b.BusinessID,ContractNo = bus.ContractNo,BillID=b.BillID,
AccountNo=bus.SavingCard,AccountName=bus.SavingUser,
DueAmount = SUM(a.DueAmt-a.ReceivedAmt),Cellphone='',
Remark='',AccountID={0},CompanyKey = b.CompanyKey,
IdenNo = c.IdenNo,SerialNo='',
BankKey = bus.BankKey,CreateTime =getdate(),IsLock=0,OnlySubject = 0,PeriodType = 13,0,'{2}'
FROM BillItem as a 
INNER JOIN Bill as b on a.BillID =b.BillID
INNER JOIN Business as bus on b.BusinessID = bus.BusinessID
INNER JOIN Customer as c on bus.CustomerID = c.CustomerID
where bus.IsRepayment=1 and  b.BillStatus!=3 and a.IsShelve = 0
and bus.CLoanStatus not in (5,6,7) and b.IsShelve=0 and bus.FrozenNo='' 
and b.BillType!=5 and bus.ProductType = 4 AND
(bus.ServiceSideID ={0} and bus.LendingSideKey='COMPANY/BHXT_LENDING' and bus.BusinessStatus<>3)
{1}
Group by bus.SavingUser,bus.SavingCard,b.CompanyKey,b.BillID,
b.BusinessID,c.Mobile,bus.BankKey,c.IdenNo,b.BillMonth,bus.ContractNo
having SUM(a.DueAmt-a.ReceivedAmt)>0 order by b.BusinessID,b.BillMonth 