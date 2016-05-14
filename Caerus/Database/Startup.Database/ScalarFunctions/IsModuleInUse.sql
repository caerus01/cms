CREATE FUNCTION [dbo].[IsModuleInUse]
(
	@ModuleType int
)
RETURNS int
AS
BEGIN
declare  @currentModule int = 0

SELECT  @currentModule = md.ServiceTypeId
FROM    ModuleConfigurations md
where md.ModuleId = @ModuleType

return @currentModule	

END