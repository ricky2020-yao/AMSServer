UPDATE Business SET FrozenNo ='{0}' WHERE BusinessID IN
(SELECT DISTINCT BusinessID FROM PayAccount WHERE
PeriodType = 32 AND TaskKey='{1}') AND FrozenNo=''
AND ProductType = 2 --»¦ËÕ³µ´û£¨µÖÑº´û¿î£©
AND ProductKind = 'PRODUCTKIND/HUSUCHEDAI'