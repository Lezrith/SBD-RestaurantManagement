CREATE FUNCTION Salary_mismatches()
RETURNS int
AS BEGIN RETURN
(
	SELECT COUNT(*)
	FROM Employees e
	JOIN Positions p
	ON e.Position_Name=p.Name
	WHERE e.Salary<p.Starting_Salary
) END