CREATE PROCEDURE [dbo].[GetTableValuesByKey]
@table_name AS SYSNAME,
@refId as bigint
AS
BEGIN

DECLARE @conform_data_type AS VARCHAR(25)
SET @conform_data_type = 'VARCHAR(255)'

if (OBJECT_ID('tempdb..#replacementvalues') is null)  
begin  
	create table #replacementvalues(valueSource varchar(50), valueTarget varchar(2000))
end  

DECLARE @column_list AS VARCHAR(MAX)
DECLARE @conform_list AS VARCHAR(MAX)

SELECT  @conform_list = COALESCE(@conform_list + ', ', '') + 'CONVERT('
        + @conform_data_type + ', ' + QUOTENAME(COLUMN_NAME) + ') AS '
        + QUOTENAME(COLUMN_NAME),
        @column_list = COALESCE(@column_list + ', ', '')
        + QUOTENAME(COLUMN_NAME)
FROM    INFORMATION_SCHEMA.COLUMNS
WHERE   TABLE_NAME = @table_name
and COLUMN_NAME not in ('RefId', 'Id', 'CreateDate', 'ModifiedDate', 'DateCreated', 'CreationDate', 'CreatedBy', 'ModifiedBy', 'ModificationDate')

DECLARE @template AS VARCHAR(MAX)

SET @template = '
WITH    conformed
          AS ( SELECT  {@conform_list}
               FROM     {@table_name}
               where RefId = {@ref}
             )
             insert into #replacementvalues
    SELECT ''~'' +  ''{@table_name}'' + ''_'' +  ColumnKey + ''~'',  dbo.CamelCase(ColumnValue)
    FROM    conformed 
   
    UNPIVOT ( ColumnValue FOR ColumnKey IN ( {@column_list} ) ) AS unpvt 
   
    '

DECLARE @sql AS VARCHAR(MAX)
SET @sql = REPLACE(REPLACE(REPLACE(REPLACE(@template, '{@conform_list}', @conform_list), '{@column_list}', @column_list), '{@table_name}',  @table_name), '{@ref}',  @refId)

EXEC ( @sql )
    
END



