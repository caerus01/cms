CREATE TABLE [dbo].[FieldValidations](
	[Id] [uniqueidentifier] NOT NULL,
	[RefId] [bigint] IDENTITY(1,1) NOT NULL,
	[OwningType] [int] NOT NULL,
	[OwningEntityType] [int] NOT NULL,
	[FieldId] [nvarchar](200) NOT NULL,
	[ValidationType] [int] NOT NULL,
	[ValidationValue] [nvarchar](max) NOT NULL,
	[ValidationMessage] [nvarchar](max) NOT NULL,
	[DateCreated] DATETIME NOT NULL, 
    [DateModified] DATETIME NOT NULL, 
    [UserCreated] NVARCHAR(128) NULL, 
    [UserModified] NVARCHAR(128) NULL, 
 CONSTRAINT [PK_FieldValidations] PRIMARY KEY CLUSTERED 
(
	[RefId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
