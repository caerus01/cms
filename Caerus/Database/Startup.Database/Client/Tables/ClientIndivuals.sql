CREATE TABLE [dbo].[ClientIndivuals]
(
	[Id] UNIQUEIDENTIFIER NOT NULL , 
    [RefId] BIGINT NOT NULL IDENTITY (1,1), 
    [FirstName] NVARCHAR(500) NULL, 
    [Surname] NVARCHAR(500) NULL, 
    [IdNumber] NVARCHAR(500) NULL, 
    [PassportNumber] NVARCHAR(500) NULL, 
    [ClientRefId] BIGINT NULL, 
    [Initials] NVARCHAR(50) NULL, 
	[DateOfBirth] DATE NULL, 
    [GenderType] INT NULL, 
	[MaritalStatus] INT NULL, 
    [Nationality] NVARCHAR(50) NULL, 
    [TitleRefId] BIGINT NULL, 
	[LanguageRefId] BIGINT NULL, 
    [EducationLevel] INT NULL, 
	[DateCreated] DATETIME NOT NULL, 
    [DateModified] DATETIME NOT NULL, 
    [UserCreated] NVARCHAR(128) NULL, 
    [UserModified] NVARCHAR(128) NULL, 
    PRIMARY KEY ([RefId])
)
