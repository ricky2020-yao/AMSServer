SELECT     dbo.Bill.BusinessID, dbo.BillItem.Subject, dbo.Received.Amount, dbo.Received.ReceivedType
FROM         dbo.Bill INNER JOIN
                      dbo.BillItem ON dbo.Bill.BillID = dbo.BillItem.BillID INNER JOIN
                      dbo.Received ON dbo.BillItem.BillItemID = dbo.Received.BillItemID
WHERE     (dbo.Bill.BusinessID = {0}) AND (dbo.Bill.BillType < 3)