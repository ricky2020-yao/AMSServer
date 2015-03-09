SELECT  bus.ContractNo ,
        bus.BusinessID ,
        info.CustId CustomerID,
        per.PersonName CustomerName,
        bus.CurrentOverAmount + bus.OverAmount + bus.OtherAmount DebtsAmt,
        dun.DunID ,
        dun.DunNumber ,
        bus.LoanCapital ,
        bus.LoanTime ,
        CAST(dun.OverMonth AS varchar(20)) OverdueNumber,
        dun.CreateTime ,
        ISNULL(u.Name, '') Duner ,
        bus.FrozenNo IsFreeze,
		bus.ResidualCapital,
		info.PersonId,
		dun.DunUnit,
		dun.BeginningDueDays DueDays,
		dun.OutSourceTime,
		bus.SavingUser
INTO    #temp
FROM    dun.Dun dun
        JOIN dbo.Business bus ON dun.BusinessID = bus.BusinessID
		JOIN customer.CustomerInfo info ON info.Bid = bus.BusinessID
        JOIN customer.Person per ON per.PersonId = info.PersonId
		LEFT JOIN dun.DunUnitCfg cfg ON cfg.RowID = dun.DunUnit 
        LEFT JOIN dbo.ConstSysAccount account ON account.LoginName = dun.Duner
        LEFT JOIN dbo.ConstSysUsers u ON u.Id = account.UserId
WHERE   1 = 1
		{0}
ORDER BY per.PersonName     
--上次跟进时间
SELECT  #temp.BusinessID ,
        MAX(track.CreateTime) LastTrackTime
INTO    #tracktmp
FROM    #temp
        LEFT JOIN dbo.CustomerTrack track ON track.BusinessID = #temp.BusinessID
WHERE	1=1
		{2}
GROUP BY #temp.BusinessID
--标签
SELECT  labletemp.BusinessID ,
        labletemp.LabelCode
INTO    #labeltmp
FROM    ( SELECT    #temp.BusinessID ,
                    label.LabelCode ,
                    label.CreateTime ,
                    ROW_NUMBER() OVER ( PARTITION BY #temp.BusinessID ORDER BY label.CreateTime DESC ) RowID
          FROM      #temp
                    LEFT JOIN dun.AllDunLabel label ON label.PersonID = #temp.PersonId
					WHERE 1=1
					{3}
        ) labletemp
WHERE   labletemp.RowID = 1
--结果代码
SELECT  duncodes.*
		INTO #codetemp
FROM    ( SELECT    tmp.* ,
                    ROW_NUMBER() OVER ( PARTITION BY tmp.PersonID ORDER BY tmp.OpDate DESC ) RowID
          FROM      ( SELECT    track.PersonID ,--AMQUE
                                track.VaildCode ,
                                track.UnVaildCode ,
                                track.OpDate
                      FROM      dun.AllDunTrack track
                                JOIN #temp ON #temp.PersonId = track.PersonID
                      UNION ALL
                      SELECT    bas.PersonId ,--VBS
                                dbo.ConvertToAmQueCode(track.VaildCode) VaildCode,
                                dbo.ConvertToAmQueCode(track.UnVaildCode) UnVaildCode,
                                track.CreateTime OpDate
                      FROM      dbo.CustomerTrack track
                                JOIN #temp ON #temp.BusinessID = track.BusinessID
                                LEFT JOIN customer.CustomerInfo bas ON track.BusinessID = bas.Bid
                    ) tmp
					--WHERE 1=1 取最后一条
						--{4}
        ) duncodes
WHERE   duncodes.RowID = 1
--最终查询结果
SELECT  temp.* ,
        CAST(#tracktmp.LastTrackTime AS DATE) LastTrackTime ,
        ( CASE WHEN #codetemp.VaildCode IS NULL THEN #codetemp.UnVaildCode
               ELSE #codetemp.VaildCode
          END ) DunCode,
		  #labeltmp.LabelCode ,
		  ROW_NUMBER() OVER ( ORDER BY temp.PersonId ) RowID
INTO #finalResult
FROM    #temp temp
        LEFT JOIN #tracktmp ON #tracktmp.BusinessID = temp.BusinessID
        LEFT JOIN #labeltmp ON #labeltmp.BusinessID = temp.BusinessID
        LEFT JOIN #codetemp ON #codetemp.PersonID = temp.PersonId
WHERE 1=1
	{4}
--分页
SELECT  #finalResult.* ,
        cfg.Name DunCodeName
FROM    #finalResult
        LEFT JOIN dbo.CustomerTruckCfg cfg ON cfg.Code = #finalResult.DunCode
					AND cfg.CfgType = 'AmQueCode'
WHERE 1=1
	{1}
--总记录数
SELECT COUNT(1) FROM #finalResult 
/*=======Drop=========*/
DROP TABLE #tracktmp
DROP TABLE #labeltmp
DROP TABLE #temp
DROP TABLE #codetemp
DROP TABLE #finalResult