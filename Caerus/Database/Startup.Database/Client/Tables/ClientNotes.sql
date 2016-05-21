CREATE TABLE [dbo].[ClientNotes](
	[Id] [uniqueidentifier] NOT NULL,
	[RefId] [bigint] IDENTITY(1,1) NOT NULL,
	[ClientRefId] [bigint] NULL,
	[Title] [nvarchar](100) NULL,
	[NoteContent] [nvarchar](500) NULL,
	[DateCreated] DATETIME NOT NULL, 
    [DateModified] DATETIME NOT NULL, 
    [UserCreated] NVARCHAR(128) NULL, 
    [UserModified] NVARCHAR(128) NULL,  
 CONSTRAINT [IX_DirectClientNotes] UNIQUE NONCLUSTERED 
(
	[RefId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
