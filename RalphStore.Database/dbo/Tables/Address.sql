CREATE TABLE [dbo].[Address]
(
	
	[ID] INT IDENTITY(1,1) NOT NULL,
	[Line1] NVARCHAR(100) NULL,
	[Line2] NVARCHAR(100) NULL,
    [City] NCHAR(10) NULL, 
    [State] NCHAR(10) NULL, 
    [PostalCode] NCHAR(10) NULL, 
    [Country] NCHAR(10) NULL, 
	[Created] DATETIME NULL DEFAULT GetUtcDate(),
	[Modified] DATETIME NULL DEFAULT GetUtcDate(),
	
    [AspNetUserID] NVARCHAR (128) NULL, 
    CONSTRAINT [PK_Address] PRIMARY KEY ([ID]),
	CONSTRAINT [FK_Address_AspNetUsers] FOREIGN KEY (AspNetUserID) REFERENCES [AspNetUsers]([ID]) ON DELETE SET NULL

)
