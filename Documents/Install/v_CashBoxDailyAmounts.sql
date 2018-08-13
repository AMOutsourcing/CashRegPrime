IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_CashBoxDailyAmounts]'))
DROP VIEW [dbo].[v_CashBoxDailyAmounts]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[v_CashBoxDailyAmounts]
as

select
	(select top 1 b2.AMOUNT from dbo.ALL_CASHREG a2 inner join dbo.ALL_CASHREGROW b2 on a2._UNITID = b2._UNITID and a2.ROWID = b2.CASHREGROWID
		where a2.CASHBOXID = a1.CASHBOXID and a2._UNITID = a1._UNITID and a2._OID < a1._OID and a2.[EVENT] = 'O' and b2.CASHFORM = 'CASH' order by a2._OID desc
) as AmountOpen
,b1.AMOUNT as AmountClose
,a1.EOD_RECEIPTNO as EOD_RECEIPTNO
,a1.CASHBOXID
,cast(a1.UPDATED_DATE as date) as UPDATED_DATE
,a1._UNITID
from 
	dbo.ALL_CASHREG a1
		inner join 
	dbo.ALL_CASHREGROW b1 on a1._UNITID = b1._UNITID and a1.ROWID = b1.CASHREGROWID
where
	b1.CASHFORM = 'CASH'
	and a1.[EVENT] = 'C'
GO