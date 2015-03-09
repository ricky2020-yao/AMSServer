--PRD003 设置延迟转担保的客户的订单状态为担保,并设置担保时间
UPDATE dbo.Business SET BusinessStatus=2,ToGuaranteeTime=GETDATE()
FROM dbo.Business b
INNER JOIN dun.GuaranteeItem g ON b.BusinessID=g.BusinessID
WHERE b.BusinessID IN({0}) 
AND b.BusinessStatus = 1
AND b.IsRepayment = 1