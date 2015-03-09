--更新提前清贷申请操作表，将清贷状态设置为注销状态
UPDATE fin.CloanApply 
SET CloanApplyStatus = 6 
FROM fin.BusinessBasic bs 
JOIN fin.BusinessCurrentStaus bcs 
ON bs.BusinessID = bcs.BusinessID 
JOIN fin.CloanApply ca 
ON bs.BusinessID = ca.BusinessID 
WHERE bcs.CLoanStatus = 7 
AND bs.ServiceSide = {0} 
AND bs.LendingSide = {1} 
AND ca.CloanApplyStatus IN (1, 2) 
AND bcs.FrozenNo=''

--更新提前清贷帐单为注销帐单
UPDATE fin.Bill 
SET BillType = 5 
FROM fin.BusinessBasic bs 
JOIN fin.Bill bl 
ON bs.BusinessID = bl.BusinessID 
AND bl.BillType = 3 
JOIN fin.BusinessCurrentStaus bcs 
ON bs.BusinessID = bcs.BusinessID 
WHERE bcs.CLoanStatus = 7 
AND bs.ServiceSide = {0} 
AND bs.LendingSide = {1} 
AND bcs.FrozenNo='' 

--取消提前清贷订单下所有帐单的搁置状态
UPDATE fin.Bill 
SET IsShelve = 0 
FROM fin.BusinessBasic bs 
JOIN fin.Bill bl 
ON bs.BusinessID = bl.BusinessID 
JOIN fin.BusinessCurrentStaus bcs 
ON bs.BusinessID = bcs.BusinessID 
WHERE bcs.CLoanStatus = 7 
AND bs.ServiceSide = {0} 
AND bs.LendingSide = {1} 
AND bcs.FrozenNo='' 

--取消提前清贷订单下所有科目的搁置状态
UPDATE fin.BillItem 
SET IsShelve = 0 
FROM fin.BusinessBasic bs
JOIN fin.Bill bl 
ON bs.BusinessID = bl.BusinessID
JOIN fin.BillItem bi 
ON bl.BillID = bi.BillID
JOIN fin.BusinessCurrentStaus bcs 
ON bs.BusinessID = bcs.BusinessID 
WHERE bcs.CLoanStatus = 7
AND bs.ServiceSide = {0}
AND bs.LendingSide = {1}
AND bcs.FrozenNo=''

--查询出特定子公司下所有提前清贷中的订单
UPDATE fin.BusinessCurrentStaus  
SET CLoanStatus = 1
FROM fin.BusinessBasic bs
JOIN fin.BusinessCurrentStaus bcs 
ON bs.BusinessID = bcs.BusinessID 
WHERE bcs.CLoanStatus = 7
AND bs.ServiceSide = {0}
AND bs.LendingSide = {1}
AND bcs.FrozenNo=''







