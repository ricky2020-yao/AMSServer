--更新提前清贷申请操作表，将清贷状态设置为注销状态
UPDATE CloanApply SET CloanApplyStatus = 4
FROM Business bs
JOIN CloanApply ca ON bs.BusinessID = ca.BusinessID
WHERE bs.CLoanStatus = 7
AND bs.ServiceSideKey = '{0}'

--更新提前清贷帐单为注销帐单
UPDATE Bill SET BillType = 5
FROM Business bs
JOIN Bill bl ON bs.BusinessID = bl.BusinessID AND bl.BillType = 3
WHERE bs.CLoanStatus = 7
AND bs.ServiceSideKey = '{0}'

--取消提前清贷订单下所有帐单的搁置状态
UPDATE Bill SET IsShelve = 0
FROM Business bs
JOIN Bill bl ON bs.BusinessID = bl.BusinessID
WHERE bs.CLoanStatus = 7
AND bs.ServiceSideKey = '{0}'

--取消提前清贷订单下所有科目的搁置状态
UPDATE BillItem SET IsShelve = 0
FROM Business bs
JOIN Bill bl ON bs.BusinessID = bl.BusinessID
JOIN BillItem bi ON bl.BillID = bi.BillID
WHERE bs.CLoanStatus = 7
AND bs.ServiceSideKey = '{0}'

--查询出特定子公司下所有提前清贷中的订单
UPDATE Business SET CLoanStatus = 1
FROM Business bs
WHERE bs.CLoanStatus = 7
AND bs.ServiceSideKey = '{0}'







