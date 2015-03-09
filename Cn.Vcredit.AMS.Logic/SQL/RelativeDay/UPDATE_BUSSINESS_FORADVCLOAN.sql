--更新提前清贷申请操作表，将清贷状态设置为注销状态
UPDATE CloanApply SET CloanApplyStatus = 4
FROM ViewAdvCLoanCancelBids bs
JOIN CloanApply ca ON bs.BusinessID = ca.BusinessID
WHERE CloanApplyStatus = 2

--取消提前清贷订单下所有帐单的搁置状态
UPDATE Bill SET IsShelve = 0
FROM ViewAdvCLoanCancelBids bs
JOIN Bill bl ON bs.BusinessID = bl.BusinessID
WHERE bl.IsShelve = 1

--取消提前清贷订单下所有科目的搁置状态
UPDATE BillItem SET IsShelve = 0
FROM ViewAdvCLoanCancelBids bs
JOIN Bill bl ON bs.BusinessID = bl.BusinessID
JOIN BillItem bi ON bl.BillID = bi.BillID
WHERE bi.IsShelve = 1

--查询出特定子公司下所有提前清贷中的订单
UPDATE Business SET CLoanStatus = 1
FROM ViewAdvCLoanCancelBids cb 
JOIN Business bs ON cb.BusinessID = bs.BusinessID
WHERE bs.CLoanStatus = 7

--更新提前清贷帐单为注销帐单
UPDATE Bill SET BillType = 5
FROM ViewAdvCLoanCancelBids bs
JOIN Bill bl ON bs.BusinessID = bl.BusinessID
WHERE bl.BillType = 3