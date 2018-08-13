IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_CustomerAddress]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_CustomerAddress]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create function [dbo].[fn_CustomerAddress]
(@iCustID int)
returns nvarchar(max)
begin
	declare @strAddress nvarchar(max)
	declare @bDetailedAddress tinyint = 1 --0: addr2. 1: addr2 + addr2e + addr1
	
	select @strAddress = case when @bDetailedAddress = 0 then a.addr2e else ltrim(rtrim(isnull(a.ADDR2, '') + ' ' + isnull(a.ADDR2e, '') + ' ' + isnull(a.ADDR1, ''))) + case when isnull(a.ADDR1, '') = '' then '' else '.' end end from dbo.CUST a where a.CUSTID = @iCustID
		
return @strAddress

end
GO