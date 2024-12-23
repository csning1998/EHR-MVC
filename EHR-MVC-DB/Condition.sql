CREATE TABLE [dbo].[Condition]
(
    [ConditionId] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    [EncounterId] INT NOT NULL,
    [Diagnosis] NVARCHAR(255) NOT NULL,
    [Code] NVARCHAR(20) NOT NULL,
    [Severity] NVARCHAR(50) NULL,
    FOREIGN KEY ([EncounterId]) REFERENCES [dbo].[Encounter]([EncounterId])
);
