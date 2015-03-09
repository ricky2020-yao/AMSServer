SELECT bs.BusinessID,p.PersonName AS CustomerName, bs.ContractNo 
FROM fin.BusinessBasic bs 
JOIN fin.BusinessCurrentStaus bcs 
ON bs.BusinessID = bcs.BusinessID 
--JOIN Customer c ON bs.CustomerID = c.CustomerID
JOIN customer.CustomerInfo c 
ON bs.BusinessID=c.Bid 
JOIN customer.Person p 
ON p.PersonId=c.PersonId 
WHERE bcs.CLoanStatus = 7 
AND bs.ServiceSide = {0} 
AND bs.LendingSide = {1} 