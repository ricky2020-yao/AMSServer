--查询担保明细科目
select BusinessID,
ResidualCapital,
ResidualInterest,
DueCapital,
DueInterest,
DueCapitalLost,
DuePenalty from dun.GuaranteeItem
where BusinessID = {0}