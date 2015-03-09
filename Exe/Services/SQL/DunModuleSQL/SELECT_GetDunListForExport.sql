SELECT  dun.DunID AS [催收单号] ,
        bus.ContractNo AS [合同编号] ,
        bac.CustomerName AS [姓名] ,
        CAST(bus.BusinessStatus AS VARCHAR(10)) AS [客户类型] ,
        CAST(dun.OverMonth AS VARCHAR(200)) AS [逾期月数] ,
        CAST(bus.LawsuitStatus AS VARCHAR(10)) AS [客户状态] ,
        dun.BeginningAmt AS [合计金额] ,
        users.Name AS [催收员] ,
        store.storeName AS [门店] ,
        bus.LoanTime AS [放贷时间] ,
        ( CASE WHEN ( SELECT    COUNT(1)
                      FROM      dun.AllDunLabel WITH ( NOLOCK )
                      WHERE     PersonID = bac.PersonID
                                AND LabelCode = 'OutSourcing'
                    ) > 0 THEN '是'
               ELSE '否'
          END ) AS [是否曾经委外] ,
        loankind.Name AS [产品类型] ,
        CAST(bus.ProductType AS VARCHAR(10)) AS [产品种类] ,
        salseman.UserName AS [理财顾问] ,
        teamleader.Name AS [销售主管] ,
        ( DATEDIFF(DAY, ( SELECT    MIN(bl.DueDate)
                          FROM      dbo.Bill bl WITH ( NOLOCK )
                          WHERE     bl.BillStatus <> 3
                                    AND bl.BillType IN ( 1, 2 )
                                    AND bl.IsShelve = 0
                                    AND bl.BusinessID = bus.BusinessID
                        ), GETDATE()) + 1 ) AS [逾期天数] ,
        bus.ResidualCapital AS [剩余本金]
FROM    dun.Dun dun WITH ( NOLOCK )
        JOIN dbo.Business bus WITH ( NOLOCK ) ON bus.BusinessID = dun.BusinessID
        JOIN customer.vw_customer_CustomerBasic bac WITH ( NOLOCK ) ON bac.Bid = bus.BusinessID
        LEFT JOIN dun.DunUnitCfg cfg WITH ( NOLOCK ) ON cfg.RowID = dun.DunUnit
        LEFT JOIN dbo.ConstSysEnum loankind WITH ( NOLOCK ) ON loankind.FullKey = bus.LoanKind
                                                              AND loankind.Super = 200
        LEFT JOIN dbo.ConstSysAccount dunner WITH ( NOLOCK ) ON dunner.LoginName = dun.Duner
        LEFT JOIN dbo.ConstSysUsers users WITH ( NOLOCK ) ON users.Id = dunner.UserId
        LEFT JOIN dbo.ConstStore store WITH ( NOLOCK ) ON store.FullKey = bus.BranchKey
        LEFT JOIN Sys.org.Agent salseman WITH ( NOLOCK ) ON salseman.UserId = bus.SalesManID
        LEFT JOIN dbo.ConstTeam team WITH ( NOLOCK ) ON team.serialId = salseman.parentId
        LEFT JOIN dbo.ConstSysUsers teamleader WITH ( NOLOCK ) ON team.teamLeader = teamleader.Id
WHERE   dun.IsClosed = 0
		{0}