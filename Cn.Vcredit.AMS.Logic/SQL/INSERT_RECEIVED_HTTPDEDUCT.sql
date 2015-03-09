SELECT
    bl.BillID ,
    bi.BillItemID ,
    bi.DueAmt - bi.ReceivedAmt amt ,
    {1} recid ,
    {3} pid ,
    GETDATE() rectime ,
    GETDATE() cretime ,
    {2} optid ,
    '手工扣款填帐' rectype ,
    0 cum1 ,
    {4} cum2 ,
    NULL tm
INTO
    #TmpTable
FROM
    dbo.BillItem bi
    JOIN dbo.Bill bl ON bi.BillID = bl.BillID
    JOIN dbo.Business bus ON bus.BusinessID = bl.BusinessID
WHERE
    bi.DueAmt - bi.ReceivedAmt > 0
    AND bl.BillStatus <> 3
    AND bl.IsShelve = 0
    AND bl.BillType <> 5
    AND bi.BillItemID IN ( {0} )
                    
INSERT  INTO dbo.Received
        ( BillID ,
          BillItemID ,
          Amount ,
          ReceivedType ,
          PayID ,
          ReceivedTime ,
          CreateTime ,
          OperatorID ,
          Explain ,
          DeductionID ,
          ToAccountID ,
          ToAcountTime              
        )
        SELECT
            *
        FROM
            #TmpTable            
UPDATE
    bi
SET 
    bi.ReceivedAmt = bi.DueAmt ,
    bi.FullPaidTime = GETDATE()
FROM
    dbo.BillItem bi ,
    #TmpTable tmp
WHERE
    tmp.BillItemID = bi.BillItemID
                
DROP TABLE #TmpTable                  