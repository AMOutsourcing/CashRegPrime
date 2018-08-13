IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_amctr62_CashReg_EOD_Report]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_amctr62_CashReg_EOD_Report]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_amctr62_CashReg_EOD_Report]
(
	@strUnitiD varchar(max), @strCashBoxID varchar(max), @dtDate date, @strEODRecNo varchar(max), @strCCType varchar(max), @bOnlyOpened tinyint
)
as

/*
declare @strUnitiD varchar(3) = 'S01'
declare @strCashBoxID varchar(10) = 'BLAG33'
declare @dtDate date = '2016-05-31'
declare @strEODRecNo int = 150000007--150000221--150000220
declare @strCCType varchar(max) = '??,01,02,03,04,05,06'
declare @bOnlyOpened tinyint = 0
*/


declare @strNameSorting varchar(1) = '1' --0: Firstname + Lastname, 1: Lastname + Firstname
declare @iAMVersion numeric(10, 4)
	set @iAMVersion = (select DbVersionNo from SECU)
declare @strSQL nvarchar(max)
declare @strSQL2 nvarchar(max)

declare @tblResult table(
	[NIMI] [varchar](250) NULL,
	[KOTIPAIKKA] [varchar](250) NULL,
	[LAHIOSO] [varchar](250) NULL,
	[PUHELIN] [varchar](250) NULL,
	[KAUPREKNO] [varchar](250) NULL,
	[FAX] [varchar](250) NULL,
	[EMAIL] [varchar](250) NULL,
	[UNITID] [varchar](3) NOT NULL,
	[UNITNAME] [varchar](50) NOT NULL,
	[DEPARTMENT] [varchar](5) NOT NULL,
	[DEPTNAME] [varchar](150) NULL,
	[CASHBOXNAME] [varchar](250) NULL,
	[RECEIPTNO] [int] NOT NULL,
	[CUSTNO] [int] NULL,
	[CUSTNAME] [varchar](243) NULL,
	[LICNO] [varchar](15) NULL,
	[RECEIVER] [varchar](30) NULL,
	[INVOICENO] [varchar](50) NULL,
	[INVOICEDATE] [datetime] NOT NULL,
	[ORDERNO] [varchar](50) NULL,
	[AMOUNT] [numeric](18, 6) NOT NULL,
	[CREDITAMOUNT] [numeric](18, 6) NOT NULL,
	[AMOUNTWITHDRAW] [numeric](18, 6) NULL,
	[AMOUNTPAID] [numeric](18, 6) NULL,
	[INVOICECATEGORY] [varchar](2) NULL,
	[INVOICECATEGORYDESCR] [varchar](150) NULL,
	[CASHFORM] [varchar](10) NOT NULL,
	[CASHFORMDESCR] [varchar](150) NULL,
	[CASHTYPE] [varchar](30) NOT NULL,
	[CASHTYPEDESCR] [varchar](50) NOT NULL,
	[TEXT] [text] NULL,
	[EOD_RECEIPTNO] [int] NULL,
	[ISOPEN] [smallint] NULL,
	[AmountOpen] [numeric](18, 6) NULL,
	[AmountClose] [numeric](18, 6) NULL,
	[SMANID] [varchar](50) NULL,
	[SMANNAME] [varchar](100) NULL,
	[CC_TYPE] [varchar](20) NULL,
	[CC_DESCR] [varchar](100) NULL,
	[QTY] [numeric](18, 6) NULL
)

if object_id('tempdb..##sp_amctr62_CashReg_EOD_Report_Unit') is not null
	drop table ##sp_amctr62_CashReg_EOD_Report_Unit
	create table ##sp_amctr62_CashReg_EOD_Report_Unit (UnitID varchar(3) collate database_default, EXPL varchar(100) collate database_default)
	create index IX_UNITID on ##sp_amctr62_CashReg_EOD_Report_Unit (UnitID)

if object_id('tempdb..##sp_amctr62_CashReg_EOD_ReportCashBox') is not null
	drop table ##sp_amctr62_CashReg_EOD_ReportCashBox
	create table ##sp_amctr62_CashReg_EOD_ReportCashBox (CashBoxID varchar(100) collate database_default)
	create index IX_UNITID on ##sp_amctr62_CashReg_EOD_ReportCashBox (CashBoxID)

if object_id('tempdb..##sp_amctr62_CashReg_EOD_EODRecNo') is not null
	drop table ##sp_amctr62_CashReg_EOD_EODRecNo
	create table ##sp_amctr62_CashReg_EOD_EODRecNo (RecNo int)
	create index IX_UNITID on ##sp_amctr62_CashReg_EOD_EODRecNo (RecNo)

if object_id('tempdb..##sp_amctr62_CashReg_EOD_ReportCCType') is not null
	drop table ##sp_amctr62_CashReg_EOD_ReportCCType
	create table ##sp_amctr62_CashReg_EOD_ReportCCType (CCCode varchar(10) collate database_default, CCDescr varchar(200) collate database_default)
	create index IX_UNITID on ##sp_amctr62_CashReg_EOD_ReportCCType (CCCode)

insert into ##sp_amctr62_CashReg_EOD_Report_Unit
	select distinct [Param], b.EXPL from dbo.fn_MVParam(@strUnitID, ',') cross apply UNIT b where Param = b.UNITID

insert into ##sp_amctr62_CashReg_EOD_ReportCashBox
	select distinct [Param] from dbo.fn_MVParam(@strCashBoxID, ',')

if @bOnlyOpened = 0
		begin
			insert into ##sp_amctr62_CashReg_EOD_EODRecNo
				select distinct [Param] from dbo.fn_MVParam(@strEODRecNo, ',')
		end
	else
		begin
			insert into ##sp_amctr62_CashReg_EOD_EODRecNo
				select -1
		end

insert into ##sp_amctr62_CashReg_EOD_ReportCCType
	select distinct CCCode, CCDescr from dbo.v_CashCCType where CCCode in (select distinct [Param] from dbo.fn_MVParam(@strCCType, ','))


set @strSQL = '
select
	k.NIMI
	,k.KOTIPAIKKA
	,k.LAHIOSO
	,k.PUHELIN
	,k.KAUPREKNO
	,k.FAX
	,k.EMAIL
	,a._UNITID as UNITID
	,g.EXPL as UNITNAME
	,b.DEPARTMENT
	,c.C2 as DEPTNAME
	,b.CASHBOXID + ''='' + co2.C2 as CASHBOXNAME
	,a.RECEIPTNO
	,b.CUSTNO
	,ltrim(rtrim(case when ' + @strNameSorting + ' = 0 then isnull(m.FNAME, '''') else '''' end + '' '' + isnull(m.LNAME, '''') 
		+ '' '' + case when ' + @strNameSorting + ' = 1 then isnull(m.FNAME, '''') else '''' end)) as CUSTNAME
	,b.LICNO
	,b.RECEIVER
	,isnull(b.EXT_INVOICENO, b.INVOICENO) as INVOICENO
	,b.UPDATED_DATE as INVOICEDATE
	,isnull(b.EXT_ORDERNO, b.ORDERNO) as ORDERNO
	,case isnull(b.CREDITNOTE, 0) when 0 then isnull(a.AMOUNT,0) else 0 end as AMOUNT
	,case isnull(b.CREDITNOTE, 0) when 1 then isnull(a.AMOUNT,0) else 0 end as CREDITAMOUNT
	,case when isnull(a.AMOUNT, 0) < 0 then a.AMOUNT else 0 end as AMOUNTWITHDRAW
	,case when isnull(a.AMOUNT, 0) >= 0 then a.AMOUNT else 0 end as AMOUNTPAID
	,b.INVOICECATEGORY
	,f.C2 as INVOICECATEGORYDESCR
	,a.CASHFORM
	,e.C2 as CASHFORMDESCR
	,a.CASHTYPE
	,d.[DESCRIPTION] as CASHTYPEDESCR
	,b.[TEXT]
	,b.EOD_RECEIPTNO
	,j.ISOPEN
	,n.AmountOpen
	,n.AmountClose
	,b.SMANID
	,sm.NAME as SMANNAME
	,r.CCCode
	,r.CCDescr
	,a.QTY
from 
	dbo.ALL_CASHTRANSR a
		inner join
	dbo.ALL_CASHTRANSH b on a.RECEIPTNO = b.RECEIPTNO and a._UNITID = b._UNITID
		left outer join
	dbo.ALL_CORW c on c.CODAID = ''OSASTOT'' and b.DEPARTMENT = c.C1 and b._UNITID = c._UNITID
		inner join
	dbo.CASHTYPE d on a.CASHFORM = d.FORM and a.CASHTYPE = d.TYPE 
		inner join
	dbo.ALL_CORW e on a.CASHFORM = e.C1 and e.CODAID = ''CASHFORM'' and a._UNITID = e._UNITID
		inner join
	dbo.ALL_CORW f on b.INVOICECATEGORY = f.C1 and f.CODAID = ''CLASKUTYY'' and b._UNITID = f._UNITID
		inner join
	dbo.UNIT g on a._UNITID = g.UNITID
		left outer join
	dbo.ALL_CORW co2 on b._UNITID = co2._UNITID and b.CASHBOXID = co2.C1 and co2.CODAID = ''CASHBOXID''
		left outer join
	dbo.v_CompanyData k on a._UNITID = k._UNITID
		left outer join
	dbo.TEMPINV h on b.INVOICENO = h.RECNO and b._UNITID = h.UNITID and h.APPAREA = ''C''
		left outer join
	dbo.ALL_CASHBOX j on b.CASHBOXID = j.CASHBOXID and b._UNITID = j._UNITID
		left outer join
	dbo.CUST m on b.CUSTNO = m.CUSTNO
		left outer join
	(	select 
			a1.AmountOpen, a1.AmountClose, cast(a1.UPDATED_DATE as date) as UPDATED_DATE, a1.EOD_RECEIPTNO
		from 
			dbo.v_CashBoxDailyAmounts a1
	 			inner join
			##sp_amctr62_CashReg_EOD_Report_Unit n1 on a1._UNITID = n1.UnitID
				inner join
			##sp_amctr62_CashReg_EOD_ReportCashBox p1 on a1.CASHBOXID = p1.CashBoxID
				inner join
			##sp_amctr62_CashReg_EOD_EODRecNo q1 on isnull(a1.EOD_RECEIPTNO, -1) = q1.RecNo
	) as n on n.UPDATED_DATE >= ''' + convert(varchar(10), @dtDate, 102) + ''' and isnull(b.EOD_RECEIPTNO, -1) = n.EOD_RECEIPTNO
		inner join
	##sp_amctr62_CashReg_EOD_Report_Unit s on a._UNITID = s.UnitID
		inner join
	##sp_amctr62_CashReg_EOD_ReportCashBox p on b.CASHBOXID = p.CashBoxID
		inner join
	##sp_amctr62_CashReg_EOD_EODRecNo q on isnull(b.EOD_RECEIPTNO, -1) = q.RecNo
		inner join
	##sp_amctr62_CashReg_EOD_ReportCCType r on isnull(case a.CC_TERMINALID when '''' then ''??'' else  a.CC_TERMINALID end, ''??'') = r.CCCode
		left outer join
	v_Salesman sm on b.SMANID = sm.SMANID and b._UNITID = sm.UNITID
' + case when @bOnlyOpened = 1 then '' else '
where
	b.UPDATED_DATE >= ''' + convert(varchar(10), @dtDate, 102) + '''
	and b.UPDATED_DATE-1 < ''' + convert(varchar(10), @dtDate, 102) + ''''
	end


set @strSQL2 = '

	select
	k.NIMI
	,k.KOTIPAIKKA
	,k.LAHIOSO
	,k.PUHELIN
	,k.KAUPREKNO
	,k.FAX
	,k.EMAIL
	,a._UNITID as UNITID
	,g.EXPL as UNITNAME
	,' + case when @iAMVersion >= 8.1 then 't.' else 'sm.' end + 'Dept
	,e.C2 as DEPTNAME
	,b.CASHBOXID + ''='' + co2.C2 as CASHBOXNAME
	,b.RECEIPTNO
	,null
	,null
	,null
	,null
	,null
	,null
	,null
	,isnull(a.AMOUNT,0) as AMOUNT
	,0 as CREDITAMOUNT
	,case when isnull(a.AMOUNT, 0) < 0 then a.AMOUNT else 0 end as AMOUNTWITHDRAW
	,case when isnull(a.AMOUNT, 0) >= 0 then a.AMOUNT else 0 end as AMOUNT
	,b.BANKCD
	,isnull(f.C1, '''') + ''='' + isnull(f.C2, '''')  as INVOICECATEGORYDESCR
	,a.CASHFORM
	,e.C2 as CASHFORMDESCR
	,a.CASHTYPE
	,c.[DESCRIPTION] as CASHTYPEDESCR
	,b.[TEXT]
	,null
	,j.ISOPEN
	,null
	,null
	,b.SMANID
	,sm.NAME as SMANNAME
	,null
	,null
	,QTY
from 
	ALL_CASHREGROW a
		inner join
	ALL_CASHREG b on a.CASHREGROWID = b.ROWID
		left outer join
	v_Salesman sm on b.SMANID = sm.SMANID and b._UNITID = sm.UNITID
		left outer join 
	ALL_CORW f on f.CODAID = ''CASHBANK'' and b.BANKCD = f.C1 and b._UNITID = f._UNITID
		inner join
	CASHTYPE c on a.CASHFORM = c.FORM and a.CASHTYPE = c.TYPE 
		inner join
	ALL_CORW d on c.FORM = d.C1 and d.CODAID = ''CASHFORM'' and a._UNITID = d._UNITID
	' + case when @iAMVersion >= 8.1 then ' inner join dbo.SMAN_SITE t on sm.SMANID = t.SMANID ' else '' end + '	
		inner join
	ALL_CORW e on e.CODAID = ''OSASTOT'' and ' + case when @iAMVersion >= 8.1 then 't.' else 'sm.' end + 'DEPT = e.C1 and '+ case when @iAMVersion >= 8.1 then 't.' else 'sm.' end + 'UNITID = e._UNITID
		inner join
	dbo.UNIT g on a._UNITID = g.UNITID
		left outer join
	dbo.ALL_CORW co2 on b._UNITID = co2._UNITID and b.CASHBOXID = co2.C1 and co2.CODAID = ''CASHBOXID''
		left outer join
	dbo.v_CompanyData k on a._UNITID = k._UNITID
		left outer join
	dbo.ALL_CASHBOX j on b.CASHBOXID = j.CASHBOXID and b._UNITID = j._UNITID
		inner join
	##sp_amctr62_CashReg_EOD_Report_Unit n on a._UNITID = n.UnitID
		inner join
	##sp_amctr62_CashReg_EOD_ReportCashBox p on b.CASHBOXID = p.CashBoxID
		inner join
	##sp_amctr62_CashReg_EOD_EODRecNo q on isnull(b.EOD_RECEIPTNO, -1) = q.RecNo
where 
	b.[EVENT] = ''D''
' + case when @bOnlyOpened = 1 then '' else
	' and b.RECEIPTNO is not null
	and b.UPDATED_DATE >= ''' + convert(varchar(10), @dtDate, 102) + '''
	and b.UPDATED_DATE-1 < ''' + convert(varchar(10), @dtDate, 102) + '''
' end

insert into @tblResult
execute sp_executesql @strSQL

insert into @tblResult
execute sp_executesql @strSQL2

select * from @tblResult



GO