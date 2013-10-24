CREATE TABLE [dbo].[Employee]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [Number] INT NOT NULL, 
	[TotalSales] INT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [CreatedBy] VARCHAR(50) NOT NULL, 
    [LastModifiedDate] DATETIME NOT NULL, 
    [LastModifiedBy] VARCHAR(50) NOT NULL, 
    [RowVersion] TIMESTAMP NOT NULL
)
