IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnMoneyToWords]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_MoneyToWords]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fn_MoneyToWords](@Money AS money, @strLangCode varchar(3))
    RETURNS VARCHAR(1024)
AS
BEGIN
  RETURN (dbo.fn_MoneyToWordNL(@Money, @@NESTLEVEL, @strLangCode))
END