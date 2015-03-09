SELECT COUNT(*) 
FROM Business bs
WHERE LEN(bs.FrozenNo) > 0 
AND bs.ServiceSideKey IN 
({0}) AND bs.LendingSideKey = 'COMPANY/BHXT_LENDING'