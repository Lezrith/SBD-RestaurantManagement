﻿-- Generated by Oracle SQL Developer Data Modeler 4.1.5.907
--   at:        2017-01-02 21:00:06 CET
--   site:      SQL Server 2012
--   type:      SQL Server 2012




CREATE
  TABLE Addresses
  (
    ID INTEGER NOT NULL IDENTITY NOT FOR REPLICATION ,
    Street NVARCHAR (50) NOT NULL ,
                NUMBER INTEGER NOT NULL ,
    Flat_number INTEGER ,
    PostalCode  NVARCHAR (6) NOT NULL ,
    City        NVARCHAR (30) NOT NULL ,
    Employee_ID INTEGER ,
    Supplier_Name NVARCHAR (50)
  )
  ON "default"
GO
ALTER TABLE Addresses
ADD
CHECK ( NUMBER >= 0 )
GO

CREATE NONCLUSTERED INDEX
Addresses_Employee_ID_IDX ON Addresses
(
Employee_ID
)
ON "default"
GO
CREATE NONCLUSTERED INDEX
Addresses_Supplier_Name_IDX ON Addresses
(
Supplier_Name
)
ON "default"
GO
ALTER TABLE Addresses ADD CONSTRAINT Addresses_PK PRIMARY KEY CLUSTERED (ID)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE Deliveries
  (
    DATE DATETIME NOT NULL ,
    Supplier_Name NVARCHAR (50) NOT NULL ,
    Cost NUMERIC (9,2) NOT NULL
  )
  ON "default"
GO
ALTER TABLE Deliveries ADD CONSTRAINT Deliveries_PK PRIMARY KEY CLUSTERED (DATE
, Supplier_Name)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE Dish_Type
  (
    Dish_ID INTEGER NOT NULL ,
    Type_Name NVARCHAR (30) NOT NULL
  )
  ON "default"
GO
ALTER TABLE Dish_Type ADD CONSTRAINT Dish_Type_PK PRIMARY KEY CLUSTERED (
Dish_ID, Type_Name)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE Dishes
  (
    ID INTEGER NOT NULL IDENTITY NOT FOR REPLICATION ,
    Name NVARCHAR (50) NOT NULL ,
    Price FLOAT NOT NULL ,
    In_menu BIT NOT NULL
  )
  ON "default"
GO
CREATE NONCLUSTERED INDEX
Dishes_Name_IDX ON Dishes
(
  Name
)
ON "default"
GO
ALTER TABLE Dishes ADD CONSTRAINT Dishes_PK PRIMARY KEY CLUSTERED (ID)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE Dishes_contents
  (
    Quantity INTEGER NOT NULL ,
    Ingredient_Name NVARCHAR (30) NOT NULL ,
    Dish_ID INTEGER NOT NULL
  )
  ON "default"
GO
ALTER TABLE Dishes_contents ADD CONSTRAINT Dishes_contents_PK PRIMARY KEY
CLUSTERED (Ingredient_Name, Dish_ID)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE Employees
  (
    ID            INTEGER NOT NULL IDENTITY NOT FOR REPLICATION ,
    Last_name     NVARCHAR (30) NOT NULL ,
    First_name    NVARCHAR (30) NOT NULL ,
    Salary        NUMERIC (9,2) NOT NULL ,
    Email_address NVARCHAR (30) NOT NULL ,
    Phone_number NVARCHAR (9) NOT NULL ,
    Address_ID    INTEGER NOT NULL ,
    Position_Name NVARCHAR (30) NOT NULL ,
    Hashed_password NVARCHAR (20) NOT NULL
  )
  ON "default"
GO
CREATE UNIQUE NONCLUSTERED INDEX
Employees_Address_ID_IDX ON Employees
(
  Address_ID
)
ON "default"
GO
ALTER TABLE Employees ADD CONSTRAINT Employees_PK PRIMARY KEY CLUSTERED (ID)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE Having_privileges
  (
    Privilege_Name NVARCHAR (30) NOT NULL ,
    Position_Name NVARCHAR (30) NOT NULL
  )
  ON "default"
GO
ALTER TABLE Having_privileges ADD CONSTRAINT Having_privileges_PK PRIMARY KEY
CLUSTERED (Privilege_Name, Position_Name)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE Ingredients
  (
    Name NVARCHAR (30) NOT NULL ,
    Quantity_in_stock INTEGER NOT NULL ,
    Unit NVARCHAR (3) NOT NULL
  )
  ON "default"
GO
ALTER TABLE Ingredients ADD CONSTRAINT Ingredients_PK PRIMARY KEY CLUSTERED (
Name)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE Items_in_deliveries
  (
    Quantity      INT NOT NULL ,
    Delivery_Date DATETIME NOT NULL ,
    Supplier_Name NVARCHAR (50) NOT NULL ,
    Ingredient_Name NVARCHAR (30) NOT NULL
  )
  ON "default"
GO
ALTER TABLE Items_in_deliveries ADD CONSTRAINT Items_in_deliveries_PK PRIMARY
KEY CLUSTERED (Delivery_Date, Supplier_Name, Ingredient_Name)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE Ordering_dishes
  (
    Order_Time DATETIME NOT NULL ,
    Table_Number INTEGER NOT NULL ,
    Employee_ID  INTEGER NOT NULL ,
    Quantity     INTEGER NOT NULL ,
    Dish_ID      INTEGER NOT NULL
  )
  ON "default"
GO
ALTER TABLE Ordering_dishes ADD CONSTRAINT Ordering_dishes_PK PRIMARY KEY
CLUSTERED (Order_Time, Table_Number, Employee_ID, Dish_ID)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE Orders
  (
    TIME DATETIME NOT NULL ,
    Table_Number INTEGER NOT NULL ,
    Employee_ID  INTEGER NOT NULL ,
    Payment_method_Name NVARCHAR (30) NOT NULL
  )
  ON "default"
GO
ALTER TABLE Orders ADD CONSTRAINT Orders_PK PRIMARY KEY CLUSTERED (TIME,
Table_Number, Employee_ID)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE Payment_methods
  (
    Name NVARCHAR (30) NOT NULL
  )
  ON "default"
GO
ALTER TABLE Payment_methods ADD CONSTRAINT Payment_methods_PK PRIMARY KEY
CLUSTERED (Name)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE Positions
  (
    Name            NVARCHAR (30) NOT NULL ,
    Starting_salary INTEGER NOT NULL
  )
  ON "default"
GO
ALTER TABLE Positions ADD CONSTRAINT Positions_PK PRIMARY KEY CLUSTERED (Name)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE PRIVILEGES
  (
    Name NVARCHAR (30) NOT NULL
  )
  ON "default"
GO
ALTER TABLE PRIVILEGES ADD CONSTRAINT Privileges_PK PRIMARY KEY CLUSTERED (Name
)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE Reservations
  (
    DATE DATE NOT NULL ,
    START TIME NOT NULL ,
    "End" TIME NOT NULL ,
    Name NVARCHAR (50) NOT NULL ,
    Table_Number INTEGER NOT NULL
  )
  ON "default"
GO
ALTER TABLE Reservations ADD CONSTRAINT Reservations_PK PRIMARY KEY CLUSTERED (
DATE, START, Table_Number)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE Suppliers
  (
    Name NVARCHAR (50) NOT NULL ,
    Email_address NVARCHAR (30) NOT NULL ,
    Phone_number NVARCHAR (9) NOT NULL ,
    Address_ID INTEGER NOT NULL
  )
  ON "default"
GO
CREATE UNIQUE NONCLUSTERED INDEX
Suppliers_Addresses_ID_IDX ON Suppliers
(
  Address_ID
)
ON "default"
GO
ALTER TABLE Suppliers ADD CONSTRAINT Suppliers_PK PRIMARY KEY CLUSTERED (Name)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE Tables
  (
                    NUMBER INTEGER NOT NULL IDENTITY NOT FOR REPLICATION ,
    Number_of_seats INTEGER NOT NULL
  )
  ON "default"
GO
ALTER TABLE Tables ADD CONSTRAINT Tables_PK PRIMARY KEY CLUSTERED (NUMBER)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE Types
  (
    Name NVARCHAR (30) NOT NULL
  )
  ON "default"
GO
ALTER TABLE Types ADD CONSTRAINT Types_PK PRIMARY KEY CLUSTERED (Name)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

ALTER TABLE Addresses
ADD CONSTRAINT Addresses_Employees_FK FOREIGN KEY
(
Employee_ID
)
REFERENCES Employees
(
ID
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE Addresses
ADD CONSTRAINT Addresses_Suppliers_FK FOREIGN KEY
(
Supplier_Name
)
REFERENCES Suppliers
(
Name
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE Deliveries
ADD CONSTRAINT Deliveries_Suppliers_FK FOREIGN KEY
(
Supplier_Name
)
REFERENCES Suppliers
(
Name
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE Dish_Type
ADD CONSTRAINT Dish_Type_Dishes_FK FOREIGN KEY
(
Dish_ID
)
REFERENCES Dishes
(
ID
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE Dish_Type
ADD CONSTRAINT Dish_Type_Types_FK FOREIGN KEY
(
Type_Name
)
REFERENCES Types
(
Name
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE Dishes_contents
ADD CONSTRAINT Dishes_contents_Dishes_FK FOREIGN KEY
(
Dish_ID
)
REFERENCES Dishes
(
ID
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE Dishes_contents
ADD CONSTRAINT Dishes_contents_Ingredientsi_FK FOREIGN KEY
(
Ingredient_Name
)
REFERENCES Ingredients
(
Name
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE Employees
ADD CONSTRAINT Employees_Addresses_FK FOREIGN KEY
(
Address_ID
)
REFERENCES Addresses
(
ID
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE Employees
ADD CONSTRAINT Employees_Positions_FK FOREIGN KEY
(
Position_Name
)
REFERENCES Positions
(
Name
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE Having_privileges
ADD CONSTRAINT Having_privileges_Positions_FK FOREIGN KEY
(
Position_Name
)
REFERENCES Positions
(
Name
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE Having_privileges
ADD CONSTRAINT Having_privileges_Privileges_FK FOREIGN KEY
(
Privilege_Name
)
REFERENCES PRIVILEGES
(
Name
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE Items_in_deliveries
ADD CONSTRAINT Items_in_deliveries_Deliveries_FK FOREIGN KEY
(
Delivery_Date,
Supplier_Name
)
REFERENCES Deliveries
(
DATE ,
Supplier_Name
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE Items_in_deliveries
ADD CONSTRAINT Items_in_deliveries_Ingredients_FK FOREIGN KEY
(
Ingredient_Name
)
REFERENCES Ingredients
(
Name
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE Ordering_dishes
ADD CONSTRAINT Ordering_dishes_Dishes_FK FOREIGN KEY
(
Dish_ID
)
REFERENCES Dishes
(
ID
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE Ordering_dishes
ADD CONSTRAINT Ordering_dishes_Orders_FK FOREIGN KEY
(
Order_Time,
Table_Number,
Employee_ID
)
REFERENCES Orders
(
TIME ,
Table_Number ,
Employee_ID
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE Orders
ADD CONSTRAINT Orders_Employees_FK FOREIGN KEY
(
Employee_ID
)
REFERENCES Employees
(
ID
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE Orders
ADD CONSTRAINT Orders_Payment_methods_FK FOREIGN KEY
(
Payment_method_Name
)
REFERENCES Payment_methods
(
Name
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE Orders
ADD CONSTRAINT Orders_Stoliki_FK FOREIGN KEY
(
Table_Number
)
REFERENCES Tables
(
NUMBER
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE Reservations
ADD CONSTRAINT Reservations_Tables_FK FOREIGN KEY
(
Table_Number
)
REFERENCES Tables
(
NUMBER
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE Suppliers
ADD CONSTRAINT Suppliers_Addresses_FK FOREIGN KEY
(
Address_ID
)
REFERENCES Addresses
(
ID
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO


-- Oracle SQL Developer Data Modeler Summary Report: 
-- 
-- CREATE TABLE                            18
-- CREATE INDEX                             5
-- ALTER TABLE                             40
-- CREATE VIEW                              0
-- ALTER VIEW                               0
-- CREATE PACKAGE                           0
-- CREATE PACKAGE BODY                      0
-- CREATE PROCEDURE                         0
-- CREATE FUNCTION                          0
-- CREATE TRIGGER                           0
-- ALTER TRIGGER                            0
-- CREATE DATABASE                          0
-- CREATE DEFAULT                           0
-- CREATE INDEX ON VIEW                     0
-- CREATE ROLLBACK SEGMENT                  0
-- CREATE ROLE                              0
-- CREATE RULE                              0
-- CREATE SCHEMA                            0
-- CREATE SEQUENCE                          0
-- CREATE PARTITION FUNCTION                0
-- CREATE PARTITION SCHEME                  0
-- 
-- DROP DATABASE                            0
-- 
-- ERRORS                                   0
-- WARNINGS                                 0
