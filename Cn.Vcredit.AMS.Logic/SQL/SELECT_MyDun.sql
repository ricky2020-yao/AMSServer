select HouseholdAddress,ContractNo,BusinessID,CustomerID,CustomerName,DebtsAmt as DebtsAmtDecimal,DunID,DunNumber,LoanCapital as LoanCapitalDecimal,DunAmount,
        LoanTime as LoanDateTime,OverMonth,Mobile as Telephone,FrozenNo,RelativeDate as RelativeDayDatetime,PenaltyAmt,rownum
        from
        (select r.AddressDetail as HouseholdAddress,d.ContractNo,d.BusinessID,d.customerid,pe.personName as CustomerName,d.DunAmount,
        (case b.PeriodType when 32 then b.CurrentOverAmount+b.OverAmount else b.OverAmount end) as DebtsAmt,
        d.dunid,d.DunNumber,b.LoanCapital,b.LoanTime,d.OverMonth,n.ContactInfo as Mobile,b.FrozenNo,b.RelativeDate,
        penaltyAmt=(select sum(amount) from PenaltyInt with(nolock) where businessid=b.businessid and createtime>d.createtime and (createtime<cast(convert(char(10),d.canceltime,120) as Date) or d.canceltime is null)),
        row_number() over(order by DunNumber asc) as rownum
        from (SELECT * FROM dun d with(nolock)
			WHERE 1=1 {3}) d 
        inner join customer.CustomerInfo c on d.customerid=c.Custid
        inner join customer.Person pe on c.personid=pe.personid
        inner join dbo.Business b on d.businessid = b.businessid
        
        LEFT JOIN Loan.customer.AddressInfo r WITH ( NOLOCK ) ON b.businessid = r.Bid
                            AND pe.PersonId = r.PersonId
                            AND r.AddrType = 'ADDRTYPE/HOUSEHOLD'
        LEFT JOIN Loan.customer.CustomerContact n on n.personid=pe.PersonId and n.bid=b.businessid
                            and ContactType='CONTACTTYPE/MOBILE'
        where {0})r
        where {1}
        Order by rownum,ContractNo

     select count(1) as DunCount,sum(DebtsAmt) as DebtsAmtSum,sum(isnull(DunAmount,0)) as DebtsIndexAmtSum
        from
        (select (case b.PeriodType when 32 then b.CurrentOverAmount+b.OverAmount else b.OverAmount end) as DebtsAmt,
		d.DunAmount
        from dbo.Dun d with(nolock)
        {2}
        inner join dbo.Business b on d.businessid = b.businessid
        WHERE {0} {3})r

    select sum(amount)as penaltyAmt
    from dbo.Dun d with(nolock)
       {2}
        inner join dbo.Business b on d.businessid = b.businessid
        left join PenaltyInt p on  p.businessid=b.businessid and p.createtime>d.createtime and 
                        (p.createtime<cast(convert(char(10),d.canceltime,120) as Date) or d.canceltime is null)
        WHERE {0} {3}