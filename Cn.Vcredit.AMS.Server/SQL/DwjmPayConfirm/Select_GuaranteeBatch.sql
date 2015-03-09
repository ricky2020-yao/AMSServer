SELECT * FROM 
(SELECT g.GuaranteeNum AS GuaranteeNum,c.NAME AS Region, m.name AS ChildCompany,
g.CreateDate,SUM(b.ToGuaranteeAmt) AS Amount,g.GuaranteeIndex,cast(g.GuaranteeMonth as varchar) as GuaranteeMonth,
ROW_NUMBER() OVER(ORDER BY c.fullkey,m.name,g.GuaranteeIndex) AS num
FROM dun.GuaranteeBatchPay g WITH (NOLOCK)
INNER JOIN dbo.Business b WITH (NOLOCK) ON b.GuaranteeNum=g.GuaranteeNum
LEFT JOIN common.MappingConfig s WITH (NOLOCK) ON b.ServiceSideKey=s.MappingKey AND MappingType='SUBCOMPANY'
LEFT JOIN dbo.ConstSysEnum m  WITH (NOLOCK) ON s.MappingValue=m.fullkey
LEFT JOIN dbo.ConstSysEnum c  WITH (NOLOCK) ON g.Region=c.fullkey
WHERE {0} {1}
GROUP BY g.GuaranteeNum,c.NAME,m.name,g.CreateDate,g.GuaranteeIndex,g.GuaranteeMonth,c.fullkey)t
{2}
ORDER BY t.num

SELECT COUNT(1) FROM 
(SELECT DISTINCT g.GuaranteeNum,c.NAME,s.MappingValue,g.CreateDate,g.GuaranteeIndex  
    FROM dun.GuaranteeBatchPay g  WITH (NOLOCK) 
    INNER JOIN dbo.Business b  WITH (NOLOCK) ON b.GuaranteeNum=g.GuaranteeNum
    LEFT JOIN common.MappingConfig s  WITH (NOLOCK) ON b.ServiceSideKey=s.MappingKey AND MappingType='SUBCOMPANY'
    LEFT JOIN dbo.ConstSysEnum c  WITH (NOLOCK) ON g.Region=c.fullkey
    WHERE {0} {1} )t 