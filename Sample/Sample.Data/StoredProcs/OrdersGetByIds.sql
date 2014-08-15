CREATE PROCEDURE [dbo].[OrdersGetByIds]
	@Ids dbo.INT32ListType READONLY
AS
	SELECT 
		[Order].Id,
		[Order].Total,
		[Order].EmployeeNumber as EmpNum
	FROM
		[Order]
		INNER JOIN @Ids i ON i.INT32Value = [Order].Id


RETURN 0
