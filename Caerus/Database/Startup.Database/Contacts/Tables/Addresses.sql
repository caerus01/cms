CREATE TABLE [dbo].[Addresses] (
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [RefId]           BIGINT              IDENTITY (1, 1) NOT NULL,
    [AddressLine]  VARCHAR (500)    NOT NULL,
    [Suburb]       VARCHAR (500)    NULL,
    [City]         VARCHAR (500)    NULL,
    [ProvinceRefId]   BIGINT              NULL,
    [IsPrimary]    BIT              CONSTRAINT [DF_Address_IsPrimary] DEFAULT ((0)) NOT NULL,
    [OwningRefId]  BIGINT NOT NULL,
    [OwningType]   INT              NOT NULL,
    [CountryRefId]      bigint              CONSTRAINT [DF_Address_Country] DEFAULT ((0)) NULL,
    [Zip]          VARCHAR (50)     NULL,
    [Longitude]    VARCHAR (100)    NULL,
    [Latitude]     VARCHAR (100)    NULL,
	[DateCreated]  DATETIME         CONSTRAINT [DF_Address_DateCreated] DEFAULT (getdate()) NOT NULL,
    [DateModified] DATETIME         CONSTRAINT [DF_Address_DateModified] DEFAULT (getdate()) NOT NULL,
    [UserCreated]  NVARCHAR(128)    NULL,
    [UserModified] NVARCHAR(128)    NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([RefId] ASC)
);

