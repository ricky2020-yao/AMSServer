UPDATE BillItem SET FullPaidTime = GETDATE(),ReceivedAmt = ReceivedAmt+{0} 
WHERE BillItemID ={1}