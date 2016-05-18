CREATE TABLE [dbo].[Documents](
	[Id] [uniqueidentifier] NOT NULL,
	[RefId] [bigint] IDENTITY(1,1) NOT NULL,
	[OwningEntityType] [int] NOT NULL,
	[OwningRefId] [bigint] NOT NULL,
	[DocumentType] [int] NOT NULL,
	[DocumentPath] [nvarchar](2000) NULL,
	[OriginalFileName] [varchar](1000) NULL,
	[ValidationDate] [datetime] NULL,
	[MimeType] [varchar](500) NULL,
	[State] [int] NOT NULL,
	[DateCreated] DATETIME NOT NULL, 
    [DateModified] DATETIME NOT NULL, 
    [UserCreated] NVARCHAR(128) NULL, 
    [UserModified] NVARCHAR(128) NULL,  
 CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED 
(
	[RefId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
