--查询出今天执行相对日任务的订单编号
SELECT bs.BusinessID
FROM Business bs 
WHERE bs.IsRepayment = 1
AND bs.PeriodType = 32
AND bs.BusinessStatus <> 3
AND bs.ResidualCapital > 0
AND bs.ServiceSideID = {0}
AND bs.LendingSideKey IN ('COMPANY/WX_CDWS_LENDING', 'COMPANY/DWJM_LENDING')
AND bs.RelativeDate < '{1}'
AND '{1}' =
CONVERT(DATE,
	DATEADD(
		MONTH,
		DATEDIFF(
			MONTH, 
			bs.RelativeDate, 
			'{1}'
		),
		bs.RelativeDate
	))
