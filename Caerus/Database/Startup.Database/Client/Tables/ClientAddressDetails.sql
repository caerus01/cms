﻿CREATE TABLE [dbo].[ClientAddressDetails] (
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [RefId]           BIGINT              IDENTITY (1, 1) NOT NULL,
	[ClientRefId]  BIGINT NULL,
    [ResidentialAddressLine]  VARCHAR (500)    NULL,
    [ResidentialSuburb]       VARCHAR (500)    NULL,
    [ResidentialCity]         VARCHAR (500)    NULL,
    [ResidentialProvinceRefId]   BIGINT        NULL,
	[ResidentialZip]          VARCHAR (50)     NULL,
    [ResidentialLongitude]    VARCHAR (100)    NULL,
    [ResidentialLatitude]     VARCHAR (100)    NULL,
	[PostalAddressLine]  VARCHAR (500)    NULL,
    [PostalSuburb]       VARCHAR (500)    NULL,
    [PostalCity]         VARCHAR (500)    NULL,
    [PostalProvinceRefId]   BIGINT        NULL,
	[PostalZip]          VARCHAR (50)     NULL,
    [PostalLongitude]    VARCHAR (100)    NULL,
    [PostalLatitude]     VARCHAR (100)    NULL,
    [IsPrimary]    BIT              CONSTRAINT [DF_Address_IsPrimary] DEFAULT ((0)) NULL,
	[DateCreated]  DATETIME         CONSTRAINT [DF_Address_DateCreated] DEFAULT (getdate()) NOT NULL,
    [DateModified] DATETIME         CONSTRAINT [DF_Address_DateModified] DEFAULT (getdate()) NOT NULL,
    [UserCreated]  NVARCHAR(128)    NULL,
    [UserModified] NVARCHAR(128)    NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([RefId] ASC)
);

