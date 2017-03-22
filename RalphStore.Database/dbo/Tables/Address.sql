CREATE TABLE [dbo].[Address]
(
	[AddressId] INT NOT NULL PRIMARY KEY,
	[Address1] NVARCHAR (60) NULL,
	[Address2] NVARCHAR (60) NULL,
	[City] NVARCHAR (30) NULL,
	[StateProvince] NVARCHAR (30) NULL,
	[PostalCode] NVARCHAR (15) NULL,

)
