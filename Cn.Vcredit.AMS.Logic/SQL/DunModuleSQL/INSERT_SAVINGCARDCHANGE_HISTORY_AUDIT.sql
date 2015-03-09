--插入储蓄卡变更表
INSERT INTO dbo.SavingCardChange(BusinessID, CustomerID, OriginalCard, 
	ChangeCard, Status, CreateTime, MeanName, MeanPath, OriginalUser, 
	ChangeUser, BankName, SubBranch, OldSubBranch, OldBankName, OperatorUser)
	VALUES({0}, {1}, '{2}', 
	'{3}', {4}, GETDATE(), '{5}', '{6}', '{7}', 
	'{8}', '{9}', '{10}', '{11}', '{12}', {13})

	
--更新dbo.Business
UPDATE dbo.Business 
SET BankKey = '{14}'
	,SavingCard = '{3}'
	,SavingUser ='{8}'
	,SubBranch = '{10}'
WHERE BusinessID = {0}


--插入CustomerChangeLog
INSERT INTO dbo.CustomerChangeLog(CustomerID, PropertyName, OriginalValue, ModifyTime, OperatorID)
VALUES({1}, '储蓄账户', '{7}', GETDATE(), {13})

--插入CustomerChangeLog
INSERT INTO dbo.CustomerChangeLog(CustomerID, PropertyName, OriginalValue, ModifyTime, OperatorID)
VALUES({1}, '储蓄卡号', '{2}', GETDATE(), {13})