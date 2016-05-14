
CREATE PROCEDURE [dbo].[SetModuleSetting]
@Module int,
@Setting int, 
@Country bigint,
@Value nvarchar(250),
@User bigint = null
AS
declare @Error nvarchar(max) set @Error = '';
if @User is null
 SET @User = 1

BEGIN TRY
	BEGIN TRANSACTION

	if (select COUNT(*) from ModuleSettings where [ModuleId] = @Module and [SettingId] = @Setting and [CountryRefId] = @Country) = 0
	begin
		insert ModuleSettings 
		select @Module,@Country,@Setting,@Value,GETDATE(),GETDATE(),@User, @User
	end
	else
	begin
		update ModuleSettings set SettingValue = @Value, DateModified = GETDATE(), UserModified = @User
		where [ModuleId] = @Module and [SettingId] = @Setting and [CountryRefId] = @Country
	end
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION
	select @Error = 'Error: ' + ERROR_MESSAGE()
	goto Error
END CATCH

Success:
	select Result = 'Success'
	return 0

Error:
	select Result = @Error
	return 1




GO