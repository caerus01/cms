CREATE TABLE [dbo].[HttpRequestLogs](
	[Id] [uniqueidentifier] NOT NULL,
	[RefId] [bigint] IDENTITY(1,1) NOT NULL,
	[RequestTime] [datetime] NOT NULL,
	[RequestBody] [varchar](max) NOT NULL,
	[UrlParams] [varchar](4096) NOT NULL,
	[Controller] [varchar](200) NOT NULL,
	[Action] [varchar](200) NOT NULL,
	[ContentType] [varchar](200) NOT NULL,
	[Source] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RefId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
