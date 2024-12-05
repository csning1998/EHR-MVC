CREATE TABLE [dbo].[MedicalStaff]
(
    [staffId] BIGINT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [staffEmail] NVARCHAR(100) NOT NULL,
    [hashedPassword] NVARCHAR(MAX) NOT NULL,
    [SecurityStamp] NVARCHAR(MAX) NOT NULL,
    [nationalIdNo] VARCHAR(10) NOT NULL,
    [staffActive] BIT NOT NULL DEFAULT 1,
    [staffFamilyName] NVARCHAR(50) NOT NULL,
    [staffGivenName] NVARCHAR(50) NOT NULL,
    [staffTelecom] VARCHAR(10),
    [Gender] VARCHAR(10) NOT NULL,
    [Birthday] DATE,
    [Address] NVARCHAR(100)
);
