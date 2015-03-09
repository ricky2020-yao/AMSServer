INSERT INTO PhoneList
SELECT {0}, b.CustomerID,BillID = a.BillID,a.BusinessID
,0,ROW_NUMBER() OVER(order by a.BusinessID),GETDATE() FROM Bill  AS A INNER JOIN (
select b.CustomerID,BillID = MAX(a.BillID),a.BusinessID from Bill as a
inner join Business as b on a.BusinessID = b.BusinessID
where a.BillStatus !=3 and a.IsShelve = 0 and b.FrozenNo ='' and
a.CompanyKey = '{1}' and a.BillType!=5 
group by a.BusinessID,b.CustomerID having COUNT(a.BillID)=1) AS B
ON A.BillID =B.BillID AND A.IsCurrent=1 AND
B.BusinessID NOT IN (SELECT BusinessID FROM Dun AS DD WHERE DD.IsCurrent = 1)
{2}