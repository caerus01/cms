CREATE TABLE [dbo].[NotificationSmsHistories]
(
	[Id] UNIQUEIDENTIFIER NOT NULL , 
    [RefId] BIGINT NOT NULL IDENTITY, 
    [CellNumber] NVARCHAR(50) NULL, 
    [Reference] NVARCHAR(200) NULL, 
    [Message] NVARCHAR(320) NULL, 
    [DateTransmitted] DATETIME NULL, 
    [DateCreated] DATETIME NULL, 
    [UserCreated] NVARCHAR(128) NULL, 
    [Status] INT NOT NULL, 
    [NotificationType] INT NULL, 
    [ErrorReason] INT NULL, 
    [ScheduleDate] DATETIME NULL, 
    [ClientRefId] BIGINT NULL, 
    PRIMARY KEY ([RefId])
)
