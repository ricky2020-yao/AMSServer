USE [PostLoan]
GO
/****** Object:  StoredProcedure [dbo].[proc_OverDueStaticByPage]    Script Date: 10/09/2014 13:55:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---- =============================================
---- Author:		wichell
---- Create date: 2013-3-24
---- Description:	催收静态报表查询
---- =============================================
ALTER PROC [dbo].[proc_OverDueStaticByPage]
    @ContractNo NVARCHAR(50) = NULL ,--合同编号
    @DunID NVARCHAR(1500) = NULL ,--催收单号
    @IdNumber NVARCHAR(150) = NULL ,--身份证号
    @CustomerName NVARCHAR(50) = NULL ,--客户姓名
    @TodayOverdueMark NVARCHAR(50) = NULL ,--单日逾期标记
    @BeginningOverdueMark NVARCHAR(50) = NULL ,--期初逾期标记
    @SaleWay NVARCHAR(100) = NULL ,--销售渠道
    @BusinessStatus NVARCHAR(100) = NULL ,--订单状态
    @LitigationStatus NVARCHAR(100) = NULL ,--诉讼状态
    @ProductType NVARCHAR(150) = NULL ,--产品类型
    @SigningCity NVARCHAR(50) = NULL ,--签约地区
    @OutStatus NVARCHAR(50) = NULL ,--委外状态
    @MinOverdueDays NVARCHAR(250) = NULL ,--逾期天数小
    @MaxOverdueDays NVARCHAR(250) = NULL ,--逾期天数大
    @StatisticsDate NVARCHAR(250) = NULL ,--统计时间
    @IsDue TINYINT = NULL ,--是否欠费
    @pageIndex INT = 1 ,
    @pageSize INT = 10 ,
    @pageCount INT = 0 OUTPUT
AS 
    BEGIN
--声明起始和结束的行号 
        DECLARE
            @start INT ,
            @end INT ,
            @pkey VARCHAR(20) 
 --设置起始和结束坐标 
        SET @start = ( @pageIndex - 1 ) * @pageSize + 1  
        SET @end = @pageIndex * @pageSize 
        DECLARE @countsql NVARCHAR(MAX)= ''
        DECLARE @sql NVARCHAR(MAX) = '
    SELECT
    ROW_NUMBER() OVER ( ORDER BY ContractNo ) AS RowID ,
    StatisticsDate ,
    ContractNo ,
    CustomerName ,
    LoanCapital ,
    SigningCity ,
    OverdueDays ,
    TodayOverdueMark ,
    BeginningOverdueMark,
    DunID
	INTO
    #OrderTemp
FROM
    dbo.OverDueStatic WITH ( NOLOCK ) where 1 = 1 '
        IF ( @ContractNo IS NOT NULL ) 
            BEGIN
                SET @sql = @sql + ' and ContractNo=' + '''' + @ContractNo
                    + ''''
            END
        IF ( @DunID IS NOT NULL ) 
            BEGIN
                SET @sql = @sql + ' and DunID=' + @DunID
            END
        IF ( @IdNumber IS NOT NULL ) 
            BEGIN
                SET @sql = @sql + ' and IdNumber=' + '''' + @IdNumber + ''''
            END
        IF ( @CustomerName IS NOT NULL ) 
            BEGIN
                SET @sql = @sql + ' and CustomerName=' + '''' + @CustomerName
                    + ''''
            END
        IF ( @TodayOverdueMark IS NOT NULL ) 
            BEGIN
                SET @sql = @sql + ' and TodayOverdueMark=' + ''''
                    + @TodayOverdueMark + ''''
            END
        IF ( @BeginningOverdueMark IS NOT NULL ) 
            BEGIN
                SET @sql = @sql + ' and BeginningOverdueMark=' + ''''
                    + @BeginningOverdueMark + ''''
            END
        IF ( @SaleWay IS NOT NULL ) 
            BEGIN
                SET @sql = @sql + ' and SalesChannels=' + ''''
                    + @SaleWay + ''''
            END
        IF ( @BusinessStatus IS NOT NULL ) 
            BEGIN
                SET @sql = @sql + ' and BusinessStatus=' + ''''
                    + @BusinessStatus + ''''
            END
        IF ( @LitigationStatus IS NOT NULL ) 
            BEGIN
                SET @sql = @sql + ' and LitigationStatus=' + ''''
                    + @LitigationStatus + ''''
            END
        IF ( @ProductType IS NOT NULL ) 
            BEGIN
                SET @sql = @sql + ' and ProductType =' + '''' + @ProductType
                    + ''''
            END
        IF ( @SigningCity IS NOT NULL ) 
            BEGIN
                SET @sql = @sql + ' and SigningCity=' + '''' + @SigningCity
                    + ''''
            END
        IF ( @OutStatus IS NOT NULL ) 
            BEGIN
                SET @sql = @sql + ' and OutStatus=' + '''' + @OutStatus + ''''
            END
        IF ( @MinOverdueDays IS NOT NULL ) 
            BEGIN
                SET @sql = @sql + ' and OverdueDays>=' + @MinOverdueDays
            END
        IF ( @MaxOverdueDays IS NOT NULL ) 
            BEGIN
                SET @sql = @sql + ' and OverdueDays<=' + @MaxOverdueDays
            END
        IF ( @StatisticsDate IS NOT NULL ) 
            BEGIN
                SET @sql = @sql + ' and StatisticsDate=' + ''''
                    + @StatisticsDate + ''''
            END
        IF ( @IsDue IS NOT NULL ) 
            BEGIN
                IF @IsDue = 1 
                    BEGIN
                        SET @sql = @sql + ' and OverdueAmount>0'
                    END
                ELSE 
                    IF @IsDue = 0 
                        BEGIN
                            SET @sql = @sql + ' and OverdueAmount=0'
                        END
            END
        SET @sql = @sql + '
        ORDER BY ContractNo '
        SET @countsql = @sql
        PRINT @sql
        SET @sql = @sql + 'select * from #OrderTemp 
        where RowID between ' + CONVERT(NVARCHAR(MAX), @start) + ' and '
            + CONVERT(NVARCHAR(MAX), @end)
 --动态执行SQL语句，查询所需数据（如果该查询语句有需要其他约束，则需要传入其他约束条件） 
        EXEC(@sql) 
    
 --执行一次全表查询，判断分页的总页数 
	PRINT @countsql
        EXEC(@countsql) 
        SET @pageCount = @@ROWCOUNT
		SET @pageCount = isnull(@pageCount,0)

    END