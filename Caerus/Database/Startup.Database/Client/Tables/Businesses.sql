CREATE TABLE [dbo].[Businesses]
(
	[Id] UNIQUEIDENTIFIER NOT NULL , 
    [RefId] BIGINT NOT NULL IDENTITY (1,1),
	[ClientRefId] BIGINT NULL,
	[Name] NVARCHAR(500) NULL, 
    [TaxNumber] NVARCHAR(500) NULL,  
    [BusinessType] INT NOT NULL, 
	[DateCreated] DATETIME NOT NULL, 
    [DateModified] DATETIME NOT NULL, 
    [UserCreated] NVARCHAR(128) NULL, 
    [UserModified] NVARCHAR(128) NULL,  
    PRIMARY KEY ([RefId])
)
