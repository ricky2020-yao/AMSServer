SELECT
    p.PersonName AS CustomerName ,
    p.IdentityNo AS IdenNo ,
    bus.ContractNo ,
    --CONVERT(DATE, bus.LoanTime) LoanDate,
	bus.LoanTime,
    lend.Description ,
    serv.Description ,
	 change.OriginalCard ,
    change.ChangeCard ,
    --CONVERT(DATE, change.CreateTime) NotifyDate
	change.CreateTime
FROM
    dbo.SavingCardChange change
    JOIN dbo.Business bus ON bus.BusinessID = change.BusinessID
    --JOIN dbo.Customer cus ON cus.CustomerID = bus.CustomerID
	INNER JOIN customer.CustomerInfo cus ON bus.BusinessID=cus.Bid
	INNER JOIN customer.Person p ON p.PersonId=cus.PersonId

    LEFT JOIN dbo.ConstSysEnum lend ON lend.FullKey = bus.LendingSideKey
    LEFT JOIN dbo.ConstSysEnum serv ON serv.FullKey = bus.ServiceSideKey
	WHERE change.BCChangeID IN ({0})
    ORDER BY change.BusinessID