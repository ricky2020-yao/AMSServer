SELECT 
bs.BusinessID,
bs.ContractNo,
p.PersonName AS CustomerName,
bs.LoanTime,
bs.ClearLoanTime,
bs.ToGuaranteeTime,
bs.ToLitigationTime,
e.Name AS BranchStore,
bs.SalesTeam,
ISNULL(u.Name, '') AS SalesMan,
bs.LoanKind,
ISNULL(k.Name, '') AS strLoanKind,
l.Name AS LendingSide,
s.Name AS ServiceSide,
g.Name AS GuaranteeSide,
bs.CLoanStatus,
bs.BusinessStatus,
bl.BillMonth,
cs.CustId AS CustomerID,
bs.LendingSideKey,
bs.ServiceSideKey,
bs.GuaranteeSideKey,
bs.SalesManID,
bs.BranchKey,
bs.OverMonth,
bs.LoanPeriod,
bs.EarnestAmt,
bs.ProceduresAmout,
ud.*,
bs.LoanCapital,
bs.ServiceRate,
bs.SavingCard,
bank.Name AS BankKey,
bs.ResidualCapital AS LoanBalance,
bs.ProceduresRate,
DATEADD(MONTH, bs.LoanPeriod, bs.LoanTime) AS [ExpireDate],
bs.ProductKind
FROM (
	SELECT 
	a.BillID,
	SUM(a.CapitalAmt) CapitalAmt,
	SUM(a.InterestAmt) InterestAmt,
	SUM(a.ServiceAmt) ServiceAmt,
	SUM(a.GuaranteeAmt) GuaranteeAmt,
	SUM(a.IntBuckleFailAmt) IntBuckleFailAmt,
	SUM(a.SevBuckleFailAmt) SevBuckleFailAmt,
	SUM(a.PenaltyIntAmt) PenaltyIntAmt,
	SUM(a.AddIntAmt) AddIntAmt,
	SUM(a.AddSevAmt) AddSevAmt,
	SUM(a.AddGuaranteeAmt) AddGuaranteeAmt,
	SUM(a.AdvSevAmt) AdvSevAmt,
	SUM(a.SurCapitalAmt) SurCapitalAmt,
	SUM(a.GBreachAmt) GBreachAmt,
	SUM(a.LawsuitAmt) LawsuitAmt,
	SUM(a.LawsuitBreachAmt) LawsuitBreachAmt
	FROM (
		SELECT 
		bi.BillID,
		bi.Subject,
		(CASE bi.Subject WHEN 1 THEN bi.DueAmt ELSE 0 END) - SUM(CASE bi.Subject WHEN 1 THEN ISNULL(r.Amount, 0) ELSE 0 END) CapitalAmt,
		(CASE bi.Subject WHEN 2 THEN bi.DueAmt ELSE 0 END) - SUM(CASE bi.Subject WHEN 2 THEN ISNULL(r.Amount, 0) ELSE 0 END) InterestAmt,
		(CASE bi.Subject WHEN 3 THEN bi.DueAmt ELSE 0 END) - SUM(CASE bi.Subject WHEN 3 THEN ISNULL(r.Amount, 0) ELSE 0 END) ServiceAmt,
		(CASE bi.Subject WHEN 4 THEN bi.DueAmt ELSE 0 END) - SUM(CASE bi.Subject WHEN 4 THEN ISNULL(r.Amount, 0) ELSE 0 END) GuaranteeAmt,
		(CASE bi.Subject WHEN 21 THEN bi.DueAmt ELSE 0 END) - SUM(CASE bi.Subject WHEN 21 THEN ISNULL(r.Amount, 0) ELSE 0 END) IntBuckleFailAmt,
		(CASE bi.Subject WHEN 22 THEN bi.DueAmt ELSE 0 END) - SUM(CASE bi.Subject WHEN 22 THEN ISNULL(r.Amount, 0) ELSE 0 END) SevBuckleFailAmt,
		(CASE bi.Subject WHEN 23 THEN bi.DueAmt ELSE 0 END) - SUM(CASE bi.Subject WHEN 23 THEN ISNULL(r.Amount, 0) ELSE 0 END) PenaltyIntAmt,
		(CASE bi.Subject WHEN 28 THEN bi.DueAmt ELSE 0 END) - SUM(CASE bi.Subject WHEN 28 THEN ISNULL(r.Amount, 0) ELSE 0 END) AddIntAmt,
		(CASE bi.Subject WHEN 29 THEN bi.DueAmt ELSE 0 END) - SUM(CASE bi.Subject WHEN 29 THEN ISNULL(r.Amount, 0) ELSE 0 END) AddSevAmt,
		(CASE bi.Subject WHEN 36 THEN bi.DueAmt ELSE 0 END) - SUM(CASE bi.Subject WHEN 36 THEN ISNULL(r.Amount, 0) ELSE 0 END) AddGuaranteeAmt,
		(CASE bi.Subject WHEN 35 THEN bi.DueAmt ELSE 0 END) - SUM(CASE bi.Subject WHEN 35 THEN ISNULL(r.Amount, 0) ELSE 0 END) AdvSevAmt,
		(CASE bi.Subject WHEN 31 THEN bi.DueAmt ELSE 0 END) - SUM(CASE bi.Subject WHEN 31 THEN ISNULL(r.Amount, 0)ELSE 0 END) SurCapitalAmt,
		(CASE bi.Subject WHEN 25 THEN bi.DueAmt ELSE 0 END) - SUM(CASE bi.Subject WHEN 25 THEN ISNULL(r.Amount, 0) ELSE 0 END) GBreachAmt,
		(CASE bi.Subject WHEN 26 THEN bi.DueAmt ELSE 0 END) - SUM(CASE bi.Subject WHEN 26 THEN ISNULL(r.Amount, 0) ELSE 0 END) LawsuitAmt,
		(CASE bi.Subject WHEN 27 THEN bi.DueAmt ELSE 0 END) - SUM(CASE bi.Subject WHEN 27 THEN ISNULL(r.Amount, 0) ELSE 0 END) LawsuitBreachAmt,
		bi.DueAmt - ISNULL(SUM(r.Amount), 0) UnDueTotal
		FROM BillItem bi
		LEFT OUTER JOIN Received r ON bi.BillItemID = r.BillItemID AND r.ReceivedType > 10 
		{0} 
		WHERE bi.IsShelve = 0
		GROUP BY bi.BillID, bi.Subject, bi.DueAmt
	) a
	WHERE a.UnDueTotal > 0
	GROUP BY a.BillID 
) ud
JOIN Bill bl ON ud.BillID = bl.BillID
JOIN Business bs ON bl.BusinessID = bs.BusinessID
--JOIN Customer cs ON bs.CustomerID = cs.CustomerID
INNER JOIN customer.CustomerInfo cs ON bs.BusinessID=cs.Bid
INNER JOIN customer.Person p ON p.PersonId=cs.PersonId

LEFT OUTER JOIN [Sys].[user].[User] u ON bs.SalesManID = u.Id
LEFT OUTER JOIN [Sys].common.Enumeration l ON bs.LendingSideKey = l.FullKey
LEFT OUTER JOIN [Sys].common.Enumeration s ON bs.ServiceSideKey = s.FullKey
LEFT OUTER JOIN [Sys].common.Enumeration g ON bs.GuaranteeSideKey = g.FullKey
LEFT OUTER JOIN [Sys].common.Enumeration e ON bs.BranchKey = e.FullKey
LEFT OUTER JOIN [Sys].common.Enumeration k ON bs.LoanKind = k.FullKey
LEFT OUTER JOIN [Sys].common.Enumeration bank ON bs.BankKey = bank.FullKey
WHERE bl.IsShelve = 0  AND bl.BillType <> 5 
