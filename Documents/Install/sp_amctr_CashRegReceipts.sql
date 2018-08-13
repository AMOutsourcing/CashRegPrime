IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_amctr_CashRegReceipts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_amctr_CashRegReceipts]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_amctr_CashRegReceipts]
(
	@strLangCode varchar(3), @strUnitID varchar(3), @iReceiptNo int
)

as

--declare @strLangCode varchar(3) = 'ENG'
--declare @iReceiptNo int = 30000283--19200044
--declare @strUnitID varchar(3) = 'S03'


declare @bNameSorting tinyint = 1 --0: Firstname + Lastname, 1: Lastname + Firstname
declare @bCashTypeIsGlobal tinyint
set @bCashTypeIsGlobal = 0


select
	g.NIMI
	,g.KOTIPAIKKA
	,g.LAHIOSO
	,g.PUHELIN
	,g.KAUPREKNO
	,g.FAX
	,g.EMAIL
	,c.UNIT as UNITID
	,d.EXPL as UNITNAME
	,c.CASHBOXID + '=' + co2.C2 as CASHBOXNAME
	,c.DEPARTMENT + '=' + isnull(co1.C2, '') as DEPTNAME
	,e.NAME as SMANNAME
	,c.UPDATED_DATE
	,c.RECEIPTNO
	,isnull(c.EXT_ORDERNO ,c.ORDERNO) as ORDERNO
	,isnull(c.EXT_INVOICENO, c.INVOICENO) as INVOICENO
	,c.CUSTNO
	,ltrim(rtrim(case when @bNameSorting = 0 then isnull(f.FNAME, '') else '' end + ' ' + isnull(f.LNAME, '') 
		+ ' ' + case when @bNameSorting = 1 then isnull(f.FNAME, '') else '' end)) as CUSTNAME
	,abs(c.AMOUNTTOPAY) as AMOUNTTOPAY
	,a.CASHTYPE
	,b.[DESCRIPTION] as CASHTYPEDESCR
	,c.[TEXT] as HDRDESCR
	,a.AMOUNT as AMOUNT
	,abs(a.AMOUNT) as AMOUNTABS
	,a.QTY
	,a.CHEQUE_NO
	,a.ROWNO
	,c.LICNO
	,isnull(c.EXT_BILLD, h.BILLD) as INVBILLD
	,a.CASHTYPE as CURRCODE
	,dbo.fn_MoneyToWords(abs(c.TOTPAID), @strLangCode) as AMOUNTPAYTOWORDS
	,f.ADDR2 as addr2e
	,f.PO
	,f.POSTCD
	,f.SPINNR
	,a.CASHFORM
	,co3.C2 as CASHFORMDESCR
	,dbo.fn_CustomerAddress(f.CUSTID) as CustAddress
from 
	dbo.ALL_CASHTRANSR a
		inner join
	dbo.CASHTYPE b on a.CASHFORM = b.FORM and a.CASHTYPE = b.[TYPE]
		inner join
	dbo.ALL_CASHTRANSH c on a.RECEIPTNO = c.RECEIPTNO and a._UNITID = c._UNITID
		inner join
	dbo.UNIT d on c.UNIT = d.UNITID
		left outer join
	dbo.ALL_CORW co1 on c._UNITID = co1._UNITID and c.DEPARTMENT = co1.C1 and co1.CODAID = 'OSASTOT'
		left outer join
	dbo.SMAN e on c.SMANID = e.SMANID
		left outer join
	dbo.ALL_CORW co2 on c._UNITID = co2._UNITID and c.CASHBOXID = co2.C1 and co2.CODAID = 'CASHBOXID'
		left outer join
	dbo.CUST f on c.CUSTNO = f.CUSTNO
		left outer join
	dbo.v_CompanyData  g on c._UNITID = g._UNITID
		left outer join
	dbo.TEMPINV h on c.INVOICENO = h.RECNO and c._UNITID = h.UNITID and h.APPAREA = 'C'
		left outer join
	dbo.ALL_CORW co3 on c._UNITID = co3._UNITID and a.CASHFORM = co3.C1 and co3.CODAID = 'CASHFORM'
where
	a._UNITID = @strUnitID
	and a.RECEIPTNO = @iReceiptNo


GO