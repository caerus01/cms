CREATE TABLE [dbo].[ClientEmploymentDetails](
	[Id] [uniqueidentifier] NOT NULL,
	[RefId] [bigint] IDENTITY(1,1) NOT NULL,
	[ClientRefId] [bigint] NULL,
	[EmployerSectorId] [int] NULL,
	[EmploymentLevel] [int] NULL,
	[EmploymentType] [int] NULL,
	[EmploymentStarted] [datetime] NULL,
	[SalaryRule] [int] NULL,
	[SalaryDayRule] [int] NULL,
	[PayslipUsername] [nvarchar](100) NULL,
	[PayslipPassword] [nvarchar](100) NULL,
	[EmployerContactPerson] [nvarchar](100) NULL,
	[EmployerContactNumber] [nvarchar](100) NULL,
	[DateCreated] DATETIME NOT NULL, 
    [DateModified] DATETIME NOT NULL, 
    [UserCreated] NVARCHAR(128) NULL, 
    [UserModified] NVARCHAR(128) NULL, 
 CONSTRAINT [PK_EmploymentDetails] PRIMARY KEY CLUSTERED 
(
	[RefId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]