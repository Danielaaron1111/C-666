-- ================================================================================
-- CLUBS MANAGEMENT DATABASE - SQL Server Script
-- ================================================================================
-- This script creates the database for the A02: Clubs assignment
-- Based on the requirements shown in the assignment mockup
-- ================================================================================

USE master;
GO

-- Drop database if it exists
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'ClubsDB')
BEGIN
    ALTER DATABASE ClubsDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE ClubsDB;
END
GO

-- Create the database
CREATE DATABASE ClubsDB;
GO

USE ClubsDB;
GO

-- ================================================================================
-- TABLE 1: Positions (Lookup table)
-- ================================================================================
CREATE TABLE Positions (
    PositionID INT PRIMARY KEY IDENTITY(1,1),
    PositionName NVARCHAR(50) NOT NULL
);
GO

-- ================================================================================
-- TABLE 2: Programs (Lookup table)
-- ================================================================================
CREATE TABLE Programs (
    ProgramID INT PRIMARY KEY IDENTITY(1,1),
    ProgramName NVARCHAR(100) NOT NULL
);
GO

-- ================================================================================
-- TABLE 3: Employees
-- ================================================================================
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(20) NOT NULL,
    LastName VARCHAR(20) NOT NULL,
    DateHired DATETIME NOT NULL,
    ReleaseDate DATETIME NULL,
    PositionID INT NOT NULL,
    ProgramID INT NOT NULL,
    LoginID VARCHAR(30) NOT NULL,
    CONSTRAINT FK_Employees_Positions FOREIGN KEY (PositionID) REFERENCES Positions(PositionID),
    CONSTRAINT FK_Employees_Programs FOREIGN KEY (ProgramID) REFERENCES Programs(ProgramID)
);
GO

-- ================================================================================
-- TABLE 4: Clubs
-- ================================================================================
CREATE TABLE Clubs (
    ClubID VARCHAR(10) PRIMARY KEY,
    ClubName VARCHAR(50) NOT NULL UNIQUE,
    Active BIT NOT NULL DEFAULT 1,
    EmployeeID INT NULL,
    Fee MONEY NOT NULL DEFAULT 0,
    CONSTRAINT FK_Clubs_Employees FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID),
    CONSTRAINT CK_Clubs_Fee_NonNegative CHECK (Fee >= 0)
);
GO

-- ================================================================================
-- INSERT SAMPLE DATA
-- ================================================================================

-- Insert Positions
INSERT INTO Positions (PositionName) VALUES
    ('Instructor'),
    ('Office Administrator'),
    ('Technical Support'),
    ('Manager'),
    ('Coordinator');
GO

-- Insert Programs
INSERT INTO Programs (ProgramName) VALUES
    ('Computer Systems Technology'),
    ('Business Administration'),
    ('Engineering'),
    ('Health Sciences'),
    ('Hospitality Management');
GO

-- Insert Employees
INSERT INTO Employees (FirstName, LastName, DateHired, ReleaseDate, PositionID, ProgramID, LoginID) VALUES
    ('Craq', 'Antmor', '2020-01-15', NULL, 1, 1, 'cantmor'),
    ('Johnny', 'Spotson', '2019-03-20', NULL, 1, 2, 'jspotson'),
    ('Sarah', 'Johnson', '2021-05-10', NULL, 2, 1, 'sjohnson'),
    ('Mike', 'Davis', '2018-07-22', NULL, 3, 3, 'mdavis'),
    ('Emily', 'Wilson', '2022-02-14', NULL, 4, 1, 'ewilson'),
    ('David', 'Brown', '2020-09-30', NULL, 1, 4, 'dbrown'),
    ('Lisa', 'Anderson', '2019-11-18', NULL, 2, 2, 'landerson'),
    ('Robert', 'Taylor', '2021-08-05', NULL, 3, 5, 'rtaylor');
GO

-- Insert Clubs
INSERT INTO Clubs (ClubID, ClubName, Active, EmployeeID, Fee) VALUES
    ('BTC', 'Bachelor of Technology Club', 1, NULL, 15.00),
    ('BSTC', 'Biological Sciences Technology Club', 1, 1, 25.00),
    ('BUSCON', 'Business Connex', 1, NULL, 10.00),
    ('CCRC', 'Captioning and Court Reporting Club at NAIT', 1, 2, 5.00),
    ('CTSC', 'Chemical Technology Student Council', 1, NULL, 0.00),
    ('ENGCLUB', 'Engineering Society', 1, 4, 20.00),
    ('HEALTHC', 'Health Sciences Club', 0, 6, 12.00),
    ('BIZTECH', 'Business Technology Association', 0, NULL, 8.00),
    ('ROBOTICS', 'Robotics and Automation Club', 1, 3, 30.00),
    ('GREENTECH', 'Green Technology Initiative', 1, 7, 15.00);
GO

-- ================================================================================
-- CREATE VIEWS (Optional - for easier querying)
-- ================================================================================

CREATE VIEW vw_ClubsWithStaff AS
SELECT
    c.ClubID,
    c.ClubName,
    c.Active,
    c.Fee,
    c.EmployeeID,
    e.FirstName,
    e.LastName,
    e.FirstName + ' ' + e.LastName AS FullName,
    p.PositionName
FROM Clubs c
LEFT JOIN Employees e ON c.EmployeeID = e.EmployeeID
LEFT JOIN Positions p ON e.PositionID = p.PositionID;
GO

CREATE VIEW vw_AvailableStaff AS
SELECT
    e.EmployeeID,
    e.FirstName,
    e.LastName,
    e.FirstName + ' ' + e.LastName AS FullName,
    p.PositionName,
    pr.ProgramName
FROM Employees e
INNER JOIN Positions p ON e.PositionID = p.PositionID
INNER JOIN Programs pr ON e.ProgramID = pr.ProgramID
WHERE p.PositionName IN ('Instructor', 'Office Administrator', 'Technical Support')
    AND e.ReleaseDate IS NULL;
GO

-- ================================================================================
-- VERIFICATION QUERIES
-- ================================================================================

-- Show all employees
SELECT * FROM Employees;

-- Show all clubs
SELECT * FROM Clubs ORDER BY ClubName;

-- Show clubs by active status
SELECT * FROM vw_ClubsWithStaff WHERE Active = 1 ORDER BY ClubName;

-- Show available staff for clubs
SELECT * FROM vw_AvailableStaff ORDER BY LastName;

PRINT 'Database created successfully!';
GO
