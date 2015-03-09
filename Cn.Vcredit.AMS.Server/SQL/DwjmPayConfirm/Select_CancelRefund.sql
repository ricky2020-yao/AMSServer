SELECT * FROM 
(SELECT b.BusinessID,b.ContractNo,pe.PersonName AS CustomerName,
b.LoanCapital-ISNULL(b.ProceduresAmout,0) AS RealLoanCapital,
ISNULL(c.RefundAmt,b.LoanCapital-ISNULL(b.ProceduresAmout,0)) AS RefundAmt,c.PayDate,c.ReceivedDate,c.PayType,c.CancelTime,
ROW_NUMBER() OVER(ORDER BY c.CancelTime) AS num
FROM finance.CancelRefund c
INNER JOIN dbo.Business b ON c.BusinessID=b.BusinessID
INNER JOIN customer.CustomerInfo r ON b.CustomerID=r.CustId
INNER JOIN customer.Person pe ON r.PersonId=pe.PersonId
{3}
WHERE {0} {1})t
{2}

SELECT COUNT(1) 
FROM finance.CancelRefund c
INNER JOIN dbo.Business b ON c.BusinessID=b.BusinessID
INNER JOIN customer.CustomerInfo r ON b.CustomerID=r.CustId
INNER JOIN customer.Person pe ON r.PersonId=pe.PersonId
{3}
WHERE {0} {1}