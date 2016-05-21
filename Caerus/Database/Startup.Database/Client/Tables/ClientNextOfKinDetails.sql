 
	CREATE TABLE [dbo].[ClientNextOfKinDetails](
		[Id] [uniqueidentifier] NOT NULL,
		[RefId] [bigint] IDENTITY(1,1) NOT NULL,
		[ClientRefId] [bigint] NULL,
		[Initials] [nvarchar](10) NULL,
		[FirstName] [nvarchar](50) NULL,
		[MiddleNames] [nvarchar](250) NULL,
		[Surname] [nvarchar](250) NULL,
		[GenderType] [int] NULL,
		[Title] [int] NULL,
		[Relationship] [int] NULL,
		[Nationality] [int] NULL,
		[TypeOfId] [int] NULL,
		[IdNumber] [nvarchar](150) NULL,
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