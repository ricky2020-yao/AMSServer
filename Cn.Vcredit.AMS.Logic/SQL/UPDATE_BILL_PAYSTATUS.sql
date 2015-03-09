--2014年6月13日 曹贝 Modify 空白账单状态设置为未付款
UPDATE Bill SET 
BillStatus = 
(CASE WHEN (SELECT SUM(DueAmt-ReceivedAmt) From BillItem WHERE BillID = Bill.BillID AND IsShelve = 0) = 0 THEN 3 
		WHEN (SELECT ISNULL(SUM(ReceivedAmt),0) From BillItem WHERE BillID = Bill.BillID) = 0 THEN 1 
		ELSE 2 END)
FROM Bill JOIN Business ON Bill.BusinessID = Business.BusinessID
WHERE Business.ServiceSideKey IN ({0}) AND Business.IsRepayment = 1