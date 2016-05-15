
CREATE TABLE [dbo].[CaerusLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Thread] [varchar](255) NOT NULL,
	[Level] [varchar](50) NOT NULL,
	[Logger] [varchar](255) NOT NULL,
	[Message] [varchar](2000) NOT NULL,
	[Exception] [varchar](MAX) NULL,
	[Source] [varchar](200) NULL,
	[Parameters] [varchar](2000) NULL, 
    [User] VARCHAR(50) NULL, 
    [Origin] VARCHAR(50) NULL, 
    CONSTRAINT [PK_Log] PRIMARY KEY ([Id])
) ON [PRIMARY]

GO
