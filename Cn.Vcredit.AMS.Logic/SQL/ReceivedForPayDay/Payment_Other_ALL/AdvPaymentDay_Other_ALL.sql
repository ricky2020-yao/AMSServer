INSERT INTO
PayAccount(BusinessID,ContractNo,BillID,AccountNo,AccountName,DueAmount,Cellphone,Remark,AccountID,
CompanyKey,IdenNo,SerialNo,BankKey,CreateTime,IsLock,OnlySubject,PeriodType,PayTraceID,TaskKey)
SELECT
BusinessID=b.BusinessID,bus.ContractNo,BillID=0,AccountNo=bus.SavingCard,
AccountName=bus.SavingUser,DueAmount = SUM(c.DueAmt-c.ReceivedAmt),
Cellphone='',Remark='',AccountID={0},CompanyKey = b.CompanyKey,
IdenNo = d.IdenNo,SerialNo='',BankKey = bus.BankKey,
CreateTime =getdate(),IsLock=0,OnlySubject = 0,PeriodType = 13,0,'{1}'
from Business as bus
inner join Customer as d on bus.CustomerID= d.CustomerID 
inner join Bill as b on bus.BusinessID = b.BusinessID
inner join BillItem as c on b.BillID = c.BillID 
where bus.IsRepayment=1 and  b.BillStatus<>3 and b.BillType<>5
and b.IsShelve = 0 and c.IsShelve = 0
and bus.CLoanStatus =7 and bus.FrozenNo='' AND bus.ProductType = 4
and (bus.ServiceSideID ={0} and bus.LendingSideKey='COMPANY/BHXT_LENDING' and bus.BusinessStatus<>3)
GROUP BY b.BusinessID,bus.SavingCard,bus.SavingUser,d.Mobile,b.CompanyKey,
d.IdenNo,bus.BankKey,bus.ContractNo HAVING SUM(c.DueAmt-c.ReceivedAmt)>0