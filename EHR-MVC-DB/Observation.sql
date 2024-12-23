CREATE TABLE [dbo].[Observation]
(
    [ObservationId] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    [EncounterId] INT NOT NULL,
    [ObservationName] NVARCHAR(255) NOT NULL,
    [Result] NVARCHAR(255) NULL,
    [Unit] NVARCHAR(20) NULL,
    [DateOfObservation] DATE NOT NULL,
    FOREIGN KEY ([EncounterId]) REFERENCES [dbo].[Encounter]([EncounterId])
);
