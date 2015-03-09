--插入储蓄卡变更表
INSERT INTO dbo.SavingCardChange(BusinessID, CustomerID, OriginalCard, 
	ChangeCard, Status, CreateTime, MeanName, MeanPath, OriginalUser, ChangeUser, BankName, SubBranch,
	OldSubBranch, OldBankName, OperatorUser)
	VALUES({0}, {1}, '{2}', 
	'{3}', {4}, GETDATE(), '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', 
	'{11}', '{12}', {13})

UPDATE dbo.SavingCardChange SET Status = 4 WHERE BCChangeID = {14}


