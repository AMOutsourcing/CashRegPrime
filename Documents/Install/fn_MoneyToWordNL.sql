IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_MoneyToWordNL]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_MoneyToWordNL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[fn_MoneyToWordNL](@Money AS money, @nl int, @strLangCode varchar(3))
    RETURNS VARCHAR(1024)
AS
BEGIN

--declare @strLangCode varchar(3) = 'ENG'
--declare @Money AS money = 30861.04
--declare @nl int = @@NESTLEVEL


      DECLARE @Number as BIGINT
      SET @Number = FLOOR(@Money)
      DECLARE @Below20 TABLE (ID int identity(0,1), Word varchar(32))
      DECLARE @Below100 TABLE (ID int identity(2,1), Word varchar(32))
 
 
      INSERT @Below20 (Word) VALUES
                        (dbo.fn_Dict(@strLangCode, 'ASREP_000644')), (dbo.fn_Dict(@strLangCode, 'ASREP_000614')), (dbo.fn_Dict(@strLangCode, 'ASREP_000615')), (dbo.fn_Dict(@strLangCode, 'ASREP_000616')),
                        (dbo.fn_Dict(@strLangCode, 'ASREP_000617')), (dbo.fn_Dict(@strLangCode, 'ASREP_000618')), (dbo.fn_Dict(@strLangCode, 'ASREP_000619')), (dbo.fn_Dict(@strLangCode, 'ASREP_000620')),
                        (dbo.fn_Dict(@strLangCode, 'ASREP_000621')), (dbo.fn_Dict(@strLangCode, 'ASREP_000622')), (dbo.fn_Dict(@strLangCode, 'ASREP_000623')), (dbo.fn_Dict(@strLangCode, 'ASREP_000624')),
                        (dbo.fn_Dict(@strLangCode, 'ASREP_000625')), (dbo.fn_Dict(@strLangCode, 'ASREP_000626')), (dbo.fn_Dict(@strLangCode, 'ASREP_000627')), (dbo.fn_Dict(@strLangCode, 'ASREP_000628')),
						(dbo.fn_Dict(@strLangCode, 'ASREP_000629')), (dbo.fn_Dict(@strLangCode, 'ASREP_000630')), (dbo.fn_Dict(@strLangCode, 'ASREP_000631')), (dbo.fn_Dict(@strLangCode, 'ASREP_000632'))
       INSERT @Below100 VALUES (dbo.fn_Dict(@strLangCode, 'ASREP_000633')), (dbo.fn_Dict(@strLangCode, 'ASREP_000634')),(dbo.fn_Dict(@strLangCode, 'ASREP_000635')), (dbo.fn_Dict(@strLangCode, 'ASREP_000636')),
                               (dbo.fn_Dict(@strLangCode, 'ASREP_000637')), (dbo.fn_Dict(@strLangCode, 'ASREP_000638')),(dbo.fn_Dict(@strLangCode, 'ASREP_000639')), (dbo.fn_Dict(@strLangCode, 'ASREP_000640'))
 

 
DECLARE @English varchar(1024) =
(
  SELECT Case
    WHEN @Number = 0 THEN  ''
    WHEN @Number BETWEEN 1 AND 19
      THEN (SELECT Word FROM @Below20 WHERE ID=@Number)
   WHEN @Number BETWEEN 20 AND 99
-- SQL Server recursive function   
     THEN  (SELECT Word FROM @Below100 WHERE ID=@Number/10)+ '-' +
           dbo.fn_MoneyToWordNL( @Number % 10, @nl, @strLangCode)
   WHEN @Number BETWEEN 100 AND 999  
     THEN  (dbo.fn_MoneyToWordNL( @Number / 100, @nl, @strLangCode))+' '+dbo.fn_Dict(@strLangCode, 'ASREP_000641')+' '+
         dbo.fn_MoneyToWordNL( @Number % 100, @nl, @strLangCode)
   WHEN @Number BETWEEN 1000 AND 999999  
     THEN  (dbo.fn_MoneyToWordNL( @Number / 1000, @nl, @strLangCode))+' '+dbo.fn_Dict(@strLangCode, 'ASREP_000642')+' '+
         dbo.fn_MoneyToWordNL( @Number % 1000, @nl, @strLangCode) 
   WHEN @Number BETWEEN 1000000 AND 999999999  
     THEN  (dbo.fn_MoneyToWordNL( @Number / 1000000, @nl, @strLangCode))+' '+dbo.fn_Dict(@strLangCode, 'ASREP_000643')+' '+
         dbo.fn_MoneyToWordNL( @Number % 1000000, @nl, @strLangCode)
   ELSE ' INVALID INPUT' END
)

SELECT @English = RTRIM(@English)
SELECT @English = RTRIM(LEFT(@English,len(@English)-1))
                 WHERE RIGHT(@English,1)='-'
IF (@@NestLevel - @nl) = 1
BEGIN
      SELECT @English = @English+' '
      SELECT @English = @English+
      right('0' + convert(varchar,convert(int,100*(@Money - @Number))), 2) + '/100'
END

RETURN (@English)
END

GO