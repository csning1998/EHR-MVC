﻿CREATE TABLE [dbo].[Patient]
(
    [PatientId] BIGINT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdNo] VARCHAR(10) NOT NULL,
    [Active] BIT NOT NULL DEFAULT 1,
    [FamilyName] NVARCHAR(50) NOT NULL,
    [GivenName] NVARCHAR(50) NOT NULL,
    [Telecom] VARCHAR(10) NOT NULL,
    [Gender] VARCHAR(10) NOT NULL,
    [Birthday] DATE NOT NULL,
    [Address] NVARCHAR(100) NOT NULL
);