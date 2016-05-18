CREATE TABLE [dbo].[ClientContactDetails](
	[Id] [uniqueidentifier] NOT NULL,
	[RefId] [bigint] IDENTITY(1,1) NOT NULL,
	[ClientRefId] [bigint] NOT NULL,
	[ContactPerson] [nvarchar](150) NOT NULL,
	[WorkTelephone] NVARCHAR(500) NULL, 
    [HomeTelephone] NVARCHAR(500) NULL, 
	[EmailAddress] [nvarchar](500) NOT NULL,
	[CellNumber] NVARCHAR(500) NULL, 
    [WebSite] NVARCHAR(500) NULL, 
    [Twitter] NVARCHAR(500) NULL, 
    [Facebook] NVARCHAR(500) NULL, 
    [YouTube] NVARCHAR(500) NULL, 
	[Skype] NVARCHAR(500) NULL, 
	[IsPrimary] [bit] NOT NULL,
	[DateCreated]  DATETIME NOT NULL,
    [DateModified] DATETIME NOT NULL,
    [UserCreated]  NVARCHAR(128)    NULL,
    [UserModified] NVARCHAR(128)    NULL,
	CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[RefId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_DirectContacts] UNIQUE NONCLUSTERED 
(
	[RefId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
