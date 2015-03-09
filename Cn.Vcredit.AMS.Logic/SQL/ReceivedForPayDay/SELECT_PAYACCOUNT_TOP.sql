SELECT * FROM PayAccount AS a WHERE a.BillID = (
SELECT TOP 1 p.BillID FROM PayAccount as p WHERE p.BusinessID = a.BusinessID
AND p.CompanyKey = '{0}')