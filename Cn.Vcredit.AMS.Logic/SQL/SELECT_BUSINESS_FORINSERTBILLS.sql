--判断放贷日是否大于当前时间，大于则不做任何操作
--判断订单是否还拖欠本息，如有则不管订单是否已至自然到期月，仍创建帐单。无则不再创建帐单
--不导出所有诉讼类型的订单
SELECT b.BusinessID 
FROM Business b
WHERE b.DSeqType = 1
AND b.PeriodType = 13 
AND b.IsRepayment = 1
AND b.BusinessStatus <> 3
AND b.LendingSideKey = 'COMPANY/BHXT_LENDING'
AND b.ServiceSideKey IN ({0})
AND b.LoanTime < DATEADD(DAY, -DATEPART(DAY, GETDATE()) + 1, 
	CAST(GETDATE() AS DATE))
AND b.ResidualCapital > 0
ORDER BY b.LoanTime DESC