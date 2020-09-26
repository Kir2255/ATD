USE master
GO

-- CREATE DATABASE
DROP DATABASE IF EXISTS IE_Book
GO

CREATE DATABASE IE_Book
GO


-- CREATE TABLES
USE IE_Book

IF OBJECT_ID ('dbo.Details', 'U') IS NOT NULL  
   DROP TABLE Details;  
GO
-- Create table Details
CREATE TABLE dbo.Details (
    Id   INT PRIMARY KEY IDENTITY(1, 1),
    Code INT NOT NULL,
);


IF OBJECT_ID ('dbo.Materials', 'U') IS NOT NULL  
   DROP TABLE Materials;  
GO
-- Create table Materials
CREATE TABLE dbo.Materials (
    Id   INT PRIMARY KEY IDENTITY(1, 1),
    Code INT NOT NULL,
	Name VARCHAR(255) NOT NULL
);


IF OBJECT_ID ('dbo.Units', 'U') IS NOT NULL  
   DROP TABLE Units;  
GO
-- Create table Units
CREATE TABLE dbo.Units (
    Id   INT PRIMARY KEY IDENTITY(1, 1),
    Description VARCHAR(255) NOT NULL
);


IF OBJECT_ID ('dbo.Components', 'U') IS NOT NULL  
   DROP TABLE Components;  
GO
-- Create table Components
CREATE TABLE dbo.Components (
    Id INT PRIMARY KEY IDENTITY(1, 1),
    DetailId INT NOT NULL,
	MaterialId INT NOT NULL,
	UnitId INT NOT NULL,
);

IF OBJECT_ID ('dbo.IncomeExpensiveBook', 'U') IS NOT NULL  
   DROP TABLE IncomeExpensiveBook;  
GO
-- Create table IncomeExpensiveBook
CREATE TABLE dbo.IncomeExpensiveBook (
    ComponentId INT,
	Income FLOAT DEFAULT NULL,
	Expensive FLOAT DEFAULT NULL
);

-- Make references between different tables
GO
ALTER TABLE [dbo].[Components]  WITH CHECK ADD CONSTRAINT [FK_Components_Details] FOREIGN KEY([DetailId])
REFERENCES [dbo].[Details] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE

GO
ALTER TABLE [dbo].[Components]  WITH CHECK ADD CONSTRAINT [FK_Components_Materials] FOREIGN KEY([MaterialId])
REFERENCES [dbo].[Materials] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE

GO
ALTER TABLE [dbo].[Components]  WITH CHECK ADD CONSTRAINT [FK_Components_Units] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Units] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE

GO
ALTER TABLE [dbo].[IncomeExpensiveBook]  WITH CHECK ADD CONSTRAINT [FK_IncomeExpensiveBook_Components] FOREIGN KEY([ComponentId])
REFERENCES [dbo].[Components] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE