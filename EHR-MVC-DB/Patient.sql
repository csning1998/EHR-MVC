CREATE TABLE [dbo].[Patient]
(
    [PatientId] BIGINT IDENTITY(1, 1) NOT FOR REPLICATION NOT NULL PRIMARY KEY,
    [IdNo] VARCHAR(10) NOT NULL UNIQUE,
    [MedicalRecordNumber] AS CONCAT('MR', RIGHT('000000' + CAST([PatientId] AS VARCHAR), 6)) PERSISTED,
    [Active] BIT NOT NULL DEFAULT 1,
    [FamilyName] NVARCHAR(50) NOT NULL,
    [GivenName] NVARCHAR(50) NOT NULL,
    [Telecom] VARCHAR(10) NOT NULL,
    [Gender] VARCHAR(10) NOT NULL,
    [Birthday] DATE NOT NULL,
    [Address] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(100) NULL,
    [PostalCode] NVARCHAR(10) NULL,
    [Country] NVARCHAR(50) NULL,
    [PreferredLanguage] NVARCHAR(50) NULL,
    [EmergencyContactName] NVARCHAR(50) NULL,
    [EmergencyContactRelationship] NVARCHAR(50) NULL,
    [EmergencyContactPhone] NVARCHAR(20) NULL
);