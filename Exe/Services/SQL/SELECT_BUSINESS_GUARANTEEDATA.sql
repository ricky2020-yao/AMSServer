SELECT
    CASE WHEN EXISTS ( SELECT
                        CustomersTrackID
                       FROM
                        dbo.CustomerTrack truck
                       WHERE
                        truck.BusinessID = list.BusinessID
                        AND truck.TrackType = 9
                        AND truck.TrackStartTime <= '{0}' ) THEN '担保'
         ELSE '正常'
    END BusinessStatus ,
    ISNULL(t2.OtherConditions, '') OtherConditions ,
    list.AgentName,
    list.BranchName,
    list.BusinessId,
    list.ContractNo,
    list.CustomerName,
    ISNULL(list.GuaranteeAmount,0) GuaranteeAmount,
    CAST(list.GuaranteeDate AS CHAR(10)) GuaranteeDate,
    list.LoanCapital,
    CAST(list.LoanDate AS CHAR(10)) LoanDate,
    list.LoanPeriod,
    list.LoanProduct,
    list.Overdue,
    list.PeriodPayment,
    list.RegionName,
    list.StatMonth,
    list.Team,
    t2.TrackType ,
    lend.Name LendingSide
FROM
    dbo.M4CustomerLists list
    JOIN Business bus ON bus.BusinessID = list.BusinessID
    JOIN dbo.ConstSysEnum lend ON lend.FullKey = bus.LendingSideKey
                                  AND lend.Super = 8006
    LEFT JOIN dbo.CustomerTrack t2 ON t2.BusinessID = list.BusinessId
                                      AND t2.TrackType NOT IN ( 7, 8, 9 )
                                      AND t2.OtherConditions <> ''
WHERE
    StatMonth = {1}
	{2}