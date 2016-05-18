CREATE TABLE [dbo].[FieldDisplaySetups](
	[Id] [uniqueidentifier] NOT NULL,
	[RefId] [bigint] IDENTITY(1,1) NOT NULL,
	[OwningType] [int] NOT NULL,
	[OwningEntityType] [int] NOT NULL,
	[View] [int] NOT NULL,
	[Sequence] [int] NOT NULL,
	[FieldId] [nvarchar](200) NOT NULL,
	[Label] [nvarchar](max) NOT NULL,
	[ToolTip] [nvarchar](max) NOT NULL,
	[FieldType] [int] NOT NULL,
	[CssClass] [nvarchar](max) NOT NULL,
	[FieldRank] [int] NOT NULL,
	[LookupType] [int] NULL,
	[ReadOnly] [int] NOT NULL,
	[FieldMask] [nvarchar](max) NULL,
	[DateCreated] DATETIME NOT NULL, 
    [DateModified] DATETIME NOT NULL, 
    [UserCreated] NVARCHAR(128) NULL, 
    [UserModified] NVARCHAR(128) NULL, 
 CONSTRAINT [PK_FieldDisplaySetups] PRIMARY KEY CLUSTERED 
(
	[RefId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



