SELECT di.* FROM pay.DeductInstruction di
where di.DeductTaskID = {0} AND NOT EXISTS
(SELECT * from pay.TenpayProtoco tp WHERE 
tp.AccountBank = di.AccountBank AND
tp.AccountName = di.AccountName AND 
tp.AccountNo = di.AccountNo AND 
di.ToAccount = tp.ToAccount)
