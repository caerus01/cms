﻿CREATE PROCEDURE GetAllTables
AS
BEGIN
SELECT *
FROM sys.objects
WHERE type = 'U'; 
END