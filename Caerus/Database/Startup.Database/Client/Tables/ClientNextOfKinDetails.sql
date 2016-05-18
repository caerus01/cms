 
	CREATE TABLE [dbo].[ClientNextOfKinDetails](
		[Id] [uniqueidentifier] NOT NULL,
		[RefId] [bigint] IDENTITY(1,1) NOT NULL,
		[ClientRefId] [bigint] NOT NULL,
		[Initials] [nvarchar](10) NOT NULL,
		[FirstName] [nvarchar](50) NOT NULL,
		[MiddleNames] [nvarchar](250) NULL,
		[Surname] [nvarchar](250) NOT NULL,
		[GenderType] [int] NOT NULL,
		[Title] [int] NOT NULL,
		[Relationship] [int] NOT NULL,
		[Nationality] [int] NOT NULL,
		[TypeOfId] [int] NOT NULL,
		[IdNumber] [nvarchar](150) NOT NULL,
		[HomeTelephone] [nvarchar](50) NULL,
		[WorkTelephone] [nvarchar](50) NULL,
		[CellNumber] [nvarchar](50) NULL,
		[DateCreated] DATETIME NOT NULL, 
		[DateModified] DATETIME NOT NULL, 
		[UserCreated] NVARCHAR(128) NULL, 
		[UserModified] NVARCHAR(128) NULL, 
	 CONSTRAINT [IX_NextOfKinDetails] UNIQUE NONCLUSTERED 
	(
		[RefId] ASC
	) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY])