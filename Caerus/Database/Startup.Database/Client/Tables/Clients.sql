CREATE TABLE [dbo].[Clients]
(
	[Id] UNIQUEIDENTIFIER NOT NULL , 
    [RefId] BIGINT NOT NULL IDENTITY (1,1),  
	[ShortDescription] NVARCHAR(500) NULL, 
    [ClientType] INT NOT NULL, 
    [ClientStatus] INT NOT NULL, 
    [OriginSourceIp] VARCHAR(250) NULL, 
	[ExternalReference] NVARCHAR(500) NULL, 
	[MainUserAccountId] NVARCHAR(128) NULL, 
	[DateCreated] DATETIME NOT NULL, 
    [DateModified] DATETIME NOT NULL, 
    [UserCreated] NVARCHAR(128) NULL, 
    [UserModified] NVARCHAR(128) NULL, 
    PRIMARY KEY ([RefId])
)
