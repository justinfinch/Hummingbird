CREATE PROCEDURE [dbo].[OrdersGet]
	@Total decimal
AS
	SELECT 
		[Order].Id,
		[Order].Total,
		[Order].EmployeeNumber as EmpNum
	FROM
		[Order]
	WHERE
		[Order].Total <= @Total


RETURN 0
