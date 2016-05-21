CREATE TABLE [dbo].[ClientAffordabilityDetails](
	[Id] [uniqueidentifier] NOT NULL,
	[RefId] [bigint] IDENTITY(1,1) NOT NULL,
	[ClientRefId] [bigint] NULL,
	[GrossSalary] [money] NULL,
	[Deductions] [money] NULL,
	[NetSalary] [money] NULL,
	[ExpensesHousing] [money] NULL,
	[ExpensesTransport] [money] NULL,
	[ExpensesConsumables] [money] NULL,
	[ExpensesEducation] [money] NULL,
	[ExpensesOther] [money] NULL,
	[ExpensesOtherDebt] [money] NULL,
	[ExpensesMedical] [money] NULL,
	[ExpensesWater] [money] NULL,
	[ExpensesMaintenance] [money] NULL,
	[DateCreated] DATETIME NOT NULL, 
    [DateModified] DATETIME NOT NULL, 
    [UserCreated] NVARCHAR(128) NULL, 
    [UserModified] NVARCHAR(128) NULL, 
 CONSTRAINT [PK_AffordabilityDetails] PRIMARY KEY CLUSTERED 
(
	[RefId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
