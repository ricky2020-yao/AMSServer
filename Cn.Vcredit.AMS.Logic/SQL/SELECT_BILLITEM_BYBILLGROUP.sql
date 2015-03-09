SELECT bs.ContractNo, p.PersonName AS CustomerName, bl.BillMonth,
ISNULL(SUM(bi1.Amount),0) AS CapitalAmt, ISNULL(SUM(bi2.Amount),0) AS InterestAmt, 
ISNULL(SUM(bi3.Amount),0) AS ServiceAmt, ISNULL(SUM(bi4.Amount),0) AS GuaranteeAmt,
ISNULL(SUM(bi21.Amount),0) AS CapitalIntBAmt, ISNULL(SUM(bi22.Amount),0) AS ServiceBAmt, 
ISNULL(SUM(bi23.Amount),0) AS PenaltyIntAmt, ISNULL(SUM(bi.Amount), 0) AS Total
FROM Received r
JOIN Bill bl ON r.BillID = bl.BillID
JOIN Business bs ON bl.BusinessID = bs.BusinessID

JOIN customer.CustomerInfo c ON c.Custid = bs.CustomerID
JOIN customer.Person p ON p.PersonId=c.PersonId

LEFT OUTER JOIN BillItem bi1 ON r.BillItemID = bi1.BillItemID AND bi1.Subject = 1
LEFT OUTER JOIN BillItem bi2 ON r.BillItemID = bi2.BillItemID AND bi2.Subject = 2
LEFT OUTER JOIN BillItem bi3 ON r.BillItemID = bi3.BillItemID AND bi3.Subject = 3
LEFT OUTER JOIN BillItem bi4 ON r.BillItemID = bi4.BillItemID AND bi4.Subject = 4
LEFT OUTER JOIN BillItem bi21 ON r.BillItemID = bi21.BillItemID AND bi21.Subject = 21
LEFT OUTER JOIN BillItem bi22 ON r.BillItemID = bi22.BillItemID AND bi22.Subject = 22
LEFT OUTER JOIN BillItem bi23 ON r.BillItemID = bi23.BillItemID AND bi23.Subject = 23
LEFT OUTER JOIN BillItem bi ON r.BillItemID = bi.BillItemID AND bi.Subject IN (1,2,3,4,21,22,23)
WHERE r.CreateTime >= '{0}' 
AND r.CreateTime < '{1}' 
AND bs.ServiceSideKey IN ('COMPANY/WX_SHWX_SERVICE', 'COMPANY/WX_SHWS_SERVICE')
AND r.ReceivedType in ({2})
GROUP BY  bs.ContractNo, p.PersonName, bl.BillMonth
