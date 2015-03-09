UPDATE dun.GuaranteeBatchPay 
SET PayDate='{0}',ReceivedDate='{1}',PayType={2}
WHERE GuaranteeNum IN({3})
