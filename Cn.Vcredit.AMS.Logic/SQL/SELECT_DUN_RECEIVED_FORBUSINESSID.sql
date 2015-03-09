SELECT 
RI.ReceivedTime ,
RI.ReceivedType ,
BUS.LoanTime ,
BL.BillMonth ,
SUM(CASE BI.Subject WHEN 1 THEN RI.Amount ELSE 0 END) AS 'Capital',
SUM(CASE BI.Subject WHEN 2 THEN RI.Amount ELSE 0 END) AS 'Interest',
SUM(CASE BI.Subject WHEN 21 THEN RI.Amount ELSE 0 END) AS 'InterestBuckleFail',
SUM(CASE BI.Subject WHEN 23 THEN RI.Amount ELSE 0 END) AS 'PunitiveInterest',
SUM(CASE BI.Subject WHEN 3 THEN RI.Amount ELSE 0 END) AS 'ServiceFee',
SUM(CASE BI.Subject WHEN 4 THEN RI.Amount ELSE 0 END) AS 'GuaranteeFee',
SUM(CASE BI.Subject WHEN 22 THEN RI.Amount ELSE 0 END) AS 'ServiceBuckleFail',
SUM(CASE BI.Subject WHEN 28 THEN RI.Amount ELSE 0 END) AS 'AddInterest',
SUM(CASE BI.Subject WHEN 29 THEN RI.Amount ELSE 0 END) AS 'AddServiceFee',
SUM(CASE BI.Subject WHEN 30 THEN RI.Amount ELSE 0 END) AS 'AdvCleanLoanServiceFee',
SUM(CASE BI.Subject WHEN 31 THEN RI.Amount ELSE 0 END) AS 'ResidualCapital',
SUM(CASE BI.Subject WHEN 35 THEN RI.Amount ELSE 0 END) AS 'AdvServiceFee',
SUM(CASE BI.Subject WHEN 36 THEN RI.Amount ELSE 0 END) AS 'AddGuaranteeFee',
SUM(CASE BI.Subject WHEN 25 THEN RI.Amount ELSE 0 END) AS 'GuaranteeLateFee',
SUM(CASE BI.Subject WHEN 26 THEN RI.Amount ELSE 0 END) AS 'Litigation',
SUM(CASE BI.Subject WHEN 27 THEN RI.Amount ELSE 0 END) AS 'LitigationLateFee',

SUM(CASE BI.Subject WHEN 1 THEN BI.DueAmt ELSE 0 END) AS 'Capital_A',
SUM(CASE BI.Subject WHEN 2 THEN BI.DueAmt ELSE 0 END) AS 'Interest_A',
SUM(CASE BI.Subject WHEN 21 THEN BI.DueAmt ELSE 0 END) AS 'InterestBuckleFail_A',
SUM(CASE BI.Subject WHEN 23 THEN BI.DueAmt ELSE 0 END) AS 'PunitiveInterest_A',
SUM(CASE BI.Subject WHEN 3 THEN BI.DueAmt ELSE 0 END) AS 'ServiceFee_A',
SUM(CASE BI.Subject WHEN 4 THEN BI.DueAmt ELSE 0 END) AS 'GuaranteeFee_A',
SUM(CASE BI.Subject WHEN 22 THEN BI.DueAmt ELSE 0 END) AS 'ServiceBuckleFail_A',
SUM(CASE BI.Subject WHEN 28 THEN BI.DueAmt ELSE 0 END) AS 'AddInterest_A',
SUM(CASE BI.Subject WHEN 29 THEN BI.DueAmt ELSE 0 END) AS 'AddServiceFee_A',
SUM(CASE BI.Subject WHEN 30 THEN BI.DueAmt ELSE 0 END) AS 'AdvCleanLoanServiceFee_A',
SUM(CASE BI.Subject WHEN 31 THEN BI.DueAmt ELSE 0 END) AS 'ResidualCapital_A',
SUM(CASE BI.Subject WHEN 35 THEN BI.DueAmt ELSE 0 END) AS 'AdvServiceFee_A',
SUM(CASE BI.Subject WHEN 36 THEN BI.DueAmt ELSE 0 END) AS 'AddGuaranteeFee_A',
SUM(CASE BI.Subject WHEN 25 THEN BI.DueAmt ELSE 0 END) AS 'GuaranteeLateFee_A',
SUM(CASE BI.Subject WHEN 26 THEN BI.DueAmt ELSE 0 END) AS 'Litigation_A',
SUM(CASE BI.Subject WHEN 27 THEN BI.DueAmt ELSE 0 END) AS 'LitigationLateFee_A'
FROM Received RI
JOIN BillItem BI ON BI.BillItemID = RI.BillItemID
JOIN Bill BL ON BL.BillID = BI.BillID
JOIN Business BUS ON BUS.BusinessID = BL.BusinessID
WHERE BUS.BusinessID ={0} AND RI.ReceivedType<>3
GROUP BY RI.ReceivedType,BI.BillID,BUS.LoanTime,BL.BillMonth,
RI.ReceivedTime
ORDER BY BL.BillMonth