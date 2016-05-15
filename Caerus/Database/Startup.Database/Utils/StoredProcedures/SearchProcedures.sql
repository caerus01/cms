CREATE PROCEDURE SearchProcedures
@search varchar(250)
AS
BEGIN
	SELECT DISTINCT o.name AS Object_Name,o.type_desc
	FROM sys.sql_modules m 
	INNER JOIN sys.objects o 
	ON m.object_id=o.object_id
	WHERE m.definition Like @search
-- or (same results as above):-------------------------------
	SELECT name
	FROM   sys.procedures
WHERE  Object_definition(object_id) LIKE @search
END