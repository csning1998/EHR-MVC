CREATE TABLE [dbo].[Users] 
(
    [UserId] BIGINT IDENTITY(1,1) PRIMARY KEY,
    [FamilyName] NVARCHAR(50) NOT NULL,
    [GivenName] NVARCHAR(50) NOT NULL,
    [UserEmail] NVARCHAR(50) NOT NULL,
    [PasswordHashed] NVARCHAR(255) NOT NULL,
    [Role] NVARCHAR(15) NOT NULL,
    [CreatedAt] DATETIME NOT NULL
);