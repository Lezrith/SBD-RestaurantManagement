ALTER TABLE Employees
ADD CONSTRAINT Salary_higher_than_starting CHECK (dbo.Salary_mismatches()=0);