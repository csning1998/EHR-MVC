CREATE TABLE [dbo].[MedicationRequest]
(
    [MedicationRequestId] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    [EncounterId] INT NOT NULL,
    [MedicationName] NVARCHAR(255) NOT NULL,
    [MedicationCode] NVARCHAR(20) NOT NULL,
    [DaysOfAdministration] INT NOT NULL,
    [TotalQuantity] NVARCHAR(50) NOT NULL,
    [Unit] NVARCHAR(20) NOT NULL,
    FOREIGN KEY ([EncounterId]) REFERENCES [dbo].[Encounter]([EncounterId])
);
