CREATE TABLE [dbo].[Product]
(
	[ProductId] INT NOT NULL PRIMARY KEY,
	[ProductName] NVARCHAR (100) NULL,
	[ProductNumber] INT NULL,
	[Price] INT NOT NULL,
	[ProductDescription] NVARCHAR (100) NULL
)
