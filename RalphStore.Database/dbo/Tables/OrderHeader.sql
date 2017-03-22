CREATE TABLE [dbo].[OrderHeader]
(
	[OrderId] INT NOT NULL PRIMARY KEY,
	[OrderDate] DATETIME NULL,
	[AccountNumber] INT NULL,
	[OrderNumber] INT  NULL,
	[OrderDetails] INT NOT NULL,
	[Address] INT NOT NULL,
	FOREIGN KEY (OrderDetails) REFERENCES OrderDetails(OrderDetailsId),
	FOREIGN KEY (Address) REFERENCES [Address](AddressId)


)
