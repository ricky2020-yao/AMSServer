update a set a.FullPaidTime = GETDATE(),a.ReceivedAmt = DueAmt 
from BillItem as a,Bill as b where a.BillID = b.BillID and 
a.DueAmt- a.ReceivedAmt>0 and b.IsShelve = 0 and a.IsShelve = 0 
and b.BillID in ({0}) and b.BillType!=5 and a.Subject <> 37 {1}
