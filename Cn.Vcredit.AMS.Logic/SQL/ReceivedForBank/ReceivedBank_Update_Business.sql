update a set a.FullPaidTime = GETDATE(),a.ReceivedAmt = DueAmt
from BillItem as a 
inner join Bill as b on a.BillID = b.BillID
inner join Business as c on b.BusinessID = c.BusinessID
INNER JOIN BankDunLevel AS D ON D.BillItemSubject = a.Subject
AND D.IsCurrent = a.IsCurrent AND D.CompanyKey = C.LendingSideKey
where c.BusinessID in ({0}) and b.IsShelve = 0 and a.IsShelve = 0 
and a.DueAmt - a.ReceivedAmt > 0 and b.BillType<>5 AND D.DunLevel = {1}  