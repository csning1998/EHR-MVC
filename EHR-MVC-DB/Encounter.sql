CREATE TABLE [dbo].[Encounter]
(
    [EncounterId] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    [EncounterDate] DATE NOT NULL,
    [PatientId] BIGINT NOT NULL,
    [OrganizationId] INT NOT NULL,
    [PractitionerId] INT NOT NULL,
    [SerialNumber] NVARCHAR(50) UNIQUE NOT NULL,
    FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patient]([PatientId]),
    FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[Organization]([OrganizationId]),
    FOREIGN KEY ([PractitionerId]) REFERENCES [dbo].[Practitioner]([PractitionerId])
);
