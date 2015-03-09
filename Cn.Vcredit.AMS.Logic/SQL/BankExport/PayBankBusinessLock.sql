update Business 
set FrozenNo='{0}'
where BusinessID in
(
	select a.BusinessID from Business as a  
	--inner join Customer as d on a.CustomerID = d.CustomerID 
	inner join customer.CustomerInfo d ON a.BusinessID=d.Bid
	inner join Bill as b on a.BusinessID = b.BusinessID 
	inner join BillItem as c on b.BillID = c.BillID  
	where a.IsRepayment=1 and  b.BillStatus<>3  
	and a.BusinessStatus<>3 and a.FrozenNo=''
	and a.CLoanStatus not in (5,6,7) and a.ServiceSideID={1}
	and a.LendingSideID = {2}
	and b.BillType<>5 and b.IsShelve=0 
	and c.DueAmt-c.ReceivedAmt>0 group by a.BusinessID
	having SUM(c.DueAmt-c.ReceivedAmt)>0
)
