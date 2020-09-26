USE master
GO

-- CREATE DATABASE
DROP DATABASE IF EXISTS RecordKeeping
GO

CREATE DATABASE RecordKeeping
GO


-- CREATE TABLES
USE RecordKeeping

IF OBJECT_ID ('dbo.Details', 'U') IS NOT NULL  
   DROP TABLE Details;  
GO
-- Create table Details
CREATE TABLE dbo.Details (
    DetailID   INT PRIMARY KEY IDENTITY(1, 1),
    Code INT NOT NULL,
);


IF OBJECT_ID ('dbo.Materials', 'U') IS NOT NULL  
   DROP TABLE Materials;  
GO
-- Create table Materials
CREATE TABLE dbo.Materials (
    MaterialID   INT PRIMARY KEY IDENTITY(1, 1),
    Code INT NOT NULL,
	Name VARCHAR(255) NOT NULL
);


IF OBJECT_ID ('dbo.Units', 'U') IS NOT NULL  
   DROP TABLE Units;  
GO
-- Create table Units
CREATE TABLE dbo.Units (
    UnitID   INT PRIMARY KEY IDENTITY(1, 1),
    Description VARCHAR(255) NOT NULL
);


IF OBJECT_ID ('dbo.Components', 'U') IS NOT NULL  
   DROP TABLE Components;  
GO
-- Create table Components
CREATE TABLE dbo.Components (
    ComponentID INT PRIMARY KEY IDENTITY(1, 1),
    DetailID INT NOT NULL,
	MaterialID INT NOT NULL,
	UnitID INT NOT NULL,
);

IF OBJECT_ID ('dbo.IncomeExpensiveBook', 'U') IS NOT NULL  
   DROP TABLE IncomeExpensiveBook;  
GO
-- Create table IncomeExpenseBook
CREATE TABLE dbo.IncomeExpenseBook (
    ComponentID INT,
	Income FLOAT DEFAULT NULL,
	Expense FLOAT DEFAULT NULL
);

-- Make references between different tables
GO
ALTER TABLE [dbo].[Components]  WITH CHECK ADD CONSTRAINT [FK_Components_Details] FOREIGN KEY([DetailID])
REFERENCES [dbo].[Details] ([DetailID])
ON UPDATE CASCADE
ON DELETE CASCADE

GO
ALTER TABLE [dbo].[Components]  WITH CHECK ADD CONSTRAINT [FK_Components_Materials] FOREIGN KEY([MaterialID])
REFERENCES [dbo].[Materials] ([MaterialID])
ON UPDATE CASCADE
ON DELETE CASCADE

GO
ALTER TABLE [dbo].[Components]  WITH CHECK ADD CONSTRAINT [FK_Components_Units] FOREIGN KEY([UnitID])
REFERENCES [dbo].[Units] ([UnitID])
ON UPDATE CASCADE
ON DELETE CASCADE

GO
ALTER TABLE [dbo].[IncomeExpenseBook]  WITH CHECK ADD CONSTRAINT [FK_IncomeExpenseBook_Components] FOREIGN KEY([ComponentID])
REFERENCES [dbo].[Components] ([ComponentID])
ON UPDATE CASCADE
ON DELETE CASCADE