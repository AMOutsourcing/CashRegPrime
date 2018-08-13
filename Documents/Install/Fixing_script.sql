
update a set a.EXT_BILLD = b.BILLD 
--select *
from CASHTRANSHS01 a, TEMPINV b where a.INVOICENO = b.RECNO and 
b.UNITID ='S01'
and a.EXT_BILLD is null and b.BILLD is not null

-- 
insert into CORWS01(CODAID,C1,C3,C4,C5) select 'INVCAT_ADD',SUBSTRING(codaid,1,1)+C1,CODAID,C1,C1 from CORWS01 where CODAID in ('klaskutyy','alaskutyy','vlaskutyy') and c5 like '%L%'
--
