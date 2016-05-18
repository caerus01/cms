
CREATE TABLE [dbo].[ClientBankingDetails](
	[Id] [uniqueidentifier] NOT NULL,
	[RefId] [bigint] IDENTITY(1,1) NOT NULL,
	[ClientRefId] [bigint] NOT NULL,
	[BankRefId] [bigint] NULL,
	[BankBranchRefId] [bigint] NULL,
	[BankAccountNumber] [nvarchar](100) NOT NULL,
	[BankAccountType] [int] NULL,
	[AccountHolder] [nvarchar](150) NULL,
	[CreditCardExiryDate] [date] NULL,
	[CardReference] [nvarchar](50) NULL,
	[CardCvv] [nvarchar](4) NULL,
	[SecurityNumber] [nvarchar](50) NULL,
	[Status] [int] NOT NULL,
	[LastActivityDate] [datetime] NOT NULL,
	[LastResponseCode] [varchar](10) NULL,
	[DateCreated] DATETIME NOT NULL, 
    [DateModified] DATETIME NOT NULL, 
    [UserCreated] NVARCHAR(128) NULL, 
    [UserModified] NVARCHAR(128) NULL, 
 CONSTRAINT [IX_BankingDetails] UNIQUE NONCLUSTERED 
(
	[RefId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
