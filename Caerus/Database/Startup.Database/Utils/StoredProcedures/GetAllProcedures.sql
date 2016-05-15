CREATE PROCEDURE GetAllProcedures
AS
BEGIN
SELECT *
FROM sys.objects
WHERE type = 'P'; 
END