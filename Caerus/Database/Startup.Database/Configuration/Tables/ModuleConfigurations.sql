CREATE TABLE [dbo].[ModuleConfigurations](
	[ModuleId] [int] NOT NULL,
	[ServiceTypeId] [int] NOT NULL,
	[DateCreated] [datetime] NULL DEFAULT getdate(),
	[DateModified] [datetime] NOT NULL DEFAULT getdate(),
 [UserCreated] NVARCHAR(128) NULL, 
    [UserModified] NVARCHAR(128) NULL, 
    CONSTRAINT [PK_ModuleConfigurations] PRIMARY KEY CLUSTERED 
(
	[ModuleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

