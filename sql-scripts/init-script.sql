USE master;
GO
-- Create a new database
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'PriceNegotiationDB')
CREATE DATABASE PriceNegotiationDB;
GO

USE PriceNegotiationDB;
GO

-- Table for products
CREATE TABLE Products (
                          ProductID INT IDENTITY(1,1) PRIMARY KEY,
                          Name NVARCHAR(100) NOT NULL,
                          BasePrice DECIMAL(10, 2) NOT NULL
);

-- Table for users
CREATE TABLE Users (
                       UserID INT IDENTITY(1,1) PRIMARY KEY,
                       Name NVARCHAR(50) NOT NULL,
                       Email NVARCHAR(50) NOT NULL,
                       Role INT NOT NULL,
                       Password NVARCHAR(150) NOT NULL,
);

-- Table for negotiations
CREATE TABLE Negotiations (
                              NegotiationID INT IDENTITY(1,1) PRIMARY KEY,
                              ProductID INT,
                              ProposedPrice DECIMAL(10, 2) NOT NULL,
                              UserAttempts INT NOT NULL,
                              Status INT,
                              CreatorUserID INT,
                              CONSTRAINT FK_ProductID FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
                              CONSTRAINT FK_CreatorUserID FOREIGN KEY (CreatorUserID) REFERENCES Users(UserID)
);

-- Inserting data into Products table
INSERT INTO Products (Name, BasePrice)
VALUES ('Smartphone', 499.99),
       ('Laptop', 899.50),
       ('Smartwatch', 199.00),
       ('Headphones', 149.99);

-- Inserting data into Users table
INSERT INTO Users (Name, Email, Role, Password)
VALUES ('testclient', 'testclient@example.com', 0, 'AQAAAAIAAYagAAAAELhqwE0SFAN44L8598yWkF0WcNbNTkuP4OmuKIYclIQmPczubiI5SQiBTD2NBwrJqQ=='), -- Password is 'toortoor'
       ('testadmin', 'testadmin@example.com', 1, 'AQAAAAIAAYagAAAAELhqwE0SFAN44L8598yWkF0WcNbNTkuP4OmuKIYclIQmPczubiI5SQiBTD2NBwrJqQ=='); -- Password is 'toortoor'

-- Inserting data into Negotiations table
INSERT INTO Negotiations (ProductID, ProposedPrice, UserAttempts, Status, CreatorUserID)
VALUES (1, 450.00, 2, 1, 1), -- Negotiation for Smartphone by testclient
       (3, 180.00, 3, 0, 1); -- Negotiation for Smartwatch by testclient
