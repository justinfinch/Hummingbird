CREATE TABLE [dbo].[Order]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlacedOn] DATETIME NOT NULL, 
    [Total] SMALLMONEY NOT NULL, 
    [EmployeeNumber] INT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [CreatedBy] VARCHAR(50) NOT NULL, 
    [LastModifiedDate] DATETIME NOT NULL, 
    [LastModifiedBy] VARCHAR(50) NOT NULL, 
    [Version] TIMESTAMP NOT NULL
)
