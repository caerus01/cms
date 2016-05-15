CREATE FUNCTION [dbo].[GetModuleSetting]
(
	@ModuleId int,
	@ModuleSettingId int
)
RETURNS varchar(250)
AS
BEGIN
	DECLARE @settingValue VARCHAR(250)
	
	SELECT @SettingValue = SettingValue
	FROM ModuleSettings 
	WHERE  ModuleId = @ModuleId
		AND SettingId = @ModuleSettingId
	
	RETURN @settingValue;	
END