CREATE TABLE [dbo].[ModuleSettings](
	[ModuleId] [int] NOT NULL,
	[SettingId] [int] NOT NULL,
	[SettingValue] [varchar](500) NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT getdate(),
	[DateModified] [datetime] NOT NULL,
	[UserCreated] NVARCHAR(128) NOT NULL,
	[UserModified] NVARCHAR(128) NOT NULL,
 CONSTRAINT [PK_ModuleSettings_1] PRIMARY KEY CLUSTERED 
(
	[ModuleId] ASC,
	[SettingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
