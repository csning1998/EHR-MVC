CREATE TABLE [dbo].[Practitioner]
(
    [PractitionerId] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    [FullName] NVARCHAR(255) NOT NULL,
    [Specialty] NVARCHAR(100) NOT NULL,
    [OrganizationId] INT NOT NULL,
    [UserId] BIGINT NULL,
    FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[Organization]([OrganizationId]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([UserId])
);
