select BillID = a.BillID,BillItemID = a.BillItemID, Amount = a.DueAmt - a.ReceivedAmt,
ReceivedType=CONVERT(tinyint,{0}),PayID = {1},ReceivedTime = CONVERT(datetime,'{2}'),
CreateTime =  GETDATE(),OperatorID = {3},
 Explain = '{4}' from BillItem as a inner join Bill as b on a.BillID = b.BillID 
inner join Business as c on b.BusinessID = c.BusinessID 
where b.IsShelve = 0 and a.IsShelve = 0 and a.DueAmt - a.ReceivedAmt > 0
and BusinessID in ({5})