CREATE PROCEDURE Employee_of_the_month @MonthYear date
AS
SELECT TOP 1 e.ID, SUM(od.Quantity*d.Price) AS S FROM Employees e 
	JOIN Orders o ON o.Employee_ID=e.ID
	JOIN Ordering_dishes od ON o.Employee_ID=od.Employee_ID AND o.Table_Number=od.Table_Number AND o.TIME=od.Order_Time
	JOIN Dishes d ON d.ID=od.Dish_ID
	WHERE MONTH(o.TIME)=MONTH(@MonthYear) AND YEAR(o.TIME)=YEAR(@MonthYear)
	GROUP BY e.ID ORDER BY S DESC;