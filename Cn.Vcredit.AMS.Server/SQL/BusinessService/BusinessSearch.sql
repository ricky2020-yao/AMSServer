SELECT * FROM 
(SELECT b.*,
p.PersonName,
p.IdentityNo,
ROW_NUMBER() OVER(ORDER BY b.BusinessID DESC) AS num
FROM dbo.Business b
INNER JOIN customer.CustomerInfo c ON b.BusinessID=c.bid
INNER JOIN customer.Person p ON c.PersonID=p.PersonID
WHERE 1=1 {0})t
{1}

SELECT COUNT(1) 
FROM dbo.Business b
INNER JOIN customer.CustomerInfo c ON b.BusinessID=c.bid
INNER JOIN customer.Person p ON c.PersonID=p.PersonID
WHERE 1=1 {0}