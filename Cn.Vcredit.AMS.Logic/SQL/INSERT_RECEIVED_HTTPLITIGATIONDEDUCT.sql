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
VALUES
        ( {0} , -- BillID - bigint
          {1} , -- BillItemID - bigint
          {2} , -- Amount - decimal
          {3} , -- ReceivedType - tinyint
          {4} , -- PayID - int
          GETDATE() , -- ReceivedTime - datetime
          GETDATE() , -- CreateTime - datetime
          {5} , -- OperatorID - int
          N'手工扣款诉讼账单填帐' , -- Explain - nvarchar(200)
          0 , -- DeductionID - int
          {6} , -- ToAccountID - int
          NULL  -- ToAcountTime - datetime
        )
        
UPDATE
    dbo.BillItem
SET 
    ReceivedAmt = ReceivedAmt + {2} ,
    FullPaidTime = CASE WHEN ReceivedAmt = ReceivedAmt + {2} THEN GETDATE()
                        ELSE NULL
                   END
WHERE
    BillItemID = {1}