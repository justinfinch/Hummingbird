CREATE TABLE [dbo].[LineItem]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [OrderId] INT NOT NULL, 
    [Sku] VARCHAR(50) NOT NULL, 
    [UnitPrice] MONEY NOT NULL, 
    [Quantity] DECIMAL NOT NULL, 
    [ExtendedPrice] MONEY NOT NULL, 
    [CreatedBy] VARCHAR(50) NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [LastModifiedBy] VARCHAR(50) NOT NULL, 
    [LastModifiedDate] DATETIME NOT NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [FK_LineItem_Order] FOREIGN KEY ([OrderId]) REFERENCES [Order]([Id])
)
