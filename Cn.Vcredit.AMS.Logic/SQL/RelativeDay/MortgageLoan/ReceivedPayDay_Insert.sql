insert into 
Received(BillID,BillItemID,Amount,ReceivedType,PayID,ReceivedTime,CreateTime,OperatorID,Explain,DeductionID)
select a.BillID,a.BillItemID, a.DueAmt - a.ReceivedAmt,
{0},PayID = {1}, CONVERT(datetime,'{2}'), GETDATE(),
{3}, '{4}',0 from BillItem as a inner join Bill as b on a.BillID = b.BillID
inner join Business as c on b.BusinessID = c.BusinessID 
where b.BillID in ({5}) and b.IsShelve = 0 and a.IsShelve = 0 
and a.DueAmt - a.ReceivedAmt > 0 and b.BillType<>5 and a.Subject <> 37 {6}