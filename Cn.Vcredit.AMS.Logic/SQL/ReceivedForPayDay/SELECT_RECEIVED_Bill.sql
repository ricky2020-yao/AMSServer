select SUM(a.DueAmt-a.ReceivedAmt) from dbo.BillItem as a 
inner join dbo.Bill as b on a.BillID = b.BillID
where a.IsShelve=0 and b.IsShelve=0 and b.BillID={0} and b.BillType!=5