CREATE TABLE [dbo].[NotificationReplyHistories]
(
	[Id] INT NOT NULL , 
    [RefId] BIGINT NOT NULL IDENTITY, 
    [EventId] NVARCHAR(150) NULL, 
    [ReplyId] NVARCHAR(150) NULL, 
    [SourceNumber] NVARCHAR(50) NULL, 
    [ReceivedResponse] NVARCHAR(320) NULL, 
    [SentDate] DATETIME NULL, 
    [ReceivedDate] DATETIME NULL, 
    PRIMARY KEY ([RefId])
)
