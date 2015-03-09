--导出催收单
SELECT A.DunID,A.ContractNo,p.PersonName AS CustomerName,
CustomerType = B.BusinessStatus,OverdueStates = A.OverMonth
,CustomerStates = B.LawsuitStatus,A.DunAmount,Dunner =A.DunerID 
FROM Dun AS A 
INNER JOIN Business AS B ON A.BusinessID=B.BusinessID
--INNER JOIN Customer AS C ON A.CustomerID =C.CustomerID
INNER JOIN customer.CustomerInfo C ON B.BusinessID=C.Bid
INNER JOIN customer.Person p ON p.PersonId=C.PersonId

WHERE A.IsCurrent=1 and A.CompanyKey in ({0})