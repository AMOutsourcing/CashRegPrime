DROP VIEW [dbo].[v_CashCCType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[v_CashCCType]
as

select distinct C1 as CCCode, C2 as CCDescr, C1 + '=' + C2 as CCFullName from ALL_CORW where CODAID = 'CASHCCTYPE'
	union all
select '??', 'Unknown', '??=Unknown'

GO