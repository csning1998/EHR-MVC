CREATE TABLE [dbo].[Organization]
(
    [OrganizationId] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    [OrganizationName] NVARCHAR(255) NOT NULL,
    [Code] NVARCHAR(20) UNIQUE NOT NULL,
    [Address] NVARCHAR(255) NOT NULL,
    [Telecom] NVARCHAR(25) NULL
);
