UPDATE Business SET FrozenNo ='{0}' where BusinessID in
(SELECT DISTINCT BusinessID FROM PayAccount WHERE
PeriodType = 32 AND AccountID = 10 AND TaskKey ='{1}') AND FrozenNo=''
AND ProductType = 5 --�ɶ�С�����޵�Ѻ���