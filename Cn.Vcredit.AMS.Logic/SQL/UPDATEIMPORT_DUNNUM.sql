UPDATE d SET DunNumber = e.RowID 
FROM Dun d
JOIN (SELECT ROW_NUMBER () OVER 
(PARTITION BY x.DunerID ORDER BY CONVERT(date,x.CreateTime),p.PersonName ASC) AS RowID, x.DunID, x.DunerID
FROM Dun x
--JOIN Customer c ON x.CustomerID = c.CustomerID
INNER JOIN customer.CustomerInfo c ON x.CustomerID=c.CustId
INNER JOIN customer.Person p ON p.PersonId=C.PersonId

WHERE x.IsCurrent = 1
) e ON d.DunID = e.DunID AND d.DunerID = e.DunerID
WHERE d.DunerID IN ({0})
